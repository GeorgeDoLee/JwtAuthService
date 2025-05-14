﻿using JwtAuthService.Application.Models.Responses;
using JwtAuthService.Application.Settings;
using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthService.Application.Services;

internal class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwt;
    public TokenService(UserManager<User> userManager, JwtSettings jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<TokenResponse> GenerateTokenAsync(User user)
    {
        var securityToken = new JwtSecurityToken(
            claims: await GetUserClaimsAsync(user),
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpirationMinutes),
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            signingCredentials: GetSigningCredentials());

        var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new TokenResponse
        {
            TokenType = "Bearer",
            Token = tokenString,
            ExpiresInMinutes = _jwt.ExpirationMinutes
        };
    }

    private async Task<IEnumerable<Claim>> GetUserClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var userClaims = await _userManager.GetClaimsAsync(user);

        claims.AddRange(userClaims);

        var userRoles = await _userManager.GetRolesAsync(user);

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private SigningCredentials GetSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key)), 
            SecurityAlgorithms.HmacSha512Signature);
    }
}
