namespace MoneyBee.Transfer.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Transfer.Application.Models;
    using MoneyBee.Transfer.Application.Services.Interfaces;
    using System.Net;

    [ApiController]
    [Route("[controller]")]
    public class TransferController(ITransactionService transactionService) : ControllerBase
    {
        /// <summary>
        /// Create Customer
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<SendMoneyResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("send")]
        public async Task<IActionResult> SendMoney([FromBody] SendMoneyRequest request)
        {
            var result = await transactionService.SendMoneyAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("receive")]
        public async Task<IActionResult> ReceiveMoney([FromBody] ReceiveMoneyRequest request)
        {
            var result = await transactionService.ReceiveMoneyAsync(request);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        /// <summary>
        /// Create Customer
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ServiceResult<string>), StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Route("{transactionCode}")]
        public async Task<IActionResult> Cancel([FromRoute] string transactionCode)
        {
            var result = await transactionService.CancelTransactionAsync(transactionCode);

            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}