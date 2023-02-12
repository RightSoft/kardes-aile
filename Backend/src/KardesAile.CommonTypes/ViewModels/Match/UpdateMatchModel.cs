using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Match;

public class UpdateMatchModel : CreateMatchModel
{
    [Required]
    public Guid? MatchId { get; set; }

    [Required]
    public bool? Active { get; set; }
}