using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using FluentValidation;
using Wheelingful.BLL.Models.Books;
using Wheelingful.API.Validators;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
    }

    public static void AddApiValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateBookModel>, CreateBookValidator>();
        services.AddScoped<IValidator<UpdateBookModel>, UpdateBookValidator>();
        services.AddScoped<IValidator<DeleteBookModel>, DeleteBookValidator>();
    }
}
