namespace MoneyBee.Customer.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Customer.Application.Services.Interfaces;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class CustomerController(ICustomerService customerService) : ControllerBase
    {
        /// <summary>
        /// Create Customer
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request)
        {
            var result = await customerService.CreateCustomerAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("by-national-id/{nationalId}")]
        public async Task<ServiceResult<CustomerDto>> GetCustomerByNationalId([FromRoute] string nationalId)
        {
            return await customerService.GetCustomerByNationalId(nationalId);
        }

        [HttpGet("by-tax-number/{taxNumber}")]
        public async Task<ServiceResult<CustomerDto>> GetCustomerByTaxNumber([FromRoute] string taxNumber)
        {
            return await customerService.GetCustomerByTaxNumber(taxNumber);
        }

        /// <summary>
        /// Update Customer
        /// </summary>
        [HttpPost("update")]
        [ProducesResponseType(typeof(ServiceResult<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest request)
        {
            var result = await customerService.UpdateCustomerAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
