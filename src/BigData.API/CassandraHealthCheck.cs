using Microsoft.Extensions.Diagnostics.HealthChecks;
using BigData.Infrastructure.Data;

namespace BigData.API;

/// <summary>
/// Health check for Cassandra
/// </summary>
public class CassandraHealthCheck : IHealthCheck
{
    private readonly CassandraContext _context;

    public CassandraHealthCheck(CassandraContext context)
    {
        _context = context;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            var result = _context.Session.Execute("SELECT now() FROM system.local");
            
            if (result.Any())
            {
                return Task.FromResult(HealthCheckResult.Healthy("Cassandra is healthy"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Cassandra query returned no results"));
        }
        catch (Exception ex)
        {
            return Task.FromResult(HealthCheckResult.Unhealthy("Cassandra is unhealthy", ex));
        }
    }
}
