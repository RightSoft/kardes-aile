using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels;

public class SortModel
{
    public string SortName { get; set; } = null!;
    public SortDirection SortDirection { get; set; } = SortDirection.Ascending;
}