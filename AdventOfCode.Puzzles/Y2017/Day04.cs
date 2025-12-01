namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 4, "High-Entropy Passphrases")]
public class Day04 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) => PassphrasesValid(span, x => x.ToString());

    public int Part2(ReadOnlySpan<char> span) => PassphrasesValid(span, x =>
    {
        Span<char> temp = stackalloc char[x.Length];
        x.CopyTo(temp);
        temp.Sort();
        return new(temp);
    });

    static int PassphrasesValid(ReadOnlySpan<char> span, Func<ReadOnlySpan<char>,string> modifier)
    {
        int count = 0;
        var list = new List<string>();
        foreach (var line in span.EnumerateLines())
        {
            count++;
            foreach (var item in line.EnumerateSlices(" "))
            {
                list.Add(modifier(item));
            }
            foreach (var item in list)
            {
                if (list.Count(x => x == item) > 1)
                {
                    count--;
                    break;
                }
            }
            list.Clear();
        }
        return count;
    }
}
