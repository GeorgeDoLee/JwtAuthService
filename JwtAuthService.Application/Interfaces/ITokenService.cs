﻿using JwtAuthService.Application.Models.Responses;
using JwtAuthService.Domain.Entities;

namespace JwtAuthService.Application.Interfaces;

public interface ITokenService
{
    Task<TokenResponse> GenerateTokens(User user);
}
