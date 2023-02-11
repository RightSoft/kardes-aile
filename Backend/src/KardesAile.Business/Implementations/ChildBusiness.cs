using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.ViewModels.Child;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class ChildBusiness : IChildBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public ChildBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }
    
    public async Task Add(Guid userId, CreateChildModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
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
            .FirstOrDefaultAsync(p => p.Id == id);

        if (child == null)
        {
            throw Errors.ChildNotFound;
        }
        
        _unitOfWork.Child.Delete(child);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<ChildResultModel>> List(Guid userId)
    {
        var result = await _unitOfWork.Child
            .AsQueryable
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