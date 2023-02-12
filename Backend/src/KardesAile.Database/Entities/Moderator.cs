using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class Moderator : BaseEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
