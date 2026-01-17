namespace BigData.Domain.Interfaces;

/// <summary>
/// Unit of Work pattern interface
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Save all changes
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Begin transaction
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commit transaction
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rollback transaction
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}
