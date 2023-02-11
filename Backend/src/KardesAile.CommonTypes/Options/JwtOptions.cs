using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.Options;

public class JwtOptions
{
    [Required] public string? Issuer { get; set; }
    [Required] public string? Key { get; set; }
    [Required] public int? TokenExpiresInMinutes { get; set; }
}