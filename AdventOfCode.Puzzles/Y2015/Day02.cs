namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 2, "I Was Told There Would Be No Math")]
public class Day02 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            var box = Box.Parse(line);
            var min = int.Min(box.Length, int.Min(box.Width, box.Height));
            var max = int.Max(box.Length, int.Max(box.Width, box.Height));

            var area = 2 * (box.Width * (box.Length + box.Height) + box.Height * box.Length);
            var extra = min * (box.Length + box.Width + box.Height - min - max);
            sum += area + extra;
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            var box = Box.Parse(line);
            var max = int.Max(box.Length, int.Max(box.Width, box.Height));

            var ribbon = 2 * (box.Length + box.Width + box.Height - max);
            var bow = box.Length * box.Width * box.Height;
            sum += ribbon + bow;
        }
        return sum;
    }
}

file struct Box
{
    public int Length;
    public int Width;
    public int Height;

    public static Box Parse(ReadOnlySpan<char> span)
    {
        Span<Range> splits = stackalloc Range[4];
        if (span.Split(splits, 'x') is not 3)
            throw new FormatException("Invalid box format");

        return new Box
        {
            Length = int.Parse(span[splits[0]]),
            Width = int.Parse(span[splits[1]]),
            Height = int.Parse(span[splits[2]]),
        };
    }

    public override readonly string ToString()
    {
        return $"{Length}x{Width}x{Height}";
    }
}
