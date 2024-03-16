using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class DeleteReviewValidator : AbstractValidator<DeleteReviewRequest>
{
    public DeleteReviewValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((bookId, cancelletion) => dbContext.Reviews.BeActualReview(bookId, currentUser.Id))
                .WithMessage("Either there is no book with such ID or you have never reviewed it.");
    }
}
