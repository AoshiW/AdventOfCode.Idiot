using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 13, "Claw Contraption")]
public class Day13 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
        => CalculateAllTokens(span, 0);

    public long Part2(ReadOnlySpan<char> span)
        => CalculateAllTokens(span, 10_000_000_000_000);

    static long CalculateAllTokens(ReadOnlySpan<char> span, long add)
    {
        var index = 0;
        Vector2<long> a = default, b = default, prize;
        long tokens = 0;
        foreach (var line in span.EnumerateLines())
        {
            switch (index)
            {
                case 0:
                    a = ParseButton(line);
                    break;
                case 1:
                    b = ParseButton(line);
                    break;
                case 2:
                    prize = ParsePrize(line);
                    prize.X += add;
                    prize.Y += add;
                    if (TrynWin(a, b, prize, out var minTokens))
                        tokens += minTokens;
                    break;
                case 3:
                    index = -1;
                    break;
            }
            index++;
        }
        return tokens;
    }

    static Vector2<long> ParseButton(ReadOnlySpan<char> span)
    {
        span = span.Slice(11);
        var index = span.IndexOf(',');
        var x = long.Parse(span.Slice(0, index));
        span = span.Slice(index + 4);
        var y = long.Parse(span);
        return new(x, y);
    }

    static Vector2<long> ParsePrize(ReadOnlySpan<char> span)
    {
        span = span.Slice(9);
        var index = span.IndexOf(',');
        var x = long.Parse(span.Slice(0, index));
        span = span.Slice(index + 4);
        var y = long.Parse(span);
        return new(x, y);
    }

    static bool TrynWin(Vector2<long> a, Vector2<long> b, Vector2<long> prize, out long minTokens)
    {
        var x = (prize.X * b.Y - prize.Y * b.X) / (a.X * b.Y - a.Y * b.X);
        var y = (prize.X - a.X * x) / b.X;

        var result = (a.X * x + b.X * y == prize.X) && (a.Y * x + b.Y * y == prize.Y);
        minTokens = x * 3 + y;
        return result;
    }
}  
