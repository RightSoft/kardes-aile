using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.ViewModels;
using KardesAile.CommonTypes.ViewModels.Match;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using KardesAile.Database.Extensions;
using Microsoft.EntityFrameworkCore;

namespace KardesAile.Business.Implementations;

public class MatchingBusiness : IMatchingBusiness
{
    private readonly IAuditContext _auditContext;
    private readonly IUnitOfWork _unitOfWork;

    public MatchingBusiness(
        IAuditContext auditContext,
        IUnitOfWork unitOfWork)
    {
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    private async Task<Supporter> GetSupporter(Guid supporterId)
    {
        var result = await _unitOfWork.Supporter
            .AsQueryable
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == supporterId && p.User!.Status == UserStatuses.Active);

        if (result == null)
        {
            throw Errors.SupporterNotFound;
        }

        return result;
    }

    private async Task<DisasterVictim> GetVictim(Guid victimId)
    {
        var result = await _unitOfWork.DisasterVictim
            .AsQueryable
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Id == victimId && p.User!.Status == UserStatuses.Active);

        if (result == null)
        {
            throw Errors.DisasterVictimNotFound;
        }

        return result;
    }

    public async Task Create(CreateMatchModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var supporter = await GetSupporter(model.SupporterId!.Value);
        var victim = await GetVictim(model.VictimId!.Value);
        
        _auditContext.Start(AuditTypes.Matching, "Matching created");
        _auditContext.AddEffectedUser(supporter.User!);
        _auditContext.AddEffectedUser(victim.User!);
        
        var match = new Match
        {
            Active = true,
            SupporterId = model.SupporterId!.Value,
            VictimId = model.VictimId!.Value,
            SupporterChildId = model.SupporterChildId,
            VictimChildId = model.VictimChildId,
        };
        _unitOfWork.Match.Add(match);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Update(UpdateMatchModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var match = await _unitOfWork.Match
            .AsQueryable
            .Include(p => p.Supporter)
            .ThenInclude(p => p!.User)
            .Include(p => p.Victim)
            .ThenInclude(p => p!.User)
            .FirstOrDefaultAsync(p=>p.Id == model.MatchId!.Value);

        if (match == null)
        {
            throw Errors.MatchNotFound;
        }
        
        _auditContext.Start(AuditTypes.Matching, "Matching updated");
        _auditContext.AddEffectedUser(match.Supporter!.User!);
        _auditContext.AddEffectedUser(match.Victim!.User!);

        match.SupporterId = model.SupporterId!.Value;
        match.VictimId = model.VictimId!.Value;
        match.SupporterChildId = model.SupporterChildId;
        match.VictimChildId = model.VictimChildId;
        match.Active = model.Active!.Value;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid matchId)
    {
        var match = await _unitOfWork.Match
            .AsQueryable
            .Include(p => p.Supporter)
            .ThenInclude(p => p!.User)
            .Include(p => p.Victim)
            .ThenInclude(p => p!.User)
            .FirstOrDefaultAsync(p=>p.Id == matchId);

        if (match == null)
        {
            throw Errors.MatchNotFound;
        }
        
        _auditContext.Start(AuditTypes.Matching, "Matching deleted");
        _auditContext.AddEffectedUser(match.Supporter!.User!);
        _auditContext.AddEffectedUser(match.Victim!.User!);

        _unitOfWork.Match.Delete(match);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<PagedResultModel<MatchResultModel>> Search(SearchMatchModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        var filter = model.Filter?.ToUpperInvariant();

        var query = _unitOfWork.Match
            .AsQueryable
            .AsNoTracking()
            .Where(p =>
                (model.IncludePassives || p.Active) &&
                (
                    string.IsNullOrEmpty(filter) ||
                    p.Supporter!.User!.FirstName.ToUpper().Contains(filter) ||
                    p.Supporter!.User!.LastName.ToUpper().Contains(filter) ||
                    p.SupporterChild!.Name.ToUpper().Contains(filter) ||
                    p.VictimChild!.Name.ToUpper().Contains(filter)));

        var result = await query
            .Include(p => p.Supporter!.User)
            .Include(p => p.SupporterChild)
            .Include(p => p.Victim!.User)
            .Include(p => p.VictimChild)
            .Select(p => new MatchResultModel
            {
                Id = p.Id,
                VictimId = p.VictimId,
                VictimFirstName = p.Victim!.User!.FirstName,
                VictimLastName = p.Victim!.User!.LastName,
                SupporterId = p.SupporterId,
                SupporterFirstName = p.Supporter!.User!.FirstName,
                SupporterLastName = p.Supporter!.User!.LastName,
                CreatedAt = p.CreatedAt,
                Active = p.Active,
                VictimChildId = p.VictimChildId,
                VictimChildName = p.VictimChild!.Name,
                SupporterChildId = p.SupporterChildId,
                SupporterChildName = p.SupporterChild!.Name
            })
            .ToPagedListAsync(model);

        return result;
    }
    
    public async Task<MatchResultModel> Get(Guid id)
    {
        if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
        
        var result = await _unitOfWork.Match
            .AsQueryable
            .AsNoTracking()
            .Include(p => p.Supporter!.User)
            .Include(p => p.SupporterChild)
            .Include(p => p.Victim!.User)
            .Include(p => p.VictimChild)
            .Select(p => new MatchResultModel
            {
                Id = p.Id,
                VictimId = p.VictimId,
                VictimFirstName = p.Victim!.User!.FirstName,
                VictimLastName = p.Victim!.User!.LastName,
                SupporterId = p.SupporterId,
                SupporterFirstName = p.Supporter!.User!.FirstName,
                SupporterLastName = p.Supporter!.User!.LastName,
                CreatedAt = p.CreatedAt,
                Active = p.Active,
                VictimChildId = p.VictimChildId,
                VictimChildName = p.VictimChild!.Name,
                SupporterChildId = p.SupporterChildId,
                SupporterChildName = p.SupporterChild!.Name
            })
            .FirstOrDefaultAsync(p => p.Id == id);

        return result;
    }
}