using System.Buffers;

namespace AdventOfCode.Puzzles;

public class SearchValuesCache
{
    public static readonly SearchValues<string> DoubleNewLine = SearchValues.Create(["\n\n", "\r\n\r\n"], StringComparison.Ordinal);
}
