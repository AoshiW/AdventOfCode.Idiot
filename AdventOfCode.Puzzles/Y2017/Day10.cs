namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 10, "Knot Hash")]
public class Day10 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        Span<byte> data = stackalloc byte[256];
        Span<byte> buffer = stackalloc byte[256];
        int skip = 0;
        var rotate = 0;
        for (int i = 1; i < data.Length; i++)
        {
            data[i] = (byte)i;
        }
        int index = 0;
        foreach (var item in span.EnumerateSlices(","))
        {
            var value = int.Parse(item);
            if (value + index >= data.Length)
            {
                rotate += index;
                data.RotateLeft(index, buffer);
                index = 0;
            }
            data.Slice(index, value).Reverse();
            index += value + skip++;
            index %= data.Length;
        }
        rotate %= data.Length;
        data.RotateRight(rotate, buffer);
        return (data[0] * data[1]).ToString();
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        span = "1,2,3";
        Span<byte> data = stackalloc byte[span.Length + 5];
        for (int i = 0; i < span.Length; i++)
        {
            data[i] = (byte)span[i];
        }
        data[^5] = 17;
        data[^4] = 31;
        data[^3] = 73;
        data[^2] = 47;
        data[^1] = 23;
        for (int i = 0; i < 64; i++)
        {

        }
        return Convert.ToHexStringLower(data);
    }
}
