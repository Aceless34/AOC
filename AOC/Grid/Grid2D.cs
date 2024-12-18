using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC.Grid
{
    public class Grid2D<T>
    {
        private Grid2DNode<T>[,] _grid;
        private int width, height;

        public Grid2D(T[,] grid) 
        {
            width = grid.GetLength(1);
            height = grid.GetLength(0);

            _grid = new Grid2DNode<T>[height, width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[y, x] = new Grid2DNode<T>(new Vector2(x, y), grid[y,x]);
        }

        public Grid2D(int width, int height, T? defaultVal = default)
        {
            Width = width; 
            Height = height;
            _grid = new Grid2DNode<T>[height, width];
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[y, x] = new Grid2DNode<T>(new Vector2(x, y), defaultVal);
        }

        public T? this[int x, int y]
        {
            get => _grid[y, x].Value;
            set => _grid[y, x].Value = value;
        }

        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }

        public void Fill(T value)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    _grid[y, x].Value = value;
        }

        public bool InBounds(Vector2 p)
        {
            return Utils.InBounds(_grid, p);
        }

        public bool InBounds(int x, int y) => InBounds(new Vector2(x, y));

        public Grid2D<TRes> Map<TRes>(Func<T?, TRes> transform)
        {
            var result = new Grid2D<TRes>(Width, Height);
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    result[x, y] = transform(_grid[y, x].Value);
            return result;
        }

        public IEnumerable<T?> FindAll(Func<T?, bool> predicate)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (predicate(_grid[y, x].Value))
                        yield return _grid[y, x].Value;
        }

        public IEnumerable<Grid2DNode<T>> FindAllNodes(Func<T?, bool> predicate)
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (predicate(_grid[y, x].Value))
                        yield return _grid[y, x];
        }

        public Vector2 WrapPosition(Vector2 position)
        {
            return new Vector2((position.X + Width) % Width, (position.Y + Height) % Height);
        }

        public Grid2DNode<T> GetPositionNode(int x, int y)
        {
            return _grid[y, x];
        }

        public Bitmap GetImage(Func<T, Color> getColor)
        {
            Bitmap bitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bitmap);

            g.Clear(Color.White);

            foreach (var node in FindAllNodes(t => true))
            {
                Vector2 pos = node.Position;
                bitmap.SetPixel((int)pos.X, (int)pos.Y, getColor(node.Value));
            }

            return bitmap;
        }

        public Bitmap DebugImage(IEnumerable<Grid2DNode<T>> nodesToHighlight, Color highkightColor, Func<T, Color> getColor)
        {
            Bitmap bitmap = GetImage(getColor);
            foreach (var node in nodesToHighlight)
                bitmap.SetPixel((int)node.Position.X, (int)node.Position.Y, highkightColor);
            return bitmap;
        }

        public IEnumerable<Grid2DNode<T>> BFS(Vector2 start, Func<Grid2DNode<T>, bool> condition)
        {
            var visited = new HashSet<Vector2>();
            var queue = new Queue<Grid2DNode<T>>();

            var statNode = GetPositionNode((int)start.X, (int)start.Y);
            queue.Enqueue(statNode);

            while (queue.Count > 0)
            {
                var currentNode = queue.Dequeue();
                yield return currentNode;

                foreach(var neighbourPos in Utils.GetNeighbouringCells(_grid, currentNode.Position))
                {
                    if(InBounds(neighbourPos) && !visited.Contains(neighbourPos))
                    {
                        var neighbourNode = GetPositionNode((int)neighbourPos.X, (int)neighbourPos.Y);
                        if (condition(neighbourNode))
                        {
                            queue.Enqueue(neighbourNode);
                            visited.Add(neighbourPos);
                        }
                    }
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                    sb.Append(_grid[y, x].Value.ToString());
                sb.AppendLine();
            }
            return sb.ToString();
        }

    }
}
