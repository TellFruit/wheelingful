namespace Wheelingful.Core.DTO.Books;

public class BookCoverOptions
{
    public int Width { get; set; }
    public int Height { get; set; }
    public string Folder { get; set; } = string.Empty;
    public string Extension { get; set; } = string.Empty;
}
