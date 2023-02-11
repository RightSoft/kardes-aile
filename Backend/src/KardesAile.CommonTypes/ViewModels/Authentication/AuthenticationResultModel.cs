using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.ViewModels.Authentication;

public class AuthenticationResultModel
{
    public string Bearer { get; set; } = null!;
    public DateTime ExpiresAt { get; set; }
    public UserRoles Role { get; set; }
    public string Username { get; set; } = null!;
    public string Fullname { get; set; } = null!;
}