using System.Security;
using KardesAile.CommonTypes.Exceptions;

namespace KardesAile.CommonTypes.Errors;

public static class Errors
{
    public static readonly BusinessException RecordAlreadyExists = new(5000, "Record already exists");
    public static readonly BusinessException SupporterNotFound = new(1000, "Supporter not found");
    public static readonly BusinessException ChildNotFound = new (2000, "Child not found");
    public static readonly BusinessException DisasterVictimNotFound = new(3000, "Disaster victim not found");
    public static readonly BusinessException MatchNotFound = new (4000, "Match not found");
    public static readonly BusinessException UserNotFound = new (5000, "User not found");
    public static readonly SecurityException UsernamePasswordDenied = new("Invalid username/password");
    public static readonly SecurityException EmailUsed = new("There is a user with this e-mail address.");
    public static readonly BusinessException EmailOrPhoneRequired = new("E-mail address or phone must be filled");
}