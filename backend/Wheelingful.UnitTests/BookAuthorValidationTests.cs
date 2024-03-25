using FluentValidation.TestHelper;
using Wheelingful.API.Validators;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.Enums;
using Wheelingful.UnitTests.Constants;

namespace Wheelingful.UnitTests;

public class BookAuthorValidationTests
{
    [Fact]
    public void Given_null_title_empty_when_validate_then_error()
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
    public void Given_long_title_empty_when_validate_then_error()
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
    public void Given_valid_title_empty_when_validate_then_ok()
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
    public void Given_enum_out_of_range_when_validate_then_error()
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
    public void Given_valid_enum_when_validate_then_ok()
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
    public void Given_invalid_base64_when_validate_then_error()
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
    public void Given_valid_base64_when_validate_then_error()
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
