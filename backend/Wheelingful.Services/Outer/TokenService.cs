using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.DTO.Auth;
using Wheelingful.Core.DTO.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;

namespace Wheelingful.Services.Outer.Auth;

internal class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly JsonWebTokenHandler _tokenHandler;

    public TokenService(IOptions<JwtOptions> options, JsonWebTokenHandler tokenHandler)
    {
        _jwtOptions = options.Value;
        _tokenHandler = tokenHandler;
    }

    public string GenerateAccessToken(UserTokenModel tokenModel)
    {
        var now = DateTime.UtcNow;

        var expires = now.AddHours(Convert.ToDouble(_jwtOptions.HoursUntilExpired));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaims(tokenModel),
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            Expires = expires,
            IssuedAt = now,
            NotBefore = now,
            SigningCredentials = GetSigningCredentials(_jwtOptions.SecretKey)
        };

        return _tokenHandler.CreateToken(tokenDescriptor);
    }

    private SigningCredentials GetSigningCredentials(string secretKey)
    {
        var signingKey = new SymmetricSecurityKey(Convert.FromBase64String(secretKey));

        return new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
    }

    private ClaimsIdentity GetClaims(UserTokenModel tokenModel)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, tokenModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(tokenModel.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims);
    }
}
