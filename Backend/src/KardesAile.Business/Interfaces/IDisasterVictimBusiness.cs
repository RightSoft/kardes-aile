using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.DisasterVictim;

namespace KardesAile.Business.Interfaces;

public interface IDisasterVictimBusiness
{
    Task<DisasterVictimSearchResultModel> Get(Guid id);
    Task Create(CreateDisasterVictimModel model);
    Task Update(UpdateDisasterVictimModel model);
    Task Delete(Guid id);
    Task<PagedResultModel<DisasterVictimSearchResultModel>> Search(SearchDisasterVictimModel model);
}