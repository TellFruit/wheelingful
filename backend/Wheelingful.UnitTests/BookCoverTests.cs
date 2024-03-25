using Moq;
using Wheelingful.BLL.Contracts.Images;
using Wheelingful.BLL.Models.Options;
using Wheelingful.BLL.Services.Books;
using Wheelingful.DAL.Entities;
using Wheelingful.DAL.Enums;
using Wheelingful.UnitTests.Constants;
using Wheelingful.UnitTests.Helpers;

namespace Wheelingful.UnitTests;

public class BookCoverTests
{
    [Fact]
    public void Given_empty_coverId_when_get_cover_URL_then_default_cover()
    {
        // Arrange

        var imageMock = MockImageService();

        var loggerMock = MockServicesHelper.MockLogger<BookCoverService>();

        var optionsMock = MockServicesHelper.MockOptions(new BookCoverOptions
        {
            Folder = BookCoverConstants.Folder,
            DefaultCover = BookCoverConstants.DefaultCover,
            Height = BookCoverConstants.Height,
            Width = BookCoverConstants.Width,
        });

        using var context = new TestDbContextFactory().CreateDbContext();

        var book = new Book
        {
            Title = "Test Book",
            Description = "Test Book",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
            CoverId = string.Empty
        };

        context.Books.Add(book);

        context.SaveChanges();

        var bookCoverService = new BookCoverService(imageMock, loggerMock, optionsMock, context);

        // Act

        var expected = $"{BookCoverConstants.Endpoint}/{BookCoverConstants.Folder}/{BookCoverConstants.DefaultCover}";
        var result = bookCoverService.GetCoverUrl(book.Id, DbConstants.AdminUserId);

        // Assert

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Given_unique_coverId_when_get_cover_URL_then_unique_cover()
    {
        // Arrange

        var imageMock = MockImageService();

        var loggerMock = MockServicesHelper.MockLogger<BookCoverService>();

        var optionsMock = MockServicesHelper.MockOptions(new BookCoverOptions
        {
            Folder = BookCoverConstants.Folder,
            DefaultCover = BookCoverConstants.DefaultCover,
            Height = BookCoverConstants.Height,
            Width = BookCoverConstants.Width,
        });

        using var context = new TestDbContextFactory().CreateDbContext();

        var book = new Book
        {
            Title = "Test Book",
            Description = "Test Book",
            Category = BookCategoryEnum.Original,
            Status = BookStatusEnum.Ongoing,
            CoverId = "12345"
        };

        context.Books.Add(book);

        context.SaveChanges();

        var bookCoverService = new BookCoverService(imageMock, loggerMock, optionsMock, context);

        // Act

        var expected = $"{BookCoverConstants.Endpoint}/{BookCoverConstants.Folder}/{book.Id}-{DbConstants.AdminUserId}";
        var result = bookCoverService.GetCoverUrl(book.Id, DbConstants.AdminUserId);

        // Assert

        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(expected, result);
    }

    private IImageService MockImageService()
    {
        var mock = new Mock<IImageService>();
        
        mock.Setup(m =>
            m.GetImageUrl(
                It.IsAny<string>(),
                It.IsNotNull<TransformationOptions>()))
                    .Returns((string inputString, TransformationOptions options) =>
                    {
                        return $"{BookCoverConstants.Endpoint}/{inputString}";
                    });

        return mock.Object;
    }
}