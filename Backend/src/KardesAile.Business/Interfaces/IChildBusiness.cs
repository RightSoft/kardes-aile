using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.Business.Interfaces;

public interface IChildBusiness
{
    Task Add(CreateChildWithUserIdModel withUserIdModel);
    Task Update(UpdateChildModel model);
    Task Remove(Guid id);
    Task<IEnumerable<ChildResultModel>> List(Guid userId);
}