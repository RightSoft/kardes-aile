using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Match;

public class CreateMatchModel
{
    [Required]
    public Guid? SupporterId { get; set; }
    [Required]
    public Guid? VictimId { get; set; }
    [Required]
    public Guid? SupporterChildId { get; set; }
    [Required]
    public Guid? VictimChildId { get; set; }
}