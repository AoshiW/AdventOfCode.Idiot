namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 12, "Hot Springs")]
public class Day12 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) 
        => CoreFnc(span, false);

    public int Part2(ReadOnlySpan<char> span)
        => throw new NotImplementedException(); //CoreFnc(span, true);

    static int CoreFnc(ReadOnlySpan<char> span, bool expand)
    {
        var sum = 0;
        var cache = new List<char>();
        var nums = new List<int>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.IndexOf(' ');
            cache.Clear();
            cache.AddRange(line.Slice(0, index));
            nums.Clear();
            foreach (var num in line.Slice(index + 1).EnumerateSlices(","))
            {
                nums.Add(int.Parse(num));
            }
            if (expand)
            {
                var initNums = nums.AsSpan();
                for (int i = 0; i < 4; i++)
                {
                    nums.AddRange(initNums);
                }
                var data = cache.AsSpan();
                for (int i = 0; i < 4; i++)
                {
                    cache.Add('?');
                    cache.AddRange(data);
                }
            }

            var arr = Arrangement(cache.AsSpan().Trim('.'), nums.AsSpan(), nums.Sum(), cache.AsSpan().Trim('.'));
            sum += arr;
        }
        return sum;
    }

    static int Arrangement(Span<char> span, ReadOnlySpan<int> nums, int numsSum, Span<char> orig)
    {
        span = span.TrimStart('.');
        if (span.IsEmpty && nums.IsEmpty)
            return 1;
        if (span.Length < numsSum)
            return 0;
        var index = span.IndexOfAnyExcept('#');
        if (index == -1)
        {
            return span.Length == numsSum && nums.Length == 1 ? 1 : 0;
        }
        if (!nums.IsEmpty && index > nums[0])
            return 0;
        if (span[index] == '.')
        {
            if (nums.Length == 0)
                return 0;
            if (index == nums[0])
                return Arrangement(span.Slice(index + 1), nums.Slice(1), numsSum - nums[0], orig);
            return 0;
        }
        else
        {
            if (!nums.IsEmpty && index > nums[0])
                return 0;
            span[index] = '#';
            var a1 = Arrangement(span, nums, numsSum, orig);
            span[index] = '.';
            var a2 = Arrangement(span, nums, numsSum, orig);
            span[index] = '?';
            return a1 + a2;
        }
    }
}
