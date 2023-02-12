namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class SearchModeratorModel : PagedSearchModel
{
    public string Query { get; set; } = string.Empty;
    public bool IncludeDeleted { get; set; }
}
