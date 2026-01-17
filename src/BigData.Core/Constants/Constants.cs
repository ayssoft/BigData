namespace BigData.Core.Constants;

/// <summary>
/// Application constants
/// </summary>
public static class Constants
{
    /// <summary>
    /// Database related constants
    /// </summary>
    public static class Database
    {
        public const string MongoDbDatabaseName = "BigDataDb";
        public const string ProductsCollectionName = "products";
        public const string CassandraKeyspace = "bigdata";
        public const string AuditLogsTableName = "audit_logs";
    }

    /// <summary>
    /// Pagination constants
    /// </summary>
    public static class Pagination
    {
        public const int DefaultPageNumber = 1;
        public const int DefaultPageSize = 10;
        public const int MaxPageSize = 100;
    }

    /// <summary>
    /// JWT related constants
    /// </summary>
    public static class Jwt
    {
        public const string SecurityKey = "JwtSettings:SecretKey";
        public const string Issuer = "JwtSettings:Issuer";
        public const string Audience = "JwtSettings:Audience";
        public const int ExpirationMinutes = 60;
    }

    /// <summary>
    /// Error messages
    /// </summary>
    public static class ErrorMessages
    {
        public const string NotFound = "Resource not found";
        public const string ValidationFailed = "Validation failed";
        public const string Unauthorized = "Unauthorized access";
        public const string InternalServerError = "An error occurred while processing your request";
    }

    /// <summary>
    /// Cache keys
    /// </summary>
    public static class CacheKeys
    {
        public const string ProductPrefix = "product:";
        public const string ProductsListKey = "products:all";
    }
}
