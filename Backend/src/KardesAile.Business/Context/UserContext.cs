using System.Security.Claims;
using KardesAile.CommonTypes.Context;
using KardesAile.CommonTypes.Enums;
using Microsoft.AspNetCore.Http;

namespace KardesAile.Business.Context;

public class UserContext : IUserContext
{
    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        Initialize(httpContextAccessor.HttpContext!);
    }

    public string Username { get; private set; } = null!;
    public IEnumerable<UserRoles> Roles { get; private set; } = null!;

    private void Initialize(HttpContext httpContext)
    {
        var roles = httpContext
            .User
            .Claims
            .Where(x => x.Type == ClaimTypes.Role)
            .Select(c => Enum.Parse<UserRoles>(c.Value, true));

        Username = httpContext.User.Identity.IsAuthenticated ? httpContext.User.Identity?.Name : "anonymous";
        Roles = httpContext.User.Identity.IsAuthenticated
            ? roles
            : new[]
            {
                UserRoles.Anonymous
            };
    }
}