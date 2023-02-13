using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class SearchModeratorResult
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserStatuses Status { get; set; }
}
