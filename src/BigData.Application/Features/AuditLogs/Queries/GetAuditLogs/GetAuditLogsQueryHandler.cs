using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.AuditLogs.Queries.GetAuditLogs;

/// <summary>
/// Handler for GetAuditLogsQuery
/// </summary>
public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, Result<IEnumerable<AuditLogDto>>>
{
    private readonly ICassandraRepository<AuditLog> _auditLogRepository;
    private readonly IMapper _mapper;

    public GetAuditLogsQueryHandler(ICassandraRepository<AuditLog> auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public async Task<Result<IEnumerable<AuditLogDto>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
    {
        var auditLogs = await _auditLogRepository.GetPagedAsync(request.PageSize, cancellationToken: cancellationToken);

        var auditLogDtos = _mapper.Map<IEnumerable<AuditLogDto>>(auditLogs);

        return Result<IEnumerable<AuditLogDto>>.Success(auditLogDtos);
    }
}
