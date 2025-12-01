using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 11, "Plutonian Pebbles")]
public class Day11 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        return CoreFunc(span, 25);
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        return CoreFunc(span, 75);
    }

    static long CoreFunc(ReadOnlySpan<char> span, int blinkCount)
    {
        var nums = new Dictionary<long, long>();
        foreach (var range in span.Split(' '))
        {
            CollectionsMarshal.GetValueRefOrAddDefault(nums, long.Parse(span[range]), out _)++;
        }

        var cache = new Dictionary<long, long>();
        Span<char> numBuff = stackalloc char[30];
        for (var i = 0; i < blinkCount; i++)
        {
            foreach (var item in nums)
            {
                if (item.Key == 0)
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(cache, 1, out _) += item.Value;
                }
                else if (item.Key.TryFormat(numBuff, out var w) && w % 2 == 0)
                {
                    var tempNum = w / 2;
                    CollectionsMarshal.GetValueRefOrAddDefault(cache, long.Parse(numBuff.Slice(0, tempNum)), out _) += item.Value;
                    CollectionsMarshal.GetValueRefOrAddDefault(cache, long.Parse(numBuff.Slice(tempNum, tempNum)), out _) += item.Value;
                }
                else
                {
                    CollectionsMarshal.GetValueRefOrAddDefault(cache, item.Key * 2024, out _) += item.Value;
                }
            }
            (cache, nums) = (nums, cache);
            cache.Clear();
        }

        long sum = 0;
        foreach (var item in nums)
        {
            sum += item.Value;
        }
        return sum;
    }
}
