using AOC.Grid;
using AOC.Visualizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2025
{
    internal class Day3 : ISolver
    {
        public uint Day => 3;

        public uint Year => 2025;

        public string Title => "Test Grid";

        public object SolvePart1(string input)
        {
            var grid = GridUtils.FromStringArray(input.Split(Environment.NewLine));

            var visualizer = new GridVisualizer<char>(grid, t => t , t => t == '.' ? ConsoleColor.Gray : ConsoleColor.Green)
            {
                OffsetX = 2,
                OffsetY = 1
            };

            visualizer.Init("Test Santa");

            visualizer.InfoPanel["Step"] = "0";
            visualizer.InfoPanel["Empty"] = "0";
            visualizer.InfoPanel["Filled"] = "0";
            visualizer.InfoPanel["Failed"] = "0";

            Random rnd = new();

            for (int i = 0; i < 1000000; i++)
            {
                int x = rnd.Next(grid.Width);
                int y = rnd.Next(grid.Height);

                if(grid[x, y] == '#')
                {
                    visualizer.InfoPanel["Failed"] = (int.Parse(visualizer.InfoPanel["Failed"]) + 1).ToString();
                }

                grid[x, y] = '#';

                visualizer.InfoPanel["Step"] = i.ToString();
                visualizer.InfoPanel["Empty"] = grid.FindAll(t => t == '.').Count().ToString();
                visualizer.InfoPanel["Filled"] = grid.FindAll(t => t == '#').Count().ToString();

                visualizer.Update();

                Thread.Sleep(50);

                if (grid.FindAll(t => t == '.').Count() == 0)
                    break;
            }

            return null;
        }

        public object SolvePart2(string input)
        {
            throw new NotImplementedException();
        }
    }
}
