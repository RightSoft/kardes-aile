using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class SupporterSearchResultModel
{
    public Guid SupporterId { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public string? Address { get; set; }
    public UserStatuses Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string MatchingStatus { get; set; }
    public bool EmailValidated { get; set; }
    public bool PhoneValidated { get; set; }
    public ICollection<ChildResultModel> Children { get; set; }
}