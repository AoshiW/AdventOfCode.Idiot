using System.Buffers;
using System.Diagnostics;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 1, "Trebuchet?!")]
public class Day01 : IDay<int>
{
    static readonly SearchValues<char> NiceNumbers = SearchValues.Create("123456789");
    static readonly string[] NotNiceNumbers = [
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine",
    ];

    public int Part1(ReadOnlySpan<char> span)
    {
        int sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            int min = line.IndexOfAny(NiceNumbers);
            int max = line.LastIndexOfAny(NiceNumbers);
            sum += ToNum(min, line) * 10 + ToNum(max, line);
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        int sum = 0;
        foreach (var line in span.EnumerateLines())
        {
            int min = line.IndexOfAny(NiceNumbers);
            int max = line.LastIndexOfAny(NiceNumbers);
            foreach (var line2 in NotNiceNumbers)
            {
                var i = line.IndexOf(line2);
                if (i is -1)
                    continue;
                min = int.Min(min, i);
                max = int.Max(max, line.LastIndexOf(line2));
            }
            sum += ToNum(min, line) * 10 + ToNum(max, line);
        }
        return sum;
    }

    static int ToNum(int index, ReadOnlySpan<char> span)
    {
        var c = span[index];
        var num = c switch
        {
            'o' => 1,
            't' => span[index + 1] == 'w' ? 2 : 3,
            'f' => span[index + 1] == 'o' ? 4 : 5,
            's' => span[index + 1] == 'i' ? 6 : 7,
            'e' => 8,
            'n' => 9,
            _ => c - '0'
        };
        Debug.Assert(num is >= 0 and <= 9);
        return num;
    }
}
