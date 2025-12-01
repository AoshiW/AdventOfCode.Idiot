using System.Text.Json.Serialization;

namespace AdventOfCode.Client;

public class PrivateLeaderboard
{
    [JsonPropertyName("owner_id")]
    public int OwnerId { get; set; }

    [JsonPropertyName("event")]
    public string Event { get; set; } = null!;

    [JsonPropertyName("day1_ts")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset StartTime { get; set; }

    [JsonPropertyName("members")]
    public Dictionary<int, Member> Members { get; set; } = null!;
}
