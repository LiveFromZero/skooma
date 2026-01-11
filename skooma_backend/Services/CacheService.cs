using Microsoft.Extensions.Caching.Memory;

namespace skooma_backend.Services;

public class CacheService
{
    private readonly IMemoryCache _memoryCache;

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public async Task<object?> GetCachedDataAsync(string cacheKey)
    {
        // Simulate async operation
        return await Task.FromResult(_memoryCache.TryGetValue(cacheKey, out var cachedData) ? cachedData : null);
    }

    public async Task SaveToCacheAsync(string cacheKey, object chartData, TimeSpan? expirationTime = null)
    {
        // Simulate async operation
        await Task.Run(() =>
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expirationTime ?? TimeSpan.FromMinutes(30)
            };
            _memoryCache.Set(cacheKey, chartData, cacheEntryOptions);
        });
    }
}