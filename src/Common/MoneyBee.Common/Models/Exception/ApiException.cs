namespace MoneyBee.Common.Models.Exception
{
    using System;

    /// <summary>
    /// ApiException
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        /// <summary>
        /// Error
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// StatusCode
        /// </summary>
        public int StatusCode { set; get; }
    }
}
