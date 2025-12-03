using AdventOfCode.Puzzles.Y2025;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2025_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(1145, 6561);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, long>(8576933996, 25663320831);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, long>(17196, 171039099596062);
    //[TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(default, default);
    //[TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(default, default);
    //[TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(default, default);
    //[TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(default, default);
    //[TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(default, default);
    //[TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, int>(default, default);
    //[TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, int>(default, default);
    //[TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, int>(default, default);
    //[TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(default, default);
}
