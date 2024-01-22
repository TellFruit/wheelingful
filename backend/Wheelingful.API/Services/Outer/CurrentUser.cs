using Wheelingful.Core.Contracts.Auth;

namespace Wheelingful.API.Services.Outer;

public class CurrentUser : ICurrentUser
{
    public string Id { get; set; } = null!;
}
