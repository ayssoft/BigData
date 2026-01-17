using MediatR;
using BigData.Core.Common;

namespace BigData.Application.Features.Products.Commands.DeleteProduct;

/// <summary>
/// Command to delete a product
/// </summary>
public record DeleteProductCommand(string Id) : IRequest<Result>;
