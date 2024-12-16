using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day16 : Day
    {
        public string Task1()
        {
            char[,] maze = InputUtils.CreateCharMap2(InputUtils.ReadLines("2024/Files/Day16.txt"));

            Vector2 start = Utils.FindObjectPositionInMap<char>(maze, 'S');
            Vector2 end = Utils.FindObjectPositionInMap<char>(maze, 'E');

            int score = Solve(maze, start, end);

            return score.ToString();
        }

        private int Solve(char[,] maze, Vector2 start, Vector2 end)
        {
            PriorityQueue<(int score, Vector2 pos, Vector2 dir), int> queue = new PriorityQueue<(int score, Vector2 pos, Vector2 dir), int>();
            HashSet<Vector2> visited = new HashSet<Vector2>();
            queue.Enqueue((0, start, new Vector2(1,0)), 0);
            visited.Add(start);

            while (maze[(int)queue.Peek().pos.Y, (int)queue.Peek().pos.X] != 'E') {
                var curr = queue.Dequeue();

                List<(Vector2 pos, Vector2 dir)> neighbours = Utils.GetNeighbouringCells(maze, curr.pos).Where(p => maze[(int)p.Y, (int)p.X] != '#').Where(p => IsDeg90(p - curr.pos, curr.dir) || (p - curr.pos) == curr.dir ).Select(n => (n,n - curr.pos)).ToList();
                foreach (var neighbour in neighbours)
                {
                    if (!visited.Contains(neighbour.pos))
                    {
                        queue.Enqueue(((IsDeg90(curr.dir, neighbour.dir)? 1001 : 1) + curr.score , neighbour.pos, neighbour.dir), (IsDeg90(curr.dir, neighbour.dir) ? 1001 : 1) + curr.score);
                    }
                }
                visited.Add(curr.pos);
            }

            return queue.Peek().score;
        }

        private bool IsDeg90(Vector2 dir1, Vector2 dir2)
        {
            return Math.Abs(dir1.X) != Math.Abs(dir2.X) && Math.Abs(dir1.Y) != Math.Abs(dir2.Y);
        }

        public string Task2()
        {
            char[,] maze = InputUtils.CreateCharMap2(InputUtils.ReadLines("2024/Files/Day16.txt"));

            Vector2 start = Utils.FindObjectPositionInMap<char>(maze, 'S');
            Vector2 end = Utils.FindObjectPositionInMap<char>(maze, 'E');

            ShortestCost = Solve(maze, start, end);
            Paths([], start, new Vector2(1, 0), 0, [], maze);

            return Seats.Count().ToString();
        }

        private HashSet<Vector2> Seats = [];
        private Dictionary<(Vector2, Vector2), int> Visited = [];
        private int ShortestCost = 0;

        public void Paths(HashSet<Vector2> seen, Vector2 curr, Vector2 dir, int cost, HashSet<Vector2> path, char[,] maze)
        {
            if (Visited.GetValueOrDefault((curr, dir), int.MaxValue) < cost) return;
            Visited[(curr, dir)] = cost;
            path.Add(curr);
            if(cost == ShortestCost && maze[(int)curr.Y, (int)curr.X] == 'E')
            {
                path.ToList().ForEach(x => Seats.Add(x));
            }else if(cost < ShortestCost)
            {
                seen.Add(curr);
                List<(Vector2 pos, Vector2 dir)> neighbours = Utils.GetNeighbouringCells(maze, curr).Where(p => maze[(int)p.Y, (int)p.X] != '#').Where(p => IsDeg90(p - curr, dir) || (p - curr) == dir).Select(n => (n, n - curr)).ToList();
                foreach (var neighbour in neighbours)
                {
                    if(seen.Contains(neighbour.pos)) continue;
                    Paths(seen, neighbour.pos, neighbour.dir, (IsDeg90(dir, neighbour.dir) ? 1001 : 1) + cost, path, maze);
                }
                seen.Remove(curr);
            }
            path.Remove(curr);
        }


    }
}
