using Microsoft.EntityFrameworkCore;
using Moq;
using System.Text.Json;
using Wheelingful.BLL.Contracts.Books;
using Wheelingful.BLL.Contracts.Chapters;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.BLL.Services.Books;
using Wheelingful.DAL.Contracts;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Enums;
using Wheelingful.UnitTests.Constants;
using Wheelingful.UnitTests.Helpers;

namespace Wheelingful.UnitTests;

public class BookAuthorServiceTests
{
    [Fact]
    public async Task BookAuthorService_CreateBook_ReturnSuccess()
    {
        // Arrange

        var mockCoverService = new Mock<IBookCoverService>();

        mockCoverService.Setup(x => 
            x.UploadCover(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<string>()))
                .ReturnsAsync(Guid.NewGuid().ToString());

        var mockChapterText = new Mock<IChapterTextService>(); 
        
        var mockCacheService = new Mock<ICacheService>();

        var mockLogger = MockServicesHelper.MockLogger<BookAuthorService>();

        var mockCurrentUser = MockServicesHelper.MockCurrentUser();

        using var context = new TestDbContextFactory().CreateDbContext();

        var service = new BookAuthorService(
            mockCoverService.Object, 
            mockChapterText.Object, 
            mockCurrentUser, 
            mockLogger,
            mockCacheService.Object,
            context
        );

        var request = new CreateBookRequest
        {
            Title = "Test Book",
            Description = "Test Description",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
            CoverBase64 = BookCoverConstants.DefaultCoverBase64,
        };

        // Act

        await service.CreateBook(request);
        var book = await context.Books.FirstOrDefaultAsync();

        // Assert

        Assert.NotNull(book);

        var responseJson = JsonSerializer.Serialize(book);
        var requestJson = JsonSerializer.Serialize(new Book
        {
            Id = book.Id,
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            Status = request.Status,
            CoverId = book.CoverId,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt,
        });

        Assert.Equal(requestJson, responseJson);
    }
}
