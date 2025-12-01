using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 14, "Parabolic Reflector Dish")]
public class Day14 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var list = new List<char[]>();
        foreach (var line in span.EnumerateLines())
        {
            list.Add(line.ToArray());
        }
        var listSpan = list.AsSpan();
        North(listSpan);
        return Calculate(listSpan);
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var list = new List<char[]>();
        foreach (var line in span.EnumerateLines())
        {
            list.Add(line.ToArray());
        }
        var listSpan = list.AsSpan();
        var cache = new Dictionary<string, int>();
        var sb = new StringBuilder(listSpan.Length * listSpan[0].Length);
        int rest = 0;
        for (int i = 0; i < 1_000_000_000; i++)
        {
            North(listSpan);
            West(listSpan);
            South(listSpan);
            East(listSpan);
            sb.Clear();
            foreach (var line in listSpan)
            {
                sb.Append(line);
            }
            ref var item = ref CollectionsMarshal.GetValueRefOrAddDefault(cache, sb.ToString(), out var exist);
            if (exist)
            {
                rest = (1_000_000_000 - item) % (i - item) - 1;
                break;
            }
            item = i;
        }
        for (int i = 0; i < rest; i++)
        {
            North(listSpan);
            West(listSpan);
            South(listSpan);
            East(listSpan);
        }
        return Calculate(listSpan);
    }

    static int Calculate(Span<char[]> span)
    {
        var sum = 0;
        var i = span[0].Length;
        foreach (var line in span)
        {
            sum += line.AsSpan().Count('O') * i--;
        }
        return sum;
    }

    static void North(Span<char[]> span)
    {
        bool isSwap = true;
        while (isSwap)
        {
            isSwap = false;
            for (int r = 1; r < span.Length; r++)
            {
                for (int c = 0; c < span[r].Length; c++)
                {
                    if (span[r][c] == 'O' && span[r - 1][c] == '.')
                    {
                        isSwap = true;
                        span[r - 1][c] = 'O';
                        span[r][c] = '.';
                    }
                }
            }
        }
    }
    static void South(Span<char[]> span)
    {
        bool isSwap = true;
        while (isSwap)
        {
            isSwap = false;
            for (int r = span.Length - 2; r >= 0; r--)
            {
                for (int c = 0; c < span[r].Length; c++)
                {
                    if (span[r][c] == 'O' && span[r + 1][c] == '.')
                    {
                        isSwap = true;
                        span[r + 1][c] = 'O';
                        span[r][c] = '.';
                    }
                }
            }
        }
    }
    static void West(Span<char[]> span)
    {
        foreach (var line in span)
        {
            bool isSwap = true;
            while (isSwap)
            {
                isSwap = false;
                for (int c = 1; c < line.Length; c++)
                {
                    if (line[c] == 'O' && line[c - 1] == '.')
                    {
                        isSwap = true;
                        line[c] = '.';
                        line[c - 1] = 'O';
                    }
                }
            }
        }
    }
    static void East(Span<char[]> span)
    {
        foreach (var line in span)
        {
            bool isSwap = true;
            while (isSwap)
            {
                isSwap = false;
                for (int c = line.Length - 2; c >= 0; c--)
                {
                    if (line[c] == 'O' && line[c + 1] == '.')
                    {
                        isSwap = true;
                        line[c] = '.';
                        line[c + 1] = 'O';
                    }
                }
            }
        }
    }
}
