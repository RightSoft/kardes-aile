using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Supporter;

namespace KardesAile.Business.Interfaces;

public interface ISupporterBusiness
{
    Task Create(CreateSupporterModel model);
    Task Update(UpdateSupporterModel model);
    Task Delete(Guid id);
    Task<PagedResultModel<SupporterSearchResultModel>> Search(SearchSupporterModel model);
}