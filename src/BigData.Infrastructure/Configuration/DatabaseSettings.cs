namespace BigData.Infrastructure.Configuration;

/// <summary>
/// Database configuration settings
/// </summary>
public class DatabaseSettings
{
    /// <summary>
    /// MongoDB settings
    /// </summary>
    public MongoDbSettings MongoDB { get; set; } = new();

    /// <summary>
    /// Cassandra settings
    /// </summary>
    public CassandraSettings Cassandra { get; set; } = new();
}
