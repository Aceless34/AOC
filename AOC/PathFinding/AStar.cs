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

        AStarNode start, end;

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
        }

        public List<Vector2> FindPath(Vector2 start, Vector2 end)
        {
            openList = new List<AStarNode>();
            closedList = new List<AStarNode>();

            this.start = map[(int)start.Y, (int)start.X];
            this.end = map[(int)end.Y, (int)end.X];
            openList.Add(this.start);

            AStarNode curr;
            while (openList.Count > 0)
            {
                curr = getTileWithLowestTotal();

                if(curr.Pos == end)
                {
                    return shortestPath().Select(p => p.Pos).ToList();
                }

                openList.Remove(curr);
                closedList.Add(curr);

                List<Vector2> neighbours = Utils.GetNeighbouringCells(map, curr.Pos).Where(p => map[(int)p.Y, (int)p.X].Value == 0).ToList();
                foreach(var neigh in neighbours)
                {
                    if(!openList.Any(p => p.Pos == neigh) && !closedList.Any(p => p.Pos == neigh))
                    {
                        AStarNode n = map[(int)neigh.Y, (int)neigh.X];
                        n.Cost = curr.Cost + 1;
                        n.Heuristic = ManhattanDistance(n.Pos, end);
                        openList.Add(n);
                    }
                }

            }
            return [];
        }

        private List<AStarNode> shortestPath()
        {
            bool startFound = false;

            AStarNode curr = end;
            List<AStarNode> path = [];

            path.Add(curr);

            while (!startFound)
            {
                IEnumerable<AStarNode> neighbours = Utils.GetNeighbouringCells(map, curr.Pos).Select(n => map[(int)n.Y, (int)n.X]);
                foreach (var neigh in neighbours)
                {
                    if(start == neigh)
                    {
                        startFound = true;
                        break;
                    }
                    if (openList.Contains(neigh) || closedList.Contains(neigh))
                    {
                        if (neigh.Cost <= curr.Cost && neigh.Cost > 0)
                        {
                            curr = neigh;
                            path.Add(neigh);
                            break;
                        }
                    }
                }
            }
            path.Reverse();
            return path;
        }

        private int ManhattanDistance(Vector2 pos, Vector2 end)
        {
            return (int)Math.Abs((end.X - pos.X) + (end.Y - pos.Y));
        }

        private AStarNode getTileWithLowestTotal()
        {
            int lowest = int.MaxValue;
            AStarNode lowestNode = new AStarNode();
            foreach(var node in openList)
            {
                if(node.Total <= lowest)
                {
                    lowest = node.Total;
                    lowestNode = node;
                }
            }
            return lowestNode;
        }

    }
}
