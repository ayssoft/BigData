using Microsoft.Extensions.Options;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;
using BigData.Infrastructure.Configuration;
using BigData.Infrastructure.Data;

namespace BigData.Infrastructure.Repositories;

/// <summary>
/// Product repository for MongoDB
/// </summary>
public class ProductRepository : MongoRepository<Product>, IMongoRepository<Product>
{
    public ProductRepository(MongoDbContext context, IOptions<MongoDbSettings> settings)
        : base(context, settings, settings.Value.ProductsCollectionName)
    {
    }
}
