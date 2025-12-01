namespace AdventOfCode.Puzzles.Y2016;

public partial class Day10
{
    sealed class Bot
    {
        public int Output { get; set; } = 0;
        public List<int> Chip { get; set; } = new(2);
        public Bot() { }
        public Bot(int i)
        {
            Chip.Add(i);
        }
    }
}
