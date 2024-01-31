﻿using Wheelingful.DAL.Enums;
using Wheelingful.DAL.Entities.Abstract;

namespace Wheelingful.DAL.Entities;

public sealed class Book : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public BookCategoryEnum Category { get; set; }
    public BookStatusEnum Status { get; set; }
    public required string CoverId { get; set; }

    public List<AppUser> Users { get; set; } = [];
}
