using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels;

public class PagedSearchModel
{
    [Required]
    [Range(1, int.MaxValue)]
    public int? Page { get; set; }
    
    [Required]
    public int? PageSize { get; set; }
    
    public List<SortModel>? SortModels { get; set; }
}