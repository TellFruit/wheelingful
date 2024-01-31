using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class FetchChapterValidator : AbstractValidator<FetchChapterRequest>
{
    public FetchChapterValidator(WheelingfulDbContext dbContext)
    {
        RuleFor(c => c.ChapterId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Chapter ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Chapters.BeActualChapter(id))
                .WithMessage("There is no chapter with such ID.");
    }
}
