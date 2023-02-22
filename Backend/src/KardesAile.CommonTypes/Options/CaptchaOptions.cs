using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.Options;

public class CaptchaOptions
{
    [Required]
    public string? ClientKey { get; set; }
    
    [Required]
    public string? ServerKey { get; set; }
}