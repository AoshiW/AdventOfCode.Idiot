using AdventOfCode.Puzzles.Y2021;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2021_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(1564, 1611);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(1692075, 1749524700);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(4174964, 4474944);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(23177, 6804);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(5197, 18605);
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, ulong>(352872, 1604361182149);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, int>(344605, 93699985);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(330, 1010472);
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, int>(480, 1045660);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, long>(462693, 3094671161);
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, int>(1681, 276);
    //[TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(default, default);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, string>("602", "CAFJHZCK");
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, ulong>(3058, 3447389044530);
    //[TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(default, default);
    [TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, ulong>(893, 4358595186090);
    [TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, int>(10878, 4716);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, int>(4347, 4721);
    //[TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(default, default);
    [TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, int>(5097, 17987);
    [TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, long>(412344, 214924284932572);
    [TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, long>(596598, default);
    //[TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(11536, default);
    //[TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, int>(default, default);
    [TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, int>(406, default);
}
