namespace Wheelingful.Core.DTO.Auth;

public class TokenResult
{
    public required string AccessToken { get; set; }
    public required string RefreshSignature { get; set; }
}
