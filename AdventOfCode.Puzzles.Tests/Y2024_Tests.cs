using AdventOfCode.Puzzles.Y2024;

namespace AdventOfCode.Puzzles.Tests;

[TestClass]
public class Y2024_Tests
{
    [TestMethod] public async Task Day01() => await Assert.TestAsync<Day01, int>(1765812, 20520794);
    [TestMethod] public async Task Day02() => await Assert.TestAsync<Day02, int>(390, 439);
    [TestMethod] public async Task Day03() => await Assert.TestAsync<Day03, int>(175015740, 112272912);
    [TestMethod] public async Task Day04() => await Assert.TestAsync<Day04, int>(2427, 1900);
    [TestMethod] public async Task Day05() => await Assert.TestAsync<Day05, int>(6267, 5184);
    [TestMethod] public async Task Day06() => await Assert.TestAsync<Day06, int>(5531, default);
    [TestMethod] public async Task Day07() => await Assert.TestAsync<Day07, long>(5702958180383, 92612386119138);
    [TestMethod] public async Task Day08() => await Assert.TestAsync<Day08, int>(394, 1277);
    [TestMethod] public async Task Day09() => await Assert.TestAsync<Day09, long>(6242766523059, 6272188244509);
    [TestMethod] public async Task Day10() => await Assert.TestAsync<Day10, int>(517, 1116);
    [TestMethod] public async Task Day11() => await Assert.TestAsync<Day11, long>(203228, 240884656550923);
    [TestMethod] public async Task Day12() => await Assert.TestAsync<Day12, int>(1387004, default);
    [TestMethod] public async Task Day13() => await Assert.TestAsync<Day13, long>(37686, 77204516023437);
    [TestMethod] public async Task Day14() => await Assert.TestAsync<Day14, int>(225810288, 6752);
    [TestMethod] public async Task Day15() => await Assert.TestAsync<Day15, int>(1383666, 1412866);
    //[TestMethod] public async Task Day16() => await Assert.TestAsync<Day16, int>(default, default);
    [TestMethod] public async Task Day17() => await Assert.TestAsync<Day17, string>("7,6,1,5,3,1,4,2,6", default);
    [TestMethod] public async Task Day18() => await Assert.TestAsync<Day18, string>("338", default);
    [TestMethod] public async Task Day19() => await Assert.TestAsync<Day19, int>(363, default);
    //[TestMethod] public async Task Day20() => await Assert.TestAsync<Day20, int>(default, default);
    //[TestMethod] public async Task Day21() => await Assert.TestAsync<Day21, int>(default, default);
    [TestMethod] public async Task Day22() => await Assert.TestAsync<Day22, long>(17163502021, 1938);
    //[TestMethod] public async Task Day23() => await Assert.TestAsync<Day23, int>(default, default);
    [TestMethod] public async Task Day24() => await Assert.TestAsync<Day24, string>("59619940979346", default);
    [TestMethod] public async Task Day25() => await Assert.TestAsync<Day25, int>(3249, default);
}
