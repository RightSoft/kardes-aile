using System.ComponentModel.DataAnnotations;

namespace KardesAile.Database.Abstracts;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    [Timestamp] public uint Version { get; set; }
}