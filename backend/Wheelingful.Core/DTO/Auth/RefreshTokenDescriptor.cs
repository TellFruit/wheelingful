namespace Wheelingful.Core.DTO.Auth;

public class RefreshTokenDescriptor
{
    public required string Signature { get; set; }
    public required string AccessTokenId { get; set; }
    public required string Email { get; set; }
    public bool IsValid { get; set; }
}
