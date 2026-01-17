using Cassandra.Mapping.Attributes;

namespace BigData.Domain.Entities;

/// <summary>
/// Audit log entity stored in Cassandra
/// </summary>
[Table("audit_logs")]
public class AuditLog
{
    /// <summary>
    /// Unique identifier
    /// </summary>
    [PartitionKey]
    [Column("id")]
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Action performed
    /// </summary>
    [Column("action")]
    public string Action { get; set; } = string.Empty;

    /// <summary>
    /// Name of the entity affected
    /// </summary>
    [Column("entity_name")]
    public string EntityName { get; set; } = string.Empty;

    /// <summary>
    /// User who performed the action
    /// </summary>
    [Column("user_id")]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp when action occurred
    /// </summary>
    [ClusteringKey(0)]
    [Column("timestamp")]
    public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;

    /// <summary>
    /// Additional details about the action
    /// </summary>
    [Column("details")]
    public string Details { get; set; } = string.Empty;
}
