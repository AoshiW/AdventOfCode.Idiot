namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 12, "Leonardo's Monorail")]
public partial class Day12 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var pc = new PC(span);
        pc.Execute();
        return pc.Registers['a'];
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var pc = new PC(span);
        pc.Registers['c'] = 1;
        pc.Execute();
        return pc.Registers['a'];
    }
}
