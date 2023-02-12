namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class UpdateModeratorModel
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool UpdatePassword { get; set; }
}
