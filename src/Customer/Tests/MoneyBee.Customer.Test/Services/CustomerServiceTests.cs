using Microsoft.Extensions.Logging;
using Microsoft.Testing.Platform.Logging;
using MoneyBee.Common.Enums;
using MoneyBee.Common.Models.Customer;
using MoneyBee.Common.Validations;
using MoneyBee.Customer.Application.Interfaces.External;
using MoneyBee.Customer.Application.Services;
using MoneyBee.Customer.Domain.Entities;
using MoneyBee.Customer.Domain.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using MoneyBee.Common.Validations;
using MoneyBee.Customer.Application.Validators;
using MoneyBee.Common.Models.Result;

[TestFixture]
public class CustomerServiceTests
{
    private Mock<ICustomerRepository> _customerRepository;
    private Mock<IKycService> _kycService;
    private Mock<IValidatorFactory> _validatorFactory;
    private Mock<Microsoft.Extensions.Logging.ILogger<CustomerService>> _logger;

    private CustomerService _service;

    [SetUp]
    public void Setup()
    {
        _customerRepository = new Mock<ICustomerRepository>();
        _kycService = new Mock<IKycService>();
        _validatorFactory = new Mock<IValidatorFactory>();
        _logger = new Mock<Microsoft.Extensions.Logging.ILogger<CustomerService>>();

        _service = new CustomerService(
            _customerRepository.Object,
            _kycService.Object,
            _validatorFactory.Object,
            _logger.Object);
    }

    #region CreateCustomerAsync

    //[Test]
    //public async Task CreateCustomerAsync_Should_ReturnFailure_When_ValidationFails()
    //{
    //    // Arrange
    //    var request = new CreateCustomerRequest
    //    {
    //        PhoneNumber = "123",
    //        Type = CustomerType.Individual
    //    };

    //    var validatorMock = new Mock<IBusinessRuleValidator>();
    //    validatorMock.Setup(v => v.Validate())
    //        .Returns(new MoneyBee.Common.Validations.ValidationResult
    //        {
    //            IsValid = false,
    //            Errors = ["Invalid phone"]
    //        });

    //    _validatorFactory
    //        .Setup(v => v.Create(It.IsAny<CustomerValidator>()))
    //        .Returns(validatorMock.Object);

    //    // Act
    //    var result = await _service.CreateCustomerAsync(request);

    //    // Assert
    //    Assert.That(result.IsSuccess, Is.False);
    //    _customerRepository.Verify(r => r.AddAsync(It.IsAny<CustomerEntity>()), Times.Never);
    //}

    [Test]
    public async Task CreateCustomerAsync_Should_ReturnFailure_When_KycFails()
    {
        // Arrange
        var request = new CreateCustomerRequest
        {
            PhoneNumber = "+905551112233",
            Type = CustomerType.Individual
        };

        var validatorMock = new Mock<IBusinessRuleValidator>();
        validatorMock.Setup(v => v.Validate())
            .Returns(new MoneyBee.Common.Validations.ValidationResult { IsValid= true, Errors=null});

        _validatorFactory
            .Setup(v => v.Create(It.IsAny<CustomerValidator>()))
            .Returns(validatorMock.Object);

        _kycService.Setup(k =>
                k.VerifyKycAsync(It.IsAny<CreateCustomerRequest>(), It.IsAny<Guid>()))
            .ReturnsAsync(ServiceResult.Failure<bool>("KYC failed"));

        // Act
        var result = await _service.CreateCustomerAsync(request);

        // Assert
        Assert.That(result.IsSuccess, Is.False);
        _customerRepository.Verify(r => r.AddAsync(It.IsAny<CustomerEntity>()), Times.Never);
    }

    [Test]
    public async Task CreateCustomerAsync_Should_CreateCustomer_When_AllValid()
    {
        // Arrange
        var request = new CreateCustomerRequest
        {
            PhoneNumber = "+905551112233",
            FirstName = "Ali",
            LastName = "Veli",
            Type = CustomerType.Individual
        };

        var validatorMock = new Mock<IBusinessRuleValidator>();
        validatorMock.Setup(v => v.Validate())
            .Returns(new MoneyBee.Common.Validations.ValidationResult());

        _validatorFactory
            .Setup(v => v.Create(It.IsAny<CustomerValidator>()))
            .Returns(validatorMock.Object);

        _kycService.Setup(k =>
                k.VerifyKycAsync(It.IsAny<CreateCustomerRequest>(), It.IsAny<Guid>()))
            .ReturnsAsync(ServiceResult.Success(true));

        // Act
        var result = await _service.CreateCustomerAsync(request);

        // Assert
        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Data, Is.Not.Null);

        _customerRepository.Verify(r => r.AddAsync(It.IsAny<CustomerEntity>()), Times.Once);
        _customerRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    #endregion

    #region GetCustomer

    [Test]
    public async Task GetCustomerByNationalId_Should_ReturnFailure_When_NotFound()
    {
        _customerRepository
            .Setup(r => r.GetByNationalId("123"))
            .ReturnsAsync((CustomerEntity)null);

        var result = await _service.GetCustomerByNationalId("123");

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task GetCustomerByNationalId_Should_ReturnCustomer_When_Found()
    {
        _customerRepository
            .Setup(r => r.GetByNationalId("123"))
            .ReturnsAsync(new CustomerEntity
            {
                NationalId = "123",
                FirstName = "Ali"
            });

        var result = await _service.GetCustomerByNationalId("123");

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Data.FirstName, Is.EqualTo("Ali"));
    }

    #endregion

    #region UpdateCustomer

    [Test]
    public async Task UpdateCustomerAsync_Should_ReturnFailure_When_CustomerNotFound()
    {
        _customerRepository
            .Setup(r => r.GetById(It.IsAny<Guid>()))
            .ReturnsAsync((CustomerEntity)null);

        var result = await _service.UpdateCustomerAsync(new UpdateCustomerRequest
        {
            CustomerId = Guid.NewGuid()
        });

        Assert.That(result.IsSuccess, Is.False);
    }

    [Test]
    public async Task UpdateCustomerAsync_Should_UpdateCustomer_When_Valid()
    {
        var customer = new CustomerEntity
        {
            Id = Guid.NewGuid(),
            FirstName = "Old",
            PhoneNumber = "+905551112233",
            Status = CustomerStatus.Active
        };

        _customerRepository
            .Setup(r => r.GetById(customer.Id))
            .ReturnsAsync(customer);

        var result = await _service.UpdateCustomerAsync(new UpdateCustomerRequest
        {
            CustomerId = customer.Id,
            FirstName = "New"
        });

        Assert.That(result.IsSuccess, Is.True);
        Assert.That(result.Data.FirstName, Is.EqualTo("New"));

        _customerRepository.Verify(r => r.UpdateAsync(customer), Times.Once);
        _customerRepository.Verify(r => r.SaveAsync(), Times.Once);
    }

    #endregion
}
