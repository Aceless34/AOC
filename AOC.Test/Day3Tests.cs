using FluentAssertions;

namespace AOC.Test
{
    public class Day3Tests : SolverTestBase<AOC._2025.Day3>
    {
        protected override string SampleInput => @"987654321111111
811111111111119
234234234234278
818181911112111";
  
        [Fact]
        public void Part1_SolveExample()
        {
            var result = Solver.SolvePart1(SampleInput);
            result.Should().Be(357);
        }

        [Fact]
        public void Part2_SolveExample()
        {
            var result = Solver.SolvePart2(SampleInput);
            result.Should().Be(3121910778619);
        }

    }
}
