using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;

namespace Wheelingful.UnitTests.Helpers;

public static class MockServicesHelper
{
    public static ILogger<T> MockLogger<T>()
    {
        return new NullLoggerFactory().CreateLogger<T>();
    }

    public static IOptions<T> MockOptions<T>(T options) where T : class
    {
        var mock = new Mock<IOptions<T>>();
        
        mock.Setup(m => m.Value).Returns(options);

        return mock.Object;
    }
}
