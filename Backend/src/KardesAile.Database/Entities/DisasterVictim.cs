using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class DisasterVictim : BaseEntity
{
    public string? Address { get; set; }
    
    public bool AddressValidated { get; set; }
    
    public string? TemporaryAddress { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid? CountryId { get; set; }
    public Country? Country { get; set; }
    
    public Guid? CityId { get; set; }
    public City? City { get; set; }
    
    public Guid? TemporaryCityId { get; set; }
    public City? TemporaryCity { get; set; }
    
    public string? IdentityNumber { get; set; }
    public bool IdentityNumberValidated { get; set; }

    public ICollection<Match>? Matches { get; set; }
}