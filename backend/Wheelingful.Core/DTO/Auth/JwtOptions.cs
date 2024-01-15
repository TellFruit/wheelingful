namespace Wheelingful.Core.DTO.Auth;

public class JwtOptions
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public int HoursUntilAccessExpired { get; set; }
    public int HoursUntilRefershExpired { get; set; }
    public required string SecretKey { get; set; }
}
