namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 8, "Haunted Wasteland")]
public class Day08 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines(1);
        var instructions = enumerator.Current;
        enumerator.MoveNext();
        var map = new Dictionary<string, (string, string)>();
        foreach (var line in enumerator)
        {
            map.Add(line.Slice(0, 3).ToString(), (
                line.Slice(7, 3).ToString(),
                line.Slice(12, 3).ToString()));
        }
        long steps = 0;
        var current = "AAA";
        while (true)
        {
            foreach (var item in instructions)
            {
                if (current == "ZZZ")
                    return steps;
                steps++;
                var next = map[current];
                current = item == 'L' ? next.Item1 : next.Item2;
            }
        }
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines(1);
        var instructions = enumerator.Current;
        enumerator.MoveNext();
        var map = new Dictionary<string, (string, string)>();
        var nodes = new List<string>();
        foreach (var line in enumerator)
        {
            var key = line.Slice(0, 3).ToString();
            if (key[2] == 'A')
                nodes.Add(key);
            map.Add(key, (
                line.Slice(7, 3).ToString(),
                line.Slice(12, 3).ToString()));
        }
        long lcm = Node(instructions, nodes[0], map);
        for (int i = 1; i < nodes.Count; i++)
        {
            lcm = long.LeastCommonMultiple(lcm, Node(instructions, nodes[i], map));
        }
        return lcm;

        static int Node(ReadOnlySpan<char> span, string node, Dictionary<string, (string, string)> map)
        {
            int steps = 0;
            while (true)
            {
                foreach (var item in span)
                {
                    if (node[2] == 'Z')
                        return steps;
                    steps++;
                    var next = map[node];
                    node = item == 'L' ? next.Item1 : next.Item2;
                }
            }
        }
    }
}
