namespace TodoApi.Models.DTOs;

public record LoginDto(string Email, string Password);
public record RegisterDto(string FirstName, string LastName, string Email, string Password);
public record ChangePasswordDto(string CurrentPassword, string NewPassword);
public record AuthResponseDto(string Token, DateTime Expiration, string UserId, string Email, string Role);