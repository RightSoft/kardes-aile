using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class CreateSupporterModel
{
    [Required] public string? FirstName { get; set; }
    [Required] public string? LastName { get; set; }
    [Phone] public string? Phone { get; set; }
    [EmailAddress] public string? Email { get; set; }
    public string? Address { get; set; }
    public Guid? CityId { get; set; }
    public Guid? CountryId { get; set; }
    public IEnumerable<CreateChildModel>? Children { get; set; }
}