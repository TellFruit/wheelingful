using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.DTO.Auth;
using Wheelingful.Data.DbContexts;
using Wheelingful.Data.Entities;

namespace Wheelingful.Data.Outer;

internal class RefreshTokenManager : IRefreshTokenManager
{
    private readonly ITokenService _tokenService;
    private readonly WheelingfulDbContext _context;
    private readonly JwtOptions _jwtOptions;

    public RefreshTokenManager(ITokenService tokenService, IOptions<JwtOptions> options, WheelingfulDbContext context)
    {
        _tokenService = tokenService;
        _jwtOptions = options.Value;
        _context = context;
    }

    public async Task<string> CreateRefreshToken(string accessToken)
    {
        var accessIdentity = await _tokenService.GetClaimsFromAccessToken(accessToken);

        var accessEmail = accessIdentity.FindFirst(nameof(AccessTokenDescriptor.Email))!.Value;

        var accessTokenId = accessIdentity.FindFirst("Jti")!.Value;

        var user = await _context.Users
            .Include(u => u.RefreshTokens)
            .FirstAsync(u => u.Email!.Equals(accessEmail));

        var expiredRefresh = user.RefreshTokens!.Where(rt => rt.IsValid.Equals(false)).ToList();

        _context.RefreshTokens.RemoveRange(expiredRefresh);

        var signature = _tokenService.GenerateRefreshSignature();

        var now = DateTime.UtcNow;
        var expiresAt = now.AddHours(_jwtOptions.HoursUntilRefershExpired);

        var newRefreshToken = new RefreshToken
        {
            Signature = signature,
            AccessTokenId = accessTokenId,
            ExpiresAt = expiresAt,
            CreatedAt = now,
            UpdatedAt = now,
        };

        await _context.RefreshTokens.AddAsync(newRefreshToken);

        return signature;
    }

    public async Task<RefreshTokenDescriptor> GetRefreshToken(string signature)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .FirstAsync(r => r.Signature == signature);

        return new RefreshTokenDescriptor
        {
            Signature = refreshToken.Signature,
            AccessTokenId = refreshToken.AccessTokenId,
            Email = refreshToken.User!.Email!,
            IsValid = refreshToken.IsValid,
        };
    }
}
