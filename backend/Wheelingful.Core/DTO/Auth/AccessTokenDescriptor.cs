namespace Wheelingful.Core.DTO.Auth;

public class AccessTokenDescriptor
{
    public required string Email { get; set; }
    public required IEnumerable<string> Roles { get; set; }
}
