using Wheelingful.Core.Contracts.Auth;

namespace Wheelingful.Core.Services.Auth;

public class CurrentUser : ICurrentUser
{
    public string Id { get; set; } = null!;
}
