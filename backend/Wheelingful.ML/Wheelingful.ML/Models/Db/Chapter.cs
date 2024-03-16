using System;
using System.Collections.Generic;

namespace Wheelingful.ML.Models.Db;

public partial class Chapter
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public int BookId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;
}
