using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KardesAile.Business.Interfaces;
using KardesAile.CommonTypes.Enums;
using KardesAile.CommonTypes.Errors;
using KardesAile.CommonTypes.Options;
using KardesAile.CommonTypes.ViewModels.Authentication;
using KardesAile.Database.Abstracts;
using KardesAile.Database.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace KardesAile.Business.Implementations;

public class AuthenticationBusiness : IAuthenticationBusiness
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationBusiness(
        IUnitOfWork unitOfWork,
        IOptions<JwtOptions> jwtOptions)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public async Task<AuthenticationResultModel> Authenticate(AuthenticationModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));
        var user = await _unitOfWork.User
            .AsQueryable
            .AsNoTracking()
            .FirstOrDefaultAsync(p =>
                p.Email == model.Email &&
                p.Status == UserStatuses.Active);

        if (user == null) throw Errors.UsernamePasswordDenied;

        var calculatedHash = KeyDerivation.Pbkdf2(
            model.Password!,
            Convert.FromBase64String(user.Salt!),
            KeyDerivationPrf.HMACSHA256,
            200000,
            256 / 8);

        if (!Convert.FromBase64String(user.Hash!)
                .SequenceEqual(calculatedHash))
            throw Errors.UsernamePasswordDenied;

        var result = GenerateAuthenticationToken(user);
        return result;
    }

    private AuthenticationResultModel GenerateAuthenticationToken(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.Value.TokenExpiresInMinutes!.Value);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Role, UserRoles.User.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

        var token = new JwtSecurityToken(
            _jwtOptions.Value.Issuer,
            audience: "System",
            expires: expiresAt,
            claims: claims,
            signingCredentials: credentials
        );

        return new AuthenticationResultModel
        {
            Bearer = new JwtSecurityTokenHandler().WriteToken(token),
            ExpiresAt = expiresAt,
            Role = user.Role,
            Username = user.Email,
            Fullname = $"{user.FirstName} {user.LastName}",
        };
    }
}