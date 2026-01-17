using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Command to create a new product
/// </summary>
public record CreateProductCommand(CreateProductDto Product) : IRequest<Result<ProductDto>>;
