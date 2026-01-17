using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Query to get a product by ID
/// </summary>
public record GetProductByIdQuery(string Id) : IRequest<Result<ProductDto>>;
