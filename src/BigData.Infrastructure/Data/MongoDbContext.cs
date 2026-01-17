using Microsoft.Extensions.Options;
using MongoDB.Driver;
using BigData.Infrastructure.Configuration;

namespace BigData.Infrastructure.Data;

/// <summary>
/// MongoDB database context
/// </summary>
public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var mongoSettings = settings.Value;
        var client = new MongoClient(mongoSettings.ConnectionString);
        _database = client.GetDatabase(mongoSettings.DatabaseName);
    }

    /// <summary>
    /// Get MongoDB collection
    /// </summary>
    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }

    /// <summary>
    /// Get database instance
    /// </summary>
    public IMongoDatabase Database => _database;
}
