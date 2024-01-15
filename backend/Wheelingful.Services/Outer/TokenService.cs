using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.DTO.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Cryptography;

namespace Wheelingful.Services.Outer.Auth;

internal class TokenService : ITokenService
{
    private static int RefreshTokenBytesLength = 32;

    private readonly JwtOptions _jwtOptions;
    private readonly JsonWebTokenHandler _tokenHandler;

    public TokenService(IOptions<JwtOptions> options, JsonWebTokenHandler tokenHandler)
    {
        _jwtOptions = options.Value;
        _tokenHandler = tokenHandler;
    }

    public string GenerateAccessToken(AccessTokenDescriptor tokenModel)
    {
        var now = DateTime.UtcNow;

        var expires = now.AddHours(Convert.ToDouble(_jwtOptions.HoursUntilAccessExpired));

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
        return new SigningCredentials(GetSigningKeyKey(secretKey), SecurityAlgorithms.HmacSha256Signature);
    }

    private ClaimsIdentity GetClaims(AccessTokenDescriptor tokenModel)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, tokenModel.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        claims.AddRange(tokenModel.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(claims);
    }

    public string GenerateRefreshSignature()
    {
        var salt = new byte[RefreshTokenBytesLength];

        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }

    public async Task<ClaimsIdentity> GetClaimsFromAccessToken(string token)
    {
        var result = await _tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = false,
            IssuerSigningKey = GetSigningKeyKey(_jwtOptions.SecretKey),
        });

        if (result.IsValid)
        {
            return result.ClaimsIdentity;
        }

        throw result.Exception;
    }

    private SymmetricSecurityKey GetSigningKeyKey(string secretKey)
    {
        return new SymmetricSecurityKey(Convert.FromBase64String(secretKey));
    }
}
