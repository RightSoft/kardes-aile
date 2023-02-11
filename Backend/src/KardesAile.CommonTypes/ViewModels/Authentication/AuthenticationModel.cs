using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Authentication;

public class AuthenticationModel
{
    [Required] [EmailAddress] public string? Email { get; set; }

    [Required] public string? Password { get; set; }
}