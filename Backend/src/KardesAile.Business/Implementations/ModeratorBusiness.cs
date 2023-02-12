using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Exceptions;
using KardesAile.CommonTypes.Helpers;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Moderators;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace KardesAile.Business.Implementations;

public class ModeratorBusiness : IModeratorBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public ModeratorBusiness(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork
            ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<Guid> CreateAsync(CreateModeratorModel model)
    {
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        Guid? id = null;

        await TrimAndValidateEmailAsync(id, model.Email);

        TrimAndValidateFullName(model.FullName);
        TrimAndValidatePassword(model.Password);

        Moderator moderator = new()
        {
            Id = Guid.NewGuid(),
            FullName = model.FullName,
            Email = model.Email,
            Password = PasswordHelper.Hash(model.Password),
            CreatedAt = DateTime.Now
        };

        _unitOfWork.Moderators.Add(moderator);

        await _unitOfWork.SaveChangesAsync();

        return moderator.Id;
    }

    private async Task TrimAndValidateEmailAsync(Guid? id, string email)
    {
        if (email != null)
            email = email.Trim();

        if (string.IsNullOrEmpty(email))
            throw new BusinessException($"{nameof(email)} is required.");

        if (!new EmailAddressAttribute().IsValid(email))
            throw new BusinessException($"{email} is not a valid email address.");

        bool isEmailInUse = await _unitOfWork.Moderators
            .AsNoTracking
            .AnyAsync(m => m.Id != id && m.Email == email);

        if (isEmailInUse)
            throw new BusinessException($"{email} is already in use.");
    }

    private static void TrimAndValidatePassword(string password)
    {
        if (password != null)
            password = password.Trim();

        if (string.IsNullOrEmpty(password))
            throw new BusinessException($"{nameof(password)} is required.");
    }

    private static void TrimAndValidateFullName(string fullName)
    {
        if (fullName != null)
            fullName = fullName.Trim();

        if (string.IsNullOrEmpty(fullName))
            throw new BusinessException($"{nameof(fullName)} is required.");

        if (fullName.Length > 100)
            throw new BusinessException($"{nameof(fullName)} should be shorter than 100 characters.");
    }

    public async Task DeleteAsync(Guid id)
    {
        var moderator = await _unitOfWork.Moderators
            .AsQueryable
            .SingleOrDefaultAsync(m => m.Id == id);

        if (moderator == null)
            throw new BusinessException($"Moderator could not be found. Id {id}");

        moderator.IsDeleted = true;
        moderator.ModifiedAt = DateTime.UtcNow;

        _unitOfWork.Moderators.Update(moderator);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ModeratorResult> ReadAsync(Guid id)
    {
        var moderator = await _unitOfWork.Moderators
            .AsNoTracking
            .SingleOrDefaultAsync(m => m.Id == id);

        if (moderator == null)
            throw new BusinessException($"Moderator could not be found. Id {id}");

        ModeratorResult result = new()
        {
            Id = moderator.Id,
            FullName = moderator.FullName,
            Email = moderator.Email,
            IsDeleted = moderator.IsDeleted
        };

        return result;
    }

    public async Task<PagedResultModel<SearchModeratorResult>> SearchAsync(SearchModeratorModel model)
    {
        PagedResultModel<SearchModeratorResult> result = new();

        var moderators = _unitOfWork.Moderators
            .AsNoTracking
            .Where(m => (model.IncludeDeleted || !m.IsDeleted)
                && (string.IsNullOrEmpty(model.Query)
                    || EF.Functions.Like(m.FullName.ToLower(), $"%{model.Query.ToLower()}%")
                    || EF.Functions.Like(m.FullName.ToLower(), $"%{model.Query.ToLower()}%")));

        result.TotalCount = await moderators.CountAsync();

        if(result.TotalCount > 0)
        {
            result.List = await moderators
                .OrderBy(m => m.Email)
                .ThenBy(m => m.FullName)
                .Skip(((model.Page ?? 1) - 1) * (model.PageSize ?? 100))
                .Take(model.PageSize ?? 100)
                .Select(moderator => new SearchModeratorResult
                {
                    Id = moderator.Id,
                    FullName = moderator.FullName,
                    Email = moderator.Email,
                    IsDeleted = moderator.IsDeleted
                })
                .ToListAsync();
        }

        return result;
    }

    public async Task UpdateAsync(Guid id, UpdateModeratorModel model)
    {
        await TrimAndValidateEmailAsync(id, model.Email);

        TrimAndValidateFullName(model.FullName);

        if (model.UpdatePassword)
            TrimAndValidatePassword(model.Password);

        var moderator = await _unitOfWork.Moderators
            .AsQueryable
            .SingleOrDefaultAsync(m => m.Id == id);

        if (moderator == null)
            throw new BusinessException($"Moderator could not be found. Id {id}");

        moderator.FullName = model.FullName;
        moderator.Email = model.Email;

        if(model.UpdatePassword)
            moderator.Password = PasswordHelper.Hash(model.Password);

        moderator.ModifiedAt = DateTime.UtcNow;

        _unitOfWork.Moderators.Update(moderator);

        await _unitOfWork.SaveChangesAsync();
    }
}
