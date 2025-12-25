using TaskHub.API.DTOs.Auth;

namespace TaskHub.API.Services.Interfaces
{
    public interface IAuthService
    {
        string? GenerateToken(string username, string role, int userId);
        bool ValidateSystemCredentials(string username, string password);
        AuthResponseDto? Login(LoginDto dto);
        AuthResponseDto? Register(RegisterDto dto);
    }
}