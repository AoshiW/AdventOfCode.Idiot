using System.Diagnostics;
using System.Text;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 17, "Chronospatial Computer")]
public class Day17 : IDay<string>
{
    public string Part1(ReadOnlySpan<char> span)
    {
        int regA = 0;
        int regB = 0;
        int regC = 0;
        Span<OpCode> instructions = default;
        foreach (var line in span.EnumerateLines())
        {
            if (line.IsEmpty)
                continue;
            switch(line[9])
            {
                case 'A':
                    regA = int.Parse(line.Slice(line.IndexOf(':') + 1));
                    break;
                case 'B':
                    regB = int.Parse(line.Slice(line.IndexOf(':') + 1));
                    break;
                case 'C':
                    regC = int.Parse(line.Slice(line.IndexOf(':') + 1));
                    break;
                default:
                    var count = line.Count(',') + 1;
                    instructions = new OpCode[count];
                    var index = 0;
                    var l = line.Slice(9);
                    foreach(var inst in l.Split(','))
                    {
                        instructions[index++] = (OpCode)(l[inst][0] - '0');
                    }
                    break;
            }
        }
        
        var sb = new StringBuilder();
        for (var pointer = 0; pointer < instructions.Length; pointer += 2)
        {
            var instruction = instructions[pointer];
            var operand = (int)instructions[pointer + 1];
            switch (instruction)
            {
                case OpCode.adv:
                    regA = regA / (1 << Combo(operand));
                    break;
                case OpCode.bxl:
                    regB ^= operand;
                    break;
                case OpCode.bst:
                    regB = Combo(operand) & 0b111;
                    break;
                case OpCode.jnz:
                    if (regA is not 0)
                        pointer = operand - 2;
                    break;
                case OpCode.bxc:
                    regB ^= regC;
                    break;
                case OpCode.Out:
                    sb.Append(Combo(operand) & 0b111).Append(',');
                    break;
                case OpCode.bvd:
                    regB = regA / (1 << Combo(operand));
                    break;
                case OpCode.cdv:
                    regC = regA / (1 << Combo(operand));
                    break;
                default:
                    throw new UnreachableException();
            }
        }
        sb.Length--;
        return sb.ToString();

        int Combo(int val)
        {
            return val switch
            {
                0 or 1 or 2 or 3 => val,
                4 => regA,
                5 => regB,
                6 => regC,
                _ => throw new UnreachableException()
            };
        }
    }

    public string Part2(ReadOnlySpan<char> span)
    {
        throw new NotImplementedException();
    }
}

file enum OpCode
{
    adv,
    bxl,
    bst,
    jnz,
    bxc,
    Out,
    bvd,
    cdv
}