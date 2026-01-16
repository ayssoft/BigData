namespace BigData.Application.DTOs;

/// <summary>
/// Audit log data transfer object
/// </summary>
public class AuditLogDto
{
    public Guid Id { get; set; }
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTimeOffset Timestamp { get; set; }
    public string Details { get; set; } = string.Empty;
}
