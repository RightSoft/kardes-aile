using System.ComponentModel.DataAnnotations;
using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Child;

public class UpdateChildModel
{
    [Required] public Guid? Id { get; set; }
    [Required] public string? Name { get; set; }
    [Required] public DateTime? BirthDate { get; set; }
    [Required] public Genders? Gender { get; set; }
}