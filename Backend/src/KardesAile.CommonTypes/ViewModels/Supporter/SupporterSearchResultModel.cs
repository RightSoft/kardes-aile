using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class SupporterSearchResultModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Phone { get; set; }
    public string Email { get; set; } = null!;
    public string? Address { get; set; }
    public Guid? CityId { get; set; }
    public string? CityName { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public UserStatuses Status { get; set; }
    public DateTime CreatedAt { get; set; }
}