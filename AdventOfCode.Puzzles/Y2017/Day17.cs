namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 17, "Spinlock")]
public class Day17 : IDay<int>
{
    /// <inheritdoc/>
    public int Part1(ReadOnlySpan<char> span)
    {
        const int total  = 2018;
        var step = int.Parse(span);
        var list = new List<int>(total)
        {
            0
        };
        var index = 0;
        for (int i = 1; i < total; i++)
        {
            index = (index + step) % i + 1;
            list.Insert(index, i);
        }
        return list[list.IndexOf(total - 1) + 1];
    }

    /// <inheritdoc/>
    public int Part2(ReadOnlySpan<char> span)
    {
        var step = int.Parse(span);
        var index = 0;
        var zeroIndex = 0;
        var afterZeroValue = 0;
        for (int i = 1; i < 50_000_001; i++)
        {
            index = (index + step) % i + 1;
            if (index == zeroIndex + 1)
            {
                afterZeroValue = i;
            }
            else if (index < zeroIndex)
            {
                zeroIndex++;
            }
        }
        return afterZeroValue;
    }
}
