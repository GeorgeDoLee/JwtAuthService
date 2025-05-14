using JwtAuthService.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuthService.API.Controllers;

[Route("api/roles")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class RolesController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetRoles()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> AddRole()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{roleId}")]
    public Task<IActionResult> UpdateRole(int roleId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{roleId}")]
    public Task<IActionResult> DeleteRole(int roleId)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{roleId}/claims")]
    public Task<IActionResult> GetClaimsByRole(int roleId)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{roleId}/claims")]
    public Task<IActionResult> AddClaimsToRoles(int roleId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{roleId}/claims/{claimId}")]
    public Task<IActionResult> RemoveClaimFromRole(int roleId, int claimId)
    {
        throw new NotImplementedException();
    }
}
