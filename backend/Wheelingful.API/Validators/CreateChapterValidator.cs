using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class CreateChapterValidator : AbstractValidator<CreateChapterBinding>
{
    public CreateChapterValidator(WheelingfulDbContext dbContext, ICurrentUser currentUser)
    {
        RuleFor(c => c.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBookAndAuthor(id, currentUser.Id))
                .WithMessage("Either you are not the author or there is no book with such ID.");

        RuleFor(c => c.Body)
            .SetValidator(new CreateChapterBodyValidator());
    }
}
