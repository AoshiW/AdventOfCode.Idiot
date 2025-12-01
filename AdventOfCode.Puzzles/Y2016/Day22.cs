namespace AdventOfCode.Puzzles.Y2016;

[AocPuzzle(2016, 22, "Grid Computing")]
public class Day22 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var nodes = new List<(int Used, int Avail)>();
        foreach (var item in span.EnumerateLines(2))
        {
            var enumerator = item.EnumerateSlices(" T", 3);
            var used = int.Parse(enumerator.Current);
            enumerator.MoveNext();
            var avail = int.Parse(enumerator.Current);
            nodes.Add((used, avail));
        }
        int match = 0;
        for (int i = 0; i < nodes.Count; i++)
        {
            var nodeA = nodes[i];
            for (int ii = 0; ii < nodes.Count; ii++)
            {
                var nodeB = nodes[ii];
                if (i != ii && nodeA.Used <= nodeB.Avail && nodeA.Used != 0)
                    match++;
            }
        }
        return match;
    }

    public int Part2(ReadOnlySpan<char> span) => throw new NotImplementedException();
}
