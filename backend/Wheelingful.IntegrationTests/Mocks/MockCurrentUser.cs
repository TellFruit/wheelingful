using Wheelingful.BLL.Contracts.Auth;
using Wheelingful.IntegrationTests.Constants;

namespace Wheelingful.IntegrationTests.Mocks;

public class MockCurrentUser : ICurrentUser
{
    private string _dumpId = null!;

    public string Id { get => DataConstants.AdminUserId; set => _dumpId = value; }
}
