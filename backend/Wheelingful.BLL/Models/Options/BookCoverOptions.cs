namespace Wheelingful.BLL.Models.Options;

public class BookCoverOptions
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Folder { get; set; } = string.Empty;
    public string DefaultCover { get; set; } = string.Empty;
}
