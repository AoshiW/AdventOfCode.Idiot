using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 4, "Scratchcards")]
public class Day04 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var point = 0;
        List<int> winNums = new(), myNums = new();
        foreach (var line in span.EnumerateLines())
        {
            var winCount = 0;
            ParseLine(line, winNums, myNums);
            foreach (var myNum in myNums.AsSpan())
            {
                if (winNums.Contains(myNum))
                {
                    winCount++;
                }
            }
            point += winCount == 0 ? 0 : 1 << winCount - 1;
        }
        return point;
    }

    static void ParseLine(ReadOnlySpan<char> line, List<int> winNums, List<int> myNums)
    {
        line = line.Slice(9);
        var index = line.IndexOf('|');
        ParseNumberSection(line.Slice(0, index), winNums);
        ParseNumberSection(line.Slice(index + 1), myNums);

        static void ParseNumberSection(ReadOnlySpan<char> span, List<int> cache)
        {
            span.Trim();
            cache.Clear();
            foreach (var item in span.EnumerateSlices(" "))
            {
                cache.Add(int.Parse(item));
            }
        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var cardId = 0;
        List<int> winNums = new(), myNums = new();
        var cards = new Dictionary<int, int>();
        foreach (var line in span.EnumerateLines())
        {
            cardId++;
            ParseLine(line, winNums, myNums);
            var winCount = 0;

            foreach (var item in myNums.AsSpan())
            {
                if (winNums.Contains(item))
                {
                    winCount++;
                }
            }
            var currentCardCount = GetWithDefaultCountValue(cards, cardId);
            for (var i = 1; i <= winCount; i++)
            {
                GetWithDefaultCountValue(cards, cardId + i) += currentCardCount;
            }
        }
        return cards.Sum(x => x.Value);
    }

    static ref int GetWithDefaultCountValue(Dictionary<int, int> dictionary, int cardId)
    {
        ref var item = ref CollectionsMarshal.GetValueRefOrAddDefault(dictionary, cardId, out var has);
        if (!has)
            item = 1;
        return ref item;
    }
}
