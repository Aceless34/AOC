using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static AOC._2024.Day12;

namespace AOC._2024
{
    public class Day12 : Day
    {
        
        public class Region
        {
            public List<Point> points;
            public char c;
            public Region(List<Point> points, char c)
            {
                this.points = points;
                this.c = c;
            }
        }

        public string Task1()
        {
            char[][] map = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day12.txt"));
            int width = map[0].Length;
            int height = map.Length;
            List<Region> regions = FindRegions(map, width, height);

            long sum = 0;
            foreach (Region region in regions)
            {
                sum += region.points.Count * CalcPerimeter(region);
            }

            return sum.ToString();

        }

        private List<Region> FindRegions(char[][] map, int width, int height)
        {
            bool[,] open = new bool[height, width];

            List<Region> regions = new List<Region>();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (!open[i, j])
                    {
                        List<Point> points = new List<Point>();
                        FindRegion(map[i][j], map, i, j, points);
                        foreach (Point p in points)
                        {
                            open[p.Y, p.X] = true;
                        }
                        regions.Add(new Region(points, map[i][j]));


                    }
                }
            }

            return regions;
        }

        private int CalcPerimeter(Region region)
        {
            int perimeter = 0;
            foreach (Point p in region.points)
            {
                List<Point> list = new List<Point>();
                int[][] dirs = [[1, 0], [-1, 0], [0, 1], [0, -1]];
                for (int i = 0; i < dirs.Length; i++)
                {
                    int x = p.X + dirs[i][1];
                    int y = p.Y + dirs[i][0];

                    list.Add(new Point(x, y));
                }

                perimeter += list.Where(p => !region.points.Contains(p)).Count();

            }

            return perimeter;
        }

        private void FindRegion(char v, char[][] map, int i, int j, List<Point> points)
        {
            if (points.Contains(new Point(j, i)))
            {
                return;
            }
            points.Add(new Point(j, i));

            List<Point> neighbours = GetNeighbours(map, new Point(j, i));

            foreach (Point p in neighbours)
            {
                if (!points.Contains(p) && map[p.Y][p.X] == v)
                {
                    FindRegion(v, map, p.Y, p.X, points);
                }
            }

        }

        public List<Point> GetNeighbours(char[][] map, Point p)
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
            char[][] map = InputUtils.CreateCharMap(InputUtils.ReadLines("2024/Files/Day12.txt"));
            int width = map[0].Length;
            int height = map.Length;
            List<Region> regions = FindRegions(map, width, height);

            long sum = 0;
            foreach (Region region in regions)
            {
                sum += region.points.Count * CalcFence(region);
            }

            return sum.ToString();
        }

        public int CalcFence(Region region)
        {
            int sides = 0;
            foreach (Point p in region.points)
            {
                List<(Point, Point)> values = new() {
                    (new Point(0,-1), new Point(1,0)),
                    (new Point(1,0), new Point(0,1)),
                    (new Point(0,1), new Point(-1,0)),
                    (new Point(-1,0), new Point(0,-1)),
                };

                foreach (var (du, dv) in values )
                {
                    if (!region.points.Contains(new Point(p.X + du.X, p.Y + du.Y)) &&
                        !region.points.Contains(new Point(p.X + dv.X, p.Y + dv.Y)))
                    {
                        sides++;
                    }
                    if (region.points.Contains(new Point(p.X + du.X, p.Y + du.Y)) &&
                        region.points.Contains(new Point(p.X + dv.X, p.Y + dv.Y)) &&
                        !region.points.Contains(new Point(p.X + dv.X + du.X, p.Y + dv.Y + du.Y)))
                    {
                        sides++;
                    }
                }


            }

            return sides;
        }

    }
}
