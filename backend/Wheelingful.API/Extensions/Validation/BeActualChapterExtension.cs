using Microsoft.EntityFrameworkCore;
using Wheelingful.DAL.Entities;

namespace Wheelingful.API.Extensions.Validation;

public static class BeActualChapterExtension
{
    public static Task<bool> BeActualChapter(this DbSet<Chapter> set, int chapterId)
    {
        return set.AnyAsync(b => b.Id == chapterId);
    }
}
