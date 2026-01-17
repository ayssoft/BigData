using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.Products.Queries.GetAllProducts;

/// <summary>
/// Handler for GetAllProductsQuery
/// </summary>
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagedResult<ProductDto>>>
{
    private readonly IMongoRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IMongoRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var (products, totalCount) = await _productRepository.GetPagedAsync(
            request.PageNumber,
            request.PageSize,
            cancellationToken: cancellationToken);

        var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);

        var pagedResult = PagedResult<ProductDto>.Create(
            productDtos,
            totalCount,
            request.PageNumber,
            request.PageSize);

        return Result<PagedResult<ProductDto>>.Success(pagedResult);
    }
}
