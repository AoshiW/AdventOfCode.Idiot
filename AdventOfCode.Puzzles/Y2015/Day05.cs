using System.Buffers;

namespace AdventOfCode.Puzzles.Y2015;

[AocPuzzle(2015, 5, "Doesn't He Have Intern-Elves For This?")]
public class Day05 : IDay<int>
{
    static readonly SearchValues<string> ForbiddenPairs = SearchValues.Create(["ab", "cd", "pq", "xy"], StringComparison.Ordinal);
    static readonly SearchValues<char> Vowels = SearchValues.Create("aeiou");

    public int Part1(ReadOnlySpan<char> span)
        => NiceCounter(span, static str => AtLeastThreeVowels(str) && TwoInRow(str) && !str.ContainsAny(ForbiddenPairs));

    public int Part2(ReadOnlySpan<char> span)
        => NiceCounter(span, static str => TwoOverOne(str) && PairOfTwoLetters(str));

    static int NiceCounter(ReadOnlySpan<char> span, Func<ReadOnlySpan<char>, bool> isNice)
    {
        int count = 0;
        foreach (var line in span.EnumerateLines())
        {
            if (isNice(line))
                count++;
        }
        return count;
    }

    static bool AtLeastThreeVowels(ReadOnlySpan<char> span)
    {
        int count = 0;
        foreach (var item in span)
        {
            if (Vowels.Contains(item) && ++count > 2)
                return true;
        }
        return false;
    }

    static bool TwoInRow(ReadOnlySpan<char> span)
    {
        for (int i = 0; i < span.Length - 1; i++)
            if (span[i] == span[i + 1])
                return true;
        return false;
    }

    static bool TwoOverOne(ReadOnlySpan<char> span)
    {
        for (int i = 2; i < span.Length; i++)
        {
            if (span[i] == span[i - 2])
                return true;
        }
        return false;
    }

    static bool PairOfTwoLetters(ReadOnlySpan<char> span)
    {
        for (int i = 2; i < span.Length; i++)
        {
            if (span.Slice(i).Contains(span.Slice(i - 2, 2), StringComparison.Ordinal))
                return true;
        }
        return false;
    }
}
