using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;

namespace AOC._2024
{
    public class Day14 : Day
    {

        public string Task1()
        {
            int width = 101;
            int height = 103;
            List<Robot> robots = new List<Robot>();

            List<string> lines = InputUtils.ReadLines("2024/Files/Day14.txt");
            foreach (string line in lines)
            {
                var match = Regex.Matches(line, @"(-?\d+)");
                int posX = int.Parse(match[0].Value);
                int posY = int.Parse(match[1].Value);
                int velX = int.Parse(match[2].Value);
                int velY = int.Parse(match[3].Value);
                robots.Add(new Robot(
                    new Vector2(posX, posY),
                    new Vector2(velX, velY)
                    ));
            }

            for(int i = 0; i < 10000; i++)
            {
                foreach (Robot robot in robots)
                {
                    robot.move();
                    robot.Position = new Vector2(
                        (robot.Position.X + width) % width,
                        (robot.Position.Y + height) % height
                    );


                    //Console.WriteLine(robot.Position);
                }
                Console.WriteLine($"Seconds: {i+1}");
                PrintRobots(robots, width, height, i+1);
            }

            long safety = CalcSafety(robots, width, height);

            return safety.ToString();
        }

        private void PrintRobots(List<Robot> robots, int width, int height, int seconds)
        {
            using Bitmap bitmap = new Bitmap(width, height);
            using Graphics graphics = Graphics.FromImage(bitmap);

            graphics.Clear(Color.White);

            foreach (Robot robot in robots)
            {
                bitmap.SetPixel((int)robot.Position.X, (int)robot.Position.Y, Color.Black);
            }

            bitmap.Save($"C:\\Users\\nico.hundertmark\\Documents\\AOCBilder\\{seconds}.png", System.Drawing.Imaging.ImageFormat.Png);

            /*
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int roboCount = robots.Where(r => r.Position.X == x && r.Position.Y == y).Count();
                    Console.Write($"{(roboCount >= 1 ? "■" : " ")}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("========================================================================================================");
            */
        }

        private long CalcSafety(List<Robot> robots, int width, int height)
        {
            int midWidth = width / 2; // 5
            int midHeight = height / 2; // 3

            int upLeft = robots.Where(r => r.Position.X < midWidth && r.Position.Y < midHeight).Count();
            int upRight = robots.Where(r => r.Position.X > midWidth && r.Position.Y < midHeight).Count();
            int downLeft = robots.Where(r => r.Position.X < midWidth && r.Position.Y > midHeight).Count();
            int downRight = robots.Where(r => r.Position.X > midWidth && r.Position.Y > midHeight).Count();

            return (long)upLeft * upRight * downLeft * downRight;
        }

        public string Task2()
        {
            return "";
        }
    }

    public class Robot
    {
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }

        public Robot(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
        }

        public void move()
        {
            Position += Velocity;
        }
    }
}
