namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 3, "Lobby")]
public class Day03 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
        => PartCore(span, 2);

    public long Part2(ReadOnlySpan<char> span)
        => PartCore(span, 12);

    static long PartCore(ReadOnlySpan<char> span, int digits)
    {
        long totalJoltageOutput = 0;
        Span<char> joltageOutput = stackalloc char[digits];
        foreach (var line in span.EnumerateLines())
        {
            int pos = 0;
            for (int i = 0; i < joltageOutput.Length; i++)
            {
                for (char num = '9'; num >= '0'; num--)
                {
                    var index = line.Slice(pos).IndexOf(num);
                    if (index == -1 || line.Length - (pos + index) < joltageOutput.Length - i)
                        continue;

                    pos += index;
                    joltageOutput[i] = line[pos++];
                    break;
                }
            }
            totalJoltageOutput += long.Parse(joltageOutput);
        }
        return totalJoltageOutput;
    }
}
