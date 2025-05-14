using JwtAuthService.API.Responses;
using JwtAuthService.Application.Interfaces;
using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthService.API.Controllers;

[Route("api/roles")]
[ApiController]
[Authorize(Roles = UserRoles.Admin)]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();

        return Ok(ApiResponse.SuccessResponse(roles, "roles fetched successfully."));
    }

    [HttpGet("{roleId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetRoleById(int roleId)
    {
        var role = await _roleService.GetRoleByIdAsync(roleId);

        return role == null ?
            NotFound(ApiResponse.FailResponse($"failed to fetch role by id: {roleId}"))
            :
            Ok(ApiResponse.SuccessResponse(role, $"successfully fetched role by id: {roleId}"));
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateRole(CreateRoleRequest request)
    {
        var result = await _roleService.CreateRoleAsync(request);

        if (!result.Succeeded)
        {
            return BadRequest(ApiResponse.FailResponse($"failed to create role: {request.Name}."));
        }

        var role = await _roleService.GetRoleByNameAsync(request.Name);

        if (role == null)
        {
            return BadRequest(ApiResponse.FailResponse($"failed to create role: {request.Name}"));
        }

        return CreatedAtAction(
            nameof(GetRoleById), 
            new { roleId = role.Id }, 
            ApiResponse.SuccessResponse(role, "role created successfully"));
    }

    [HttpPut("{roleId}")]
    public async Task<IActionResult> UpdateRole(int roleId, [FromBody] UpdateRoleRequest request)
    {
        var result = await _roleService.UpdateRoleAsync(roleId, request);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to update role.";
            return BadRequest(ApiResponse.FailResponse(errorMessage));
        }

        return Ok(ApiResponse.SuccessResponse(null!, "Role updated successfully."));
    }

    [HttpDelete("{roleId}")]
    public async Task<IActionResult> DeleteRole(int roleId)
    {
        var result = await _roleService.DeleteRoleAsync(roleId);

        if (!result.Succeeded)
        {
            var errorMessage = result.Errors.FirstOrDefault()?.Description ?? "Failed to delete role.";
            return NotFound(ApiResponse.FailResponse(errorMessage));
        }

        return Ok(ApiResponse.SuccessResponse(null!, "Role deleted successfully."));
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
