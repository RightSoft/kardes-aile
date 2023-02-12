namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class ModeratorResult
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
