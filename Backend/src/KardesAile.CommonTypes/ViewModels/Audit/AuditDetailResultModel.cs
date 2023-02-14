using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Audit;

public class AuditDetailResultModel
{
    public string EntityName { get; set; } = null!;
    public Guid EntityId { get; set; }
    public OperationTypes Operation { get; set; }
    public string Data { get; set; } = null!;
}