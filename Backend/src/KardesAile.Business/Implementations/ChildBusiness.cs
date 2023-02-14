using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.ViewModels.Child;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class ChildBusiness : IChildBusiness
{
    private readonly IAuditContext _auditContext;
    private readonly IUnitOfWork _unitOfWork;

    public ChildBusiness(
        IAuditContext auditContext,
        IUnitOfWork unitOfWork)
    {
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    private async Task<User> GetUser(Guid userId)
    {
        var result = await _unitOfWork.User
            .AsQueryable
            .FirstOrDefaultAsync(p => p.Id == userId && p.Status == UserStatuses.Active);

        if (result == null)
        {
            throw Errors.UserNotFound;
        }

        return result;
    }
    
    public async Task Add(Guid userId, CreateChildModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var user = await GetUser(userId);
        
        _auditContext.Start(AuditTypes.Child, "Child added");
        _auditContext.AddEffectedUser(user);

        _unitOfWork.Child.Add(new Child
        {
            Name = model.Name!,
            BirthDate = DateOnly.FromDateTime(model.BirthDate!.Value),
            Gender = model.Gender!.Value,
            UserId = userId,
        });

        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task Remove(Guid id)
    {
        var child = await _unitOfWork.Child
            .AsQueryable
            .Include(p=>p.User)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (child == null)
        {
            throw Errors.ChildNotFound;
        }
        
        _auditContext.Start(AuditTypes.Child, "Child removed");
        _auditContext.AddEffectedUser(child.User);
        _unitOfWork.Child.Delete(child);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<ChildResultModel>> List(Guid userId)
    {
        var result = await _unitOfWork.Child
            .AsQueryable
            .AsNoTracking()
            .Where(p => p.UserId == userId)
            .Select(p=> new ChildResultModel
            {
                Name = p.Name,
                BirthDate = p.BirthDate.ToDateTime(TimeOnly.MinValue),
                Gender = p.Gender
            })
            .ToListAsync();

        return result;
    }
}