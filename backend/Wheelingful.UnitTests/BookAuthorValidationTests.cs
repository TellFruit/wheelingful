using FluentValidation.TestHelper;
using Wheelingful.API.Validators;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Enums;
using Wheelingful.UnitTests.Constants;

namespace Wheelingful.UnitTests;

public class BookAuthorValidationTests
{
    [Fact]
    public void BookValidator_Validate_ReturnTitleEmpty()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = string.Empty,
            Description = string.Empty,
            Category = default,
            Status = default,
            CoverBase64 = string.Empty,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldHaveValidationErrorFor(b => b.Title);
    }

    [Fact]
    public void BookValidator_Validate_ReturnTitleTooLong()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = "a".PadRight(300),
            Description = string.Empty,
            Category = default,
            Status = default,
            CoverBase64 = string.Empty,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldHaveValidationErrorFor(b => b.Title);
    }

    [Fact]
    public void BookValidator_Validate_ReturnTitleCorrect()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = "a".PadRight(100),
            Description = string.Empty,
            Category = default,
            Status = default,
            CoverBase64 = string.Empty,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldNotHaveValidationErrorFor(b => b.Title);
    }

    [Fact]
    public void BookValidator_Validate_ReturnCategoryOutOfRange()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = string.Empty,
            Description = string.Empty,
            Category = (BookCategoryEnum)100,
            Status = default,
            CoverBase64 = string.Empty,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldHaveValidationErrorFor(b => b.Category);
    }

    [Fact]
    public void BookValidator_Validate_ReturnCategoryCorrect()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = string.Empty,
            Description = string.Empty,
            Category = (BookCategoryEnum)1,
            Status = default,
            CoverBase64 = string.Empty,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldNotHaveValidationErrorFor(b => b.Category);
    }

    [Fact]
    public void BookValidator_Validate_ReturnImageCorrupted()
    {
        // Arrange

        var validBase64 = BookCoverConstants.DefaultCoverBase64;

        var invalidBase64 = validBase64.Substring(validBase64.Length - 1);

        var request = new CreateBookRequest
        {
            Title = string.Empty,
            Description = string.Empty,
            Category = default,
            Status = default,
            CoverBase64 = invalidBase64,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldHaveValidationErrorFor(b => b.CoverBase64);
    }

    [Fact]
    public void BookValidator_Validate_ReturnImageCorrect()
    {
        // Arrange

        var request = new CreateBookRequest
        {
            Title = string.Empty,
            Description = string.Empty,
            Category = default,
            Status = default,
            CoverBase64 = BookCoverConstants.DefaultCoverBase64,
        };

        var validator = new CreateBookValidator();

        // Act

        var result = validator.TestValidate(request);

        // Assert

        result.ShouldNotHaveValidationErrorFor(b => b.CoverBase64);
    }
}
