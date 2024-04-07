using System;
using System.Collections.Generic;

namespace Wheelingful.ML.Models.Db;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int Category { get; set; }

    public int Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string CoverId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public virtual ICollection<Chapter> Chapters { get; set; } = new List<Chapter>();

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual Aspnetuser User { get; set; } = null!;
}
