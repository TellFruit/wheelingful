using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Books;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class DeleteBookValidator : AbstractValidator<DeleteBookModel>
{
    public DeleteBookValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.Id)
            .NotEmpty()
                .WithMessage("There is no book with such ID")
            .MustAsync((bookId, cancellation) => dbContext.Books.BeActualBookAndAuthor(bookId, currentUser.Id))
                .WithMessage("There is no book with such ID");
    }
}
