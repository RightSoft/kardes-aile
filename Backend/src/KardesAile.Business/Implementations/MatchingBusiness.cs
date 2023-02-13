using KardesAile.Business.Interfaces;
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
    private readonly IUnitOfWork _unitOfWork;

    public MatchingBusiness(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task Create(CreateMatchModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var match = new Match
        {
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

        var match = await _unitOfWork.Match.FindById(model.MatchId!.Value);

        if (match == null)
        {
            throw Errors.MatchNotFound;
        }

        match.SupporterId = model.SupporterId!.Value;
        match.VictimId = model.VictimId!.Value;
        match.SupporterChildId = model.SupporterChildId;
        match.VictimChildId = model.VictimChildId;
        match.Active = model.Active!.Value;

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task Delete(Guid matchId)
    {
        var match = await _unitOfWork.Match.FindById(matchId);

        if (match == null)
        {
            throw Errors.MatchNotFound;
        }

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
}