using KardesAile.CommonTypes.ViewModels.Child;

namespace KardesAile.Business.Interfaces;

public interface IChildBusiness
{
    Task Add(Guid userId, CreateChildModel model);
    Task Remove(Guid id);
    Task<IEnumerable<ChildResultModel>> List(Guid userId);
}