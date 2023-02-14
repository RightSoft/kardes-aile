using System.ComponentModel.DataAnnotations;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class AuditEffectedUser : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AuditId { get; set; }
    public Audit? Audit { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    [Timestamp] public uint Version { get; set; }
}