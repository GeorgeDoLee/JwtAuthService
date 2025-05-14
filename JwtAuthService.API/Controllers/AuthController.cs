using JwtAuthService.API.Responses;
using JwtAuthService.Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = JwtAuthService.Application.Models.Requests.LoginRequest;

namespace JwtAuthService.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthorizationService _authorizationService;
    private readonly ITokenService _tokenService;

    public AuthController(
        IAuthorizationService authorizationService,
        ITokenService tokenService)
    {
        _authorizationService = authorizationService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(LoginRequest loginUser)
    {
        var userRegistered = await _authorizationService.RegisterUserAsync(loginUser);

        return userRegistered ?
            Ok(ApiResponse.SuccessResponse("Registration finished successfully."))
            :
            BadRequest(ApiResponse.FailResponse("Registration failed."));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginUser)
    {
        var loggedInUser = await _authorizationService.LoginUserAsync(loginUser);

        if (loggedInUser != null)
        {
            var tokenResponse = await _tokenService.GenerateTokenAsync(loggedInUser);

            return Ok(ApiResponse.SuccessResponse(tokenResponse, "Login successful"));
        }

        return BadRequest(ApiResponse.FailResponse("Invalid credentials"));
    }

    [HttpPost("refresh")]
    public Task<IActionResult> Refresh(RefreshRequest refreshRequest)
    {
        throw new NotImplementedException();
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
