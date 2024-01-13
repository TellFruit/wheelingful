namespace Wheelingful.Core.DTO.Options;

public class JwtOptions
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int HoursUntilExpired { get; set; }
    public string SecretKey { get; set; }
}
