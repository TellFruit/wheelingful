namespace Wheelingful.ML.Models.Db;

public class Aspnetuserrole
{
    public string UserId { get; set; } = null!;
    public string RoleId { get; set; } = null!;

    public virtual Aspnetuser User { get; set; } = null!;
    public virtual Aspnetrole Role { get; set; } = null!;
}
