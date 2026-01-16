namespace BigData.API.Services;

/// <summary>
/// Authentication service interface
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Generate JWT token for user
    /// </summary>
    string GenerateToken(string username, string userId);

    /// <summary>
    /// Validate credentials
    /// </summary>
    Task<(bool IsValid, string UserId)> ValidateCredentialsAsync(string username, string password);

    /// <summary>
    /// Register new user
    /// </summary>
    Task<(bool Success, string Message, string UserId)> RegisterUserAsync(string username, string email, string password);
}
