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
        private int[,] _highlightFrame;
        private bool[,] _wasHighlightedLastFrame;


        public T?[,] _lastFrame;

        public bool EnableDiffHighlight { get; set; } = false;
        public ConsoleColor DiffHighlightColor { get; set; } = ConsoleColor.Yellow;
        public int HighlightForFrames { get; set; } = 4;

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
            _highlightFrame = new int[grid.Width, grid.Height];
            _wasHighlightedLastFrame = new bool[grid.Width, grid.Height];

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

            for (int y = 0; y < _grid.Height; y++)
            {
                for (int x = 0; x < _grid.Width; x++)
                {
                    var current = _grid[x, y];
                    var old = _lastFrame[x, y];

                    bool changed = !EqualityComparer<T>.Default.Equals(current, old);

                    // 1️⃣ Änderung → Highlight starten
                    if (changed)
                    {
                        _highlightFrame[x, y] = HighlightForFrames;
                        _wasHighlightedLastFrame[x, y] = true;

                        DrawCell(x, y, EnableDiffHighlight ? DiffHighlightColor : _colorMapper(current));
                        _lastFrame[x, y] = current;
                        continue;
                    }

                    // 2️⃣ Highlight läuft noch
                    if (_highlightFrame[x, y] > 0)
                    {
                        _highlightFrame[x, y]--;
                        _wasHighlightedLastFrame[x, y] = true;

                        if (EnableDiffHighlight)
                            DrawCell(x, y, DiffHighlightColor);

                        continue;
                    }

                    // 3️⃣ Highlight endet JETZT
                    if (_wasHighlightedLastFrame[x, y])
                    {
                        _wasHighlightedLastFrame[x, y] = false;

                        // Normale Farbe neu zeichnen
                        DrawCell(x, y, _colorMapper(current));
                        continue;
                    }

                    // 4️⃣ Nichts tun → Zelle bleibt wie vorher → MAXIMALE Performance
                }
            }

            DrawInfoPanel();
        }

        private void DrawCell(int x, int y, ConsoleColor color)
        {
            Console.SetCursorPosition(_gridStartX + OffsetX + x,
                                      _gridStartY + OffsetY + y);

            Console.ForegroundColor = color;
            Console.Write(_charMapper(_grid[x, y]));
        }
        private void DrawInfoPanel()
        {
            int infoStartX = _gridStartX + _grid.Width + 4;
            int infoStartY = _gridStartY;

            int i = 0;
            foreach (var pair in InfoPanel)
            {
                Console.SetCursorPosition(infoStartX, infoStartY + i);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write($"{pair.Key}: ");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(pair.Value + "   ");
                i++;
            }

            Console.ResetColor();
        }
    }
}
