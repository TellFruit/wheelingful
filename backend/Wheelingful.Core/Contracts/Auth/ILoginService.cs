using Wheelingful.Core.DTO.Auth;

namespace Wheelingful.Core.Contracts.Auth
{
    public interface ILoginService
    {
        Task<TokenResult> LoginAsync(LoginRequest request);
        Task<TokenResult> RefreshAsync(RefreshRequest request);
    }
}
