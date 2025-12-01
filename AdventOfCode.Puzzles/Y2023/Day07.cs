namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 7, "Camel Cards")]
public class Day07 : IDay<int>
{
    static int CoreFnc(ReadOnlySpan<char> span, Func<ReadOnlySpan<char>, List<CamelCard>> Parse, char[] chars)
    {
        var cards = Parse(span);
        cards.Sort((l, r) =>
        {
            if (l.Rank != r.Rank)
                return r.Rank - l.Rank;
            for (var i = 0; i < l.Orig.Length; i++)
                if (r.Orig[i] != l.Orig[i])
                    return chars.AsSpan().IndexOf(r.Orig[i]) - chars.AsSpan().IndexOf(l.Orig[i]);
            return 0;
        });
        var total = 0;
        int i = 1;
        foreach (var item in cards.AsSpan())
        {
            total += i++ * item.Number;
        }
        return total;
    }

    public int Part1(ReadOnlySpan<char> span) => CoreFnc(span, Parse1, SR.ToArray());
    static ReadOnlySpan<char> SR => ['A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'];
    static ReadOnlySpan<char> SR2 => ['A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'];

    static List<CamelCard> Parse1(ReadOnlySpan<char> span)
    {
        var cards = new List<CamelCard>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.IndexOf(' ');
            var str = line.Slice(0, index).ToString();
            var num = int.Parse(line.Slice(index + 1));

            var groups = str.GroupBy(x => x);
            var rank = groups.Count() switch
            {
                1 => CamelCardsType.Five,
                2 => groups.Any(x => x.Count() == 4) ? CamelCardsType.Four : CamelCardsType.FullHouse,
                3 => groups.Any(x => x.Count() == 3) ? CamelCardsType.Three : CamelCardsType.TwoPair,
                4 => CamelCardsType.Pair,
                5 => CamelCardsType.HighCard,
            };
            cards.Add(new() { Number = num, Rank = rank, Orig = str });
        }
        return cards;
    }
    static List<CamelCard> Parse2(ReadOnlySpan<char> span)
    {
        var cards = new List<CamelCard>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.IndexOf(' ');
            var str = line.Slice(0, index).ToString();
            var num = int.Parse(line.Slice(index + 1));
            var groups = str.GroupBy(x => x);
            var rank = groups.Count() switch
            {
                1 => CamelCardsType.Five,
                2 => groups.Any(x => x.Count() == 4) ? CamelCardsType.Four : CamelCardsType.FullHouse,
                3 => groups.Any(x => x.Count() == 3) ? CamelCardsType.Three : CamelCardsType.TwoPair,
                4 => CamelCardsType.Pair,
                5 => CamelCardsType.HighCard,
            };

            var jokers = str.AsSpan().Count('J');
            rank = jokers switch
            {
                1 => rank switch
                {
                    CamelCardsType.HighCard => CamelCardsType.Pair,
                    CamelCardsType.Pair => CamelCardsType.Three,
                    CamelCardsType.TwoPair => CamelCardsType.FullHouse,
                    CamelCardsType.Three => CamelCardsType.Four,
                    CamelCardsType.FullHouse => CamelCardsType.Four,
                    CamelCardsType.Four => CamelCardsType.Five,
                    _ => rank,
                },
                2 => rank switch
                {
                    CamelCardsType.Pair => CamelCardsType.Three,
                    CamelCardsType.TwoPair => CamelCardsType.Four,
                    CamelCardsType.FullHouse => CamelCardsType.Five,
                    _ => rank,
                },
                3 => rank switch
                {
                    CamelCardsType.Three => CamelCardsType.Four,
                    CamelCardsType.FullHouse => CamelCardsType.Five,
                    _ => rank,
                },
                4 => rank switch
                {
                    CamelCardsType.Four => CamelCardsType.Five,
                    _ => rank,
                },
                _ => rank,
            };
            cards.Add(new() { Number = num, Rank = rank, Orig = str });
        }
        return cards;
    }
    public int Part2(ReadOnlySpan<char> span) => CoreFnc(span, Parse2, SR2.ToArray());

    class CamelCard
    {
        public int Number;
        public CamelCardsType Rank;
        public string Orig;

        public override string ToString()
        {
            return Orig;
        }
    }

    enum CamelCardsType
    {
        Five,
        Four,
        FullHouse,
        Three,
        TwoPair,
        Pair,
        HighCard
    }
}
