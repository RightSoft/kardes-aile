using KardesAile.CommonTypes.Enums;
using KardesAile.Database.Abstracts;

namespace KardesAile.Database.Entities;

public class User : BaseEntity
{
    public User()
    {
        Cocuklar = new HashSet<Cocuk>();
    }
    
    public UserStatuses Status { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailValidated { get; set; }
    public string Phone { get; set; } = null!;
    public bool PhoneValidated { get; set; }
    public string? Hash { get; set; }
    public string? Salt { get; set; }
    public UserRoles Role { get; set; }

    public ICollection<Cocuk> Cocuklar { get; set; }
}