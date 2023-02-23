using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Supporter : BaseEntity
{
    public string? Address { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    public bool PublicOnboarding { get; set; }

    public ICollection<Match>? Matches { get; set; }
}