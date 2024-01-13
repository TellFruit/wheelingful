using Wheelingful.Core.DTO.Auth;

namespace Wheelingful.Core.Contracts.Auth;

public interface ITokenService
{
    string GenerateAccessToken(UserTokenModel tokenModel);
}
