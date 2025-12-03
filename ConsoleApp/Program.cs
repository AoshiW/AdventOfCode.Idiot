using AdventOfCode.Client;
using AdventOfCode.Client.Caching;
using AdventOfCode.Puzzles;
using AdventOfCode.Puzzles.Y2025;

Console.WriteLine("Hello, AdventOfCode!");

var options = new AdventOfCodeClientOptions
{
    Session = Environment.GetEnvironmentVariable("AOC_SESSION")!,
    ContactInformation = new("(User: Aoshi.W@gmail.com)"),
};
Client = new AdventOfCodeClient(options, new FileSystemCache(Environment.GetEnvironmentVariable("AOC_CACHE")!));

// Y22/20, Y17/10

Run<Day03>();

void Run<T>() where T : IDay, new()
{
    RunPuzzle<T>();
    RunBechmark<T>();
}
