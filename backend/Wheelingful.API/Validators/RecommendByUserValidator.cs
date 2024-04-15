using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests.Queries;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class RecommendByUserValidator : AbstractValidator<RecommendByUserQuery>
{
    public RecommendByUserValidator(ICurrentUser currentUser, WheelingfulDbContext dbContext)
    {
        RuleFor(r => r)
            .MustAsync((_, cancelletion) => dbContext.Reviews.HaveReviews(currentUser.Id))
                .WithMessage("Please, review at least one book to form personal recommendations.");
    }
}
