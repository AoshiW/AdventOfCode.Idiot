using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 24, "Crossed Wires")]
public class Day24 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        var dic = new Dictionary<string, object>();
        foreach(var line in span.EnumerateLines())
        {
            if (line.IsEmpty)
                continue;
            var index = line.IndexOf(':');
            if (index is not -1)
            {
                var key = line.Slice(0, index).ToString();
                var value = line.Slice(index + 2);
                dic.Add(key, int.TryParse(value, out var num) ? num : value.ToString());
            }
            else
            {
                index = line.LastIndexOf(' ');
                var key = line.Slice(index+1).ToString();
                var value = line.Slice(0, index - 3);
                dic.Add(key, value.ToString());
            }
        }
        var result = 0L;
        foreach (var item in dic)
        {
            if (item.Key[0] is 'z')
            {
                long value = Get(item.Key, dic.GetAlternateLookup<ReadOnlySpan<char>>());
                result |= value << int.Parse(item.Key.AsSpan().Slice(1));
            }
        }
        return result.ToString();
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }

    static int Get(ReadOnlySpan<char> wire, Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> dic)
    {
        ref var signal = ref CollectionsMarshal.GetValueRefOrNullRef(dic, wire);
        if (signal is int number)
            return number;

        var valueSpan = (signal as string).AsSpan();
        Span<Range> ranges = stackalloc Range[3];
        var rangesCount = valueSpan!.Split(ranges, ' ', StringSplitOptions.RemoveEmptyEntries);
        var result = rangesCount switch
        {
            1 => Get(valueSpan[ranges[0]], dic),
            2 => ~Get(valueSpan[ranges[1]], dic),
            3 => Calculate(valueSpan[ranges[0]], valueSpan[ranges[1]], valueSpan[ranges[2]], dic),
            _ => throw new ArgumentOutOfRangeException()
        };
        signal = result;
        return result;

        static int Calculate(ReadOnlySpan<char> left, ReadOnlySpan<char> operation, ReadOnlySpan<char> right, Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> dic)
        {
            if (!int.TryParse(left, out var x))
            {
                x = Get(left, dic);
            }
            if (!int.TryParse(right, out var y))
            {
                y = Get(right, dic);
            }

            return operation switch
            {
                "AND" => x & y,
                "OR" => x | y,
                "XOR" => x ^ y,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
