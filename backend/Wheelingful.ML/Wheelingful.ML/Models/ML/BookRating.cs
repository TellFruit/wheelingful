using Microsoft.ML.Data;

namespace Wheelingful.ML.Models.ML;

public class BookRating
{
    [LoadColumn(1)]
    public string UserId;
    [LoadColumn(0)]
    public float BookId;
    [LoadColumn(2)]
    public float ReviewScore;
}
