using Wheelingful.API.Extensions;
using Wheelingful.API.Extensions.MinimalAPI;
using Wheelingful.Core;
using Wheelingful.Data;
using Wheelingful.Data.Entities;
using Wheelingful.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowClientApp", corsBuilder =>
    {
        corsBuilder.WithOrigins(builder.Configuration["CORS:ClientOrigin"]!)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext(builder.Configuration);
builder.Services
    .AddIdentityApiEndpoints<AppUser>()
    .AddIdentityDataStores();

builder.Services.AddApiOuter();
builder.Services.AddDataOuter();
builder.Services.AddServicesOuter();

builder.Services.AddApiInner();

builder.Services.AddOptions();
builder.Services.AddCoreOptions(builder.Configuration);

builder.Services
    .AddAuthentication()
    .AddBearerToken();

builder.Services.AddAuthorizationBuilder()
  .AddPolicy("allow_author", policy =>
        policy
            .RequireRole("Admin", "Author"));

var app = builder.Build();

app.UseCors("AllowClientApp");

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
