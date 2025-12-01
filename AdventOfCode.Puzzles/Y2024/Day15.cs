using AdventOfCode.Puzzles.Numerics;
using System.Buffers;
using System.Diagnostics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024,15, "Warehouse Woes")]
public class Day15 : IDay<int>
{
    static SearchValues<string> DoubleNewLine = SearchValues.Create(["\n\n", "\r\n\r\n"], StringComparison.Ordinal);
    
    public int Part1(ReadOnlySpan<char> span)
    {
        var index = span.IndexOfAny(DoubleNewLine);

        var map = new Input2D(span.Slice(0, index));
        char[][] mapAll = new char[map.Rows][];
        for(int row =0;row < map.Rows; row++)
        {
            mapAll[row] = map.GetRow(row).ToArray();
        }
        var position = map.GetPointFromRawIndex(span.IndexOf('@'));
        span = span.Slice(index).TrimStart();
        foreach (var direction in span)
        {
            if (direction is '\n')
                continue;
            var move = direction switch
            {
                '^' => Vector2<int>.Up,
                'v' => Vector2<int>.Down,
                '<' => Vector2<int>.Left,
                '>' => Vector2<int>.Right,
            };
            var newPos = position + move;
            switch (mapAll[newPos.Y][newPos.X])
            {
                case '#': continue;
                case '.':
                    mapAll[position.Y][position.X] = '.';
                    mapAll[newPos.Y][newPos.X] = '@';
                    position += move;
                    break;
                case 'O':
                    if(CanMoveNext(mapAll, newPos, move))
                    {
                        goto case '.';
                    }
                    break;
            }
        }
        var sum = 0;
        for (var row = 0; row < mapAll.Length; row++)
        {
            for (var col = 0; col < mapAll[row].Length; col++)
            {
                if (mapAll[row][col] == 'O')
                    sum += row * 100 + col;
            }
        }
        return sum;

        static bool CanMoveNext(char[][] map, Vector2<int> pos, Vector2<int> direction)
        {
            var newPos = pos + direction;
            switch (map[newPos.Y][newPos.X])
            {
                case '.':
                    map[newPos.Y][newPos.X] = 'O';
                    return true;
                case 'O':
                    if (!CanMoveNext(map, pos + direction, direction))
                        return false;
                    goto case '.';
                case '#':
                    return false;
                default: throw new UnreachableException();
            }
        }
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var index = span.IndexOfAny(DoubleNewLine);

        var map = new Input2D(span.Slice(0, index));
        char[][] mapAll = new char[map.Rows][];
        for (int row = 0; row < map.Rows; row++)
        {
            var array = new char[map.Rows * 2];
            mapAll[row] = array;
            var index2 = 0;
            foreach (var item in map.GetRow(row))
            {
                var temp = item switch
                {
                    '.' => "..",
                    '#' => "##",
                    '@' => "@.",
                    'O' => "[]",
                };
                array[index2++] = temp[0];
                array[index2++] = temp[1];
            }
        }
        var position = map.GetPointFromRawIndex(span.IndexOf('@'));
        position.X = position.X * 2;
        span = span.Slice(index).TrimStart();
        foreach (var direction in span)
        {
            if (direction is '\n')
                continue;
            var move = direction switch
            {
                '^' => (Vector2<int>.Up, false),
                'v' => (Vector2<int>.Down, false),
                '<' => (Vector2<int>.Left, true),
                '>' => (Vector2<int>.Right, true),
            };
            var newPos = position + move.Item1;
            switch (mapAll[newPos.Y][newPos.X])
            {
                case '#': continue;
                case '.':
                    mapAll[position.Y][position.X] = '.';
                    mapAll[newPos.Y][newPos.X] = '@';
                    position += move.Item1;
                    break;
                case '[':
                    if (move.Item2)
                    {
                        if(CanMoveNextLR(mapAll, newPos, move.Item1))
                        goto case '.';
                    }
                    else if(CanMoveNextUD(mapAll, newPos, move.Item1, false) && CanMoveNextUD(mapAll, newPos + Vector2<int>.Right, move.Item1, false))
                    {
                        _ = CanMoveNextUD(mapAll, newPos, move.Item1, true);
                        _ = CanMoveNextUD(mapAll, newPos + Vector2<int>.Right, move.Item1, true);
                        goto case '.';
                    }
                    break;
                case ']':
                    if (move.Item2)
                    {
                        if(CanMoveNextLR(mapAll, newPos, move.Item1))
                        goto case '.';
                    }
                    else if (CanMoveNextUD(mapAll, newPos, move.Item1, false) && CanMoveNextUD(mapAll, newPos + Vector2<int>.Left, move.Item1, false))
                    {
                        _ = CanMoveNextUD(mapAll, newPos, move.Item1, true);
                        _ = CanMoveNextUD(mapAll, newPos + Vector2<int>.Left, move.Item1, true);
                        goto case '.';
                    }
                    break;
            }
        }
        var sum = 0;
        for (var row = 0; row < mapAll.Length; row++)
        {
            for (var col = 0; col < mapAll[row].Length; col++)
            {
                if (mapAll[row][col] == '[')
                    sum += row * 100 + col;
            }
        }
        return sum;

        static bool CanMoveNextLR(char[][] map, Vector2<int> pos, Vector2<int> direction)
        {
            var newPos = pos + direction;
            switch (map[newPos.Y][newPos.X])
            {
                case '.':
                    map[newPos.Y][newPos.X] = map[pos.Y][pos.X];
                    return true;
                case '[' or ']':
                    if (!CanMoveNextLR(map, newPos, direction))
                        return false;
                    goto case '.';
                case '#':
                    return false;
                default: throw new UnreachableException();
            }
        }
        static bool CanMoveNextUD(char[][] map, Vector2<int> pos, Vector2<int> direction, bool print)
        {
            var newPos = pos + direction;
            switch (map[newPos.Y][newPos.X])
            {
                case '.':
                    if (print) {
                        map[newPos.Y][newPos.X] = map[pos.Y][pos.X]; 
                        map[pos.Y][pos.X] = '.';
                    }
                    return true;
                case '[':
                    if (!CanMoveNextUD(map, newPos, direction, print) ||
                        !CanMoveNextUD(map, newPos + Vector2<int>.Right, direction, print) )
                            return false;
                    if (print)
                    {
                        map[newPos.Y][newPos.X] = map[pos.Y][pos.X];
                        map[pos.Y][pos.X] = '.';
                    }
                    return true;
                case ']':
                    if (!CanMoveNextUD(map, newPos, direction, print) || 
                        !CanMoveNextUD(map, newPos + Vector2<int>.Left, direction, print) )
                            return false;
                    if (print)
                    {
                        map[newPos.Y][newPos.X] = map[pos.Y][pos.X];
                        map[pos.Y][pos.X] = '.';
                    }
                    return true;
                case '#':
                    return false;
                default: 
                    throw new UnreachableException();
            }
        }
    }
}
