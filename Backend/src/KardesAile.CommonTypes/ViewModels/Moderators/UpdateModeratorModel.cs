using System.ComponentModel.DataAnnotations;

namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class UpdateModeratorModel
{
    [Required]
    public string? FirstName { get; set; }
    
    [Required]
    public string? LastName { get; set; }
    
    [Required]
    [EmailAddress] 
    public string? Email { get; set; }
    
    public string? Password { get; set; }
    
    public bool UpdatePassword { get; set; }
}
