namespace BigData.Application.DTOs;

/// <summary>
/// Create audit log data transfer object
/// </summary>
public class CreateAuditLogDto
{
    public string Action { get; set; } = string.Empty;
    public string EntityName { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Details { get; set; } = string.Empty;
}
