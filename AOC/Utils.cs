﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public static class Utils
    {

        public static Bitmap CreateBitmap(List<Object> objects, int width, int height,Func<Object, Vector2> getPos,  Func<Object, Color> getColor)
        {
            using Bitmap bitmap = new Bitmap(width, height);
            using Graphics g = Graphics.FromImage(bitmap);

            g.Clear(Color.White);

            foreach (Object obj in objects) { 
                Vector2 pos = getPos(obj);
                bitmap.SetPixel((int)pos.X, (int)pos.Y, getColor(obj));
            }

            return bitmap;
        }

        public static void SaveBitmap(Bitmap bitmap, string path)
        {
            bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Png);
        }

        public static void PrintMap(object[,] map)
        {
            for(int i = 0;  i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.WriteLine(map[i, j].ToString());
                }
            }
        }

        public static void PrintMap(object[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    Console.WriteLine(map[i][j].ToString());
                }
            }
        }

        public static bool InBounds(object[,] map, Vector2 p)
        {
            return p.X >= 0 && p.X < map.GetLength(0) && p.Y >= 0 && p.Y < map.GetLength(1);
        }

        public static List<Vector2> GetNeighbouringCells(object[,] map, Vector2 p, bool diagonals = false)
        {
            List<Vector2> cells = new List<Vector2>();
            List<Vector2> dirs = [
                new Vector2(1, 0),
                new Vector2(-1, 0),
                new Vector2(0, 1),
                new Vector2(0, -1),
            ];
            if (diagonals)
            {
                dirs.Add(new Vector2(-1, -1));
                dirs.Add(new Vector2(1, -1));
                dirs.Add(new Vector2(-1, 1));
                dirs.Add(new Vector2(1, 1));
            }

            foreach (var dir in dirs)
            {
                Vector2 n = p + dir;
                if (InBounds(map, n))
                    cells.Add(n);
            }

            return cells;
        }
    }
}