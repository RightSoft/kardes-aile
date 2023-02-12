using System.Security;
using KardesAile.CommonTypes.Exceptions;

namespace KardesAile.CommonTypes.Errors;

public static class Errors
{
    public static BusinessException SupporterNotFound = new(1000, "Supporter not found");
    public static BusinessException ChildNotFound = new (2000, "Child not found");
    public static BusinessException DisasterVictimNotFound = new(3000, "Disaster victim not found");
    public static BusinessException MatchNotFound = new (4000, "Match not found");
    public static SecurityException UsernamePasswordDenied = new("Invalid username/password");
    public static SecurityException EmailUsed = new("There is a user with this e-mail address.");
}