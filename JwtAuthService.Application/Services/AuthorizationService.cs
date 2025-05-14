﻿using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Constants;
using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthService.Application.Services
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationService(
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
    }
}
