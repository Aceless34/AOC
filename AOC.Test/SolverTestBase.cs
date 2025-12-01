using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace AOC.Test
{
    public abstract class SolverTestBase<TSolver> where TSolver : AOC.ISolver, new()
    {
        protected TSolver Solver { get; } = new TSolver();

        protected abstract string SampleInput { get; }

        [Fact]
        public void Solver_HasCorrectDay()
        {
            var typeName = typeof(TSolver).Name;
            var expectedDayString = uint.Parse(typeName.Replace("Day", ""));

            Solver.Day.Should().Be(expectedDayString);
        }
    }
}
