﻿using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.BLL.Models.Requests;
using Wheelingful.DAL.DbContexts;

namespace Wheelingful.API.Validators;

public class CreateChapterValidator : AbstractValidator<CreateChapterRequest>
{
    public CreateChapterValidator(WheelingfulDbContext dbContext, ICurrentUser currentUser)
    {
        RuleFor(c => c.BookId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
                .WithMessage("Book ID is required.")
            .MustAsync((id, cancelletion) => dbContext.Books.BeActualBookAndAuthor(id, currentUser.Id))
                .WithMessage("Either you are not the author or there is no book with such ID.");

        RuleFor(c => c.Title)
            .NotEmpty()
                .WithMessage("Title is required.");

        RuleFor(c => c.Text)
            .NotEmpty()
                .WithMessage("Text is required.");
    }
}
