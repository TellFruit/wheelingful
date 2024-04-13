namespace Wheelingful.BLL.Models.Predictions;

public class ModelInput
{
    public required string UserId { get; set; }
    public int BookId { get; set; }
    public float ReviewScore { get; set; }
}
