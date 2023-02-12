using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Child : BaseEntity
{
    public string Name { get; set; } = null!;
    public DateOnly BirthDate { get; set; }
    public Genders Gender { get; set; }

    public Guid UserId { get; set; }
    public User? User { get; set; }
    
    public Match? VictimMatch { get; set; }
    public Match? SupporterMatch { get; set; }
}