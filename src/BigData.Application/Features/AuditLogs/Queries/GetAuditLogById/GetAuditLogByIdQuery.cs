using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.AuditLogs.Queries.GetAuditLogById;

/// <summary>
/// Query to get an audit log by ID
/// </summary>
public record GetAuditLogByIdQuery(Guid Id) : IRequest<Result<AuditLogDto>>;
