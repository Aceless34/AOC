using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2025
{
    public class Day5 : ISolver
    {
        public uint Day => 5;

        public uint Year => 2025;

        public string Title => "Cafeteria";

        public object SolvePart1(string input)
        {
            string rangesString = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0];
            string ingredientIdsString = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[1];

            List<(long min, long max)> ranges = rangesString.Split(Environment.NewLine).Select(rs => (long.Parse(rs.Split("-")[0]), long.Parse(rs.Split("-")[1]))).ToList();
            List<long> ingredientIds = ingredientIdsString.Split("\n").Select(id => long.Parse(id)).ToList();

            return ingredientIds.Where(id => ranges.Any(r => id >= r.min && id <= r.max)).Count();

        }

        public object SolvePart2(string input)
        {
            string rangesString = input.Split(new string[] { Environment.NewLine + Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)[0];
            List<(long min, long max)> ranges = rangesString.Split(Environment.NewLine).Select(rs => (min: long.Parse(rs.Split("-")[0]), max: long.Parse(rs.Split("-")[1]))).OrderBy(r => r.min).ToList();

            var merged = new List<(long min, long max)>();

            foreach (var r in ranges)
            {
                if (merged.Count == 0 || r.min > merged[^1].max + 1)
                    merged.Add(r);
                else
                    merged[^1] = (merged[^1].min, Math.Max(merged[^1].max, r.max));
            }

            long total = merged.Sum(r => r.max + 1 - r.min);
            return total;
        }
    }
}
