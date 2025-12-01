using System.Runtime.InteropServices;
using System.Text;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 5,"")]
public class Day05_2 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var sections = ParseInput(span, out var seeds);
        var from = new List<MyRange>();
        var to = new List<MyRange>();
        for (int i = 0; i < seeds.Count; i++)
        {
            to.Add(new(seeds[i], 1));
        }
        foreach (var item in sections)
        {
            from.Clear();
            from.AddRange(to);
            to.Clear();
            MapData(from, to, item);
        }
        return to.Select(x=>x.from).Min();
    }

    //static void MapData(List<long> from, List<long> to, ReadOnlySpan<(long, long, long)> map)
    //{
    //    foreach (var seed in from)
    //    {
    //        bool isAdded = false;
    //        foreach (var line in map)
    //        {
    //            if (seed >= line.Item2 && seed < line.Item2 + line.Item3)
    //            {
    //                isAdded = true;
    //                to.Add(seed + line.Item1 - line.Item2);
    //            }
    //        }
    //        if (!isAdded)
    //            to.Add(seed);
    //    }
    //}

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

    public long Part2(ReadOnlySpan<char> span)
    {
        var sections = ParseInput(span, out var seeds);
        var from = new List<MyRange>();
        var to = new List<MyRange>();
        for (int i = 0; i < seeds.Count; i += 2)
        {
            to.Add(new(seeds[i], seeds[i + 1]));
        }
        foreach (var item in sections)
        {
            from.Clear();
            from.AddRange(to);
            to.Clear();
            MapData(from, to, item);
        }
        return to.MinBy(x => x.from)!.from;
    }

    static void MapData(List<MyRange> from, List<MyRange> to, ReadOnlySpan<(long, long, long)> map)
    {
        foreach (var line in map)
        {
            for (var i = 0; i < from.Count; i++)
            {
                if (from[i].IsOverlap(new(line.Item2, line.Item3), out var overlap))
                {
                    to.Add(new MyRange(overlap.from + line.Item1 - line.Item2, overlap.length));
                    var split = from[i].Split(overlap);
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

    //todo rewrite
    readonly record struct MyRange(long from, long length)
    {
        public long To => from + length;

        public bool IsOverlap(MyRange range, out MyRange overlap)
        {
            overlap = default!;
            (var min, var max) = from < range.from ? (this, range) : (range, this);
            if (min.To < max.from)
                return false;
            var from2 = long.Max(min.from, max.from);
            var to2 = long.Min(min.To, max.To);
            overlap = new(from2, to2 - from2);
            return true;
        }

        public (MyRange? Min, MyRange? Max) Split(MyRange range)
        {
            if (from >= range.from && To <= range.To)
                return default;
            if (from < range.from && To > range.To)
            {
                var min = new MyRange(from, range.from - from);
                var max = new MyRange(range.To, To - range.To);
                return (min, max);
            }
            else
            {
                var min = new MyRange(range.from, range.length - length);
                var max = new MyRange(from, length - range.length);
                return min.length < 0
                    ? (default, max)
                    : (min, default);
            }
        }
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
