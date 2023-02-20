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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace KardesAile.Business.Implementations;

public class AuthenticationBusiness : IAuthenticationBusiness
{
    private readonly IOptions<JwtOptions> _jwtOptions;
    private readonly IAuditContext _auditContext;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationBusiness(
        IAuditContext auditContext,
        IUnitOfWork unitOfWork,
        IOptions<JwtOptions> jwtOptions)
    {
        _auditContext = auditContext ?? throw new ArgumentNullException(nameof(auditContext));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
    }

    public async Task<AuthenticationResultModel> Authenticate(AuthenticationModel model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        User? user = null;
        
        if (!string.IsNullOrEmpty(model.Email))
        {
            user = await _unitOfWork.User
                .AsQueryable
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.Status == UserStatuses.Active &&
                    p.Email == model.Email);
        } else if (!string.IsNullOrEmpty(model.Phone))
        {
            user = await _unitOfWork.User
                .AsQueryable
                .AsNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.Status == UserStatuses.Active &&
                    p.Phone == model.Phone);
        }
        
        if (user?.Hash == null) throw Errors.UsernamePasswordDenied;

        var calculatedHash = KeyDerivation.Pbkdf2(
            model.Password!,
            Convert.FromBase64String(user.Salt!),
            KeyDerivationPrf.HMACSHA256,
            200000,
            256 / 8);

        if (!Convert.FromBase64String(user.Hash!)
                .SequenceEqual(calculatedHash))
            throw Errors.UsernamePasswordDenied;

        _auditContext.Start(AuditTypes.User, "Logged in");
        _auditContext.AddEffectedUser(user);
        var result = GenerateAuthenticationToken(user);

        return result;
    }

    private AuthenticationResultModel GenerateAuthenticationToken(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwtOptions.Value.TokenExpiresInMinutes!.Value);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, (user.Email ?? user.Phone)!),
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
            Username = (user.Email ?? user.Phone)!,
            Fullname = $"{user.FirstName} {user.LastName}",
        };
    }
}