using Microsoft.AspNetCore.Identity;
using Wheelingful.API.Constants;
using Wheelingful.API.Extensions;
using Wheelingful.API.Extensions.MinimalAPI;
using Wheelingful.Core;
using Wheelingful.Data;
using Wheelingful.Data.Enums;
using Wheelingful.Data.Entities;
using Wheelingful.Data.DbContexts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(PolicyContants.AllowClientOrigin, corsBuilder =>
    {
        corsBuilder.WithOrigins(builder.Configuration["CORS:ClientOrigin"]!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext(builder.Configuration);
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WheelingfulDbContext>();

builder.Services.AddCoreServices();
builder.Services.AddApiServices();

builder.Services.AddOptions();
builder.Services.AddCoreOptions(builder.Configuration);

builder.Services
    .AddAuthentication()
    .AddBearerToken();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy(PolicyContants.AuthorizeAuthor, policy =>
        policy
            .RequireRole(nameof(UserRoleEnum.Admin), nameof(UserRoleEnum.Author)));

var app = builder.Build();

app.UseCors(PolicyContants.AllowClientOrigin);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityPartialApi<AppUser>();
app.MapBookAuthorApi();

app.Run();
