using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.CommonTypes.ViewModels.DisasterVictim;

public class CreateDisasterVictimModel
{
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Phone] [Required] public string? Phone { get; set; }
    [Required] [EmailAddress] public string? Email { get; set; }
    public string? Address { get; set; }
    public string? TemporaryAddress { get; set; }
    public Guid? CityId { get; set; }
    public Guid? TemporaryCityId { get; set; }
    public Guid? CountryId { get; set; }
    public IEnumerable<CreateChildWithUserIdModel>? Children { get; set; }
    public string? IdentityNumber { get; set; }
    public bool IdentityNumberValidated { get; set; }
    public bool AddressValidated { get; set; }
}