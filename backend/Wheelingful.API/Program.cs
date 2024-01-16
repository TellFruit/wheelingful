using Microsoft.AspNetCore.Identity;
using Wheelingful.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext(builder.Configuration);
builder.Services
    .AddIdentityApiEndpoints<IdentityUser>()
    .AddIdentityDataStores();

builder.Services.AddDataOuter();

builder.Services.AddOptions();

builder.Services
    .AddAuthentication()
    .AddBearerToken();
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityPartialApi<IdentityUser>();

app.Run();
