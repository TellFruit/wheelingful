using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
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

        RuleFor(b => b.Body)
            .SetValidator(new UpdateBookBodyValidator());
    }
}
