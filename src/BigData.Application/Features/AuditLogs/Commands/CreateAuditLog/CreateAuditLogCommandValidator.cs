using FluentValidation;

namespace BigData.Application.Features.AuditLogs.Commands.CreateAuditLog;

/// <summary>
/// Validator for CreateAuditLogCommand
/// </summary>
public class CreateAuditLogCommandValidator : AbstractValidator<CreateAuditLogCommand>
{
    public CreateAuditLogCommandValidator()
    {
        RuleFor(x => x.AuditLog.Action)
            .NotEmpty().WithMessage("Action is required")
            .MaximumLength(100).WithMessage("Action must not exceed 100 characters");

        RuleFor(x => x.AuditLog.EntityName)
            .NotEmpty().WithMessage("Entity name is required")
            .MaximumLength(100).WithMessage("Entity name must not exceed 100 characters");

        RuleFor(x => x.AuditLog.UserId)
            .NotEmpty().WithMessage("User ID is required");

        RuleFor(x => x.AuditLog.Details)
            .MaximumLength(2000).WithMessage("Details must not exceed 2000 characters");
    }
}
