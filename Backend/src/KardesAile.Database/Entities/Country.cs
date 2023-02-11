using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Country : IEntity
{
    public Country()
    {
        Cities = new HashSet<City>();
        Supporters = new HashSet<Supporter>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public uint Version { get; set; }

    public ICollection<City> Cities { get; set; }
    public ICollection<Supporter> Supporters { get; set; }
}