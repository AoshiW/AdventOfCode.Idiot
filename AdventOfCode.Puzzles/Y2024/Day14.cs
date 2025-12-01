using AdventOfCode.Puzzles.Numerics;

namespace AdventOfCode.Puzzles.Y2024;

[AocPuzzle(2024, 14, "Restroom Redoubt")]
public class Day14 : IDay<int>
{
    const int Wide = 101;
    const int Tall = 103;
    static readonly Vector2<int>[] Directions = [Vector2<int>.Right, Vector2<int>.Up, Vector2<int>.Down, Vector2<int>.Left];

    public int Part1(ReadOnlySpan<char> span)
    {
        const int midX = Wide / 2;
        const int midY = Tall / 2;
        Span<int> quadrants = stackalloc int[4];
        foreach (var line in span.EnumerateLines())
        {
            var robot = Robot.Parse(line);
            robot.Position += robot.Velocity * 100;

            robot.Position.X %= Wide;
            if (robot.Position.X < 0)
                robot.Position.X += Wide;

            robot.Position.Y %= Tall;
            if (robot.Position.Y < 0)
                robot.Position.Y += Tall;

            if (robot.Position.X != midX && robot.Position.Y != midY)
            {
                int index = 0;
                if (robot.Position.X < midX)
                    index |= 1;
                if (robot.Position.Y < midY)
                    index |= 2;
                quadrants[index]++;
            }
        }
        return quadrants[3] * quadrants[2] * quadrants[1] * quadrants[0];
    }

    public int Part2(ReadOnlySpan<char> span)
    {
        var robotsList = new List<Robot>();
        foreach (var line in span.EnumerateLines())
        {
            robotsList.Add(Robot.Parse(line));
        }

        var robots = CollectionsExtensions.AsSpan(robotsList);
        var robotPositionsCache = new HashSet<Vector2<int>>(robots.Length);
        var neighboursThreshold = robots.Length * 7 / 10;

        for (var i = 1; ; i++)
        {
            robotPositionsCache.Clear();
            foreach (ref var robot in robots)
            {
                robot.Position += robot.Velocity;
                if (robot.Position.X < 0)
                {
                    robot.Position.X += Wide;
                }
                else if (robot.Position.X >= Wide)
                {
                    robot.Position.X -= Wide;
                }
                if (robot.Position.Y < 0)
                {
                    robot.Position.Y += Tall;
                }
                else if (robot.Position.Y >= Tall)
                {
                    robot.Position.Y -= Tall;
                }
                robotPositionsCache.Add(robot.Position);
            }
            if (robots.Length == robotPositionsCache.Count && IsTree(robots, neighboursThreshold, robotPositionsCache))
                return i;
        }

        static bool IsTree(ReadOnlySpan<Robot> robots, int neighboursThreshold, HashSet<Vector2<int>> robotPositionsCache)
        {
            var neighbours = 0;
            foreach (var robot in robots)
            {
                foreach (var offset in Directions)
                {
                    if (robotPositionsCache.Contains(robot.Position + offset))
                    {
                        neighbours++;
                        break;
                    }
                }
            }
            return neighbours > neighboursThreshold;
        }
    }

    struct Robot
    {
        public Vector2<int> Position;
        public Vector2<int> Velocity;

        public static Robot Parse(ReadOnlySpan<char> span)
        {
            var robot = default(Robot);
            var space = span.IndexOf(' ');
            robot.Position = ParseVector2(span.Slice(2, space - 2));
            robot.Velocity = ParseVector2(span.Slice(space + 3));
            return robot;

            static Vector2<int> ParseVector2(ReadOnlySpan<char> span)
            {
                var index = span.IndexOf(',');
                return new(
                    int.Parse(span.Slice(0, index)),
                    int.Parse(span.Slice(index + 1))
                    );
            }
        }
    }
}
 