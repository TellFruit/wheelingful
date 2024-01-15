using System.Security.Claims;
using Wheelingful.Core.DTO.Auth;

namespace Wheelingful.Core.Contracts.Auth;

public interface ITokenService
{
    string GenerateAccessToken(AccessTokenDescriptor tokenModel);
    Task<ClaimsIdentity> GetClaimsFromAccessToken(string token);
    string GenerateRefreshSignature();
}
