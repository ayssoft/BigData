using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.AuditLogs.Commands.CreateAuditLog;

/// <summary>
/// Command to create a new audit log
/// </summary>
public record CreateAuditLogCommand(CreateAuditLogDto AuditLog) : IRequest<Result<AuditLogDto>>;
