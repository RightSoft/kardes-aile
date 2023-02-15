using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.DisasterVictim;

public class DisasterVictimSearchResultModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? Email { get; set; } = null!;
    public string? Address { get; set; }
    public Guid? CityId { get; set; }
    public Guid? TemporaryCityId { get; set; }
    public string? CityName { get; set; }
    public string? TemporaryCityName { get; set; }
    public Guid? CountryId { get; set; }
    public string? CountryName { get; set; }
    public UserStatuses Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? IdentityNumber { get; set; }
    public bool IdentityNumberValidated { get; set; }
    public bool AddressValidated { get; set; }
    public string? TemporaryAddress { get; set; }
}