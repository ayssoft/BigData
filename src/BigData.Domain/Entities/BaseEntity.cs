namespace BigData.Domain.Entities;

/// <summary>
/// Base entity class with common properties
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Date and time when entity was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Date and time when entity was last updated
    /// </summary>
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
