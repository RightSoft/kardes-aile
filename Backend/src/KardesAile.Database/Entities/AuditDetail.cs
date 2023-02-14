using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class AuditDetail : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string EntityName { get; set; } = null!;
    public Guid EntityId { get; set; }
    public OperationTypes Operation { get; set; }
    public string Data { get; set; } = null!;
    public Guid AuditId { get; set; }
    public Audit? Audit { get; set; }
    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = null!;

    [Timestamp] public uint Version { get; set; }
}