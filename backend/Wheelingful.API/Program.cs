using Microsoft.AspNetCore.Identity;
using Wheelingful.API.Constants;
using Wheelingful.API.Extensions;
using Wheelingful.BLL;
using Wheelingful.DAL;
using Wheelingful.DAL.Enums;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.DbContexts;
using Wheelingful.API.Extensions.Endpoints;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddLogging();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddCors(builder.Configuration);

builder.Services.AddDbContext(builder.Configuration);
builder.Services.AddCacheService(builder.Configuration);
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<WheelingfulDbContext>();

builder.Services.AddOptions();
builder.Services.AddCoreOptions(builder.Configuration);

builder.Services.AddApiValidators();

builder.Services.AddCoreServices();
builder.Services.AddApiServices();

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
} else
{
    app.UseExceptionHandler(exceptionHandlerApp => 
        exceptionHandlerApp.Run(async context => 
            await Results.Problem()
                .ExecuteAsync(context)));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityPartialApi<AppUser>();

var bookGroup = app.MapGroup("/books")
    .AddFluentValidationAutoValidation();

bookGroup.MapBookApi();
bookGroup.MapChapterApi();

Directory.CreateDirectory("App_Data");

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<WheelingfulDbContext>();
dbContext.Database.Migrate();

app.Run();
