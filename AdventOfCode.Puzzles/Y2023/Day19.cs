using System.Runtime.InteropServices;

namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 19, "Aplenty")]
public class Day19 : IDay<long>
{
    public long Part1(ReadOnlySpan<char> span)
    {
        var data = ParseInput(span);
        var sum = 0;
        foreach (var item in data.Item2)
        {
            var workflow = "in";
            while (workflow is not "A" and not "R")
            {
                workflow = NextWorkflow(item, data.Item1[workflow]);
            }
            if (workflow is "A")
                sum += item.X + item.M + item.A + item.S;
        }
        return sum;
    }

    static string NextWorkflow((int X, int M, int A, int S) item, List<(char Part, char, int, string)> rules)
    {
        foreach (var rule in rules)
        {
            if (rule.Part == default)
                return rule.Item4;

            var index = indxs.IndexOf(rule.Part);
            var tupleSpan = MemoryMarshal.CreateReadOnlySpan(ref item.X, 4);

            if (rule.Item2 == '>' && tupleSpan[index] > rule.Item3)
                return rule.Item4;
            if (rule.Item2 == '<' && tupleSpan[index] < rule.Item3)
                return rule.Item4;
        }
        throw new FormatException();
    }

    public long Part2(ReadOnlySpan<char> span)
    {
        var workflows = ParseInput(span).Item1;
        var data = new MyRange[]
        {
            new(1,4000),
            new(1,4000),
            new(1,4000),
            new(1,4000),
        };

        return FindCombunation(data, "in", workflows);
    }
    static ReadOnlySpan<char> indxs => ['x', 'm', 'a', 's'];
    static long FindCombunation(MyRange[] data, string workflow, Dictionary<string, List<(char Part, char, int, string)>> workflows)
    {
        if (workflow == "R")
            return 0;
        if (workflow == "A")
            return data.Aggregate(1L, (p, c) => p * c.Lenght);
        var sum = 0L;
        foreach (var rule in workflows[workflow])
        {
            if (rule.Part == default)
            {
                sum += FindCombunation(data, rule.Item4, workflows);
                continue;
            }

            var temp = data.ToArray();
            var index = indxs.IndexOf(rule.Part);
            if (rule.Item2 == '>')
            {
                temp[index] = data[index].Clamp(rule.Item3 + 1, 4000);
                data[index] = data[index].Clamp(1, rule.Item3);
            }
            else if (rule.Item2 == '<')
            {
                temp[index] = data[index].Clamp(1, rule.Item3 - 1);
                data[index] = data[index].Clamp(rule.Item3, 4000);
            }
            else
                throw new FormatException();
            sum += FindCombunation(temp, rule.Item4, workflows);
        }
        return sum;
    }

    record struct MyRange(int From, int To)
    {
        public int Lenght => To - From + 1;

        public MyRange Clamp(int min, int max)
        {
            min = int.Max(min, From);
            max = int.Min(max, To);
            return new(min, max);
        }
    }

    static (Dictionary<string, List<(char, char, int, string)>>, List<(int X, int M, int A, int S)>) ParseInput(ReadOnlySpan<char> span)
    {
        var enumerator = span.EnumerateLines();
        var workflows = new Dictionary<string, List<(char, char, int, string)>>();
        while (enumerator.MoveNext() && !enumerator.Current.IsEmpty)
        {
            var line = enumerator.Current;
            var index = line.IndexOf('{');
            var workflowKey = line.Slice(0, index).ToString();
            ref var item = ref CollectionsMarshal.GetValueRefOrAddDefault(workflows, workflowKey, out var exists);
            if (!exists)
                item = new();
            line = line.Slice(index + 1).TrimEnd('}');
            foreach (var slice in line.EnumerateSlices(","))
            {
                (char, char, int, string) listItem = default;
                index = slice.IndexOf(':');
                if (index == -1)
                {
                    listItem.Item4 = slice.ToString();
                }
                else
                {
                    listItem = (slice[0], slice[1], 0, null!);
                    listItem.Item3 = int.Parse(slice.Slice(2, index - 2));
                    listItem.Item4 = slice.Slice(index + 1).ToString();
                }
                item!.Add(listItem);
            }
        }
        var list = new List<(int X, int M, int A, int S)>();
        Span<Range> split = stackalloc Range[4];
        while (enumerator.MoveNext())
        {
            var slice = enumerator.Current.Trim("{}");
            slice.Split(split, ',');
            var tuple = (0, 0, 0, 0);
            var tupleSpan = MemoryMarshal.CreateSpan(ref tuple.Item1, 4);
            for (int i = 0; i < 4; i++)
            {
                var str = slice[split[i]];
                tupleSpan[i] = int.Parse(str.Slice(2));
            }
            list.Add(tuple);
        }
        return (workflows, list);
    }
}
