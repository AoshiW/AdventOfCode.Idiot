namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 25, "Code Chronicle")]
public class Day25 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var keys = new List<byte[]>();
        var locks = new List<byte[]>();

        bool? type = default;
        Span<byte> buffer = stackalloc byte[5];
        foreach (var line in span.EnumerateLines())
        {
            type ??= line[0] == '#';
            if (line.IsEmpty)
            {
                (type!.Value ? keys : locks).Add(buffer.ToArray());
                buffer.Clear();
                type = default;
                continue;
            }
            for (int i = 0; i < line.Length; i++)
            {
                buffer[i] += line[i] == '#' ? (byte)1 : (byte)0;
            }
        }
        (type!.Value ? keys : locks).Add(buffer.ToArray());

        var sum = 0;
        foreach (var key in keys)
        {
            foreach(var @lock in locks)
            {
                if (key.Zip(@lock).All(x => x.First + x.Second <= 7))
                    sum++;
            }
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        return default;
    }
}
