using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using BigData.Domain.Entities;
using BigData.Domain.Interfaces;
using BigData.Infrastructure.Configuration;
using BigData.Infrastructure.Data;
using BigData.Infrastructure.Repositories;

namespace BigData.Infrastructure;

/// <summary>
/// Dependency injection for Infrastructure layer
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Add Infrastructure services
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure settings
        services.Configure<MongoDbSettings>(options => configuration.GetSection("DatabaseSettings:MongoDB").Bind(options));
        services.Configure<CassandraSettings>(options => configuration.GetSection("DatabaseSettings:Cassandra").Bind(options));

        // Add database contexts
        services.AddSingleton<MongoDbContext>();
        services.AddSingleton<CassandraContext>();

        // Add repositories
        services.AddScoped<IMongoRepository<Product>, ProductRepository>();
        services.AddScoped<ICassandraRepository<AuditLog>, AuditLogRepository>();

        return services;
    }
}
