namespace Wheelingful.Core.DTO.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int HoursUntilAccessExpired { get; set; }
    public int HoursUntilRefershExpired { get; set; }
    public string SecretKey { get; set; }
}
