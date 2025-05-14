﻿using JwtAuthService.Domain.Constants;

namespace JwtAuthService.Infrastructure.Seeders;

internal static class DummyData
{
    public static IEnumerable<(string Username, string Password, string Role)> Users 
        => new List<(string Username, string Password, string Role)>
        {
            ("admin", "Admin123&", UserRoles.Admin),
            ("user", "User123&", UserRoles.User)
        };

    public static IEnumerable<string> AllUserRoles => [UserRoles.User, UserRoles.Admin];
}
