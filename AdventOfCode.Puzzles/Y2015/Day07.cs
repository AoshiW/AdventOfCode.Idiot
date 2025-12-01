using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 7, "Some Assembly Required")]
public class Day07 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var dic = ParseInput(span).GetAlternateLookup<ReadOnlySpan<char>>();
        return GetSignal("a", dic);
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var dic = ParseInput(span);
        dic["b"] = GetSignal("a", dic.ToDictionary().GetAlternateLookup<ReadOnlySpan<char>>());
        return GetSignal("a", dic.GetAlternateLookup<ReadOnlySpan<char>>());
    }

    static Dictionary<string, object> ParseInput(ReadOnlySpan<char> span)
    {
        var dic = new Dictionary<string, object>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.LastIndexOf(' ');
            var temp = line.Slice(0, index - 3);
            dic.Add(
                line.Slice(index + 1).ToString(),
                int.TryParse(temp, out var value) ? value : temp.ToString()
                );
        }
        return dic;
    }

    static int GetSignal(ReadOnlySpan<char> wire, Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> dic)
    {
        ref var signal = ref CollectionsMarshal.GetValueRefOrNullRef(dic, wire);
        if (signal is int number)
            return number;

        var valueSpan = (signal as string).AsSpan();
        Span<Range> ranges = stackalloc Range[3];
        var rangesCount = valueSpan!.Split(ranges, ' ', StringSplitOptions.RemoveEmptyEntries);
        var result = rangesCount switch
        {
            1 => GetSignal(valueSpan[ranges[0]], dic),
            2 => ~GetSignal(valueSpan[ranges[1]], dic),
            3 => CalculateSignal(valueSpan[ranges[0]], valueSpan[ranges[1]], valueSpan[ranges[2]], dic),
            _ => throw new ArgumentOutOfRangeException()
        };
        signal = result;
        return result;

        static int CalculateSignal(ReadOnlySpan<char> left, ReadOnlySpan<char> operation, ReadOnlySpan<char> right, Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> dic)
        {
            if (!int.TryParse(left, out var x))
            {
                x = GetSignal(left, dic);
            }
            if (!int.TryParse(right, out var y))
            {
                y = GetSignal(right, dic);
            }

            return operation switch
            {
                "AND" => x & y,
                "OR" => x | y,
                "LSHIFT" => x << y,
                "RSHIFT" => x >> y,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
