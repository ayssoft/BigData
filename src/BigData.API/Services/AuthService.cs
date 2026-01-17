using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BigData.API.Services;

/// <summary>
/// Authentication service implementation
/// </summary>
public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    // In-memory user store for demonstration (replace with real database in production)
    private static readonly Dictionary<string, (string UserId, string Email, string PasswordHash)> _users = new();

    public AuthService(IConfiguration configuration, ILogger<AuthService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string GenerateToken(string username, string userId)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "YourSecretKeyHere-ChangeThisInProduction-MinimumLength32Characters!"));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.NameIdentifier, userId),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpirationMinutes"] ?? "60")),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public Task<(bool IsValid, string UserId)> ValidateCredentialsAsync(string username, string password)
    {
        if (_users.TryGetValue(username, out var user))
        {
            // Simple password comparison for demo (use proper hashing in production)
            if (user.PasswordHash == HashPassword(password))
            {
                return Task.FromResult((true, user.UserId));
            }
        }

        return Task.FromResult((false, string.Empty));
    }

    public Task<(bool Success, string Message, string UserId)> RegisterUserAsync(string username, string email, string password)
    {
        if (_users.ContainsKey(username))
        {
            return Task.FromResult((false, "Username already exists", string.Empty));
        }

        var userId = Guid.NewGuid().ToString();
        var passwordHash = HashPassword(password);

        _users[username] = (userId, email, passwordHash);

        _logger.LogInformation("User registered: {Username}", username);

        return Task.FromResult((true, "User registered successfully", userId));
    }

    private static string HashPassword(string password)
    {
        // Simple hash for demo (use BCrypt or similar in production)
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }
}
