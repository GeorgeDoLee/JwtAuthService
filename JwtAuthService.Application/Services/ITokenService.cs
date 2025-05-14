using JwtAuthService.Application.Models.Responses;
using JwtAuthService.Domain.Entities;

namespace JwtAuthService.Application.Services;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokenAsync(User user);
}
