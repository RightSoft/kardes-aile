using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace KardesAile.AspNetCoreHost.Authentication;

public static class TokenHelpers
{
    public const string DefaultAuthenticationScheme = "System";
    
    public static TokenValidationParameters SetAuthenticationDefaults(this TokenValidationParameters parameters,
        IConfiguration configuration)
    {
        parameters.NameClaimType = JwtRegisteredClaimNames.Sub;
        parameters.ValidIssuer = configuration["Jwt:Issuer"]!;
        parameters.ValidAudience = DefaultAuthenticationScheme;
        parameters.IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!));
        parameters.RequireAudience = true;
        parameters.RequireExpirationTime = true;
        parameters.RequireSignedTokens = true;
        parameters.ValidateActor = false;
        parameters.ValidateIssuer = true;
        parameters.ValidateAudience = true;
        parameters.ValidateLifetime = true;
        parameters.ValidateTokenReplay = false;
        parameters.ValidateIssuerSigningKey = true;
        return parameters;
    }
}