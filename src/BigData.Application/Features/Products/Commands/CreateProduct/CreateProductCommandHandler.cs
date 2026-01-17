using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.Products.Commands.CreateProduct;

/// <summary>
/// Handler for CreateProductCommand
/// </summary>
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<ProductDto>>
{
    private readonly IMongoRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IMongoRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.Product);
        
        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        
        var productDto = _mapper.Map<ProductDto>(createdProduct);
        
        return Result<ProductDto>.Success(productDto);
    }
}
