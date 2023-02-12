using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.DisasterVictim;

public class UpdateDisasterVictimModel
{
    [Required] public Guid? Id { get; set; }
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Phone] [Required] public string? Phone { get; set; }
    [Required] [EmailAddress] public string? Email { get; set; }
    [Required] public string? Address { get; set; }
    [Required] public string? TemporaryAddress { get; set; }
    [Required] public Guid? CityId { get; set; }
    [Required] public Guid? TemporaryCityId { get; set; }
    [Required] public Guid? CountryId { get; set; }
    [Required] public UserStatuses? Status { get; set; }
    [Required] public bool AddressValidated { get; set; }
    [Required] public string? IdentityNumber { get; set; }
    [Required] public bool IdentityNumberValidated { get; set; }
}