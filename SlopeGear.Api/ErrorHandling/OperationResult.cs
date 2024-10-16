namespace SlopeGear.Api.ErrorHandling;

/// <summary>
/// Provides confirmation and information about the result of an operation
/// </summary>
public class OperationResult
{
    public OperationResult(bool isSuccess, string? errorMessage = null, Exception? exception = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
        Exception = exception;
        OperationTime = DateTime.UtcNow;
    }

    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }
    public Exception? Exception { get; }

    /// <summary>
    /// Gets the date and time when the operation was performed, in UTC-format.
    /// </summary>
    public DateTime OperationTime { get; }

    public static OperationResult Success() => new(true);
    public static OperationResult Failure(string errorMessage) => new(false, errorMessage);
    public static OperationResult Failure(string errorMessage, Exception exception) => new(false, errorMessage, exception);
}

/// <summary>
/// Provides confirmation and information about the result of an operation of generic type
/// </summary>
public sealed class OperationResult<T> : OperationResult
{
    private OperationResult(bool isSuccess, T? data = default, string? errorMessage = null, Exception? exception = null)
        : base(isSuccess, errorMessage, exception)
    {
        Data = data;
    }

    public T? Data { get; }

    public static OperationResult<T> Success(T data) => new(true, data);
    new public static OperationResult<T> Failure(string errorMessage, Exception exception) => new(false, default, errorMessage, exception);
}