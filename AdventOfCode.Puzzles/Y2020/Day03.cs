namespace AdventOfCode.Puzzles.Y2020;

[AocPuzzle(2020, 3, "Toboggan Trajectory")]
public class Day03 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        int x = 0, tree = 0;
        foreach (var item in span.EnumerateLines())
        {
            if (item[x] == '#')
                tree++;
            x = (x + 3) % item.Length;
        }
        return tree;
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        int x, tree;
        long treeSum = 1;
        ReadOnlySpan<(int, int)> qq = stackalloc[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };
        for (int q = 0; q < qq.Length; q++)
        {
            tree = x = 0;
            var enumerator = span.EnumerateLines();
            while (enumerator.MoveNext())
            {
                var item = enumerator.Current;
                if (item[x] == '#')
                    tree++;
                x = (x + qq[q].Item1) % item.Length;
                for (int i = 1; i < qq[q].Item2; i++)
                {
                    enumerator.MoveNext();
                }
            }
            treeSum *= tree;
        }
        return treeSum;
    }
}
