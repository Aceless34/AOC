using FluentAssertions;

namespace AOC.Test
{
    public class Day1Tests : SolverTestBase<AOC._2025.Day1>
    {
        protected override string SampleInput => @"L68
L30
R48
L5
R60
L55
L1
L99
R14
L82";
  
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
            result.Should().Be(6);
        }

    }
}
