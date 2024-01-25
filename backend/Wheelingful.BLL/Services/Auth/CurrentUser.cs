using Wheelingful.BLL.Contracts.Auth;

namespace Wheelingful.BLL.Services.Auth;

public class CurrentUser : ICurrentUser
{
    public string Id { get; set; } = null!;
}
