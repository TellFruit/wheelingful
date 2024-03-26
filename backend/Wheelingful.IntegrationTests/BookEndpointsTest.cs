using System.Text;
using System.Text.Json;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Enums;
using Wheelingful.IntegrationTests.Abstract;

namespace Wheelingful.IntegrationTests;

public class BookEndpointsTest : BaseTest
{
    [Fact]
    public async Task CreateBook_ReturnsUnauthorized()
    {
        // Arrange

        var createBookRequest = new CreateBookRequest
        {
            Title = "Sample Title",
            CoverBase64 = "Sample Cover Base64",
            Description = "Sample Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
        };

        var jsonContent = JsonSerializer.Serialize(createBookRequest);

        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        // Act

        var result = await HttpClient.PostAsync("/books", content);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(System.Net.HttpStatusCode.Unauthorized, result.StatusCode);
    }
}
