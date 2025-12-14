using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2025;

[AocPuzzle(2025, 11, "Reactor")]
public class Day11 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var dic = new Dictionary<string, List<string>>();
        foreach (var line in span.EnumerateLines())
        {
            var i = line.IndexOf(':');
            var key = line.Slice(0, i).ToString();

            var values = new List<string>();
            var subLine = line.Slice(i + 2);
            foreach (var value in subLine.Split(" "))
            {
                values.Add(subLine[value].ToString());
            }
            dic[key] = values;
        }

        return Get("you", dic);
    }

    int Get(string key, Dictionary<string, List<string>> dic)
    {
        int s = 0;
        foreach (var k in dic[key])
        {
            if (k is "out")
            {
                s++;
            }
            else
                s += Get(k, dic);
        }
        return s;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        //span = """
        //    svr: aaa bbb
        //    aaa: fft
        //    fft: ccc
        //    bbb: tty
        //    tty: ccc
        //    ccc: ddd eee
        //    ddd: hub
        //    hub: fff
        //    eee: dac
        //    dac: fff
        //    fff: ggg hhh
        //    ggg: out
        //    hhh: out
        //    """;
        var dic = new Dictionary<string, List<string>>();
        foreach (var line in span.EnumerateLines())
        {
            var i = line.IndexOf(':');
            var key = line.Slice(0, i).ToString();

            var values = new List<string>();
            var subLine = line.Slice(i + 2);
            foreach (var value in subLine.Split(" "))
            {
                values.Add(subLine[value].ToString());
            }
            dic[key] = values;
        }

        var hs = new List<string>();
        return 1 * Get("svr", "you", dic, hs)
            * Get("fft", "dac", dic, hs)
            * Get("dac", "out", dic, hs)
            * Get("svr", "fft", dic, hs)
            ;
    }

    int Get(string key, string end, Dictionary<string, List<string>> dic, List<string> h)
    {
        if (h.Contains(key))
            return 0;
        h.Add(key);
        var level = h.Count;
        int s = 0;
        if (!dic.TryGetValue(key, out var values))
            return 0;
        foreach (var k in values)
        {
            if (k == end)
            {
                    s++;
            }
            else
            {
                CollectionsMarshal.SetCount(h, level);
                s += Get(k, end, dic, h);
            }
        }
        return s;
    }
}
