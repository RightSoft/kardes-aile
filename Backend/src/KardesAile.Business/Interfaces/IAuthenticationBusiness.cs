using KardesAile.CommonTypes.ViewModels.Authentication;

namespace KardesAile.Business.Interfaces;

public interface IAuthenticationBusiness
{
    Task<AuthenticationResultModel> Authenticate(AuthenticationModel model);
}