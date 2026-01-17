using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.AuditLogs.Queries.GetAuditLogs;

/// <summary>
/// Query to get all audit logs
/// </summary>
public record GetAuditLogsQuery(int PageSize = 50) : IRequest<Result<IEnumerable<AuditLogDto>>>;
