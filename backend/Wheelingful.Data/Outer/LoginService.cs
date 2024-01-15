using Microsoft.AspNetCore.Identity;
using Wheelingful.Core.Contracts.Auth;
using Wheelingful.Core.DTO.Auth;
using Wheelingful.Data.Entities;

namespace Wheelingful.Data.Outer;

internal class LoginService : ILoginService
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenManager _refreshTokenManager;
    private readonly UserManager<User> _userManager;

    public LoginService(ITokenService tokenService, IRefreshTokenManager refreshTokenManager, UserManager<User> userManager)
    {
        _tokenService = tokenService;
        _refreshTokenManager = refreshTokenManager;
        _userManager = userManager;
    }

    public async Task<TokenResult> LoginAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new InvalidOperationException("Email is incorrect");
        }

        var rolesTask = _userManager.GetRolesAsync(user);

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

        if (isValidPassword)
        {
            throw new ArgumentException("Password is incorrect.");
        }

        var tokenDescription = new AccessTokenDescriptor
        {
            Email = request.Email,
            Roles = await rolesTask
        };

        var accessToken = _tokenService.GenerateAccessToken(tokenDescription);

        var refreshSignature = await _refreshTokenManager.CreateRefreshToken(accessToken);

        return new TokenResult
        {
            AccessToken = accessToken,
            RefreshSignature = refreshSignature
        };
    }

    public async Task<TokenResult> RefreshAsync(RefreshRequest request)
    {
        var refreshTokenTask = _refreshTokenManager.GetRefreshToken(request.RefreshSignature);

        var accessClaims = await _tokenService.GetClaimsFromAccessToken(request.AccessToken);

        var accessTokenEmail = accessClaims.Claims
            .First(c => c.Type.Equals(nameof(AccessTokenDescriptor.Email)))
            .Value;

        var userTask = _userManager.FindByEmailAsync(accessTokenEmail);

        var accessTokenId = accessClaims.Claims
            .First(c => c.Type.Equals("Jti"))
            .Value;

        var refreshToken = await refreshTokenTask;

        if (!refreshToken.Email.Equals(accessTokenEmail))
        {
            throw new ArgumentException("Invalid refresh token");
        }

        if (!refreshToken.AccessTokenId.Equals(accessTokenId))
        {
            throw new ArgumentException("Invalid refresh token");
        }

        if (!refreshToken.IsValid)
        {
            throw new ArgumentException("Invalid refresh token");
        }

        var user = await userTask;

        var roles = await _userManager.GetRolesAsync(user!);

        var tokenDescription = new AccessTokenDescriptor
        {
            Email = accessTokenEmail,
            Roles = roles
        };

        var accessToken = _tokenService.GenerateAccessToken(tokenDescription);

        var refreshSignature = await _refreshTokenManager.CreateRefreshToken(accessToken);

        return new TokenResult
        {
            AccessToken = accessToken,
            RefreshSignature = refreshSignature,
        };
    }
}
