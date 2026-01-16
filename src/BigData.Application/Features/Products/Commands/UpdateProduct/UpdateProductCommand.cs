using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Command to update an existing product
/// </summary>
public record UpdateProductCommand(string Id, UpdateProductDto Product) : IRequest<Result<ProductDto>>;
