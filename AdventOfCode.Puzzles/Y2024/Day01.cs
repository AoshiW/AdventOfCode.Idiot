using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 1, "Historian Hysteria")]
public class Day01 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var left = new List<int>();
        var right = new List<int>();
        foreach (var line in span.EnumerateLines())
        {
            var i = line.IndexOf(' ');
            var num1 = int.Parse(line.Slice(0, i));
            left.Add(num1);
            var num2 = int.Parse(line.Slice(i + 3));
            right.Add(num2);
        }
        left.Sort();
        right.Sort();
        var sum = 0;
        for (var i = 0; i < left.Count; i++)
        {
            sum += int.Abs(left[i] - right[i]);
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var left = new Dictionary<int, int>();
        var right = new Dictionary<int, int>();
        foreach (var line in span.EnumerateLines())
        {
            var i = line.IndexOf(' ');
            var num1 = int.Parse(line.Slice(0, i));
            CollectionsMarshal.GetValueRefOrAddDefault(left, num1, out _)++;
            var num2 = int.Parse(line.Slice(i + 3));
            CollectionsMarshal.GetValueRefOrAddDefault(right, num2, out _)++;
        }
        var sum = 0;
        foreach (var item in left)
        {
            sum += item.Key * item.Value * right.GetValueOrDefault(item.Key);
        }
        return sum;
    }
}
