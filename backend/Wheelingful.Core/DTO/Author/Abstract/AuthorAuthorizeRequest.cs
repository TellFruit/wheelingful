using System.Security.Claims;

namespace Wheelingful.Core.DTO.Author.Abstract;

public abstract class AuthorAuthorizeRequest
{
    public required ClaimsPrincipal AuthorClaims { get; set; }
}
