namespace JwtAuthService.Application.Models.Responses;

public class TokenResponse
{
    public required string TokenType { get; set; }
    public required string Token { get; set; }
    public required int ExpiresInMinutes { get; set; }
}
