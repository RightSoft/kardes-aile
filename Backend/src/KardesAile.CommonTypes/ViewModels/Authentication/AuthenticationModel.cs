using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Authentication;

public class AuthenticationModel : IValidatableObject
{
    [EmailAddress] public string? Email { get; set; }
    [Phone] public string? Phone { get; set; }

    [Required] public string? Password { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
        {
            yield return new ValidationResult("Email or phone must be filled", new[]
            {
                nameof(Email),
                nameof(Phone)
            });
        }
    }
}