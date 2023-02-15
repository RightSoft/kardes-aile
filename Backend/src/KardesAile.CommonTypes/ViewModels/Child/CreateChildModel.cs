using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Child;

public class CreateChildModel
{
    public Guid? UserId { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public DateTime? BirthDate { get; set; }
    [Required] public Genders? Gender { get; set; }
}