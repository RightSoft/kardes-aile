using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Audit : IEntity
{
    public Audit()
    {
        AuditDetails = new HashSet<AuditDetail>();
        AuditEffectedUsers = new HashSet<AuditEffectedUser>();
    }

    public AuditTypes Type { get; set; }
    public string Action { get; set; } = null!;

    public ICollection<AuditDetail> AuditDetails { get; set; }
    public ICollection<AuditEffectedUser> AuditEffectedUsers { get; set; }

    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    [Timestamp] public uint Version { get; set; }
}