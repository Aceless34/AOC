using FluentAssertions;

namespace AOC.Test
{
    public class Day5Tests : SolverTestBase<AOC._2025.Day5>
    {
        protected override string SampleInput => @"3-5
10-14
16-20
12-18

1
5
8
11
17
32";
  
        [Fact]
        public void Part1_SolveExample()
        {
            var result = Solver.SolvePart1(SampleInput);
            result.Should().Be(3);
        }

        [Fact]
        public void Part2_SolveExample()
        {
            var result = Solver.SolvePart2(SampleInput);
            result.Should().Be(14);
        }

    }
}
