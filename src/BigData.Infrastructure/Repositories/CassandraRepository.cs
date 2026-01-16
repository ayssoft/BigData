using BigData.Domain.Interfaces;
using BigData.Infrastructure.Data;

namespace BigData.Infrastructure.Repositories;

/// <summary>
/// Generic Cassandra repository implementation
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class CassandraRepository<T> : ICassandraRepository<T> where T : class
{
    protected readonly CassandraContext _context;

    public CassandraRepository(CassandraContext context)
    {
        _context = context;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cql = $"SELECT * FROM {GetTableName()} WHERE id = ?";
        var statement = await _context.Session.PrepareAsync(cql);
        var boundStatement = statement.Bind(id);
        var result = await _context.Session.ExecuteAsync(boundStatement);
        return _context.Mapper.SingleOrDefault<T>($"SELECT * FROM {GetTableName()} WHERE id = ?", id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Task.Run(() => 
            _context.Mapper.Fetch<T>($"SELECT * FROM {GetTableName()}"), 
            cancellationToken);
    }

    public virtual async Task<IEnumerable<T>> GetPagedAsync(int pageSize, byte[]? pagingState = null, CancellationToken cancellationToken = default)
    {
        return await Task.Run(() =>
        {
            var cql = $"SELECT * FROM {GetTableName()}";
            return _context.Mapper.Fetch<T>(cql);
        }, cancellationToken);
    }

    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Mapper.InsertAsync(entity);
        return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Mapper.UpdateAsync(entity);
        return true;
    }

    public virtual async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cql = $"DELETE FROM {GetTableName()} WHERE id = ?";
        var statement = await _context.Session.PrepareAsync(cql);
        var boundStatement = statement.Bind(id);
        await _context.Session.ExecuteAsync(boundStatement);
        return true;
    }

    public virtual async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await GetByIdAsync(id, cancellationToken);
        return entity != null;
    }

    protected virtual string GetTableName()
    {
        return typeof(T).Name.ToLower() + "s";
    }
}
