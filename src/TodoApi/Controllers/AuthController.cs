using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models.DTOs;
using TodoApi.Services;
using TodoApi.Helpers;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // Login Endpoint
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (loginDto == null)
        {
            return ApiResponser.BadRequest("Login data is required.");
        }

        var response = await _authService.LoginAsync(loginDto);

        if (response == null)
        {
            return ApiResponser.Unauthorized("Invalid username or password.");
        }

        return ApiResponser.Success(response, "Login successful.");
    }

    // Register Endpoint
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            return ApiResponser.BadRequest("Registration data is required.");
        }

        var response = await _authService.RegisterAsync(registerDto);

        if (response == null)
        {
            return ApiResponser.BadRequest("Registration failed.");
        }

        return ApiResponser.Success(response, "Registration successful.", (int)System.Net.HttpStatusCode.Created);
    }

    // Change Password Endpoint
    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
        if (changePasswordDto == null)
        {
            return ApiResponser.BadRequest("Password data is required.");
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null)
        {
            return ApiResponser.Unauthorized("Unauthorized access.");
        }

        await _authService.ChangePasswordAsync(userId!, changePasswordDto);

        return ApiResponser.Success(null, "Password changed successfully", (int)System.Net.HttpStatusCode.NoContent);
    }
}

