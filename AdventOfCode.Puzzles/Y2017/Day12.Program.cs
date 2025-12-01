namespace AdventOfCode.Puzzles.Y2017;

public partial class Day12
{
    class Program
    {
        public int Id;
        public List<int> Pipes;
        public override string ToString() => $"{Id} <-> {string.Join(", ", Pipes)}";
    }
}
