namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 16, "Permutation Promenade")]
public class Day16 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        Span<char> data = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'];
        Span<char> buffer = stackalloc char[16];
        Span<Range> splits = stackalloc Range[2];
        Core(span, data, buffer, splits);
        return data.ToString();
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        Span<char> data = ['a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p'];
        Span<char> buffer = stackalloc char[16];
        Span<Range> splits = stackalloc Range[2];
        var seen = new HashSet<string>();
        var seenSpan = seen.GetAlternateLookup<ReadOnlySpan<char>>();
        while (seenSpan.Add(data))
        {
            Core(span, data, buffer, splits);
        }
        var seenCount = 1_000_000_000 % seen.Count;
        for (int i = 0; i < seenCount; i++)
        {
            Core(span, data, buffer, splits);
        }
        return data.ToString();
    }

    static void Core(ReadOnlySpan<char> span, Span<char> data, Span<char> buffer, Span<Range> splits)
    {
        foreach (var item in span.EnumerateSlices(","))
        {
            switch (item[0])
            {
                case 's':
                    int value = int.Parse(item.Slice(1));
                    data.RotateRight(value, buffer);
                    break;
                case 'x':
                    var temp = item.Slice(1);
                    temp.Split(splits, '/');
                    int a = int.Parse(temp[splits[0]]);
                    int b = int.Parse(temp[splits[1]]);
                    (data[a], data[b]) = (data[b], data[a]);
                    break;
                case 'p':
                    int indexA = data.IndexOf(item[1]);
                    int indexB = data.IndexOf(item[3]);
                    (data[indexA], data[indexB]) = (data[indexB], data[indexA]);
                    break;
            }
        }
    }
}
