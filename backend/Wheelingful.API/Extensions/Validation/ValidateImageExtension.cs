﻿namespace Wheelingful.API.Extensions.Validation;

public static class ValidateImageExtension
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
