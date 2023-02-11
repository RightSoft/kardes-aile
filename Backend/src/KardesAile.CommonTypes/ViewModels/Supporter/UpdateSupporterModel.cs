using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class UpdateSupporterModel
{
    [Required] public Guid? Id { get; set; }
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Phone] [Required] public string? Phone { get; set; }
    [Required] [EmailAddress] public string? Email { get; set; }
    [Required] public string? Address { get; set; }
    [Required] public Guid? CityId { get; set; }
    [Required] public Guid? CountryId { get; set; }
    [Required] public UserStatuses? Status { get; set; }
}