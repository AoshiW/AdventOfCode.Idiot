using System.Text;
using System.Text.Json;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 12, "JSAbacusFramework.io")]
public class Day12 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        Span<byte> utf8 = new byte[span.Length];
        Encoding.UTF8.GetBytes(span, utf8);
        var reader = new Utf8JsonReader(utf8);
        reader.Read();
        return GetSumOfAllNumbers(ref reader);
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        Span<byte> utf8 = new byte[span.Length];
        Encoding.UTF8.GetBytes(span, utf8);
        var reader = new Utf8JsonReader(utf8);
        reader.Read();
        return GetSumOfAllNumbers2(ref reader)!.Value;
    }

    static int GetSumOfAllNumbers(ref Utf8JsonReader reader)
    {
        int sum = 0;
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                sum += GetSumOfAllNumbers(ref reader);
            }
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                reader.Read();
                sum += GetSumOfAllNumbers(ref reader);
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            sum = reader.GetInt32();
        }
        return sum;
    }

    static int? GetSumOfAllNumbers2(ref Utf8JsonReader reader)
    {
        int sum = 0;
        if (reader.TokenType == JsonTokenType.StartArray)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
            {
                sum += GetSumOfAllNumbers2(ref reader) ?? 0;
            }
        }
        else if (reader.TokenType == JsonTokenType.StartObject)
        {
            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                reader.Read();
                var val = GetSumOfAllNumbers2(ref reader);
                if (val is null)
                {
                    while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
                        reader.Skip();
                    return 0;
                }
                sum += val.Value;
            }
        }
        else if (reader.TokenType == JsonTokenType.Number)
        {
            sum = reader.GetInt32();
        }
        else if (reader.ValueTextEquals("red"u8))
        {
            return null;
        }
        return sum;
    }
}
