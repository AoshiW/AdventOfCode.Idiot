using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 2, "Red-Nosed Reports")]
public class Day02 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
        => CoreFunc(span, IsSafe1);

    public int Part2(ReadOnlySpan<char> span)
        => CoreFunc(span, IsSafe2);

    private static int CoreFunc(ReadOnlySpan<char> span, Func<ReadOnlySpan<int>, bool> isSafe)
    {
        var sum = 0;
        var buffer = new List<int>();
        foreach (var line in span.EnumerateLines())
        {
            buffer.Clear();
            foreach (var numberRange in line.Split(' '))
            {
                    buffer.Add(int.Parse(line[numberRange]));
            }

            if (isSafe(CollectionsMarshal.AsSpan(buffer)))
                sum++;
        }
        return sum;
    }

    private static bool IsSafe1(ReadOnlySpan<int> numbers)
    {
        var isIncrease = numbers[0] < numbers[1];
        for (var i = 0; i < numbers.Length - 1; i++)
        {
            var diff = numbers[i] - numbers[i + 1];
            if (isIncrease != numbers[i] < numbers[i + 1])
                return false;
            if (int.Abs(diff) is > 3 or < 1)
                return false;
        }
        return true;
    }

    private static bool IsSafe2(ReadOnlySpan<int> numbers)
    {
        if (IsSafe1(numbers))
            return true;
        Span<int> buffer = stackalloc int[numbers.Length-1];
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers.Slice(0, i).CopyTo(buffer);
            numbers.Slice(i + 1).CopyTo(buffer.Slice(i));

            if (IsSafe1(buffer))
                return true;
        }
        return false;
    }
}
