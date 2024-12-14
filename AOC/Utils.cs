using System;
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

    }
}
