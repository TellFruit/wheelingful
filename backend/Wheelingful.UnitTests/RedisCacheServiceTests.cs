using Microsoft.Extensions.Caching.Distributed;
using Moq;
using StackExchange.Redis;
using Wheelingful.DAL.Services;

namespace Wheelingful.UnitTests;

public class RedisCacheServiceTests
{
    [Fact]
    public async Task RedisCacheService_GetAndSet_ReturnCachedData()
    {
        // Arrange

        var mockDistributedCache = new Mock<IDistributedCache>();

        var mockRedis = new Mock<IConnectionMultiplexer>();

        var existingData = new TestData { Value = "Cached Data" };

        var cachedDataBytes = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(existingData);

        mockDistributedCache.Setup(cache => cache.GetAsync("testKey", default)).ReturnsAsync(cachedDataBytes);

        var cacheService = new RedisCacheService(mockDistributedCache.Object, mockRedis.Object);

        // Act

        var result = await cacheService.GetAndSet("testKey", () => Task.FromResult(new TestData { Value = "New Data" }), TimeSpan.FromSeconds(10));

        // Assert

        Assert.Equal(existingData.Value, result.Value);
        mockDistributedCache.Verify(cache => cache.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Never);
    }

    [Fact]
    public async Task RedisCacheService_GetAndSet_ReturnFetchedData()
    {
        // Arrange

        var mockDistributedCache = new Mock<IDistributedCache>();

        var mockRedis = new Mock<IConnectionMultiplexer>();

        var newData = new TestData { Value = "New Data" };

        mockDistributedCache.Setup(cache => cache.GetAsync("testKey", default)).ReturnsAsync((byte[])null);

        var cacheService = new RedisCacheService(mockDistributedCache.Object, mockRedis.Object);

        // Act

        var result = await cacheService.GetAndSet("testKey", () => Task.FromResult(newData), TimeSpan.FromSeconds(10));

        // Assert

        Assert.Equal(newData.Value, result.Value);
        mockDistributedCache.Verify(cache => cache.SetAsync("testKey", It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), default), Times.Once);
    }

    private class TestData
    {
        public required string Value { get; set; }
    }
}
