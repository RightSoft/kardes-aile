namespace KardesAile.CommonTypes.ViewModels.Match;

public class SearchMatchModel : PagedSearchModel
{
    public bool IncludePassives { get; set; }
    public string? Filter { get; set; }
}