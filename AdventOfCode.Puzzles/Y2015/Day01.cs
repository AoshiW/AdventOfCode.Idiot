namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 1, "Not Quite Lisp")]
public class Day01 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        return span.Length - span.Count(')') * 2;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        for (int position = 0, florr = 0; position < span.Length; position++)
        {
            florr += span[position] == '(' ? 1 : -1;
            if (florr == -1)
                return position + 1;
        }
        throw new ArgumentException(null, nameof(span));
    }
}
