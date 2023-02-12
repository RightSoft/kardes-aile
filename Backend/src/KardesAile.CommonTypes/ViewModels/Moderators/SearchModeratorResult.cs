namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class SearchModeratorResult
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
