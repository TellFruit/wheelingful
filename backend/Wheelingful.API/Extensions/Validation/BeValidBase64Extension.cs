namespace Wheelingful.API.Extensions.Validation;

public static class BeValidBase64Extension
{
    public static bool BeValidBase64(this string value)
    {
        try
        {
            _ = Convert.FromBase64String(value);
            
            return true;
        }
        catch
        {
            return false;
        }
    }
}
