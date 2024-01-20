namespace Wheelingful.Data.Entities.Abstract;

internal interface IBaseTimestamp
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
