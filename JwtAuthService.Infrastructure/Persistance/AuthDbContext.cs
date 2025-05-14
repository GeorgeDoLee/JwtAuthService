using JwtAuthService.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JwtAuthService.API")]
namespace JwtAuthService.Infrastructure.Persistance;

internal class AuthDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options)
        : base(options)
    {
    }
}
