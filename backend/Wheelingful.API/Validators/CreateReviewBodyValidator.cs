using FluentValidation;
using Wheelingful.API.Models.Bindings.Bodies;

namespace Wheelingful.API.Validators;

public class CreateReviewBodyValidator : AbstractValidator<CreateReviewBody>
{
    public CreateReviewBodyValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.");

        RuleFor(b => b.Text)
            .NotEmpty()
                .WithMessage("Text is required.")
            .MaximumLength(1000)
                .WithMessage("Text must not exceed 1000 characters.");

        RuleFor(b => b.Score)
            .GreaterThan(0)
                .WithMessage("Score must be greater than 0.")
            .LessThanOrEqualTo(5)
                .WithMessage("Score must not exceed 5.");
    }
}
