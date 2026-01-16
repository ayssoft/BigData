using Cassandra;
using Cassandra.Mapping;
using Microsoft.Extensions.Options;
using BigData.Infrastructure.Configuration;

namespace BigData.Infrastructure.Data;

/// <summary>
/// Cassandra database context
/// </summary>
public class CassandraContext : IDisposable
{
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private bool _disposed;

    public CassandraContext(IOptions<CassandraSettings> settings)
    {
        var cassandraSettings = settings.Value;
        
        var cluster = Cluster.Builder()
            .AddContactPoints(cassandraSettings.ContactPoints.Split(','))
            .WithPort(cassandraSettings.Port)
            .WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy(cassandraSettings.LocalDatacenter))
            .Build();

        _session = cluster.Connect(cassandraSettings.Keyspace);
        _mapper = new Mapper(_session);

        InitializeSchema();
    }

    /// <summary>
    /// Get Cassandra session
    /// </summary>
    public ISession Session => _session;

    /// <summary>
    /// Get Cassandra mapper
    /// </summary>
    public IMapper Mapper => _mapper;

    /// <summary>
    /// Initialize database schema
    /// </summary>
    private void InitializeSchema()
    {
        var createTableCql = @"
            CREATE TABLE IF NOT EXISTS audit_logs (
                id uuid,
                action text,
                entity_name text,
                user_id text,
                timestamp timestamp,
                details text,
                PRIMARY KEY (id, timestamp)
            ) WITH CLUSTERING ORDER BY (timestamp DESC);";

        _session.Execute(createTableCql);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _session?.Dispose();
            }
            _disposed = true;
        }
    }
}
