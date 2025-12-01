using AdventOfCode.Puzzles.Numerics;
using System.Buffers;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 8, "Resonant Collinearity")]
public class Day08 : IDay<int>
{
    static SearchValues<char> s_searchValues = SearchValues.Create(".\r\n");

    public int Part1(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var dic = new Dictionary<char, List<Vector2<int>>>();
        for (int rawIndex = 0; true;)
        {
            var i = span.Slice(rawIndex).IndexOfAnyExcept(s_searchValues);
            if (i is -1)
                break;

            rawIndex += i;
            var c = span[rawIndex];
            var pos = map.GetPointFromRawIndex(rawIndex);
            ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(dic, c, out _);
            list ??= new();
            list.Add(pos);
            rawIndex++;
        }
        HashSet<Vector2<int>> cache = new();
        foreach (var frequency in dic.Values)
        {
            for (var i = 0; i < frequency.Count - 1; i++)
            {
                var pos1 = frequency[i];
                for (var i2 = i + 1; i2 < frequency.Count; i2++)
                {
                    var pos2 = frequency[i2];
                    var diff = pos1 - pos2;
                    var pos1Temp = pos1 + diff;
                    if (map.IsPointValid(pos1Temp))
                        cache.Add(pos1Temp);
                    if (map.IsPointValid(pos2 -= diff))
                        cache.Add(pos2);

                }
            }
        }
        return cache.Count;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var map = new Input2D(span);
        var dic = new Dictionary<char, List<Vector2<int>>>();
        for (int rawIndex = 0; true;)
        {
            var i = span.Slice(rawIndex).IndexOfAnyExcept(s_searchValues);
            if (i is -1)
                break;

            rawIndex += i;
            var c = span[rawIndex];
            var pos = map.GetPointFromRawIndex(rawIndex);
            ref var list = ref CollectionsMarshal.GetValueRefOrAddDefault(dic, c, out _);
            list ??= new();
            list.Add(pos);
            rawIndex++;
        }
        HashSet<Vector2<int>> cache = new();
        foreach (var frequency in dic.Values)
        {
            for (var i = 0; i < frequency.Count - 1; i++)
            {
                var pos1 = frequency[i];
                for (var i2 = i + 1; i2 < frequency.Count; i2++)
                {
                    var pos2 = frequency[i2];
                    var diff = pos1 - pos2;
                    var p1 = pos1;

                    cache.Add(p1);
                    while (map.IsPointValid(p1 += diff))
                    {
                        cache.Add(p1);
                    }
                    cache.Add(pos2);
                    while (map.IsPointValid(pos2 -= diff))
                    {
                        cache.Add(pos2);
                    }
                }
            }
        }
        return cache.Count;
    }
}
