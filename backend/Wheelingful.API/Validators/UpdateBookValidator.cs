using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Books;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class UpdateBookValidator : AbstractValidator<UpdateBookModel>
{
    public UpdateBookValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBookAndAuthor(id, currentUser.Id))
                .WithMessage("You are not the author.");

        RuleFor(b => b.Title)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.");

        RuleFor(b => b.Description)
            .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(b => b.Status)
            .IsInEnum()
                .WithMessage("Book status is out of defined options.");

        RuleFor(b => b.Category)
            .IsInEnum()
                .WithMessage("Book category is out of defined options.");
    }
}
