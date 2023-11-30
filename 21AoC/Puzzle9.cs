using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace AoC2021
{
    class Puzzle9
    {
        public static void SolvePuzzle()
        {
            List<string> heights = new List<string>();
            heights.Add("");
            StreamReader sr = new StreamReader("inputs/Puzzle9Input.txt");
            while (!sr.EndOfStream)
                heights.Add("9"+sr.ReadLine()+"9");
            int width = heights[1].Length;
            heights.Add(new string('9', width));
            heights[0] = new string('9', width);
            int height = heights.Count;
            int riskLevel = 0;
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                    if (
                        heights[y - 1][x] > heights[y][x] &&
                        heights[y][x - 1] > heights[y][x] &&
                        heights[y][x + 1] > heights[y][x] &&
                        heights[y + 1][x] > heights[y][x]
                        )
                    {
                        Console.WriteLine($"risk found at x,y: {x} {y}");
                        riskLevel += 1 + heights[y][x] - '0';
                    }
            Console.WriteLine(riskLevel);
        }
        class Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }
        class Basin
        {
            public Point lowPoint;
            public int size;
            public Basin(int x,int y)
            {
                lowPoint = new Point(x, y);
                size = 0;
            }
        }
        public static void SolvePuzzle2()
        {
            
            List<string> heights = new List<string>();
            heights.Add("");
            StreamReader sr = new StreamReader("inputs/Puzzle9Input.txt");
            while (!sr.EndOfStream)
                heights.Add("9" + sr.ReadLine() + "9");
            int width = heights[1].Length;
            heights.Add(new string('9', width));
            heights[0] = new string('9', width);
            int height = heights.Count;
            List<Tuple<int, int>> riskLevelPoints = new List<Tuple<int, int>>();
            List<Basin> basins = new List<Basin>();
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                    if (
                        heights[y - 1][x] > heights[y][x] &&
                        heights[y][x - 1] > heights[y][x] &&
                        heights[y][x + 1] > heights[y][x] &&
                        heights[y + 1][x] > heights[y][x]
                        )
                    {
                        Console.WriteLine($"risk found at x,y: {x} {y}");
                        basins.Add(new Basin(x, y));
                    }
            bool[,] _checked = new bool[width, height]; 
            bool IsUnchecked(Point p)
            {
                if (heights[p.y][p.x] != '9' && !_checked[p.x,p.y])
                {
                    _checked[p.x,p.y]=true;
                    return true;
                }
                return false;
            }
            foreach(Basin b in basins)
            {
                Queue<Point> queue = new Queue<Point>();
                queue.Enqueue(b.lowPoint);
                while (queue.Count > 0)
                {
                    var p = queue.Dequeue();
                    if (IsUnchecked(p))
                    {
                        queue.Enqueue(new Point(p.x + 1, p.y));
                        queue.Enqueue(new Point(p.x - 1, p.y));
                        queue.Enqueue(new Point(p.x, p.y - 1));
                        queue.Enqueue(new Point(p.x, p.y + 1));
                        b.size++;
                    }
                }
            }
            long acc = 1;
            for (int i = 0; i < 3; i++)
            {
                var biggest = basins.Max(b => b.size);
                basins.Remove(basins.Find(b => b.size == biggest));
                acc *= biggest;
            }
            Console.WriteLine(acc);


        }
    }
}
