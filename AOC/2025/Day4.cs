using AOC.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2025
{
    public class Day4 : ISolver
    {
        public uint Day => 4;

        public uint Year => 2025;

        public string Title => "Printing Department";

        public object SolvePart1(string input)
        {
            var grid = GridUtils.FromStringArray(input.Split(Environment.NewLine));

            int movablePaperrolls = 0;
            movablePaperrolls = grid.FindAllNodes(n => n.Value == '@' && grid.GetAllNeighbours((int)n.Position.X, (int)n.Position.Y).ToList().Where(nei => nei.Value == '@').Count() < 4 ).Count();

            return movablePaperrolls;
        }

        public object SolvePart2(string input)
        {
            var grid = GridUtils.FromStringArray(input.Split(Environment.NewLine));

            int movablePaperrolls = 0;
            List<Grid2DNode<char>> toRemove = new List<Grid2DNode<char>>();

            toRemove = [.. grid.FindAllNodes(n => n.Value == '@' && grid.GetAllNeighbours((int)n.Position.X, (int)n.Position.Y).ToList().Where(nei => nei.Value == '@').Count() < 4)];
            while (toRemove.Count != 0)
            {
                toRemove.Clear();

                toRemove = [.. grid.FindAllNodes(n => n.Value == '@' && grid.GetAllNeighbours((int)n.Position.X, (int)n.Position.Y).ToList().Where(nei => nei.Value == '@').Count() < 4)];

                foreach (var node in toRemove)
                {
                    grid[(int)node.Position.X, (int)node.Position.Y] = '.';
                    movablePaperrolls++;
                }

            }
            return movablePaperrolls;
        }
    }
}
