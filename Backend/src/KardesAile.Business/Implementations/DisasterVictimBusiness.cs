using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.DisasterVictim;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using KardesAile.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class DisasterVictimBusiness : IDisasterVictimBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public DisasterVictimBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Create(CreateDisasterVictimModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var emailUsed = await _unitOfWork.User.AsQueryable.AnyAsync(u => u.Email == model.Email);

        if (emailUsed)
        {
            throw Errors.EmailUsed;
        }

        var user = new User
        {
            Status = UserStatuses.Active,
            FirstName = model.FirstName!,
            LastName = model.LastName!,
            Email = model.Email!,
            EmailValidated = false,
            Phone = model.Phone!,
            PhoneValidated = false,
            Role = UserRoles.DisasterVictim,
            Children = model.Children?.Select(i => new Child
            {
                Name = i.Name!,
                BirthDate = DateOnly.FromDateTime(i.BirthDate!.Value),
                Gender = i.Gender!.Value,
            }).ToList()!,
        };

        user.DisasterVictims.Add(new DisasterVictim
        {
            Address = model.Address,
            AddressValidated = model.AddressValidated,
            TemporaryAddress = model.TemporaryAddress,
            CountryId = model.CountryId,
            CityId = model.CityId,
            TemporaryCityId = model.TemporaryCityId,
            IdentityNumber = model.IdentityNumber,
            IdentityNumberValidated = model.IdentityNumberValidated
        });

        _unitOfWork.User.Add(user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(UpdateDisasterVictimModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var disasterVictim = await _unitOfWork.DisasterVictim
            .AsQueryable
            .Include(p => p.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.UserId == model.Id);

        if (disasterVictim == null)
        {
            throw Errors.DisasterVictimNotFound;
        }

        disasterVictim.Address = model.Address;
        disasterVictim.AddressValidated = model.AddressValidated;
        disasterVictim.TemporaryAddress = model.TemporaryAddress;
        disasterVictim.CountryId = model.CountryId;
        disasterVictim.CityId = model.CityId;
        disasterVictim.TemporaryCityId = model.TemporaryCityId;
        disasterVictim.IdentityNumber = model.IdentityNumber;
        disasterVictim.IdentityNumberValidated = model.IdentityNumberValidated;
        disasterVictim.User!.Email = model.Email!;
        disasterVictim.User!.EmailValidated = false;
        disasterVictim.User!.Phone = model.Phone!;
        disasterVictim.User!.PhoneValidated = false;
        disasterVictim.User!.FirstName = model.FirstName!;
        disasterVictim.User!.LastName = model.LastName!;
        disasterVictim.User!.Status = model.Status!.Value;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await _unitOfWork.User
            .AsQueryable
            .FirstOrDefaultAsync(p => p.Id == id);

        if (user == null)
        {
            throw Errors.DisasterVictimNotFound;
        }

        user.Status = UserStatuses.Deleted;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResultModel<DisasterVictimSearchResultModel>> Search(SearchDisasterVictimModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var query = _unitOfWork.DisasterVictim
            .AsQueryable
            .Where(p => model.IncludeDeleted ||
                        p.User!.Status == UserStatuses.Active || p.User.Status == UserStatuses.Suspended);

        var result = await query
            .Include(p => p.City)
            .Include(p => p.TemporaryCity)
            .Include(p => p.Country)
            .Include(p => p.User)
            .AsNoTracking()
            .Select(p => new DisasterVictimSearchResultModel
            {
                Id = p.UserId,
                FirstName = p.User!.FirstName,
                LastName = p.User!.LastName,
                Phone = p.User!.Phone,
                Email = p.User!.Email,
                Address = p.Address,
                AddressValidated = p.AddressValidated,
                CityId = p.CityId,
                CityName = p.City!.Name,
                TemporaryAddress = p.TemporaryAddress,
                TemporaryCityId = p.TemporaryCityId,
                TemporaryCityName = p.TemporaryCity!.Name,
                CountryId = p.CountryId,
                CountryName = p.Country!.Name,
                IdentityNumber = p.IdentityNumber,
                IdentityNumberValidated = p.IdentityNumberValidated,
                Status = p.User.Status,
                CreatedAt = p.CreatedAt
            })
            .ToPagedListAsync(model);

        return result;
    }
}