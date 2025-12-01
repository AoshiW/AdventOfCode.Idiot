using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 9, "Mirage Maintenance")]
public class Day09 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
        => CoreFnc(span, GetExtrapolatedNextValue);

    public int Part2(ReadOnlySpan<char> span)
        => CoreFnc(span, GetExtrapolatedPreviousValue);

    static int CoreFnc(ReadOnlySpan<char> span, Func<List<List<int>>, int> extrapolatedValue)
    {
        var sum = 0;
        var data = new List<List<int>>(20)
        {
            new(16)
        };
        foreach (var line in span.EnumerateLines())
        {
            Get_size(data) = 1;
            var firstLine = data[0];
            firstLine.Clear();
            foreach (var num in line.EnumerateSlices(" "))
            {
                firstLine.Add(int.Parse(num));
            }
            var lastLine = firstLine;
            for (int r = 0; !AllIsZero(lastLine.AsSpan()); r++)
            {
                var index = data.Count;
                CollectionsMarshal.SetCount(data, index + 1);
                ref List<int> nextLine = ref data.AsSpan()[index];
                if (nextLine is null)
                    nextLine = new(data[r].Count - 1);
                else
                    nextLine.Clear();

                var dataLine = data[r];
                for (var i = 1; i < dataLine.Count; i++)
                {
                    var diff = dataLine[i] - dataLine[i - 1];
                    nextLine.Add(diff);
                }
                lastLine = nextLine;
            }
            sum += extrapolatedValue(data);
        }
        return sum;
    }

    [UnsafeAccessor(UnsafeAccessorKind.Field, Name = "_size")]
    static extern ref int Get_size(List<List<int>> list);

    static bool AllIsZero(ReadOnlySpan<int> values)
    {
        foreach (var item in values)
        {
            if (item != 0)
                return false;
        }
        return true;

    }

    static int GetExtrapolatedNextValue(List<List<int>> values)
    {
        var last = 0;
        for (var i = values.Count - 2; i >= 0; i--)
        {
            var prevLast = values[i][^1];
            last += prevLast;
        }
        return last;
    }

    static int GetExtrapolatedPreviousValue(List<List<int>> values)
    {
        var last = 0;
        for (var i = values.Count - 2; i >= 0; i--)
        {
            var prevLast = values[i][0];
            last = prevLast - last;
        }
        return last;
    }
}
