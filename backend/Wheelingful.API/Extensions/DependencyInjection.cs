using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using FluentValidation;
using Wheelingful.API.Validators;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.API.Models.Bindings;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddApiServices(this IServiceCollection services)
    {
        services.AddScoped<IClaimsTransformation, ClaimsTransformation>();
    }

    public static void AddApiValidators(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateBookRequest>, CreateBookValidator>();
        services.AddScoped<IValidator<UpdateBookBinding>, UpdateBookValidator>();
        services.AddScoped<IValidator<DeleteBookRequest>, DeleteBookValidator>();
        services.AddScoped<IValidator<FetchBookRequest>, FetchBookValidator>();
        services.AddScoped<IValidator<FetchBookPaginationRequest>, FetchBookPaginationValidator>();

        services.AddScoped<IValidator<CreateChapterRequest>,  CreateChapterValidator>();
        services.AddScoped<IValidator<UpdateChapterBinding>, UpdateChapterValidator>();
        services.AddScoped<IValidator<DeleteChapterRequest>, DeleteChapterValidator>();
        services.AddScoped<IValidator<FetchChapterRequest>, FetchChapterValidator>();
        services.AddScoped<IValidator<FetchChapterPaginationRequest>, FetchChapterPaginationValidator>();
    }
}
