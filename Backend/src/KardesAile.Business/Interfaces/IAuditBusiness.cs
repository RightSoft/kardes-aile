using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Audit;

namespace KardesAile.Business.Interfaces;

public interface IAuditBusiness
{
    Task<IEnumerable<AuditDetailResultModel>> GetAuditDetails(Guid auditId);
    Task<IEnumerable<AuditEffectedUserResultModel>> GetAuditEffectedUsers(Guid auditId);
    Task<PagedResultModel<AuditSearchResultModel>> Search(SearchAuditModel model);
}