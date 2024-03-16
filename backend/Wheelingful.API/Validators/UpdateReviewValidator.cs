using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class UpdateReviewValidator : AbstractValidator<UpdateReviewBinding>
{
    public UpdateReviewValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(b => b.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((bookId, cancelletion) => dbContext.Reviews.BeActualReview(bookId, currentUser.Id))
                .WithMessage("Either there is no book with such ID or you have never reviewed it.");

        RuleFor(b => b.Body)
            .SetValidator(new UpdateReviewBodyValidator());
    }
}
