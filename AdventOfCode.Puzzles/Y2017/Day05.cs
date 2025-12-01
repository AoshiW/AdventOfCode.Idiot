namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 5, "A Maze of Twisty Trampolines, All Alike")]
public class Day05 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) => StepsCounter(span, x => x + 1);
    
    public int Part2(ReadOnlySpan<char> span) => StepsCounter(span, x => x + (x >= 3 ? -1 : 1));

    static int StepsCounter(ReadOnlySpan<char> span, Func<int, int> instructionModifier)
    {
        var nums = new List<int>();
        foreach (var item in span.EnumerateLines())
        {
            nums.Add(int.Parse(item));
        }
        int steps = 0;
        for (int i = 0; i < nums.Count; steps++)
        {
            var ii = i;
            i += nums[i];
            nums[ii] = instructionModifier(nums[ii]);
        }
        return steps;
    }
}
