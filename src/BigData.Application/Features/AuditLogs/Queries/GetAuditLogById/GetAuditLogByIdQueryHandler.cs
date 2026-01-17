using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Core.Exceptions;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.AuditLogs.Queries.GetAuditLogById;

/// <summary>
/// Handler for GetAuditLogByIdQuery
/// </summary>
public class GetAuditLogByIdQueryHandler : IRequestHandler<GetAuditLogByIdQuery, Result<AuditLogDto>>
{
    private readonly ICassandraRepository<AuditLog> _auditLogRepository;
    private readonly IMapper _mapper;

    public GetAuditLogByIdQueryHandler(ICassandraRepository<AuditLog> auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public async Task<Result<AuditLogDto>> Handle(GetAuditLogByIdQuery request, CancellationToken cancellationToken)
    {
        var auditLog = await _auditLogRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (auditLog == null)
        {
            throw new NotFoundException(nameof(AuditLog), request.Id);
        }

        var auditLogDto = _mapper.Map<AuditLogDto>(auditLog);
        
        return Result<AuditLogDto>.Success(auditLogDto);
    }
}
