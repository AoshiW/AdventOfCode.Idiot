namespace AdventOfCode.Puzzles.Y2021;

public partial class Day11
{
    class MapItem
    {
        public MapItem(char cc) => CharNum = cc;
        public bool Flashed;
        public char CharNum;
    }
}
