using AutoMapper;
using MediatR;
using BigData.Application.DTOs;
using BigData.Core.Common;
using BigData.Core.Exceptions;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.Products.Commands.UpdateProduct;

/// <summary>
/// Handler for UpdateProductCommand
/// </summary>
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<ProductDto>>
{
    private readonly IMongoRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IMongoRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Result<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingProduct == null)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        _mapper.Map(request.Product, existingProduct);
        
        await _productRepository.UpdateAsync(request.Id, existingProduct, cancellationToken);
        
        var productDto = _mapper.Map<ProductDto>(existingProduct);
        
        return Result<ProductDto>.Success(productDto);
    }
}
