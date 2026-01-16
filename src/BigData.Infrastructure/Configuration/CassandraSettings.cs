namespace BigData.Infrastructure.Configuration;

/// <summary>
/// Cassandra configuration settings
/// </summary>
public class CassandraSettings
{
    /// <summary>
    /// Cassandra contact points (comma-separated)
    /// </summary>
    public string ContactPoints { get; set; } = "localhost";

    /// <summary>
    /// Cassandra port
    /// </summary>
    public int Port { get; set; } = 9042;

    /// <summary>
    /// Keyspace name
    /// </summary>
    public string Keyspace { get; set; } = "bigdata";

    /// <summary>
    /// Username for authentication
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// Password for authentication
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Datacenter name
    /// </summary>
    public string LocalDatacenter { get; set; } = "datacenter1";
}
