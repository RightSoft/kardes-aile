namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class SearchModeratorModel : PagedSearchModel
{
    public string? Query { get; set; }
    public bool IncludeDeleted { get; set; }
}
