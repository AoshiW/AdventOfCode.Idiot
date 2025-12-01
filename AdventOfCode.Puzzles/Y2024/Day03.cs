using System.Buffers;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 3, "Mull It Over")]
public class Day03 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var sum = 0;
        int i;
        while ((i = span.IndexOf("mul(")) is not -1)
        {
            span = span.Slice(i + 4);
            if (span.IsEmpty)
                break;
            
            i = span.IndexOf(',');
            if (!int.TryParse(span.Slice(0, i), out var num1))
                continue;
            span = span.Slice(i + 1);

            i = span.IndexOf(')');
            if (!int.TryParse(span.Slice(0, i), out var num2))
                continue;
            span = span.Slice(i + 1);
            
            sum += num1 * num2;
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var sum = 0;
        int i;
        var sv = SearchValues.Create(["mul(", "don't()"], StringComparison.Ordinal);
        while ((i = span.IndexOfAny(sv)) is not -1)
        {
            if (span[i] is 'd') // don't()
            {
                span = span.Slice(i + 7);
                i = span.IndexOf("do()");
                if (i is -1)
                    return sum;
                span = span.Slice(i + 3);
                continue;
            }

            span = span.Slice(i + 4);
            if (span.IsEmpty)
                break;
            
            i = span.IndexOf(',');
            if (!int.TryParse(span.Slice(0, i), out var num1))
                continue;
            span = span.Slice(i + 1);

            i = span.IndexOf(')');
            if (!int.TryParse(span.Slice(0, i), out var num2))
                continue;
            span = span.Slice(i + 1);
            
            sum += num1 * num2;
        }
        return sum;
    }
}
