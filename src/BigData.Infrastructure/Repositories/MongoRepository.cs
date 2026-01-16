using System.Linq.Expressions;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BigData.Domain.Interfaces;
using BigData.Infrastructure.Configuration;
using BigData.Infrastructure.Data;

namespace BigData.Infrastructure.Repositories;

/// <summary>
/// Generic MongoDB repository implementation
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class MongoRepository<T> : IMongoRepository<T> where T : class
{
    protected readonly IMongoCollection<T> _collection;

    public MongoRepository(MongoDbContext context, IOptions<MongoDbSettings> settings, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
    }

    public virtual async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(_ => true).ToListAsync(cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
    {
        return await _collection.Find(filter).ToListAsync(cancellationToken);
    }

    public virtual async Task<(IEnumerable<T> Items, long TotalCount)> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        CancellationToken cancellationToken = default)
    {
        var filterDefinition = filter != null
            ? Builders<T>.Filter.Where(filter)
            : Builders<T>.Filter.Empty;

        var totalCount = await _collection.CountDocumentsAsync(filterDefinition, cancellationToken: cancellationToken);

        var items = await _collection
            .Find(filterDefinition)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _collection.InsertOneAsync(entity, cancellationToken: cancellationToken);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(string id, T entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public virtual async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var result = await _collection.DeleteOneAsync(filter, cancellationToken);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public virtual async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq("_id", id);
        var count = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);
        return count > 0;
    }

    public virtual async Task<long> CountAsync(Expression<Func<T, bool>>? filter = null, CancellationToken cancellationToken = default)
    {
        var filterDefinition = filter != null
            ? Builders<T>.Filter.Where(filter)
            : Builders<T>.Filter.Empty;

        return await _collection.CountDocumentsAsync(filterDefinition, cancellationToken: cancellationToken);
    }
}
