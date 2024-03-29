﻿using Microsoft.AspNetCore.Authentication;
using Wheelingful.API.Services;
using FluentValidation;
using Wheelingful.API.Validators;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.API.Models.Bindings;
using Microsoft.OpenApi.Models;
using Wheelingful.API.Constants;
using Serilog;
using Wheelingful.API.Models;

namespace Wheelingful.API.Extensions;

public static class DependencyInjection
{
    public static void AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddApplicationInsightsTelemetry(builder.Configuration);
    }

    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Authorization header using the Bearer scheme",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "Access Token"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public static void AddCors(this IServiceCollection services, IConfiguration config)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(PolicyContants.AllowClientOrigin, corsBuilder =>
            {
                corsBuilder.WithOrigins(config["CORS:ClientOrigin"]!)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

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
        services.AddScoped<IValidator<FetchReviewsByBookRequest>, FetchReviewsByBookValidator>();
        services.AddScoped<IValidator<FetchBookPaginationRequest>, FetchBookPaginationValidator>();

        services.AddScoped<IValidator<CreateChapterBinding>,  CreateChapterValidator>();
        services.AddScoped<IValidator<UpdateChapterBinding>, UpdateChapterValidator>();
        services.AddScoped<IValidator<DeleteChapterRequest>, DeleteChapterValidator>();
        services.AddScoped<IValidator<FetchChapterRequest>, FetchChapterValidator>();
        services.AddScoped<IValidator<FetchChapterPaginationRequest>, FetchChapterPaginationValidator>();

        services.AddScoped<IValidator<CreateReviewBinding>, CreateReviewValidator>();
        services.AddScoped<IValidator<UpdateReviewBinding>, UpdateReviewValidator>();
        services.AddScoped<IValidator<DeleteReviewRequest>, DeleteReviewValidator>();
        services.AddScoped<IValidator<FetchReviewRequest>, FetchReviewValidator>();
        services.AddScoped<IValidator<FetchReviewsByCurrentUserRequest>, FetchReviewsByCurrentUserValidator>();
    }
}
