using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Grid
{
    public static class GridUtils
    {
        public static Grid2D<char> FromStringArray(string[] lines)
        {
            int height = lines.Length;
            int width = lines[0].Length;
            var grid = new Grid2D<char>(width, height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    grid[x, y] = lines[y][x];
            return grid;
        }
    }
}
