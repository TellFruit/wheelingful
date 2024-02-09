using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class UpdateChapterValidator : AbstractValidator<UpdateChapterBinding>
{
    public UpdateChapterValidator(WheelingfulDbContext dbContext, ICurrentUser currentUser)
    {
        RuleFor(c => c.ChapterId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Chapter ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Chapters.BeActualChapter(id))
                .WithMessage("There is no chapter with such ID.");

        RuleFor(c => c.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBookAndAuthor(id, currentUser.Id))
                .WithMessage("Either you are not the author or there is no book with such ID.");

        RuleFor(c => c.Body.Title)
            .NotEmpty()
                .WithMessage("Title is required.");

        RuleFor(c => c.Body.Text)
            .NotEmpty()
                .WithMessage("Text is required.")
            .When(c => c.Body.Text != null);
    }
}
