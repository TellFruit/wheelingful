using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using FluentValidation;
using Wheelingful.API.Validators;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Requests.General;

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
        services.AddScoped<IValidator<UpdateBookModel>, UpdateBookValidator>();
        services.AddScoped<IValidator<DeleteBookRequest>, DeleteBookValidator>();
        services.AddScoped<IValidator<FetchBookRequest>, FetchBookValidator>();

        services.AddScoped<IValidator<CountPagesRequest>, CountPagesValidator>();
        services.AddScoped<IValidator<FetchPaginationRequest>, FetchPaginationValidatior>();
    }
}
