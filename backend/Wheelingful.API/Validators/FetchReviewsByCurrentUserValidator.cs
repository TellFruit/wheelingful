using FluentValidation;
using Wheelingful.API.Models;

namespace Wheelingful.API.Validators;

public class FetchReviewsByCurrentUserValidator : AbstractValidator<FetchReviewsByCurrentUserRequest>
{
    public FetchReviewsByCurrentUserValidator()
    {
        RuleFor(b => b.PageNumber)
            .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.");

        RuleFor(b => b.PageSize)
            .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100)
                .WithMessage("Page size must be less than 100.");
    }
}
