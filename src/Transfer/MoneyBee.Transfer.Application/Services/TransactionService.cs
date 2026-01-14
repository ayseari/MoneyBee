namespace MoneyBee.Transfer.Application.Services
{
    using Microsoft.Extensions.Options;
    using MoneyBee.Common.Enums;
    using MoneyBee.Common.Models.Customer;
    using MoneyBee.Common.Models.Fraud;
    using MoneyBee.Common.Models.Result;
    using MoneyBee.Common.Validations;
    using MoneyBee.Transfer.Application.Models;
    using MoneyBee.Transfer.Application.Models.Settings;
    using MoneyBee.Transfer.Application.Services.Interfaces;
    using MoneyBee.Transfer.Application.Services.Interfaces.External;
    using MoneyBee.Transfer.Application.Validators;
    using MoneyBee.Transfer.Domain.Entities;
    using MoneyBee.Transfer.Domain.Repositories;
    using Newtonsoft.Json;
    using TransactionStatus = Domain.Enums.TransactionStatus;

    /// <summary>
    /// Transaction Service
    /// </summary>
    public class TransactionService(
            ITransactionRepository transactionRepository,
            ICustomerExternalService customerService,
            IFraudDetectionService fraudService,
            IExchangeRateService exchangeRateService,
            IOptionsMonitor<MoneyTransferSettings> moneyTransferSettings,
            IValidatorFactory validatorFactory) : ITransactionService
    {

        private readonly MoneyTransferSettings _moneyTransferSettings = moneyTransferSettings.CurrentValue;

        /// <summary>
        /// SendMoneyAsync
        /// </summary>
        public async Task<ServiceResult<SendMoneyResponse>> SendMoneyAsync(SendMoneyRequest request)
        {
            var senderResult = await ValidateCustomerForSendMoney(request.SenderInfo);

            if (!senderResult.IsSuccess)
            {
                return ServiceResult.Failure<SendMoneyResponse>(senderResult.ErrorMessage);
            }

            var sender = senderResult.Data;

            var receiverResult = await ValidateCustomerForSendMoney(request.ReceiverInfo);

            if (!receiverResult.IsSuccess)
            {
                return ServiceResult.Failure<SendMoneyResponse>(receiverResult.ErrorMessage);
            }

            var receiver = receiverResult.Data;

            var dailyTotal = await transactionRepository.GetDailyTotalByCustomerIdAsync(sender.Id);

            decimal amountInTRY = request.Amount;
            decimal? exchangeRate = null;

            if (request.Currency != Currency.TRY)
            {
                var exchangeResult = await exchangeRateService.Exchange(new ExchangeRequest(request.Currency.ToString(), Currency.TRY.ToString(), request.Amount));
                if (!exchangeResult.IsSuccess)
                {
                    return ServiceResult.Failure<SendMoneyResponse>(exchangeResult.ErrorMessage);
                }

                exchangeRate = exchangeResult.Data.rate;
                amountInTRY = exchangeResult.Data.exchangedValue;
            }

            if (dailyTotal + amountInTRY > _moneyTransferSettings.DailyTransferLimit)
            {
                return ServiceResult.Failure<SendMoneyResponse>($"Daily limit exceeded. Remaining limit.: {_moneyTransferSettings.DailyTransferLimit - dailyTotal} TRY");
            }

            var txCode = GenerateTxCode();

            var fraudCheckResult = await fraudService.FraudCheck(new FraudCheckRequest
            {
                UserId = sender.Id.ToString(),
                ToUserId = receiver.Id.ToString(),
                Amount = amountInTRY,
                Currency = Currency.TRY.ToString(),
                TransactionId = txCode
            });

            if (!fraudCheckResult.IsSuccess)
            {
                return ServiceResult.Failure<SendMoneyResponse>(fraudCheckResult.ErrorMessage);
            }

            var fraudParams = fraudCheckResult.Data;

            var transaction = new TransactionEntity
            {
                Id = Guid.NewGuid(),
                SenderId = sender.Id,
                ReceiverId = receiver.Id,
                Amount = request.Amount,
                Currency = request.Currency,
                AmountInTRY = amountInTRY,
                ExchangeRate = exchangeRate,
                Fee = amountInTRY * _moneyTransferSettings.FeeRate,
                RiskLevel = fraudParams.riskLevel,
                RiskScore = fraudParams.riskScore,
                TransactionCode = txCode,
                CreatedAt = DateTime.UtcNow
            };

            if (transaction.RiskLevel == RiskLevel.HIGH)
            {
                transaction.Status = TransactionStatus.REJECTED;

                transaction.AddStatusLog(request.RequestedBy, "High fraud ridk level");
                await transactionRepository.AddAsync(transaction);
                await transactionRepository.SaveAsync();

                return ServiceResult.Failure<SendMoneyResponse>("The transaction was rejected because of a high risk level");
            }

            if (amountInTRY > _moneyTransferSettings.HighAmountThreshold)
            {
                transaction.Status = TransactionStatus.WAITING_APPROVAL;
                transaction.AddStatusLog(request.RequestedBy, "High amount threshold");

                //TODO: 5 dk sonra onaylanıp command fırlat 
            }
            else
            {
                transaction.Status = TransactionStatus.APPROVED;                
                transaction.AddStatusLog(request.RequestedBy, "Approved otomatically");
            }

            await transactionRepository.AddAsync(transaction);
            await transactionRepository.SaveAsync();

            return ServiceResult.Success(new SendMoneyResponse { TransactionCode = transaction.TransactionCode });
        }

        /// <summary>
        /// IsTransactionCodeExist
        /// </summary>
        public async Task<bool> IsTransactionCodeExist(string transactionCode)
        {
            return await transactionRepository.AnyByTransactionCode(transactionCode);
        }

        /// <summary>
        /// ReceiveMoneyAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> ReceiveMoneyAsync(ReceiveMoneyRequest request)
        {
            var transaction = await transactionRepository.GetByTransactionCodeAsync(request.TransactionCode);

            if (transaction == null)
            {
                return ServiceResult.Failure<string>("Transaction not found");
            }

            if (transaction.Status != TransactionStatus.APPROVED)
            {
                return ServiceResult.Failure<string>($"Transaction status not available. Current status: {transaction.Status}");
            }

            var verificationResult = await ValidateCustomerForReceiveMoney(request.IdentityInfo);

            if (!verificationResult.IsSuccess)
            {
                return ServiceResult.Failure<string>(verificationResult.ErrorMessage);
            }

            transaction.Status = TransactionStatus.COMPLETED;
            transaction.AddStatusLog(request.CompletedByUser, "Transaction completed");
            await transactionRepository.UpdateAsync(transaction);
            await transactionRepository.SaveAsync();

            return ServiceResult.Success(
                $"Money transfer completed successfully. Amount: {transaction.Amount} {transaction.Currency}");
        }

        /// <summary>
        /// Cancel Transaction
        /// </summary>
        /// <param name="transactionCode"></param>
        /// <returns></returns>
        public async Task<ServiceResult<string>> CancelTransactionAsync(string transactionCode)
        {
            var transaction = await transactionRepository.GetByTransactionCodeAsync(transactionCode);

            if (transaction == null)
            {
                return ServiceResult.Failure<string>("Transaction not found");
            }

            var validator = validatorFactory.Create(new CancelTransactionValidator(transaction.Status));

            var validationResult = validator.Validate();
            if (!validationResult.IsValid)
            {
                return ServiceResult.Failure<string>(JsonConvert.SerializeObject(validationResult.Errors));
            }

            transaction.Status = TransactionStatus.CANCELLED;
            transaction.FeeRefunded = true;
            transaction.AddStatusLog("todo:", "Transaction completed");

            await transactionRepository.UpdateAsync(transaction);
            await transactionRepository.SaveAsync();

            return ServiceResult.Success(
                $"The transaction was cancelled. The refund amount is: {transaction.Fee} TRY");
        }

        /// <summary>
        /// Validate Customer
        /// </summary>
        private async Task<ServiceResult<CustomerDto>> ValidateCustomerForSendMoney(CustomerInfo customerInfo)
        {
            var customerResult = await customerService.GetOrCreateCustomerAsync(customerInfo);
            if (!customerResult.IsSuccess)
            {
                return ServiceResult.Failure<CustomerDto>(customerResult.ErrorMessage);
            }

            var customer = customerResult.Data;
            var validator = validatorFactory.Create(new CustomerMoneyTransferEligibilityValidator(customer.Status));

            var validationResult = validator.Validate();
            if (!validationResult.IsValid)
            {
                return ServiceResult.Failure<CustomerDto>(JsonConvert.SerializeObject(validationResult.Errors));
            }

            return ServiceResult.Success(customer);
        }

        /// <summary>
        /// Validate Customer
        /// </summary>
        private async Task<ServiceResult<CustomerDto>> ValidateCustomerForReceiveMoney(CustomerInfo customerInfo)
        {
            var customerResult = await customerService.GetCustomerAsync(customerInfo);
            if (!customerResult.IsSuccess)
            {
                return ServiceResult.Failure<CustomerDto>(customerResult.ErrorMessage);
            }

            var customer = customerResult.Data;
            var validator = validatorFactory.Create(new CustomerMoneyTransferEligibilityValidator(customer.Status));

            var validationResult = validator.Validate();
            if (!validationResult.IsValid)
            {
                return ServiceResult.Failure<CustomerDto>(JsonConvert.SerializeObject(validationResult.Errors));
            }

            return ServiceResult.Success(customer);
        }

        private string GenerateTxCode()
        {
            var guid = Guid.NewGuid().ToString("N").ToUpperInvariant();

            return $"TX-{guid.Substring(0, 12)}";
        }
    }
}
