using JwtAuthService.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthService.API.Controllers;

[Route("api/claims")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class ClaimsController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> GetClaims()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public Task<IActionResult> AddClaim()
    {
        throw new NotImplementedException();
    }

    [HttpPut("{claimId}")]
    public Task<IActionResult> UpdateClaim(int claimId)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{claimId}")]
    public Task<IActionResult> DeleteClaim(int claimId)
    {
        throw new NotImplementedException();
    }
}
