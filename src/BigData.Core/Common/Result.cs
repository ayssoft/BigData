namespace BigData.Core.Common;

/// <summary>
/// Result pattern for operation outcomes
/// </summary>
public class Result
{
    /// <summary>
    /// Indicates if operation succeeded
    /// </summary>
    public bool IsSuccess { get; protected set; }

    /// <summary>
    /// Indicates if operation failed
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string Error { get; protected set; } = string.Empty;

    /// <summary>
    /// List of validation errors
    /// </summary>
    public List<string> Errors { get; protected set; } = new();

    protected Result(bool isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    protected Result(bool isSuccess, List<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors;
        Error = errors.Count > 0 ? string.Join("; ", errors) : string.Empty;
    }

    /// <summary>
    /// Create success result
    /// </summary>
    public static Result Success() => new(true, string.Empty);

    /// <summary>
    /// Create failure result
    /// </summary>
    public static Result Failure(string error) => new(false, error);

    /// <summary>
    /// Create failure result with multiple errors
    /// </summary>
    public static Result Failure(List<string> errors) => new(false, errors);
}
