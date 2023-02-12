namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class SearchSupporterModel : PagedSearchModel
{
    public bool IncludeDeleted { get; set; }
    public string Keyword { get; set; }
}