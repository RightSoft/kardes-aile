using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.ViewModels.Address;
using KardesAile.Database.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace KardesAile.Business.Implementations;

public class AddressBusiness : IAddressBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMemoryCache _cache;

    public AddressBusiness(IUnitOfWork unitOfWork, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task<IEnumerable<CountryResultModel>> GetCountries()
    {
        var result = await _cache.GetOrCreateAsync("countries", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return _unitOfWork.Country
                .AsQueryable
                .AsNoTracking()
                .Select(p => new CountryResultModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CountryCode = p.CountryCode
                })
                .OrderBy(p => p.Name)
                .ToListAsync();
        });

        return result;
    }

    public async Task<IEnumerable<CityResultModel>> GetCities(Guid countryId)
    {
        var result = await _cache.GetOrCreateAsync($"cities_{countryId:N}", entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
            return _unitOfWork.City
                .AsQueryable
                .AsNoTracking()
                .Where(p => p.CountryId == countryId)
                .Select(p => new CityResultModel
                {
                    Id = p.Id,
                    CountryId = p.CountryId,
                    Name = p.Name,
                    StateCode = p.StateCode
                })
                .ToListAsync();
        });

        return result;
    }
}