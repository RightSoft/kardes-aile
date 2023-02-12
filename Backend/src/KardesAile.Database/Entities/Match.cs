using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Match : BaseEntity
{
    public Guid SupporterId { get; set; }
    public Supporter? Supporter { get; set; }
    public Guid VictimId { get; set; }
    public DisasterVictim? Victim { get; set; }
    public Guid? SupporterChildId { get; set; }
    public Child? SupporterChild { get; set; }
    public Guid? VictimChildId { get; set; }
    public Child? VictimChild { get; set; }
    public bool Active { get; set; }
}