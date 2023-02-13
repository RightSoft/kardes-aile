using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class CreateModeratorModel
{
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    [EmailAddress] 
    public string? Email { get; set; }
    
    [Required]
    public string? Password { get; set; }
}
