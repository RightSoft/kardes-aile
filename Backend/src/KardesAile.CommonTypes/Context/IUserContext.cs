using KardesAile.CommonTypes.Enums;

namespace KardesAile.CommonTypes.Context;

public interface IUserContext
{
    string Username { get; }
    IEnumerable<UserRoles> Roles { get; }
}