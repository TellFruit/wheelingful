﻿using FluentValidation;
using Wheelingful.API.Extensions.Validation;
using Wheelingful.API.Models.Bindings.Bodies;

namespace Wheelingful.API.Validators;

public class UpdateBookBodyValidator : AbstractValidator<UpdateBookBody>
{
    public UpdateBookBodyValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty()
                .WithMessage("Title is required.")
            .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.");

        RuleFor(b => b.Description)
            .NotEmpty()
                .WithMessage("Description is required.")
            .MaximumLength(1000)
                .WithMessage("Description must not exceed 1000 characters.");

        RuleFor(b => b.Status)
            .IsInEnum()
                .WithMessage("Book status is out of defined options.");

        RuleFor(b => b.Category)
            .IsInEnum()
                .WithMessage("Book category is out of defined options.");

        RuleFor(b => b.CoverBase64)
            .NotEmpty()
                .WithMessage("Cover image is required.")
            .Must((x) => x!.BeValidBase64())
                .WithMessage("Cover image is corrupted.")
            .When(b => b.CoverBase64 != null);
    }
}
