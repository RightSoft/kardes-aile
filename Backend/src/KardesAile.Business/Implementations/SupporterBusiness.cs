using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Supporter;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class SupporterBusiness : ISupporterBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public SupporterBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Create(CreateSupporterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var user = new User
        {
            Status = UserStatuses.Active,
            FirstName = model.FirstName!,
            LastName = model.LastName!,
            Email = model.Email!,
            EmailValidated = false,
            Phone = model.Phone!,
            PhoneValidated = false,
            Role = UserRoles.Supporter,
            Children = model.Children?.Select(i => new Child
            {
                Name = i.Name!,
                BirthDate = DateOnly.FromDateTime(i.BirthDate!.Value),
                Gender = i.Gender!.Value,
            }).ToList()!,
        };
        
        user.Supporters.Add(new Supporter
        {
            Address = model.Address,
            CountryId = model.CountryId,
            CityId = model.CityId,
        });
        
        _unitOfWork.User.Add(user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(UpdateSupporterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var supporter = await _unitOfWork.Supporter
            .AsQueryable
            .Include(p => p.User)
            .AsSplitQuery()
            .FirstOrDefaultAsync(p => p.UserId == model.Id);

        if (supporter == null)
        {
            throw Errors.SupporterNotFound;
        }
        
        supporter.Address = model.Address;
        supporter.CountryId = model.CountryId;
        supporter.CityId = model.CityId;
        supporter.User!.Email = model.Email!;
        supporter.User!.EmailValidated = false;
        supporter.User!.Phone = model.Phone!;
        supporter.User!.PhoneValidated = false;
        supporter.User!.FirstName = model.FirstName!;
        supporter.User!.LastName = model.LastName!;
        supporter.User!.Status = model.Status!.Value;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var user = await _unitOfWork.User
            .AsQueryable
            .FirstOrDefaultAsync(p => p.Id == id);

        if (user == null)
        {
            throw Errors.SupporterNotFound;
        }

        user.Status = UserStatuses.Deleted;
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResultModel<SupporterSearchResultModel>> Search(SearchSupporterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var query = _unitOfWork.Supporter
            .AsQueryable
            .Where(p => model.IncludeDeleted ||
                        p.User!.Status == UserStatuses.Active || p.User.Status == UserStatuses.Suspended);
        
        var result = new PagedResultModel<SupporterSearchResultModel>();

        if (model.Page == 1)
        {
            result.TotalCount = await _unitOfWork.Supporter
                .AsQueryable
                .CountAsync();

            if (result.TotalCount == 0)
            {
                return result;
            }
        }
        
        var list = await query
            .Include(p=>p.City)
            .Include(p=>p.Country)
            .Include(p=>p.User)
            .AsNoTracking()
            .Skip((model.Page!.Value - 1) * model.PageSize!.Value)
            .Take(model.PageSize!.Value)
            .Select(p=> new SupporterSearchResultModel
            {
                Id = p.UserId,
                FirstName = p.User!.FirstName,
                LastName = p.User!.LastName,
                Phone = p.User!.Phone,
                Email = p.User!.Email,
                Address = p.User!.Email,
                CityId = p.CityId,
                CityName = p.City!.Name,
                CountryId = p.CountryId,
                CountryName = p.Country!.Name,
                Status = p.User.Status,
                CreatedAt = p.CreatedAt
            })
            .ToListAsync();

        result.List = list;
        return result;
    }
}