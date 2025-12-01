namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 23, "Safe Cracking")]
public class Day23 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var pc = new Day12.PC(span);
        pc.Registers['a'] = 7;
        pc.Execute();
        return pc.Registers['a'];
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var pc = new Day12.PC(span);
        pc.Registers['a'] = 12;
        pc.Execute();
        return pc.Registers['a'];
    }
}
