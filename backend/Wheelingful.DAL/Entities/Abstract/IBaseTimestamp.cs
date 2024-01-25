namespace Wheelingful.DAL.Entities.Abstract;

internal interface IBaseTimestamp
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
