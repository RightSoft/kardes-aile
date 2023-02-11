using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Child;

public class ChildResultModel
{
    public string Name { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public Genders Gender { get; set; }
}