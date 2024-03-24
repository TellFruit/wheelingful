using Microsoft.AspNetCore.Diagnostics;

namespace Wheelingful.API.Extensions;

public static class AppExtension
{
    public static void UseAppExtension(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseExceptionHandler(exceptionHandlerApp =>
                exceptionHandlerApp.Run(async context =>
                {
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionFeature!.Error;
                    await Results.Problem(detail: exception.Message).ExecuteAsync(context);
                }));
        }
        else
        {
            app.UseExceptionHandler(exceptionHandlerApp =>
                exceptionHandlerApp.Run(async context =>
                    await Results.Problem()
                        .ExecuteAsync(context)));
        }
    }
}
