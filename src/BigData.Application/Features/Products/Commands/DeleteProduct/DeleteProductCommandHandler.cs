using MediatR;
using BigData.Core.Common;
using BigData.Core.Exceptions;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;

namespace BigData.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Handler for DeleteProductCommand
/// </summary>
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IMongoRepository<Product> _productRepository;

    public DeleteProductCommandHandler(IMongoRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var exists = await _productRepository.ExistsAsync(request.Id, cancellationToken);
        
        if (!exists)
        {
            throw new NotFoundException(nameof(Product), request.Id);
        }

        await _productRepository.DeleteAsync(request.Id, cancellationToken);
        
        return Result.Success();
    }
}
