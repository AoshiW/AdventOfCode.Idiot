using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 6, "Signals and Noise")]
public class Day06 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        var input = ParseInput(span);
        return string.Create(input.Length, input, (s, n) =>
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = n[i].MaxBy(kv => kv.Value).Key;
            }
        });
    }

    static Dictionary<char, int>[] ParseInput(ReadOnlySpan<char> span)
    {
        var input = new Dictionary<char, int>[span.IndexOf('\n')];
        for (var i = 0; i < input.Length; i++)
        {
            input[i] = new();
        }
        foreach (var item in span.EnumerateLines())
        {
            for (int i = 0; i < item.Length; i++)
            {
                ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(input[i], item[i], out _);
                value++;
            }
        }
        return input;
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        var input = ParseInput(span);
        return string.Create(input.Length, input, (s, n) =>
        {
            for (int i = 0; i < s.Length; i++)
            {
                s[i] = n[i].MinBy(kv => kv.Value).Key;
            }
        });
    }
}
