using Microsoft.AspNetCore.Identity;
using Wheelingful.API.Constants;
using Wheelingful.API.Extensions;
using Wheelingful.BLL;
using Wheelingful.DAL;
using Wheelingful.DAL.Enums;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.DbContexts;
using Wheelingful.API.Extensions.Endpoints;

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
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityPartialApi<AppUser>();
app.MapBookAuthorApi();
app.MapBookReaderApi();
app.MapChapterAuthorApi();

app.Run();
