using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.ViewModels.Address;
using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class AddressBusiness : IAddressBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public AddressBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<CountryResultModel>> GetCountries()
    {
        var result = await _unitOfWork.Country
            .AsQueryable
            .AsNoTracking()
            .Select(p => new CountryResultModel
            {
                Id = p.Id,
                Name = p.Name
            })
            .ToListAsync();
        return result;
    }

    public async Task<IEnumerable<CityResultModel>> GetCities(Guid countryId)
    {
        var result = await _unitOfWork.City
            .AsQueryable
            .AsNoTracking()
            .Where(p => p.CountryId == countryId)
            .Select(p=> new CityResultModel
            {
                Id = p.Id,
                CountryId = p.CountryId,
                Name = p.Name
            })
            .ToListAsync();
        return result;
    }
}