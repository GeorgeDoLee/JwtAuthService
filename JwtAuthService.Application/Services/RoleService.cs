using JwtAuthService.Application.Models.Requests;
using JwtAuthService.Application.Models.Responses;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthService.Application.Interfaces;

internal class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public RoleService(RoleManager<IdentityRole<int>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
    {
        var roles = await _roleManager.Roles.ToListAsync();

        return roles.Adapt<List<RoleResponse>>();
    }

    public async Task<RoleResponse?> GetRoleByIdAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        return role.Adapt<RoleResponse>();
    }

    public async Task<RoleResponse?> GetRoleByNameAsync(string roleName)
    {
        var role = await _roleManager.Roles
            .Where(r => r.Name == roleName)
            .Select(r => r.Adapt<RoleResponse>())
            .FirstOrDefaultAsync();

        return role;
    }

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleRequest request)
    {
        if (await _roleManager.RoleExistsAsync(request.Name))
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = $"Role '{request.Name}' already exists."
            });
        }

        return await _roleManager.CreateAsync(new IdentityRole<int>(request.Name));
    }

    public async Task<IdentityResult> UpdateRoleAsync(int roleId, UpdateRoleRequest request)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = "Role not found."
            });
        }

        role.Name = request.Name;

        return await _roleManager.UpdateAsync(role);
    }

    public async Task<IdentityResult> DeleteRoleAsync(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role == null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Description = "Role not found."
            });
        }

        return await _roleManager.DeleteAsync(role);
    }
}
