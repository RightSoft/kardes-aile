using System.Security;
using KardesAile.CommonTypes.Exceptions;

namespace KardesAile.CommonTypes.Errors;

public static class Errors
{
    public static BusinessException SupporterNotFound = new(1000, "Supporter not found");
    public static BusinessException ChildNotFound = new (2000, "Child not found");
    public static SecurityException UsernamePasswordDenied = new("Invalid username/password");
}