using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Entities;

namespace JwtAuthService.Application.Interfaces;

public interface IAuthorizationService
{
    Task<bool> RegisterUserAsync(LoginRequest loginUser);

    Task<User?> LoginUserAsync(LoginRequest loginUser);
}