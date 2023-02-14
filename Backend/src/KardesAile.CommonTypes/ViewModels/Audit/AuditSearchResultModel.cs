using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Audit;

public class AuditSearchResultModel
{
    public Guid Id { get; set; }
    public AuditTypes Type { get; set; }
    public string Action { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}