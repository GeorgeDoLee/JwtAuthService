using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Application.Models.Responses;
using Microsoft.AspNetCore.Identity;

namespace JwtAuthService.Application.Interfaces;

public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllRolesAsync();

    Task<RoleResponse?> GetRoleByIdAsync(int roleId);

    Task<IdentityResult> CreateRoleAsync(CreateRoleRequest request);

    Task<RoleResponse?> GetRoleByNameAsync(string roleName);

    Task<IdentityResult> UpdateRoleAsync(int roleId, UpdateRoleRequest request);

    Task<IdentityResult> DeleteRoleAsync(int roleId);
}