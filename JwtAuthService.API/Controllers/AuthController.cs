using JwtAuthService.API.Responses;
using JwtAuthService.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using LoginRequest = JwtAuthService.Application.Models.Requests.LoginRequest;

namespace JwtAuthService.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IAuthService authService,
        ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequest loginUser)
    {
        var userRegistered = await _authService.RegisterUserAsync(loginUser);

        return userRegistered ?
            Ok(ApiResponse.SuccessResponse("Registration finished successfully."))
            :
            BadRequest(ApiResponse.FailResponse("Registration failed."));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginUser)
    {
        var loggedInUser = await _authService.LoginUserAsync(loginUser);

        if (loggedInUser != null)
        {
            var tokenResponse = await _tokenService.GenerateTokens(loggedInUser);

            return Ok(ApiResponse.SuccessResponse(tokenResponse, "Logged in successfully"));
        }

        return BadRequest(ApiResponse.FailResponse("Invalid credentials"));
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest refreshRequest)
    {
        if (refreshRequest == null || refreshRequest.RefreshToken.IsNullOrEmpty())
        {
            return BadRequest(ApiResponse.FailResponse("Invalid data."));
        }

        var user = await _authService.FindUserByRefreshToken(refreshRequest.RefreshToken);

        if (user != null)
        {
            var tokenResponse = await _tokenService.GenerateTokens(user);

            return Ok(ApiResponse.SuccessResponse(tokenResponse, "token refreshed successfully"));
        }

        return NotFound(ApiResponse.FailResponse("User not found."));
    }

    [HttpPost("logout")]
    public Task<IActionResult> Logout()
    {
        throw new NotImplementedException();
    }

    [HttpPost("forgot-password")]
    public Task<IActionResult> ForgotPassword()
    {
        throw new NotImplementedException();
    }

    [HttpPost("reset-password")]
    public Task<IActionResult> ResetPassword()
    {
        throw new NotImplementedException();
    }

    [HttpPost("confirm-email")]
    public Task<IActionResult> ConfirmEmail()
    {
        throw new NotImplementedException();
    }
}
