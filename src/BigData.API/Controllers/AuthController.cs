using Microsoft.AspNetCore.Mvc;
using BigData.API.Services;
using BigData.Application.DTOs;

namespace BigData.API.Controllers;

/// <summary>
/// Authentication controller
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Login user
    /// </summary>
    [HttpPost("login")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var (isValid, userId) = await _authService.ValidateCredentialsAsync(loginDto.Username, loginDto.Password);

        if (!isValid)
        {
            return Unauthorized(new { message = "Invalid username or password" });
        }

        var token = _authService.GenerateToken(loginDto.Username, userId);

        _logger.LogInformation("User logged in: {Username}", loginDto.Username);

        return Ok(new
        {
            token,
            username = loginDto.Username,
            userId
        });
    }

    /// <summary>
    /// Register new user
    /// </summary>
    [HttpPost("register")]
    [ProducesResponseType(typeof(object), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (registerDto.Password != registerDto.ConfirmPassword)
        {
            return BadRequest(new { message = "Passwords do not match" });
        }

        var (success, message, userId) = await _authService.RegisterUserAsync(
            registerDto.Username,
            registerDto.Email,
            registerDto.Password);

        if (!success)
        {
            return BadRequest(new { message });
        }

        var token = _authService.GenerateToken(registerDto.Username, userId);

        _logger.LogInformation("User registered: {Username}", registerDto.Username);

        return CreatedAtAction(nameof(Login), new
        {
            token,
            username = registerDto.Username,
            userId,
            message = "User registered successfully"
        });
    }
}
