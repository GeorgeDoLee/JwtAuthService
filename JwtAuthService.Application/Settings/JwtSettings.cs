using System.ComponentModel.DataAnnotations;

namespace JwtAuthService.Application.Settings;

public class JwtSettings
{
    [Required]
    public required string Key { get; set; }

    [Required]
    public required string Issuer { get; set; }

    [Required]
    public required string Audience { get; set; }

    [Range(1, 1440)]
    public required int ExpirationMinutes { get; set; }
}
