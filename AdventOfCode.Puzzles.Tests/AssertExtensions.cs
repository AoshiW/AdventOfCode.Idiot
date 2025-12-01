using AdventOfCode.Client;
using AdventOfCode.Client.Caching;
using System.Reflection;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public static class AssertExtensions
{
    private static AdventOfCodeClient Client = null!;

    [AssemblyInitialize]
    public static void AssemblyInitialize(TestContext testContext)
    {
        var cacheDir = Environment.GetEnvironmentVariable("AOC_CACHE");
        ArgumentNullException.ThrowIfNull(cacheDir);

        var options = new AdventOfCodeClientOptions
        {
            Session = Environment.GetEnvironmentVariable("AOC_SESSION")!,
            ContactInformation = new("(User: Aoshi.W@gmail.com)"),
        };
        Client = new AdventOfCodeClient(options, new FileSystemCache(cacheDir!));
    }

    extension(Assert)
    {
        public static async Task TestAsync<TDay, TResult>(TResult part1, TResult part2) where TDay : class, IDay<TResult>, new()
        {
            var type = typeof(TDay);
            var attribute = type.GetCustomAttribute<AocPuzzleAttribute>();
            Assert.IsNotNull(attribute, null); // todo message

            var day = new TDay();
            var input = await Client.GetPuzzleInputAsStringAsync(attribute.Year, attribute.Day);
            var inputSpan = input.AsSpan().TrimEnd();

            Assert.AreEqual(part1, day.Part1(inputSpan), nameof(day.Part1));
            try
            {
                Assert.AreEqual(part2, day.Part2(inputSpan), nameof(day.Part2));
            }
            catch (NotImplementedException)
            {
                Assert.Inconclusive("Part2 isn't solve.");
            }
        }
    }
}
