namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 19, "Linen Layout")]
public class Day19 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var towelPatterns = new HashSet<string>();
        var enumerator = span.EnumerateLines(1);
        var min = int.MaxValue;
        var max = int.MinValue;
        foreach(var range in enumerator.Current.Split(", "))
        {
            var towelPattern = enumerator.Current[range];
            if (towelPattern.IsEmpty)
                continue;

            min = int.Min(min, towelPattern.Length);
            max = int.Max(max, towelPattern.Length);
            towelPatterns.Add(towelPattern.ToString());
        }
        enumerator.MoveNext();
        var count = 0;
        foreach(var line in enumerator)
        {
            if (IsPossible(line, towelPatterns.GetAlternateLookup<ReadOnlySpan<char>>(), min, max))
                count++;
        }
        return count;
    }

    static bool IsPossible(ReadOnlySpan<char> span, HashSet<string>.AlternateLookup<ReadOnlySpan<char>> towelPatterns, int min, int max)
    {
        if (span.IsEmpty)
            return true;
        var i = min;
        max = int.Min(max, span.Length);

        while(i <= max)
        {
            if (towelPatterns.Contains(span.Slice(0, i)))
                if (IsPossible(span.Slice(i), towelPatterns, min, max))
                    return true;
            i++;
        }
        return false;
    }
    public int Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }
}