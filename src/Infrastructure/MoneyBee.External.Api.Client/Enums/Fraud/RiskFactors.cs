namespace MoneyBee.External.Api.Client.Enums.Fraud
{
    /// <summary>
    /// Risk factors
    /// </summary>
    public enum RiskFactors
    {
        /// <summary>
        /// High amount
        /// </summary>
        high_amount,

        /// <summary>
        /// High velocity
        /// </summary>
        high_velocity,

        /// <summary>
        /// Daily limit exceeded
        /// </summary>
        daily_limit_exceeded,

        /// <summary>
        /// Repeated recipient
        /// </summary>
        repeated_recipient,

        /// <summary>
        /// Suspicious time
        /// </summary>
        suspicious_time,

        /// <summary>
        /// Non round amount
        /// </summary>
        non_round_amount,

        /// <summary>
        /// New user
        /// </summary>
        new_user,

        /// <summary>
        /// Blacklisted user
        /// </summary>
        blacklisted_user,

        /// <summary>
        /// Test transaction
        /// </summary>
        test_transaction
    }
}
