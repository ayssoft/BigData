using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;

namespace BigData.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Query to get all products with pagination
/// </summary>
public record GetAllProductsQuery(int PageNumber = 1, int PageSize = 10) : IRequest<Result<PagedResult<ProductDto>>>;
