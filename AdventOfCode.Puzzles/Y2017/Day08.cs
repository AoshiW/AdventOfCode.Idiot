namespace AdventOfCode.Puzzles.Y2017;

[AocPuzzle(2017, 8, "I Heard You Like Registers")]
public class Day08 : IDay<int>
{
    static readonly Func<int, int, bool>[] Operations = new Func<int, int, bool>[]{
        (x,y) => x > y,
        (x,y) => x < y,
        (x,y) => x >= y,
        (x,y) => x <= y,
        (x,y) => x == y,
        (x,y) => x != y,
    };

    public int Part1(ReadOnlySpan<char> span)
    {
        var registers = new Dictionary<string, int>();
        foreach (var item in span.EnumerateLines())
        {
            var (reg, isInc, value, reg1, ifOperation, val2) = ParseLine(item);
            if (Operations[ifOperation](registers.GetValueOrDefault(reg1),val2))
            {
                registers[reg] = registers.GetValueOrDefault(reg) + (isInc ? value : -value);
            }
        }
        return registers.Max(x => x.Value);
    }

    static (string, bool, int, string, int, int) ParseLine(ReadOnlySpan<char> span)
    {
        int state = 0, value = 0, ifOperation = 0, val2 = 0;
        string reg = null!, reg1 = null!;
        bool isInc = false;
        foreach (var item in span.EnumerateSlices(" "))
        {
            switch (state)
            {
                case 0:
                    reg = item.ToString();
                    break;
                case 1:
                    isInc = item[0] == 'i';
                    break;
                case 2:
                    value = int.Parse(item);
                    break;
                case 4:
                    reg1 = item.ToString();
                    break;
                case 5:
                    ifOperation = (item[0], item.Length) switch
                    {
                        ('>', 1) => 0,
                        ('<', 1) => 1,
                        ('>', _) => 2,
                        ('<', _) => 3,
                        ('=', _) => 4,
                        _ => 5
                    };
                    break;
                case 6:
                    val2 = int.Parse(item);
                    break;
                default: break;
            }
            state++;
        }
        return (reg, isInc, value, reg1, ifOperation,val2);
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var registers = new Dictionary<string, int>();
        var max = int.MinValue;
        foreach (var item in span.EnumerateLines())
        {
            var (reg, isInc, value, reg1, ifOperation, val2) = ParseLine(item);
            if (Operations[ifOperation](registers.GetValueOrDefault(reg1), val2))
            {
                var newValue = registers.GetValueOrDefault(reg) + (isInc ? value : -value);
                registers[reg] = newValue;
                if (newValue > max)
                {
                    max = newValue;
                }
            }
        }
        return max;
    }
}
