using System;
using System.Collections.Generic;

namespace Wheelingful.ML.Models.Db;

public partial class Review
{
    public int BookId { get; set; }

    public string UserId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Text { get; set; } = null!;

    public int Score { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
}
