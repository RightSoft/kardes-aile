using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Moderators;

namespace KardesAile.Business.Interfaces;

public interface IModeratorBusiness
{
    Task<Guid> CreateAsync(CreateModeratorModel model);
    Task DeleteAsync(Guid id);
    Task<ModeratorResult> ReadAsync(Guid id);
    Task<PagedResultModel<SearchModeratorResult>> SearchAsync(SearchModeratorModel model);
    Task UpdateAsync(Guid id, UpdateModeratorModel model);
}
