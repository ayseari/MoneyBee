namespace MoneyBee.Auth.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoneyBee.Auth.Application.Models;
    using MoneyBee.Auth.Application.Services;
    using MoneyBee.Auth.Application.Services.Interfaces;
    using MoneyBee.Common.Models.Result;
    using Swashbuckle.AspNetCore.Annotations;
    using System.Net;
    using System.Reflection;

    [ApiController]
    [Route("[controller]")]
    public class ApiKeyController(IApiKeyService apiKeyService) : ControllerBase
    {
        /// <summary>
        /// Generate new API key
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<ApiKeyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("generate")]
        public async Task<IActionResult> GenerateApiKey([FromBody] CreateApiKeyRequest request)
        {
            var result = await apiKeyService.GenerateApiKeyAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Validate an API key
        /// </summary>
        /// <param name="request">API key to validate</param>
        /// <returns>Validation result with client information and rate limit</returns>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /api/apikey/validate
        ///     {
        ///        "apiKey": "mb_xK9mP2nQ5rT8vW3yZ4aB6cD7eF9gH0jK1lM2"
        ///     }
        /// 
        /// This endpoint is used by other microservices to validate incoming API keys.
        /// Response includes client name and rate limit information if valid.
        /// </remarks>
        /// <response code="200">Validation result returned</response>
        [HttpPost("validate")]
        [ProducesResponseType(typeof(ValidateApiKeyResponse), StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Validate API key",
            Description = "Checks if an API key is valid and active. Used by microservices for authentication.",
            OperationId = "ValidateApiKey",
            Tags = new[] { "API Key Validation" }
        )]
        public async Task<IActionResult> ValidateApiKey([FromBody] ValidateApiKeyRequest request)
        {
            var response = await apiKeyService.ValidateApiKeyAsync(request.ApiKey);
            return Ok(response);
        }
        /// <summary>
        /// Get all API keys
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(ServiceResult<List<ApiKeyDetailResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllApiKeys()
        {
            var result = await apiKeyService.GetAllApiKeysAsync();

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Get API key by ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ServiceResult<ApiKeyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetApiKeyById(long id)
        {
            var result = await apiKeyService.GetApiKeyByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Update API key
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ServiceResult<ApiKeyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateApiKey(long id, [FromBody] UpdateApiKeyRequest request)
        {
            var result = await apiKeyService.UpdateApiKeyAsync(id, request);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Revoke API key
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ServiceResult), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> RevokeApiKey(long id)
        {
            var result = await apiKeyService.RevokeApiKeyAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Update rate limit
        /// </summary>
        [HttpPatch("{id}/rate-limit")]
        [ProducesResponseType(typeof(ServiceResult<ApiKeyDetailResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateRateLimit(long id, [FromBody] UpdateRateLimitRequest request)
        {
            var result = await apiKeyService.UpdateRateLimitAsync(id, request);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        /// <summary>
        /// Health check endpoint
        /// </summary>
        /// <returns>Service health status</returns>
        /// <response code="200">Service is healthy</response>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Health check",
            Description = "Returns the current health status of the Auth API service.",
            OperationId = "HealthCheck",
            Tags = new[] { "Health" }
        )]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "healthy",
                service = "auth",
                timestamp = DateTime.UtcNow,
                version = "1.0.0"
            });
        }
    }
}
