using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AOC._2024
{
    public class Day15 : Day
    {
        public string Task1()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day15.txt");
            int breakLine = lines.IndexOf("");
            char[,] warehouse = InputUtils.CreateCharMap2(lines.GetRange(0, breakLine));
            char[] commands = string.Join("", lines.Skip(breakLine+1).ToList()).ToCharArray();

            Vector2 robotPos = Utils.FindObjectPositionInMap<char>(warehouse, '@');
            //List<Vector2> boxesPos = Utils.FindAllObjectsPositionInMap<char>(warehouse, 'O');

            Utils.PrintMap<char>(warehouse);
            Console.WriteLine();

            for(int i = 0; i < commands.Length; i++)
            {
                char command = commands[i];
                Vector2 dir = command == '^'? new Vector2(0,-1) : command == 'v'? new Vector2(0,1) : command == '<'? new Vector2(-1,0) : new Vector2(1,0);

                Vector2 next = robotPos + dir;

                if (warehouse[(int)next.Y, (int)next.X] == '#')
                    continue;

                //box
                if (warehouse[(int)next.Y, (int)next.X] == 'O')
                {
                    Vector2 nextBox = next + dir;
                    while(warehouse[(int)nextBox.Y, (int)nextBox.X] == 'O')
                    {
                        nextBox += dir;
                    }
                    if (warehouse[(int)nextBox.Y, (int)nextBox.X] == '#')
                        continue;
                    warehouse[(int)next.Y, (int)next.X] = '.';
                    warehouse[(int)nextBox.Y, (int)nextBox.X] = 'O';
                }

                warehouse[(int)robotPos.Y, (int)robotPos.X] = '.';
                warehouse[(int)next.Y, (int)next.X] = '@';
                robotPos += dir;

                //Utils.PrintMap(warehouse);
            }
            Utils.PrintMap(warehouse);

            float sum = Utils.FindAllObjectsPositionInMap<char>(warehouse, 'O').Sum(pos => 100 * pos.X + pos.Y);

            return sum.ToString();
        }

        public string Task2()
        {
            List<string> lines = InputUtils.ReadLines("2024/Files/Day15.txt");
            int breakLine = lines.IndexOf("");
            char[,] warehouse = InputUtils.CreateCharMap2(lines.GetRange(0, breakLine));
            char[] commands = string.Join("", lines.Skip(breakLine + 1).ToList()).ToCharArray();

            Vector2 robotPos = Utils.FindObjectPositionInMap<char>(warehouse, '@');
            robotPos.Y *= 2;
            List<WideBox> boxes = Utils.FindAllObjectsPositionInMap<char>(warehouse, 'O').Select((pos, i) => new WideBox(new Vector2(pos.X, pos.Y*2), new Vector2(pos.X, pos.Y * 2 + 1))).ToList();
            List<WideBox> walls = Utils.FindAllObjectsPositionInMap<char>(warehouse, '#').Select((pos, i) => new WideBox(new Vector2(pos.X, pos.Y*2), new Vector2(pos.X, pos.Y * 2 + 1))).ToList();

            for (int i = 0; i < commands.Length; i++)
            {
                char command = commands[i];
                Vector2 dir = command == '^' ? new Vector2(-1, 0) : command == 'v' ? new Vector2(1, 0) : command == '<' ? new Vector2(0, -1) : new Vector2(0, 1);

                var next = robotPos + dir;
                if (walls.Any(w => w.Contains(next)))
                    continue;

                var nextBox = boxes.FirstOrDefault(b => b.Contains(next));
                var move = true;

                if(nextBox != null)
                {
                    var boxesToMove = new HashSet<WideBox>() { nextBox };
                    var queue = new Queue<WideBox>();
                    queue.Enqueue(nextBox);

                    while(queue.Count > 0)
                    {
                        var box = queue.Dequeue();
                        var nextLeft = box.Left + dir;
                        var nextRight = box.Right + dir;
                        
                        if(walls.Any(w => w.Contains(nextLeft)) || walls.Any(w => w.Contains(nextRight)))
                        {
                            move = false;
                            break;
                        }

                        var nextLeftBox = boxes.FirstOrDefault(b => b.Contains(nextLeft));
                        if(nextLeftBox != null && boxesToMove.Add(nextLeftBox))
                            queue.Enqueue(nextLeftBox);

                        var nextRightBox = boxes.FirstOrDefault(b => b.Contains(nextRight));
                        if (nextRightBox != null && boxesToMove.Add(nextRightBox))
                            queue.Enqueue(nextRightBox);
                    }

                    if (move)
                    {
                        foreach( var box in boxesToMove)
                        {
                            boxes.Remove(box);
                            boxes.Add(new WideBox(box.Left + dir, box.Right + dir));
                        }
                    }
                }

                if (move)
                {
                    robotPos += dir;
                }

                /*
                for(int y = 0; y < warehouse.GetLength(0); y++)
                {
                    for(int x = 0; x < warehouse.GetLength(1)*2; x++)
                    {
                        var pos = new Vector2(y, x);
                        if(walls.Any(w => w.Contains(pos)))
                            Console.Write('#');
                        else if (boxes.Any(b => b.Left == pos))
                            Console.Write('[');
                        else if (boxes.Any(b => b.Right == pos))
                            Console.Write(']');
                        else if(pos == robotPos)
                            Console.Write('@');
                        else
                            Console.Write('.');
                    }
                    Console.WriteLine();
                }
                */
            }

            float sum = boxes.Sum(b => 100 * b.Left.X + b.Left.Y);

            return sum.ToString();
        }

        public record WideBox(Vector2 Left, Vector2 Right)
        {
            public bool Contains(Vector2 pos) => Left.X <= pos.X && pos.X <= Right.X &&
                                                 Left.Y <= pos.Y && pos.Y <= Right.Y;
        }
    }
}
