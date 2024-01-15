using Wheelingful.Core.DTO.Auth;

namespace Wheelingful.Core.Contracts.Auth;

public interface IRefreshTokenManager
{
    Task<string> CreateRefreshToken(string accessToken);
    Task<RefreshTokenDescriptor> GetRefreshToken(string signature); 
}
