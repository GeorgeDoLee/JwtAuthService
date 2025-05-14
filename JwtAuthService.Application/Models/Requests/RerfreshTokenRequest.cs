namespace JwtAuthService.Application.Models.Requests;

public class RerfreshTokenRequest
{
    public required string RefreshToken { get; set; }
}
