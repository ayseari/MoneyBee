namespace MoneyBee.External.Api.Client.Enums.Fraud
{
    /// <summary>
    /// Required actions
    /// </summary>
    public enum RequiredActions
    {
        /// <summary>
        /// Block transaction
        /// </summary>
        block_transaction,

        /// <summary>
        /// Notify security team
        /// </summary>
        notify_security_team,

        /// <summary>
        /// Freeze account
        /// </summary>
        freeze_account,

        /// <summary>
        /// Request OTP
        /// </summary>
        request_otp,

        /// <summary>
        /// Request KYC documents
        /// </summary>
        request_kyc_documents,

        /// <summary>
        /// Add cooldown period
        /// </summary>
        add_cooldown_period
    }
}
