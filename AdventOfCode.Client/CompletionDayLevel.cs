using System.Text.Json.Serialization;

namespace AdventOfCode.Client;

public class CompletionDayLevel
{

    [JsonPropertyName("star_index")]
    public int StarIndex { get; set; }

    [JsonPropertyName("get_star_ts")]
    [JsonConverter(typeof(DateTimeOffsetConverter))]
    public DateTimeOffset GetStar { get; set; }
}
