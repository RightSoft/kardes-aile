using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Child;

public class CreateChildWithUserIdModel : CreateChildModel
{
    [Required]
    public Guid? UserId { get; set; }
}