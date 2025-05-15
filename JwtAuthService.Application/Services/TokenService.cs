using JwtAuthService.Application.Interfaces;
using JwtAuthService.Application.Models.Responses;
using JwtAuthService.Application.Settings;
using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JwtAuthService.Application.Interfaces;

internal class TokenService : ITokenService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwt;
    public TokenService(UserManager<User> userManager, JwtSettings jwt)
    {
        _userManager = userManager;
        _jwt = jwt;
    }

    public async Task<TokenResponse> GenerateTokens(User user)
    {
        var token = await GenerateJwtTokenAsync(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        await _userManager.UpdateAsync(user);

        return new TokenResponse
        {
            TokenType = "Bearer",
            Token = token,
            RefreshToken = refreshToken,
            ExpiresInMinutes = _jwt.ExpirationMinutes
        };
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }

    private async Task<string> GenerateJwtTokenAsync(User user)
    {
        var securityToken = new JwtSecurityToken(
            claims: await GetUserClaimsAsync(user),
            expires: DateTime.UtcNow.AddMinutes(_jwt.ExpirationMinutes),
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            signingCredentials: GetSigningCredentials());

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
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
