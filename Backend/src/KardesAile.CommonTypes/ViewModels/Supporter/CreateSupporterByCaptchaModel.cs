using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Supporter;

public class CreateSupporterByCaptchaModel : CreateSupporterModel
{
    [Required]
    public string? ReCaptchaToken { get; set; }
}