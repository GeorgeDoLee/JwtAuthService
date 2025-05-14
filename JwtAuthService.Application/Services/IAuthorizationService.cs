using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Entities;

namespace JwtAuthService.Application.Services;

public interface IAuthorizationService
{
    Task<bool> RegisterUserAsync(LoginRequest loginUser);

    Task<User?> LoginUserAsync(LoginRequest loginUser);
}