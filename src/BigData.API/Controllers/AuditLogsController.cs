using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BigData.Application.DTOs;
using BigData.Application.Features.AuditLogs.Commands.CreateAuditLog;
using BigData.Application.Features.AuditLogs.Queries.GetAuditLogById;
using BigData.Application.Features.AuditLogs.Queries.GetAuditLogs;

namespace BigData.API.Controllers;

/// <summary>
/// Audit logs controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuditLogsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuditLogsController> _logger;

    public AuditLogsController(IMediator mediator, ILogger<AuditLogsController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all audit logs
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AuditLogDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] int pageSize = 50)
    {
        var query = new GetAuditLogsQuery(pageSize);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Error);
    }

    /// <summary>
    /// Get audit log by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AuditLogDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetAuditLogByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Error);
    }

    /// <summary>
    /// Create new audit log
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AuditLogDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateAuditLogDto auditLogDto)
    {
        var command = new CreateAuditLogCommand(auditLogDto);
        var result = await _mediator.Send(command);

        if (result.IsSuccess)
        {
            return CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, result.Value);
        }

        return BadRequest(result.Error);
    }
}
