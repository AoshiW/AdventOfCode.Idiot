using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 17, "No Such Thing as Too Much")]
public class Day17 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var nums = ParseInput(span);
        int count = 0;
        for (int i = 1; i < 1 << nums.Count; i++)
        {
            var sum = 0;
            for (int num = 0; num < nums.Count; num++)
            {
                if ((i & 1 << num) != 0)
                {
                    sum += nums[num];
                }
            }
            if (sum == 150)
            {
                count++;
            }
        }
        return count;
    }

    static List<int> ParseInput(ReadOnlySpan<char> span)
    {
        var list = new List<int>();
        foreach (var item in span.EnumerateLines())
        {
            list.Add(int.Parse(item));
        }
        return list;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var nums = ParseInput(span);
        var dic = new Dictionary<int, int>();
        for (int i = 1; i < 1 << nums.Count; i++)
        {
            var sum = 0;
            for (int num = 0; num < nums.Count; num++)
            {
                if ((i & 1 << num) != 0)
                {
                    sum += nums[num];
                }
            }
            if (sum == 150)
            {
                int key = 0;
                var tempI = i;
                while (tempI != 0)
                {
                    key += tempI & 1;
                    tempI >>= 1;
                }
                ref var value = ref CollectionsMarshal.GetValueRefOrAddDefault(dic, key, out _);
                value++;
            }
        }
        return dic.MinBy(x => x.Key).Value;
    }
}
