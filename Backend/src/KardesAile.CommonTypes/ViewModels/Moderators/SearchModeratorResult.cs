using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Moderators;

public class SearchModeratorResult
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public UserStatuses Status { get; set; }
}
