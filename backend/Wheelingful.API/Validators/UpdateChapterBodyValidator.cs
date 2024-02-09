using FluentValidation;
using Wheelingful.API.Models.Bindings.Bodies;

namespace Wheelingful.API.Validators;

public class UpdateChapterBodyValidator : AbstractValidator<UpdateChapterBody>
{
    public UpdateChapterBodyValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
                .WithMessage("Title is required.");

        RuleFor(c => c.Text)
            .NotEmpty()
                .WithMessage("Text is required.")
            .When(c => c.Text != null);
    }
}
