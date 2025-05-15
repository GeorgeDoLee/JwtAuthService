using Microsoft.AspNetCore.Identity;

namespace JwtAuthService.Domain.Entities;

public class User : IdentityUser<int>
{
    public string? RefreshToken { get; set; }
}
