using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Audit;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class AuditBusiness : IAuditBusiness
{
    private readonly IUnitOfWork _unitOfWork;

    public AuditBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<IEnumerable<AuditDetailResultModel>> GetAuditDetails(Guid auditId)
    {
        var result = await _unitOfWork.AuditDetail
            .AsQueryable
            .AsNoTracking()
            .Where(p => p.AuditId == auditId)
            .Select(p => new AuditDetailResultModel
            {
                EntityName = p.EntityName,
                EntityId = p.EntityId,
                Operation = p.Operation,
                Data = p.Data
            })
            .ToListAsync();

        return result;
    }

    public async Task<IEnumerable<AuditEffectedUserResultModel>> GetAuditEffectedUsers(Guid auditId)
    {
        var result = await _unitOfWork.AuditEffectedUser
            .AsQueryable
            .AsNoTracking()
            .Include(p => p.User)
            .Where(p => p.AuditId == auditId)
            .Select(p => new AuditEffectedUserResultModel
            {
                UserId = p.UserId,
                Username = p.User.Email
            })
            .ToListAsync();

        return result;
    }

    public async Task<PagedResultModel<AuditSearchResultModel>> Search(SearchAuditModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var filter = model.Filter?.ToUpperInvariant();

        var result = await _unitOfWork.Audit
            .AsQueryable
            .AsNoTracking()
            .Where(p =>
                (model.Type == null || p.Type == model.Type!.Value) &&
                (model.Start == null || p.CreatedAt >= model.Start!.Value) &&
                (model.End == null || p.CreatedAt <= model.End!.Value) &&
                (string.IsNullOrEmpty(filter) ||
                 p.Action.ToUpper().Contains(filter) ||
                 p.CreatedBy.ToUpper().Contains(filter) ||
                 p.AuditEffectedUsers.Any(e => e.User.Email.ToUpper().Contains(filter))))
            .Select(p => new AuditSearchResultModel
            {
                Id = p.Id,
                Type = p.Type,
                Action = p.Action,
                CreatedBy = p.CreatedBy,
                CreatedAt = p.CreatedAt
            })
            .ToPagedListAsync(model);

        return result;
    }
}