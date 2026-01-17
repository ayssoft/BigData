using FluentValidation;

namespace BigData.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Validator for UpdateProductCommand
/// </summary>
public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Product ID is required");

        RuleFor(x => x.Product.Name)
            .NotEmpty().WithMessage("Product name is required")
            .MaximumLength(200).WithMessage("Product name must not exceed 200 characters");

        RuleFor(x => x.Product.Description)
            .NotEmpty().WithMessage("Product description is required")
            .MaximumLength(1000).WithMessage("Product description must not exceed 1000 characters");

        RuleFor(x => x.Product.Price)
            .GreaterThan(0).WithMessage("Product price must be greater than 0");

        RuleFor(x => x.Product.Category)
            .NotEmpty().WithMessage("Product category is required")
            .MaximumLength(100).WithMessage("Product category must not exceed 100 characters");
    }
}
