using FluentAssertions;
using MoneyBee.Auth.Application.Models;
using MoneyBee.Auth.Application.Services;
using MoneyBee.Auth.Domain.Entities;
using MoneyBee.Auth.Domain.Repositories;
using MoneyBee.Common.Caching;
using MoneyBee.Common.Constants;
using MoneyBee.Common.Helpers;
using Moq;

[TestFixture]
public class ApiKeyServiceTests
{
    private Mock<IApiKeyRepository> _apiKeyRepositoryMock;
    private Mock<IRedisDistributedCacheService> _cacheServiceMock;
    private ApiKeyService _service;

    [SetUp]
    public void Setup()
    {
        _apiKeyRepositoryMock = new Mock<IApiKeyRepository>();
        _cacheServiceMock = new Mock<IRedisDistributedCacheService>();

        _service = new ApiKeyService(
            _apiKeyRepositoryMock.Object,
            _cacheServiceMock.Object);
    }

    #region GenerateApiKeyAsync

    [Test]
    public async Task GenerateApiKeyAsync_Should_Create_ApiKey_And_Update_Cache()
    {
        // Arrange
        var request = new CreateApiKeyRequest
        {
            ClientName = "Test Client",
            Description = "Test Desc",
            CreatedBy = "admin",
            RateLimit = 200
        };

        _cacheServiceMock
            .Setup(x => x.GetAsync<List<string>>(It.IsAny<string>()))
            .ReturnsAsync(new List<string>());

        // Act
        var result = await _service.GenerateApiKeyAsync(request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.ApiKey.Should().NotBeNullOrEmpty();
        result.Data.ClientName.Should().Be(request.ClientName);

        _apiKeyRepositoryMock.Verify(x => x.AddAsync(It.IsAny<ApiKeyEntity>()), Times.Once);
        _apiKeyRepositoryMock.Verify(x => x.SaveAsync(), Times.Once);
        _cacheServiceMock.Verify(
            x => x.SetAsync(
                RedisConstants.ApiKeysCacheKey,
                It.IsAny<List<string>>()),
            Times.Once);
    }

    #endregion

    #region ValidateApiKeyAsync

    [Test]
    public async Task ValidateApiKeyAsync_Should_Return_Success_When_Key_Is_Valid()
    {
        // Arrange
        var apiKey = "plain-api-key";
        var hashedKey = ApiKeyHelper.HashApiKey(apiKey);

        var entity = new ApiKeyEntity
        {
            HashedKey = hashedKey,
            IsActive = true
        };

        _apiKeyRepositoryMock
            .Setup(x => x.GetActiveApiKeyAsync(hashedKey))
            .ReturnsAsync(entity);

        // Act
        var result = await _service.ValidateApiKeyAsync(apiKey);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().BeTrue();

        _apiKeyRepositoryMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task ValidateApiKeyAsync_Should_Fail_When_Key_Is_Invalid()
    {
        // Arrange
        _apiKeyRepositoryMock
            .Setup(x => x.GetActiveApiKeyAsync(It.IsAny<string>()))
            .ReturnsAsync((ApiKeyEntity)null);

        // Act
        var result = await _service.ValidateApiKeyAsync("invalid-key");

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Invalid api key");
    }

    #endregion

    #region GetAllApiKeysAsync

    [Test]
    public async Task GetAllApiKeysAsync_Should_Return_List_And_Update_Cache()
    {
        // Arrange
        var entities = new List<ApiKeyEntity>
        {
            new ApiKeyEntity { Id = 1, HashedKey = "key1", ClientName = "A" },
            new ApiKeyEntity { Id = 2, HashedKey = "key2", ClientName = "B" }
        };

        _apiKeyRepositoryMock
            .Setup(x => x.GetAllAsync())
            .ReturnsAsync(entities);

        _cacheServiceMock
            .Setup(x => x.GetAsync<List<string>>(It.IsAny<string>()))
            .ReturnsAsync(new List<string>());

        // Act
        var result = await _service.GetAllApiKeysAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.Should().HaveCount(2);

        _cacheServiceMock.Verify(
            x => x.SetAsync(
                RedisConstants.ApiKeysCacheKey,
                It.Is<List<string>>(l => l.Count == 2)),
            Times.Once);
    }

    #endregion

    #region UpdateApiKeyAsync

    [Test]
    public async Task UpdateApiKeyAsync_Should_Update_Entity_When_Found()
    {
        // Arrange
        var entity = new ApiKeyEntity
        {
            Id = 1,
            ClientName = "Old",
            RateLimit = 100,
            IsActive = true
        };

        _apiKeyRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(entity);

        var request = new UpdateApiKeyRequest
        {
            ClientName = "New",
            RateLimit = 500
        };

        // Act
        var result = await _service.UpdateApiKeyAsync(1, request);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Data.ClientName.Should().Be("New");
        result.Data.RateLimit.Should().Be(500);

        _apiKeyRepositoryMock.Verify(x => x.UpdateAsync(entity), Times.Once);
        _apiKeyRepositoryMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    [Test]
    public async Task UpdateApiKeyAsync_Should_Fail_When_Not_Found()
    {
        // Arrange
        _apiKeyRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<long>()))
            .ReturnsAsync((ApiKeyEntity)null);

        // Act
        var result = await _service.UpdateApiKeyAsync(99, new UpdateApiKeyRequest());

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("API key not found");
    }

    #endregion

    #region RevokeApiKeyAsync

    [Test]
    public async Task RevokeApiKeyAsync_Should_Revoke_Key()
    {
        // Arrange
        var entity = new ApiKeyEntity { Id = 1, IsActive = true };

        _apiKeyRepositoryMock
            .Setup(x => x.GetByIdAsync(1))
            .ReturnsAsync(entity);

        // Act
        var result = await _service.RevokeApiKeyAsync(1);

        // Assert
        result.IsSuccess.Should().BeTrue();
        entity.IsActive.Should().BeFalse();
        entity.RevokedAt.Should().NotBeNull();

        _apiKeyRepositoryMock.Verify(x => x.UpdateAsync(entity), Times.Once);
        _apiKeyRepositoryMock.Verify(x => x.SaveAsync(), Times.Once);
    }

    #endregion
}
