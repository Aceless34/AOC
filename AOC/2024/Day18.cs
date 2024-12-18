using AOC.PathFinding;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day18 : Day
    {
        public string Task1()
        {
            int[,] map = new int[71, 71];

            int count = 0;
            foreach (var pair in InputUtils.ReadLines("2024/Files/Day18.txt").Select(s => s.Split(",").Select(int.Parse).ToArray()))
            {
                if( count < 1024)
                {
                    count++;
                    map[pair[1], pair[0]] = 1;
                }
            };

            Utils.PrintMap(map);

            AStar astar = new AStar(map);
            List<Vector2> path = astar.FindPath(new Vector2(0, 0), new Vector2(70, 70));

            foreach (var item in path)
            {
                map[(int)item.Y, (int)item.X] = 2;
            }
            //Utils.PrintMap(map);

            //return string.Join("\n", path.Select(p => p.ToString()));
            return path.Count.ToString();
        }

        public string Task2()
        {
            int[,] map = new int[71, 71];
            string lastBit = "";
            int count = 0;
            foreach (var pair in InputUtils.ReadLines("2024/Files/Day18.txt").Select(s => s.Split(",").Select(int.Parse).ToArray()))
            {
                Console.Write($"Bit falls <{pair[0]},{pair[1]}>: ");
                map[pair[1], pair[0]] = 1;

                AStar astar = new AStar(map);
                List<Vector2> path = astar.FindPath(new Vector2(0, 0), new Vector2(70, 70));

                Console.WriteLine($"path: {path.Count}");
                if (path.Count == 0)
                {
                    lastBit = $"{pair[0]},{pair[1]}";
                    break;
                }

                List<(Vector2, int)> z = new(); 
                for (int i = 0; i < map.GetLength(0); i++) {
                    for (int j = 0; j < map.GetLength(1); j++)
                    {
                        if(path.Contains(new Vector2(j, i)))
                        {
                            z.Add((new Vector2(j, i), 2));
                        }
                        else
                        {
                            z.Add((new Vector2(j, i), map[i, j]));
                        }
                    }
                }

                using Bitmap bitmap = Utils.CreateBitmap(z, 71, 71, p => p.Item1, p => p.Item2 == 0 ? Color.Black : p.Item2 == 1 ? Color.Red : Color.Green);
                Utils.SaveBitmap(bitmap, $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Images\\Day18Part2", $"Path-{count.ToString("0000")}.png");
                count++;

            };


            

            return lastBit;
        }
    }
}
