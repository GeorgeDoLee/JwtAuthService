using JwtAuthService.Application.Interfaces;
using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Constants;
using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using LoginRequest = JwtAuthService.Application.Models.Requests.LoginRequest;

namespace JwtAuthService.Application.Interfaces;

internal class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> RegisterUserAsync(LoginRequest loginUser)
    {
        var newUser = new User
        {
            UserName = loginUser.Username,
            Email = loginUser.Username
        };

        var result = await _userManager.CreateAsync(newUser, loginUser.Password);
        
        if (!result.Succeeded)
        {
            return false;
        }

        await _userManager.AddToRoleAsync(newUser, UserRoles.User);

        return true;
    }

    public async Task<User?> LoginUserAsync(LoginRequest loginUser)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            loginUser.Username,
            loginUser.Password,
            isPersistent: false,
            lockoutOnFailure: true
        );

        if (!signInResult.Succeeded)
        {
            return null;
        }

        return await _userManager.FindByNameAsync(loginUser.Username);
    }

    public async Task<User?> FindUserByRefreshToken(string refreshToken)
    {
        var user = await _userManager.Users
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);

        return user;
    }
}
