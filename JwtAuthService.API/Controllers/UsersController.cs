using JwtAuthService.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthService.API.Controllers;

[Route("api/users")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet("me")]
    public Task<IActionResult> GetCurrentUser()
    {
        throw new NotImplementedException();
    }

    [HttpPut("me")]
    public Task<IActionResult> UpdateCurrentUser()
    {
        throw new NotImplementedException();
    }

    [HttpPut("me/password")]
    public Task<IActionResult> UpdateCurrentUsersPassword()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{userId}")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> DeleteUserById(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}/roles")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> GetRolesByUser(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{userId}/roles")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> AddRolesToUser(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{userId}/roles/{roleId}")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> RemoveRoleFromUser(int userId, int roleId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{userId}/claims")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> GetClaimsByUser(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{userId}/claims")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> AddClaimsToUser(int userId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{userId}/claims/{claimId}")]
    [Authorize(Roles = UserRoles.Admin)]
    public Task<IActionResult> RemoveClaimFromUser(int userId, int claimId)
    {
        throw new NotImplementedException();
    }
}
