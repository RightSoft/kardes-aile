using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Audit;

public class SearchAuditModel : PagedSearchModel
{
    public string? Filter { get; set; }
    public AuditTypes? Type { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}