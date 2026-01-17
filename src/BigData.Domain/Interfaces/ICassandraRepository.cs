namespace BigData.Domain.Interfaces;

/// <summary>
/// Generic repository interface for Cassandra operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface ICassandraRepository<T> where T : class
{
    /// <summary>
    /// Get entity by ID
    /// </summary>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all entities
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Get paged entities
    /// </summary>
    Task<IEnumerable<T>> GetPagedAsync(int pageSize, byte[]? pagingState = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new entity
    /// </summary>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing entity
    /// </summary>
    Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an entity by ID
    /// </summary>
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if entity exists
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
