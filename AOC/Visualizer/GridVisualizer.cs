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

        public T?[,] _lastFrame;

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

            _lastFrame = new T?[grid.Width, grid.Height];
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

            // Force full redraw on first update
            for (int y = 0; y < _grid.Height; y++)
                for (int x = 0; x < _grid.Width; x++)
                    _lastFrame[x, y] = default;
        }

        public void Update()
        {
            Console.ResetColor();

            // Nur geänderte Zellen zeichnen
            for (int y = 0; y < _grid.Height; y++)
            {
                for (int x = 0; x < _grid.Width; x++)
                {
                    var current = _grid[x, y];
                    var old = _lastFrame[x, y];

                    // Vergleich: wenn unverändert → weiter
                    if (EqualityComparer<T>.Default.Equals(current, old))
                        continue;

                    // Neue Zelle zeichnen
                    Console.SetCursorPosition(
                        _gridStartX + OffsetX + x,
                        _gridStartY + OffsetY + y
                    );

                    Console.ForegroundColor = _colorMapper(current);
                    Console.Write(_charMapper(current));

                    // Speichern
                    _lastFrame[x, y] = current;
                }
            }

            Console.ResetColor();

            // InfoPanel neu zeichnen
            int infoStartX = _gridStartX + _grid.Width + 4;
            int infoStartY = _gridStartY;

            int i = 0;
            foreach (var pair in InfoPanel)
            {
                Console.SetCursorPosition(infoStartX, infoStartY + i);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{pair.Key}: ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(pair.Value + "   "); // überschreibt alte Werte

                i++;
            }

            Console.ResetColor();
        }
    }
}
