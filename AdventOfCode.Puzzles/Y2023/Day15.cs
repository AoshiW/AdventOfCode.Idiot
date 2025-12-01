namespace AdventOfCode.Puzzles.Y2023;

[AocPuzzle(2023, 15, "Lens Library")]
public class Day15 : IDay<int>
{
    public int Part1(ReadOnlySpan<char> span)
    {
        var sum = 0;
        foreach (var item in span.EnumerateSlices(","))
        {
            sum += Hash(item);
        }
        return sum;
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var boxes = new List<(string Label, int FocalLength)>[256];
        foreach (ref var item in boxes.AsSpan())
        {
            item = new();
        }
        foreach (var item in span.EnumerateSlices(","))
        {
            var index = item.LastIndexOfAny("=-");
            var label = item.Slice(0, index);
            var box = boxes[Hash(label)];
            var boxIndex = FindIndex(box, label, out var labelStr);
            if (item[index] == '=')
            {
                var focalLength = item[^1] - '0';
                if (boxIndex == -1)
                {
                    box.Add((label.ToString(), focalLength));
                }
                else
                {
                    box[boxIndex] = (labelStr!, focalLength);
                }
            }
            else if (boxIndex != -1)
            {
                box.RemoveAt(boxIndex);
            }
        }
        var focusingPower = 0;
        var boxNumber = 0;
        foreach (var box in boxes.AsSpan())
        {
            boxNumber++;
            var slotNumber = 0;
            foreach (var item in box.AsSpan())
            {
                slotNumber++;
                focusingPower += boxNumber * slotNumber * item.FocalLength;
            }
        }
        return focusingPower;
    }

    static int FindIndex(List<(string Label, int FocalLength)> list, ReadOnlySpan<char> label, out string? labelStr)
    {
        for (int i = 0; i < list.Count; i++)
        {
            var item = list[i];
            if (label.SequenceEqual(item.Label))
            {
                labelStr = item.Label;
                return i;
            }
        }
        labelStr = default;
        return -1;
    }

    static int Hash(ReadOnlySpan<char> span)
    {
        byte value = 0;
        foreach (char c in span)
        {
            unchecked
            {
                value = (byte)((value + c) * 17);
            }
        }
        return value;
    }
}
