using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Models.Responses;
using Wheelingful.BLL.Models.Responses.Generic;
using Wheelingful.DAL.DbContexts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Enums;
using Wheelingful.IntegrationTests.Abstract;
using Wheelingful.IntegrationTests.Constants;
using Wheelingful.IntegrationTests.Helpers;

namespace Wheelingful.IntegrationTests;

public class BookEndpointsTest : BaseTest
{
    [Fact]
    public async Task CreateBook_ReturnsUnauthorized()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = "Test Title",
            CoverBase64 = "Test Cover Base64",
            Description = "Test Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
        };

        // Act

        var result = await HttpClient.PostAsJsonAsync("/books", request);

        // Assert
        
        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.Unauthorized, result.StatusCode);
    }

    [Fact]
    public async Task Update_ReturnsValidationNotAuthor()
    {
        // Arrange

        using var scope = Factory.Services.CreateScope();

        await using var db = scope.ServiceProvider.GetRequiredService<WheelingfulDbContext>();

        var newUserId = await AuthHelper.RegisterUser(scope, "new.user@gmail.com", "#123Admin");

        var newBook = new Book
        {
            Title = "Test Title",
            Description = "Test Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
            CoverId = string.Empty,
            UserId = newUserId
        };

        db.Add(newBook);

        await db.SaveChangesAsync();

        var request = new UpdateBookRequest
        {
            BookId = newBook.Id,
            Title = "Test Title",
            CoverBase64 = DataConstants.DefaultCoverBase64,
            Description = "Test Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
        };

        var accessToken = await AuthHelper.LoginUser(HttpClient, DataConstants.AdminEmail, DataConstants.AdminPassword);

        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Act

        var result = await HttpClient.PutAsJsonAsync($"/books/{newBook.Id}", request);

        var resultContent = await result.Content.ReadAsStringAsync();
        
        // Assert

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);
        Assert.Contains("Either you are not the author or there is no book with such ID.", resultContent);
    }

    [Fact]
    public async Task GetAll_ReturnsGeneralPagination()
    {
        // Arrange

        using var scope = Factory.Services.CreateScope();

        await using var db = scope.ServiceProvider.GetRequiredService<WheelingfulDbContext>();

        var newBook = new Book
        {
            Title = "Test Title",
            Description = "Test Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
            CoverId = string.Empty,
            UserId = DataConstants.AdminUserId
        };

        db.Add(newBook);

        await db.SaveChangesAsync();

        // Act

        var result = await HttpClient.GetFromJsonAsync<FetchPaginationResponse<FetchBookResponse>>("/books");

        // Assert

        Assert.NotNull(result);
        Assert.Equal(1, result.PageCount);
    }

    [Fact]
    public async Task GetAll_ReturnsPaginationByAuthor()
    {
        // Arrange

        using var scope = Factory.Services.CreateScope();

        await using var db = scope.ServiceProvider.GetRequiredService<WheelingfulDbContext>();

        var newUserId = await AuthHelper.RegisterUser(scope, "new.user@gmail.com", "#123Admin");

        var books = new List<Book>
        {
            new Book
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = BookCategoryEnum.Original,
                Status = BookStatusEnum.Ongoing,
                CoverId = string.Empty,
                UserId = newUserId,
            },
            new Book
            {
                Title = "Test Title",
                Description = "Test Description",
                Category = BookCategoryEnum.Original,
                Status = BookStatusEnum.Ongoing,
                CoverId = string.Empty,
                UserId = DataConstants.AdminUserId,
            },
        };

        db.AddRange(books);

        await db.SaveChangesAsync();

        // Act

        var result = await HttpClient.GetFromJsonAsync<FetchPaginationResponse<FetchBookResponse>>("/books?doFetchByCurrentUser=true");

        // Assert

        Assert.NotNull(result);
        Assert.Single(result.Items);
    }
}
