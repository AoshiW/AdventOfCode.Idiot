using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 5, "Print Queue")]
public class Day05 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var rules = new Dictionary<int, List<int>>();
        var enumerator = span.EnumerateLines();
        while (enumerator.MoveNext() && !enumerator.Current.IsEmpty)
        {
            var index = enumerator.Current.IndexOf('|');
            var n1 = int.Parse(enumerator.Current.Slice(0, index));
            var n2 = int.Parse(enumerator.Current.Slice(index + 1));
            ref var ruless = ref CollectionsMarshal.GetValueRefOrAddDefault(rules, n1, out _);
            ruless ??= new();
            ruless.Add(n2);
        }
        var numbers = new List<int>();
        var sum = 0;
        foreach (var line in enumerator)
        {
            numbers.Clear();
            foreach (var numRange in line.Split(','))
            {
                numbers.Add(int.Parse(line[numRange]));
            }
            var ico = true;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (!rules.TryGetValue(numbers[i], out var rule) || !rule.Contains(numbers[i + 1]))
                {
                    ico = false; break;
                }
            }
            if (ico)
                sum += numbers[numbers.Count / 2];
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var rules = new Dictionary<int, HashSet<int>>();
        var enumerator = span.EnumerateLines();
        while (enumerator.MoveNext() && !enumerator.Current.IsEmpty)
        {
            var index = enumerator.Current.IndexOf('|');
            var n1 = int.Parse(enumerator.Current.Slice(0, index));
            var n2 = int.Parse(enumerator.Current.Slice(index + 1));
            ref var ruless = ref CollectionsMarshal.GetValueRefOrAddDefault(rules, n1, out _);
            ruless ??= new();
            ruless.Add(n2);
        }
        var numbers = new List<int>();
        var sum = 0;
        foreach (var line in enumerator)
        {
            numbers.Clear();
            foreach (var numRange in line.Split(','))
            {
                numbers.Add(int.Parse(line[numRange]));
            }
            var ico = true;
            for (int i = 0; i < numbers.Count - 1; i++)
            {
                if (!rules.TryGetValue(numbers[i], out var rule) || !rule.Contains(numbers[i + 1]))
                {
                    ico = false;
                    numbers.Sort((l, r) => rules.TryGetValue(l, out var rule) && rule.Contains(r) ? 1 : -1);
                    break;
                }
            }

            if (!ico)
                sum += numbers[numbers.Count / 2];
        }
        return sum;
    }
}
