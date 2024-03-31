namespace Wheelingful.ML.Models.Db;

public class Authorship
{
    public string UsersId { get; set; } = null!;
    public int BooksId { get; set; }

    public virtual Aspnetuser User { get; set; } = null!;
    public virtual Book Book { get; set; } = null!;
}
