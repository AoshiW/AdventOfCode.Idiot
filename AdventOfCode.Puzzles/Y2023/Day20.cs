namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 20, "Pulse Propagation")]
public class Day20 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var dict = ParseInput(span);
        var low = 0;
        var high = 0;
        var outQ = new Queue<(string, bool, string)>();
        var inQ = new Queue<(string, bool, string)>();
        for (var i = 0; i < 1000; i++)
        {
            outQ.Enqueue(("broadcaster", false, ""));
            while (outQ.Count > 0)
            {
                (outQ, inQ) = (inQ, outQ);
                while (inQ.TryDequeue(out var pulse))
                {
                    if (dict.TryGetValue(pulse.Item1, out var module))
                        switch (module.Operation)
                        {
                            case '%':
                                if (!pulse.Item2)
                                {
                                    module.IsOn = !module.IsOn;
                                    ref var increment = ref module.IsOn ? ref high : ref low;
                                    foreach (var item in module.Modules)
                                    {
                                        increment++;
                                        outQ.Enqueue((item, module.IsOn, pulse.Item1));
                                    }
                                }
                                break;
                            case '&':
                                module.Memory![pulse.Item3] = pulse.Item2;
                                var next = !module.Memory!.All(x => x.Value);
                                foreach (var item in module.Modules)
                                {
                                    (next ? ref high : ref low)++;
                                    outQ.Enqueue((item, next, pulse.Item1));
                                }
                                break;
                            default:
                                low++;
                                foreach (var item in module.Modules)
                                {
                                    low++;
                                    outQ.Enqueue((item, false, "broadcaster"));
                                }
                                break;
                        }
                }
            }
        }
        return low * high;
    }

    static Dictionary<string, Module> ParseInput(ReadOnlySpan<char> span)
    {
        var dict = new Dictionary<string, Module>();
        foreach (var line in span.EnumerateLines())
        {
            var index = line.IndexOf(' ');
            var key = line.Slice(0, index).TrimStart("%&").ToString();
            var l = new List<string>();
            foreach (var item in line.Slice(index + 4).EnumerateSlices(","))
            {
                l.Add(item.Trim().ToString());
            }
            dict.Add(key, new(line[0], l)
            {
                Memory = line[0] == '&' ? new() : default
            });
        }

        foreach (var memoryModule in dict.Where(x => x.Value.Memory is not null))
        {
            foreach (var module in dict)
            {
                if (module.Value.Modules.Contains(memoryModule.Key))
                    memoryModule.Value.Memory![module.Key] = false;
            }
        }
        return dict;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }

    record Module(char Operation, List<string> Modules)
    {
        public bool IsOn { get; set; }
        public Dictionary<string, bool>? Memory { get; set; }
    }
}
