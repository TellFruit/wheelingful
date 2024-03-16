using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class CreateReviewValidator : AbstractValidator<CreateReviewBinding>
{
    public CreateReviewValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((bookId, cancelletion) => dbContext.Books.BeActualBook(bookId))
                .WithMessage("There is no book with such ID.")
            .MustAsync(async (bookId, cancelletion) => !await dbContext.Reviews.BeActualReview(bookId, currentUser.Id))
                .WithMessage("You have already reviewed this book.");

        RuleFor(b => b.Body)
            .SetValidator(new CreateReviewBodyValidator());
    }
}
