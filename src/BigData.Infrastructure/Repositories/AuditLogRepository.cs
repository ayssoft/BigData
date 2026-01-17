using BigData.Domain.Entities;
using BigData.Domain.Interfaces;
using BigData.Infrastructure.Data;

namespace BigData.Infrastructure.Repositories;

/// <summary>
/// Audit log repository for Cassandra
/// </summary>
public class AuditLogRepository : CassandraRepository<AuditLog>, ICassandraRepository<AuditLog>
{
    public AuditLogRepository(CassandraContext context) : base(context)
    {
    }

    protected override string GetTableName()
    {
        return "audit_logs";
    }
}
