namespace BigData.Core.Exceptions;

/// <summary>
/// Exception thrown when validation fails
/// </summary>
public class ValidationException : Exception
{
    /// <summary>
    /// Validation errors
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException() : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors) 
        : this()
    {
        Errors = errors;
    }

    public ValidationException(string message, Exception innerException) 
        : base(message, innerException)
    {
        Errors = new Dictionary<string, string[]>();
    }
}
