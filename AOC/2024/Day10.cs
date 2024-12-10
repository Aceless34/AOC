using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day10 : Day
    {
        public string Task1()
        {
            int[][] topographicMap = InputUtils.CreateIntMap(InputUtils.ReadLines("2024/Files/Day10.txt"));
            int width = topographicMap[0].Length;
            int height = topographicMap.Length;

            List<Point> trailheads = GetTrailheads(topographicMap);

            int trailheadScore = trailheads.Sum(x => CountTrailheadScore(topographicMap, x));

            return trailheadScore.ToString();
        }

        private int CountTrailheadScore(int[][] map, Point p)
        {
            HashSet<Point> visited = new();
            Queue<Point> queue = new();

            queue.Enqueue(p);
            int score = 0;
            while(queue.Count > 0)
            {
                Point next = queue.Dequeue();

                foreach(Point neighbour in GetPossibleNeighbours(map, next))
                {
                    if (visited.Contains(neighbour)) continue;
                    visited.Add(neighbour);

                    if (map[neighbour.Y][neighbour.X] == 9)
                        score++;
                    else
                        queue.Enqueue(neighbour);
                }
            }
            return score;

        }

        private List<Point> GetTrailheads(int[][] topographicMap)
        {
            List<Point> trailheads = new List<Point>();
            for(int i = 0; i < topographicMap.Length; i++)
            {
                for (int j = 0; j < topographicMap[i].Length; j++)
                {
                    if(topographicMap[i][j] == 0) trailheads.Add(new Point(j, i));
                }
            }
            return trailheads;
        }

        public List<Point> GetPossibleNeighbours(int[][] map, Point p)
        {
            List<Point> neighbours = GetNeighbours(map, p);
            var a =  neighbours.Where(po => map[p.Y][p.X] + 1 == map[po.Y][po.X]).ToList();
            return a;
        }

        public List<Point> GetNeighbours(int[][] map, Point p)
        {
            List<Point> list = new List<Point>();
            int[][] dirs = [[1, 0], [-1, 0], [0, 1], [0, -1]];
            for (int i = 0; i < dirs.Length; i++)
            {
                int x = p.X + dirs[i][1];
                int y = p.Y + dirs[i][0];

                if (x < 0 || x >= map[0].Length || y < 0 || y >= map.Length) continue;
                list.Add(new Point(x, y));
            }
            return list;
        }

        public string Task2()
        {
            int[][] topographicMap = InputUtils.CreateIntMap(InputUtils.ReadLines("2024/Files/Day10.txt"));
            int width = topographicMap[0].Length;
            int height = topographicMap.Length;

            List<Point> trailheads = GetTrailheads(topographicMap);

            int trailheadRating = trailheads.Sum(x => CountTrailheadRating(topographicMap, x));

            return trailheadRating.ToString();
        }

        private int CountTrailheadRating(int[][] topographicMap, Point p)
        {
            Stack<(Point point, HashSet<Point> visited)> stack = new();
            stack.Push((p, new HashSet<Point> { p }));

            int trails = 0;

            while (stack.Count > 0)
            {
                var (current, visited) = stack.Pop();

                if (topographicMap[current.Y][current.X] == 9)
                {
                    trails++;
                    continue;
                }

                foreach (Point neighbour in GetPossibleNeighbours(topographicMap, current))
                {
                    if (!visited.Contains(neighbour))
                    {
                        var newVisited = new HashSet<Point>(visited) { neighbour };
                        stack.Push((neighbour, newVisited));
                    }
                }
            }

            return trails;
        }
    }
}
