using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 22, "Monkey Market")]
public class Day22 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var sum = 0L;
        foreach (var line in span.EnumerateLines())
        {
            var secretNumber = long.Parse(line);
            for (var i = 0; i < 2_000; i++)
            {
                secretNumber = EvolveSecretNumber(secretNumber);
            }
            sum += secretNumber;
        }
        return sum;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var dic = new Dictionary<int, int>();
        var cache = new HashSet<int>();
        foreach (var line in span.EnumerateLines())
        {
            cache.Clear();
            var secretNumber = long.Parse(line);
            var lastNum = (int)(secretNumber % 10);
            int diff = 0;
            for (var i = 0; i < 3; i++)
            {
                secretNumber = EvolveSecretNumber(secretNumber);
                var val = (int)(secretNumber % 10);
                diff = (diff << 8) + val - lastNum;
                lastNum = val;
            }
            for (var i = 3; i < 2_000; i++)
            {
                secretNumber = EvolveSecretNumber(secretNumber);
                var val = (int)(secretNumber % 10);
                diff = (diff << 8) + val - lastNum;
                lastNum = val;
                if (cache.Add(diff))
                    CollectionsMarshal.GetValueRefOrAddDefault(dic, diff, out _) += val;
            }
        }
        return dic.MaxBy(x => x.Value).Value;
    }

    static long EvolveSecretNumber(long secretNumber)
    {
        secretNumber = ((secretNumber << 6) ^ secretNumber) & 16777215;
        secretNumber = ((secretNumber >> 5) ^ secretNumber) & 16777215;
        secretNumber = ((secretNumber << 11) ^ secretNumber) & 16777215;
        return secretNumber;
    }
}
