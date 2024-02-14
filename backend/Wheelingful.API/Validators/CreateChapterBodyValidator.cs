using FluentValidation;
using Wheelingful.API.Models.Bindings.Bodies;

namespace Wheelingful.API.Validators;

public class CreateChapterBodyValidator : AbstractValidator<CreateChapterBody>
{
    public CreateChapterBodyValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
                .WithMessage("Title is required.");

        RuleFor(c => c.Text)
            .NotEmpty()
                .WithMessage("Text is required.");
    }
}
