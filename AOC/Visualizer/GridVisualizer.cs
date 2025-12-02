using AOC.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Visualizer
{
    public class GridVisualizer<T>
    {
        private readonly Grid2D<T> _grid;
        private readonly Func<T, char> _charMapper;
        private readonly Func<T, ConsoleColor> _colorMapper;

        private int _gridStartX;
        private int _gridStartY;
        private string _title = "";

        public Dictionary<string, string> InfoPanel { get; } = new();

        public int OffsetX { get; set; } = 0;
        public int OffsetY { get; set; } = 0;

        public GridVisualizer(
            Grid2D<T> grid,
            Func<T, char> charMapper,
            Func<T, ConsoleColor>? colorMapper = null)
        {
            _grid = grid;
            _charMapper = charMapper;
            _colorMapper = colorMapper ?? (_ => ConsoleColor.White);
        }

        public void Init(string title = "")
        {
            Console.CursorVisible = false;
            Console.Clear();

            _title = "┌─────" + title + "─────┐";

            // Titel schreiben
            if (!string.IsNullOrWhiteSpace(title))
            {
                Console.SetCursorPosition(
                    Math.Max((Console.WindowWidth - title.Length) / 2, 0),
                    0);
                Console.Write(title);
            }

            // Grid zentrieren
            _gridStartX = (Console.WindowWidth - _grid.Width) / 2;
            _gridStartY = (Console.WindowHeight - _grid.Height) / 2;

            _gridStartX = Math.Max(_gridStartX, 0);
            _gridStartY = Math.Max(_gridStartY, 1);  // Zeile 1 wegen Titel
        }

        public void Update()
        {
            Console.ResetColor();

            // Grid zeichnen
            for (int y = 0; y < _grid.Height; y++)
            {
                Console.SetCursorPosition(
                    _gridStartX + OffsetX,
                    _gridStartY + OffsetY + y
                );

                for (int x = 0; x < _grid.Width; x++)
                {
                    Console.ForegroundColor = _colorMapper(_grid[x, y]);
                    Console.Write(_charMapper(_grid[x, y]));
                }
            }

            Console.ResetColor();

            // Info-Panel (rechts)
            int infoStartX = _gridStartX + _grid.Width + 4;
            int infoStartY = _gridStartY;

            int i = 0;
            foreach (var pair in InfoPanel)
            {
                Console.SetCursorPosition(infoStartX, infoStartY + i);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{pair.Key}: ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(pair.Value + "       ");

                i++;
            }

            Console.ResetColor();
        }
    }
}
