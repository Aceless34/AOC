using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC.PathFinding
{
    class AStarNode
    {
        public int Value;
        public int Cost;
        public int Heuristic;
        public Vector2 Pos;
        public AStarNode? Parent;
        public int Total => Cost + Heuristic;
    }

    public class AStar
    {
        int width;
        int height;
        AStarNode[,] map;
        int[,] orgMap;

        List<AStarNode> openList;
        List<AStarNode> closedList;

        AStarNode? start, end;

        public AStar(int[,] map)
        {
            this.width = map.GetLength(0);
            this.height = map.GetLength(1);
            this.orgMap = map;

            this.map = new AStarNode[width,height];
            for (int j = 0; j < this.height; j++)
            {
                for (int i = 0; i < this.width; i++)
                {
                    this.map[j, i] = new AStarNode
                    {
                        Value = map[j,i],
                        Cost = 0,
                        Heuristic = 0,
                        Pos = new Vector2(i,j),
                    };
                }
            }

            openList = new();
            closedList = new();
        }

        public List<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            openList = new List<AStarNode>();
            closedList = new List<AStarNode>();

            this.start = map[(int)start.Y, (int)start.X];
            this.end = map[(int)end.Y, (int)end.X];
            openList.Add(this.start);

            AStarNode? curr;
            while (openList.Count > 0)
            {
                curr = getTileWithLowestTotal();
                if (curr == null)
                    throw new NullReferenceException("No neighbours");

                if(curr.Pos == end)
                {
                    return shortestPath().Select(p => p.Pos).ToList();
                }

                openList.Remove(curr);
                closedList.Add(curr);

                List<Vector2> neighbours = Utils.GetNeighbouringCells(map, curr.Pos).Where(p => map[(int)p.Y, (int)p.X].Value == 0).ToList();
                foreach (var neigh in neighbours)
                {
                    AStarNode n = map[(int)neigh.Y, (int)neigh.X];
                    int newCost = curr.Cost + 1;

                    if (closedList.Contains(n)) continue;

                    if (!openList.Contains(n) || newCost < n.Cost)
                    {
                        n.Cost = newCost;
                        n.Heuristic = ManhattanDistance(n.Pos, end);
                        n.Parent = curr; // Speichere den aktuellen Knoten als Parent

                        if (!openList.Contains(n))
                            openList.Add(n);
                    }
                }

            }
            return new();
        }

        private List<AStarNode> shortestPath()
        {
            if (end == null || start == null)
                throw new NullReferenceException("End and Start can't be null");

            AStarNode? curr = end;
            List<AStarNode> path = [];

            path.Add(curr);

            while (curr != start && curr != null)
            {
                path.Add(curr);
                curr = Utils.GetNeighbouringCells(map, curr.Pos)
                    .Select(n => map[(int)n.Y, (int)n.X])
                    .Where(n => closedList.Contains(n))
                    .OrderBy(n => n.Cost)
                    .FirstOrDefault();
            }
            path.Add(start);
            path.Reverse();
            return path;
        }

        private int ManhattanDistance(Vector2 pos, Vector2 end)
        {
            return (int)Math.Abs((end.X - pos.X) + (end.Y - pos.Y));
        }

        private AStarNode? getTileWithLowestTotal()
        {
            return openList.OrderBy(n => n.Total).ThenBy(n => n.Heuristic).FirstOrDefault();
        }

    }
}
