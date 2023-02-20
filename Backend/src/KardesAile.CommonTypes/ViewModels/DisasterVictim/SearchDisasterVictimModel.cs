namespace KardesAile.CommonTypes.ViewModels.DisasterVictim;

public class SearchDisasterVictimModel : PagedSearchModel
{
    public bool IncludeDeleted { get; set; }
    public string? Keyword { get; set; }
}