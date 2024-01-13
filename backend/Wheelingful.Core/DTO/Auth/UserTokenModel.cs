namespace Wheelingful.Core.DTO.Auth;

public class UserTokenModel
{
    public required string Email { get; set; }

    public required IEnumerable<string> Roles { get; set; }
}
