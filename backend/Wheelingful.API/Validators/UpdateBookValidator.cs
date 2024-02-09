using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookBinding>
{
    public UpdateBookValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBookAndAuthor(id, currentUser.Id))
                .WithMessage("Either you are not the author or there is no book with such ID.");

        RuleFor(b => b.Body.Title)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.");

        RuleFor(b => b.Body.Description)
            .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(b => b.Body.Status)
            .IsInEnum()
                .WithMessage("Book status is out of defined options.");

        RuleFor(b => b.Body.Category)
            .IsInEnum()
                .WithMessage("Book category is out of defined options.");

        RuleFor(b => b.Body.CoverBase64)
            .NotEmpty()
                .WithMessage("Cover image is required.")
            .Must((x) => x!.BeValidBase64())
                .WithMessage("Cover image is corrupted.")
            .When(b => b.Body.CoverBase64 != null);
    }
}
