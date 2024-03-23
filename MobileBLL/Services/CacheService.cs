namespace MobileBLL.Services;

public class CacheService<T>(IDistributedCache cache)
    : ICacheService<T> where T : class
{
  private readonly IDistributedCache _cache = cache;

  public async Task<IEnumerable<T>?> GetFromCacheAsync(string key)
  {
    IEnumerable<T>? countries = null;
    try
    {
      var countriesFromCache = await _cache.GetStringAsync(key);
      countries = (countriesFromCache == null) ? null
          : System.Text.Json.JsonSerializer.Deserialize<IEnumerable<T>>(countriesFromCache);
    }
    catch (Exception ex)
    {
      //_logger.LogError(ex, $"Exception occurred in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
    }

    return countries;
  }

  public async Task RemoveFromCacheAsync(string key)
      => await _cache.RemoveAsync(key);

  public async Task SaveToCacheAsync(string content, string key)
  {
    try
    {
      await _cache.SetStringAsync(key, content, new DistributedCacheEntryOptions
      {
        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
      });
    }
    catch (Exception ex)
    {
      //_logger.LogError(ex, $"Exception occurred in {MethodBase.GetCurrentMethod().Name}: {ex.Message}");
    }
  }
}
