namespace MoneyBee.External.Api.Client.Enums.Kyc
{
    /// <summary>
    /// Verification reason
    /// </summary>
    public enum KycVerificationReason
    {
        /// <summary>
        /// Verification successful
        /// </summary>
        success,

        /// <summary>
        /// Invalid Turkish National ID
        /// </summary>
        invalid_tcno,

        /// <summary>
        /// User is underage
        /// </summary>
        underage,

        /// <summary>
        /// Verification failed
        /// </summary>
        verification_failed
    }
}
