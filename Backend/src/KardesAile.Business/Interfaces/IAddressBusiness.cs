using KardesAile.CommonTypes.ViewModels.Address;

namespace KardesAile.Business.Interfaces;

public interface IAddressBusiness
{
    Task<IEnumerable<CountryResultModel>> GetCountries();
    Task<IEnumerable<CityResultModel>> GetCities(Guid countryId);
}