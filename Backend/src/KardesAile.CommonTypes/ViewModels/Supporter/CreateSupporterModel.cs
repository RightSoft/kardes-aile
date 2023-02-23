using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class CreateSupporterModel : IValidatableObject
{
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Phone] public string? Phone { get; set; }
    [EmailAddress] public string? Email { get; set; }
    public string? Address { get; set; }
    public Guid? CityId { get; set; }
    public Guid? CountryId { get; set; }
    public IEnumerable<CreateChildModel>? Children { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrEmpty(Phone) && string.IsNullOrEmpty(Email))
        {
            yield return new ValidationResult("Email veya telefon girmelisiniz", new[]
            {
                nameof(Email),
                nameof(Phone)
            });
        }
    }
}