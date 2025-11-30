namespace Dsw2025Tpi.Application.Dtos
{
    public record LoginRequest(string Username, string Password);
    public record LoginResponse(Guid Id, string Username, string Role, string Token);

    public record RegisterRequest(string Username, string Password, string Role);
    public record RegisterResponse(Guid Id, string Username, string Role);

    public record UserResponse(Guid Id, string Username, string Role);
}
