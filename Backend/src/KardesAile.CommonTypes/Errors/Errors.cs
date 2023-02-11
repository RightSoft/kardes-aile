using System.Security;

namespace KardesAile.CommonTypes.Errors;

public static class Errors
{
    public static SecurityException UsernamePasswordDenied = new("Invalid username/password");
}