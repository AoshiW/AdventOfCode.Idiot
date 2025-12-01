using System.Text.Json.Serialization;

namespace AdventOfCode.Client;

public class Member
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("stars")]
    public int Stars { get; set; }

    // todo replace Dictionary<int, CompletionDayLevel> with custom class?
    [JsonPropertyName("completion_day_level")]
    public Dictionary<int, Dictionary<int, CompletionDayLevel>> CompletionDayLevel { get; set; } = null!;

    // todo (can return 0 whne member doesnt have any star)
    [JsonPropertyName("last_star_ts")]
    public int LastStar_ts { get; set; }

    [JsonPropertyName("global_score")]
    public int GlobalScore { get; set; }

    [JsonPropertyName("local_score")]
    public int LocalScore { get; set; }
}
