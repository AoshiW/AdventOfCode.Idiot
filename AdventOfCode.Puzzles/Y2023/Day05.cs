using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 5, "If You Give A Seed A Fertilizer")]
public class Day05 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span) => CoreFnc(span, (seeds, to) =>
    {
        foreach (var seed in seeds)
        {
            to.Add(new(seed, seed));
        }
    });

    public int Part2(ReadOnlySpan<char> span) => CoreFnc(span, (seeds, to) =>
    {
        for (int i = 0; i < seeds.Count; i += 2)
        {
            to.Add(new(seeds[i], seeds[i] + seeds[i + 1] - 1));
        }
    });

    static int CoreFnc(ReadOnlySpan<char> span, Action<List<long>, List<Range<long>>> convert)
    {
        var sections = ParseInput(span, out var seeds);
        var from = new List<Range<long>>();
        var to = new List<Range<long>>();
        convert(seeds, to);
        foreach (var item in sections)
        {
            from.Clear();
            from.AddRange(to);
            to.Clear();
            MapData(from, to, item);
        }
        return (int)to.MinBy(x => x.From).From;
    }

    static void MapData(List<Range<long>> from, List<Range<long>> to, ReadOnlySpan<(long, long, long)> map)
    {
        foreach (var line in map)
        {
            for (var i = 0; i < from.Count; i++)
            {
                if (from[i].Overlaps(new(line.Item2, line.Item2 + line.Item3 - 1), out var overlap))
                {
                    var diff = line.Item1 - line.Item2;
                    to.Add(new(overlap.From + diff, overlap.To + diff));
                    var split = Split(from[i], overlap);
                    switch (split.Min.HasValue, split.Max.HasValue)
                    {
                        case (true, true):
                            from[i] = split.Min.Value;
                            i++;
                            from.Insert(i, split.Max.Value);
                            break;
                        case (false, false):
                            from.RemoveAt(i);
                            i--;
                            break;
                        default:
                            from[i] = split.Min ?? split.Max ?? throw new System.Diagnostics.UnreachableException();
                            break;
                    }
                }

            }
        }
        to.AddRange(from);
    }

    static (Range<long>? Min, Range<long>? Max) Split(Range<long> source, Range<long> range)
    {
        if (source.From >= range.From && source.To <= range.To)
            return default;
        Range<long>? min = default, max = default;
        if (source.From < range.From)
        {
            min = new Range<long>(source.From, range.From - 1);
        }
        if (source.To > range.To)
        {
            max = new Range<long>(range.To + 1, source.To);
        }
        return (min, max);
    }

    static InputSectionEnumerator ParseInput(ReadOnlySpan<char> span, out List<long> seeds)
    {
        var enumerator = span.EnumerateLines(1);
        seeds = new List<long>();
        foreach (var seed in enumerator.Current.Slice(7).EnumerateSlices(" "))
        {
            seeds.Add(long.Parse(seed));
        }
        enumerator.MoveNext();
        return new InputSectionEnumerator(enumerator);
    }

    ref struct InputSectionEnumerator
    {
        SpanLineEnumerator _enumerator;
        readonly List<(long, long, long)> _numbers = new();

        public InputSectionEnumerator(SpanLineEnumerator enumerator)
        {
            _enumerator = enumerator;
        }

        public ReadOnlySpan<(long, long, long)> Current { get; private set; }

        public bool MoveNext()
        {
            if (!_enumerator.MoveNext())
                return false;
            _numbers.Clear();
            while (_enumerator.MoveNext() && !_enumerator.Current.IsEmpty)
            {
                ValueTuple<long, long, long> t = default;
                var tupleSpan = MemoryMarshal.CreateSpan(ref t.Item1, 3);
                int i = 0;
                foreach (var number in _enumerator.Current.EnumerateSlices(" "))
                {
                    tupleSpan[i++] = long.Parse(number);
                }
                _numbers.Add(t);
            }
            Current = _numbers.AsSpan();
            return true;
        }

        public InputSectionEnumerator GetEnumerator() => this;
    }
}
