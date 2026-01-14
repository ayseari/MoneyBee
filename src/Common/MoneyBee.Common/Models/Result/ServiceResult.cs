namespace MoneyBee.Common.Models.Result
{

    /// <summary>
    /// Result wrapper for operations that don't return data
    /// </summary>
    public class ServiceResult
    {
        /// <summary>
        /// Indicates if the operation was successful
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Error message if operation failed (null if successful)
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// Create a successful result
        /// </summary>
        /// <returns>Success result</returns>
        public static ServiceResult Success() => new()
        {
            IsSuccess = true
        };

        /// <summary>
        /// Create a failed result with error message
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns>Failure result</returns>
        public static ServiceResult Failure(string error) => new()
        {
            IsSuccess = false,
            ErrorMessage = error
        };

        /// <summary>
        /// Create a successful result with data
        /// </summary>
        /// <param name="data">Data to return</param>
        /// <returns>Success result</returns>
        public static ServiceResult<T> Success<T>(T data) => new()
        {
            IsSuccess = true,
            Data = data
        };

        /// <summary>
        /// Create a failed result with error message
        /// </summary>
        /// <param name="error">Error message</param>
        /// <returns>Failure result</returns>
        public static ServiceResult<T> Failure<T>(string error) => new()
        {
            IsSuccess = false,
            ErrorMessage = error
        };
    }

    /// <summary>
    /// Generic result wrapper for operations that return data
    /// </summary>
    /// <typeparam name="T">Type of data to return</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult() { }

        /// <summary>
        /// Data returned from the operation (null if failed)
        /// </summary>
        public T? Data { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
            ErrorMessage = null;
        }
    }
}