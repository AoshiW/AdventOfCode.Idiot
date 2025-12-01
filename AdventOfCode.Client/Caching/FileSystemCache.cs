using Microsoft.Extensions.Caching.Distributed;

namespace AdventOfCode.Client.Caching;

public class FileSystemCache : IDistributedCache
{
    (string CacheDirectory, int) _options;

    public FileSystemCache(string options)
    {
        _options = (options, 0);
    }

    public byte[]? Get(string key)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        if (File.Exists(key))
            return File.ReadAllBytes(key);
        return null;
    }

    public async Task<byte[]?> GetAsync(string key, CancellationToken token = default)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        if (File.Exists(key))
            return await File.ReadAllBytesAsync(key, token).ConfigureAwait(false);
        return null;
    }

    public void Refresh(string key)
    {
    }

    public Task RefreshAsync(string key, CancellationToken token = default)
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        File.Delete(key);
    }

    public Task RemoveAsync(string key, CancellationToken token = default)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        File.Delete(key);
        return Task.CompletedTask;
    }

    public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        Directory.CreateDirectory(Path.GetDirectoryName(key));
        File.WriteAllBytes(key, value);
    }

    public async Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
    {
        key = Path.Combine(_options.CacheDirectory, key);
        Directory.CreateDirectory(Path.GetDirectoryName(key));
        await File.WriteAllBytesAsync(key, value, token).ConfigureAwait(false);
    }
}
