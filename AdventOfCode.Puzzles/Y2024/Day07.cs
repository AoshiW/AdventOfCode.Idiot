using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 7, "Bridge Repair")]
public class Day07 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        return CoreFunc(span, 2);
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        return CoreFunc(span, 3);
    }

    static ReadOnlySpan<char> Zeros => "0000000000000000000000000000000000000";

    static long CoreFunc(ReadOnlySpan<char> span, int numBase)
    {
        long totalCalibrationResult = 0;
        var numbers = new List<long>();
        foreach (var line in span.EnumerateLines())
        {
            var i = line.IndexOf(':');
            var calibrationResult = long.Parse(line.Slice(0, i));
            numbers.Clear();
            var newLine = line.Slice(i + 2);
            foreach (var range in newLine.Split(" "))
            {
                numbers.Add(long.Parse(newLine[range]));
            }
            if (IsValid(calibrationResult, numbers[0], CollectionsMarshal.AsSpan(numbers).Slice(1), Opts.AsSpan().Slice(0, numBase)))
                totalCalibrationResult += calibrationResult;
        }
        return totalCalibrationResult;
    }

    static bool IsValid(long calibrationResult, long currentResult, ReadOnlySpan<long> numbers, ReadOnlySpan<Func<long, long, long>> opts)
    {
        if (numbers.IsEmpty)
            return calibrationResult == currentResult;

        if (currentResult > calibrationResult)
            return false;

        foreach (var opt in opts)
        {
            if (IsValid(calibrationResult, opt(currentResult, numbers[0]), numbers.Slice(1), opts))
                return true;
        }
        return false;
    }
    static readonly Func<long, long, long>[] Opts = [
        (long x, long y) => x + y,
        (long x, long y) => x * y,
        (long x, long y) =>
        {
            Span<char> numBuf = stackalloc char[64];
            numBuf.TryWrite($"{x}{y}", out var written);
            return long.Parse(numBuf.Slice(0, written));
        }
    ];
}
