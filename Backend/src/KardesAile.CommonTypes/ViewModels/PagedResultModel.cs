namespace KardesAile.CommonTypes.ViewModels;

public class PagedResultModel<T> where T: class
{
    public int? TotalCount { get; set; }
    public IEnumerable<T> List { get; set; } = Enumerable.Empty<T>();
}