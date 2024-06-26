﻿using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class ValidateReviewExtension
{
    public static Task<bool> BeActualReview(this DbSet<Review> reviews, int bookId, string userId)
    {
        return reviews.AnyAsync(r => r.BookId == bookId && r.UserId == userId);
    }

    public static Task<bool> HaveReviews(this DbSet<Review> reviews, string userId)
    {
        return reviews.AnyAsync(review => review.UserId == userId);
    }
}
