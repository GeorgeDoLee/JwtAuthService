using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity.Data;
using LoginRequest = JwtAuthService.Application.Models.Requests.LoginRequest;

namespace JwtAuthService.Application.Interfaces;

public interface IAuthService
{
    Task<bool> RegisterUserAsync(LoginRequest loginUser);

    Task<User?> LoginUserAsync(LoginRequest loginUser);

    Task<User?> FindUserByRefreshToken(string refreshToken);
}