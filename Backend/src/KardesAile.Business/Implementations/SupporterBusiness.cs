using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.Exceptions;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Child;
using KardesAile.CommonTypes.ViewModels.Supporter;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using KardesAile.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class SupporterBusiness : ISupporterBusiness
{
    private readonly IAuditContext _auditContext;
    private readonly IUnitOfWork _unitOfWork;

    public SupporterBusiness(IAuditContext auditContext,
        IUnitOfWork unitOfWork)
    {
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<SupporterSearchResultModel> Get(Guid id)
    {
        var supporter = await _unitOfWork.Supporter
            .AsQueryable
            .Include(p => p.City)
            .Include(p => p.Country)
            .Include(p => p.User).ThenInclude(p => p!.Children)
            .AsNoTracking()
            .Select(p => new SupporterSearchResultModel
            {
                UserId = p.UserId,
                SupporterId = p.Id,
                FirstName = p.User!.FirstName,
                LastName = p.User!.LastName,
                Phone = p.User!.Phone,
                Email = p.User!.Email,
                CityId = p.CityId,
                CityName = p.City!.Name,
                CountryId = p.CountryId,
                CountryName = p.Country!.Name,
                Address = p.Address,
                Status = p.User.Status,
                CreatedAt = p.CreatedAt,
                Children = p.User.Children.Select(c => new ChildResultModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    BirthDate = c.BirthDate.ToDateTime(TimeOnly.MinValue),
                    Gender = c.Gender
                }).ToList()
            }).FirstOrDefaultAsync(p => p.SupporterId == id);

        if (supporter == null)
            throw new BusinessException($"Supporter could not be found. Id {id}");

        return supporter;
    }

    public async Task Create(CreateSupporterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        if (string.IsNullOrEmpty(model.Phone) && string.IsNullOrEmpty(model.Email))
        {
            throw Errors.EmailOrPhoneRequired;
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
            Role = UserRoles.Supporter,
            Children = model.Children?.Select(i => new Child
            {
                Name = i.Name!,
                BirthDate = DateOnly.FromDateTime(i.BirthDate!.Value),
                Gender = i.Gender!.Value,
            }).ToList()!,
        };

        _auditContext.Start(AuditTypes.Supporter, "Supporter created");
        _auditContext.AddEffectedUser(user);

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
            .FirstOrDefaultAsync(p => p.UserId == model.UserId);

        if (supporter == null)
        {
            throw Errors.SupporterNotFound;
        }

        _auditContext.Start(AuditTypes.Supporter, "Supporter updated");
        _auditContext.AddEffectedUser(supporter.User!);

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
            .FirstOrDefaultAsync(p =>
                p.Id == id &&
                p.Role == UserRoles.Supporter &&
                (p.Status == UserStatuses.Active || p.Status == UserStatuses.Suspended));

        if (user == null)
        {
            throw Errors.SupporterNotFound;
        }

        _auditContext.Start(AuditTypes.Supporter, "Supporter deleted");
        _auditContext.AddEffectedUser(user);

        user.Status = UserStatuses.Deleted;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResultModel<SupporterSearchResultModel>> Search(SearchSupporterModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var filter = model.Keyword?.ToUpperInvariant();
        var query = _unitOfWork.Supporter
            .AsQueryable
            .Where(p => model.IncludeDeleted ||
                        p.User!.Status == UserStatuses.Active ||
                        p.User.Status == UserStatuses.Suspended)
            .Where(p => string.IsNullOrEmpty(filter) ||
                        p.User!.FirstName.ToUpper().Contains(filter) ||
                        p.User!.LastName.ToUpper().Contains(filter) ||
                        p.City!.Name.ToUpper().Contains(filter) ||
                        p.Country!.Name.ToUpper().Contains(filter) ||
                        p.Address!.ToUpper().Contains(filter));

        var result = await query
            .Include(p => p.City)
            .Include(p => p.Country)
            .Include(p => p.User).ThenInclude(u => u!.Children)
            .Include(p => p.Matches)
            .AsNoTracking()
            .Select(p => new SupporterSearchResultModel
            {
                UserId = p.UserId,
                SupporterId = p.Id,
                FirstName = p.User!.FirstName,
                LastName = p.User!.LastName,
                Phone = p.User!.Phone,
                Email = p.User!.Email,
                CityId = p.CityId,
                CityName = p.City!.Name,
                CountryId = p.CountryId,
                CountryName = p.Country!.Name,
                Address = p.Address,
                Status = p.User.Status,
                CreatedAt = p.CreatedAt,
                MatchingStatus = $"{p.Matches!.Count}/{p.User.Children.Count}",
                EmailValidated = p.User!.EmailValidated,
                PhoneValidated = p.User!.PhoneValidated,
            })
            .ToPagedListAsync(model);

        return result;
    }
}