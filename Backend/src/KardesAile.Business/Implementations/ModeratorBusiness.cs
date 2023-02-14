using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Exceptions;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Moderators;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Extensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace KardesAile.Business.Implementations;

public class ModeratorBusiness : IModeratorBusiness
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuditContext _auditContext;

    public ModeratorBusiness(IUnitOfWork unitOfWork, IAuditContext auditContext)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
    }

    public async Task CreateAsync(CreateModeratorModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        await TrimAndValidateEmailAsync(null, model.Email!);

        var password = GeneratePasswordHash(model.Password!);

        User user = new()
        {
            FirstName = model.FirstName!.Trim(),
            LastName = model.LastName!.Trim(),
            Email = model.Email!,
            Salt = password.Item1,
            Hash = password.Item2,
            EmailValidated = false,
            Phone = "123",
            PhoneValidated = false,
            Role = UserRoles.Moderator,
            Status = UserStatuses.Active
        };

        _auditContext.Start(AuditTypes.Moderator, "Moderator created");
        _auditContext.AddEffectedUser(user);

        _unitOfWork.User.Add(user);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await _unitOfWork.User
            .AsQueryable
            .SingleOrDefaultAsync(m =>
                m.Id == id &&
                m.Role == UserRoles.Moderator &&
                (m.Status == UserStatuses.Active || m.Status == UserStatuses.Suspended));

        if (user is null)
        {
            throw new BusinessException($"Moderator could not be found. Id {id}");
        }

        _auditContext.Start(AuditTypes.Moderator, "Moderator deleted");
        _auditContext.AddEffectedUser(user);

        user.Status = UserStatuses.Deleted;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<ModeratorResult> ReadAsync(Guid id)
    {
        var user = await _unitOfWork.User
            .AsNoTracking
            .SingleOrDefaultAsync(m => m.Id == id && m.Role == UserRoles.Moderator);

        if (user is null)
        {
            throw new BusinessException($"Moderator could not be found. Id {id}");
        }

        ModeratorResult result = new()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Status = user.Status
        };

        return result;
    }

    public async Task<PagedResultModel<SearchModeratorResult>> SearchAsync(SearchModeratorModel model)
    {
        PagedResultModel<SearchModeratorResult> result = new();

        var users = _unitOfWork.User
            .AsNoTracking
            .Where(m =>
                m.Role == UserRoles.Moderator &&
                (model.IncludeDeleted || m.Status == UserStatuses.Active || m.Status == UserStatuses.Suspended) &&
                (string.IsNullOrEmpty(model.Query) ||
                 EF.Functions.Like(m.FirstName.ToLower(), $"%{model.Query.ToLower()}%") ||
                 EF.Functions.Like(m.LastName.ToLower(), $"%{model.Query.ToLower()}%") ||
                 EF.Functions.Like(m.Email.ToLower(), $"%{model.Query.ToLower()}%")
                ));
        result.TotalCount = await users.CountAsync();

        result = await users
            .OrderBy(m => m.Email)
            .ThenBy(m => m.FirstName)
            .ThenBy(m => m.LastName)
            .Select(user => new SearchModeratorResult
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Status = user.Status
            })
            .ToPagedListAsync(model);

        return result;
    }

    public async Task UpdateAsync(Guid id, UpdateModeratorModel model)
    {
        await TrimAndValidateEmailAsync(id, model.Email!);

        var user = await _unitOfWork.User
            .AsQueryable
            .SingleOrDefaultAsync(m => m.Id == id && m.Role == UserRoles.Moderator);

        if (user is null)
        {
            throw new BusinessException($"Moderator could not be found. Id {id}");
        }

        _auditContext.Start(AuditTypes.Moderator, "Moderator updated");
        _auditContext.AddEffectedUser(user);

        user.FirstName = model.FirstName!.Trim();
        user.LastName = model.LastName!.Trim();
        //user.Email = model.Email!.Trim();

        if (model.UpdatePassword)
        {
            var password = GeneratePasswordHash(model.Password!.Trim());
            user.Salt = password.Item1;
            user.Hash = password.Item2;
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private async Task TrimAndValidateEmailAsync(Guid? id, string email)
    {
        email = email.Trim();

        if (!new EmailAddressAttribute().IsValid(email))
        {
            throw new BusinessException($"{email} is not a valid email address.");
        }

        var isEmailInUse = await _unitOfWork.User
            .AsNoTracking
            .AnyAsync(m => m.Id != id && m.Email == email);

        if (isEmailInUse)
        {
            throw new BusinessException($"{email} is already in use.");
        }
    }

    private static Tuple<string, string> GeneratePasswordHash(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(256 / 8);
        var calculatedHash = KeyDerivation.Pbkdf2(
            password,
            salt,
            KeyDerivationPrf.HMACSHA256,
            200000,
            256 / 8);
        return new Tuple<string, string>(Convert.ToBase64String(salt),
            Convert.ToBase64String(calculatedHash));
    }
}