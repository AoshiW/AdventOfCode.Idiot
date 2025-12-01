using Microsoft.Extensions.Caching.Distributed;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace AdventOfCode.Client;

public class AdventOfCodeClient
{
    private readonly IDistributedCache _cache;
    private readonly HttpClient _client;

    public AdventOfCodeClient(AdventOfCodeClientOptions options, IDistributedCache cache)
    {
        _cache = cache;

        var uri = new Uri("https://adventofcode.com/");
        var handler = new HttpClientHandler();
        handler.CookieContainer.Add(uri, new System.Net.Cookie("session", options.Session));
        _client = new HttpClient(handler)
        {
            BaseAddress = uri,
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    new("Kunc.AdventOfCode", Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown"),
                    new("(https://github.com/AoshiW/AdventOfCode by Aoshi.W@gmail.com)"),
                    options.ContactInformation
                }
            }
        };
    }

    public async Task<byte[]> GetPuzzleInputAsync(int year, int day, CancellationToken cancellationToken = default)
    {
        var key = $"inputs/{year}_{day:00}.txt";
        var bytes = await _cache.GetAsync(key, cancellationToken).ConfigureAwait(false);
        if (bytes is null)
        {
            bytes = await _client.GetByteArrayAsync($"/{year}/day/{day}/input", cancellationToken).ConfigureAwait(false);
            await _cache.SetAsync(key, bytes, cancellationToken).ConfigureAwait(false);
        }
        return bytes!;
    }

    public async Task<string> GetPuzzleInputAsStringAsync(int year, int day, CancellationToken cancellationToken = default)
    {
        var bytes = await GetPuzzleInputAsync(year, day, cancellationToken).ConfigureAwait(false);
        return Encoding.UTF8.GetString(bytes);
    }

    public async Task<PrivateLeaderboard> GetPrivateLeaderboardAsync(int year, int ownerId, CancellationToken cancellationToken = default)
    {
        var key = $"privateLeaderboard/{year}_{ownerId}.json";
        var bytes = await _cache.GetAsync(key, cancellationToken).ConfigureAwait(false);
        if (bytes is null)
        {
            bytes = await _client.GetByteArrayAsync($"/{year}/leaderboard/private/view/{ownerId}.json", cancellationToken).ConfigureAwait(false);
            var cacheOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(15),
            };
            await _cache.SetAsync(key, bytes, cacheOptions, cancellationToken).ConfigureAwait(false);
        }
        return JsonSerializer.Deserialize<PrivateLeaderboard>(bytes)!;
    }

    public async Task PostAnswerAsync<T>(int year, int day, int part, T answer, CancellationToken cancellationToken = default)
    {
        using var content = new StringContent($"level={part}&answer={answer}");
        using var response = await _client.PostAsync($"{year}/day/{day}/answer", content, cancellationToken).ConfigureAwait(false);
    }
}
