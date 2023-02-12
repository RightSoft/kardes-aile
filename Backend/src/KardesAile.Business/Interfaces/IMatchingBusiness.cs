using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Match;

namespace KardesAile.Business.Interfaces;

public interface IMatchingBusiness
{
    Task Create(CreateMatchModel model);
    Task Update(UpdateMatchModel model);
    Task Delete(Guid matchId);
    Task<PagedResultModel<MatchResultModel>> Search(SearchMatchModel model);
}