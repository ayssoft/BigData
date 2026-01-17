using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Core.Exceptions;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.Products.Queries.GetProductById;

/// <summary>
/// Handler for GetProductByIdQuery
/// </summary>
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IMongoRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IMongoRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (product == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        var productDto = _mapper.Map<ProductDto>(product);
        
        return Result<ProductDto>.Success(productDto);
    }
}
