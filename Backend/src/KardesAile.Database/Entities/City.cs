using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class City : IEntity
{
    public City()
    {
        Supporters = new HashSet<Supporter>();
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public uint Version { get; set; }

    public Guid CountryId { get; set; }
    public Country? Country { get; set; }
    public ICollection<Supporter> Supporters { get; set; }
}