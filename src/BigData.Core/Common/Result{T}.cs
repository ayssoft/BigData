namespace BigData.Core.Common;

/// <summary>
/// Result pattern with value for operation outcomes
/// </summary>
/// <typeparam name="T">Type of value returned</typeparam>
public class Result<T> : Result
{
    /// <summary>
    /// The value returned by operation
    /// </summary>
    public T? Value { get; private set; }

    protected Result(T value, bool isSuccess, string error) : base(isSuccess, error)
    {
        Value = value;
    }

    protected Result(T value, bool isSuccess, List<string> errors) : base(isSuccess, errors)
    {
        Value = value;
    }

    /// <summary>
    /// Create success result with value
    /// </summary>
    public static Result<T> Success(T value) => new(value, true, string.Empty);

    /// <summary>
    /// Create failure result
    /// </summary>
    public new static Result<T> Failure(string error) => new(default!, false, error);

    /// <summary>
    /// Create failure result with multiple errors
    /// </summary>
    public new static Result<T> Failure(List<string> errors) => new(default!, false, errors);
}
