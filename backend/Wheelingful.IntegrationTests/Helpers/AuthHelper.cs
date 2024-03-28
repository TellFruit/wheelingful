using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text;
using Wheelingful.DAL.Entities;

namespace Wheelingful.IntegrationTests.Helpers;

public static class AuthHelper
{
    public static async Task<string> RegisterUser(IServiceScope scope, string email, string password)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
        var userStore = scope.ServiceProvider.GetRequiredService<IUserStore<AppUser>>();
        var emailStore = (IUserEmailStore<AppUser>)userStore;

        var user = new AppUser();
        await userStore.SetUserNameAsync(user, email, CancellationToken.None);
        await emailStore.SetEmailAsync(user, email, CancellationToken.None);
        await userManager.CreateAsync(user, password);

        return user.Id;
    }

    public static async Task<string> LoginUser(HttpClient client, string email, string password)
    {
        var credentials = new LoginRequest
        {
            Email = email,
            Password = password,
        };

        var authJsonContent = JsonSerializer.Serialize(credentials);

        var authContent = new StringContent(authJsonContent, Encoding.UTF8, "application/json");

        var authResult = await client.PostAsync("/auth/login", authContent);

        var responseContent = await authResult.Content.ReadAsStringAsync();

        var token = JsonSerializer.Deserialize<AccessTokenResponse>(responseContent, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return token?.AccessToken ?? string.Empty;
    }
}
