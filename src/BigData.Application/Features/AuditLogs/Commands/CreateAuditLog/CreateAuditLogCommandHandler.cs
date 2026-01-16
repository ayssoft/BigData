using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.AuditLogs.Commands.CreateAuditLog;

/// <summary>
/// Handler for CreateAuditLogCommand
/// </summary>
public class CreateAuditLogCommandHandler : IRequestHandler<CreateAuditLogCommand, Result<AuditLogDto>>
{
    private readonly ICassandraRepository<AuditLog> _auditLogRepository;
    private readonly IMapper _mapper;

    public CreateAuditLogCommandHandler(ICassandraRepository<AuditLog> auditLogRepository, IMapper mapper)
    {
        _auditLogRepository = auditLogRepository;
        _mapper = mapper;
    }

    public async Task<Result<AuditLogDto>> Handle(CreateAuditLogCommand request, CancellationToken cancellationToken)
    {
        var auditLog = _mapper.Map<AuditLog>(request.AuditLog);
        
        var createdAuditLog = await _auditLogRepository.CreateAsync(auditLog, cancellationToken);
        
        var auditLogDto = _mapper.Map<AuditLogDto>(createdAuditLog);
        
        return Result<AuditLogDto>.Success(auditLogDto);
    }
}
