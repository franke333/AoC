using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Puzzle1
{
    class Puzzle5
    {
        class Line
        {
            public static int max=-1;
            public int x1, x2, y1, y2;
            public Line(string input)
            {
                var i = input.Split(" -> "); var i1 = i[0].Split(','); var i2 = i[1].Split(',');
                x1 = int.Parse(i1[0]); y1 = int.Parse(i1[1]);
                x2 = int.Parse(i2[0]); y2 = int.Parse(i2[1]);
                max = Math.Max(Math.Max(Math.Max(Math.Max(x1, y1), x2), y2), max);
                if (IsDiagonal()) return; // coords must not change for diagonals
                if (x1 > x2) { int t = x1; x1 = x2; x2 = t; }   //x1 <= x2
                if (y1 > y2) { int t = y1; y1 = y2; y2 = t; }  //y1 <= y2
                
            }
            public bool IsDiagonal() => x1 != x2 && y1 != y2;
            public Tuple<int, int> GetDirections()
                => new Tuple<int, int>(x1 < x2 ? +1 : -1, y1 < y2 ? +1 : -1);
            public int GetLength() => Math.Abs(x1 - x2);
        }
        class Floor
        {
            int[,] floor;
            public Floor(StreamReader sr,bool countDiagonals)
            {
                List<Line> lines = new List<Line>();
                List<string> strings = new List<string>();
                while (!sr.EndOfStream)
                    strings.Add(sr.ReadLine());
                var watch = System.Diagnostics.Stopwatch.StartNew();
                foreach (var s in strings)
                    lines.Add(new Line(s));
                floor = new int[Line.max+1, Line.max+1];
                foreach (var l in lines)
                {
                    if (!l.IsDiagonal())
                    {
                        if (l.x1 != l.x2)
                            for (int x = l.x1; x <= l.x2; x++)
                                floor[x, l.y1]++;
                        else
                            for (int y = l.y1; y <= l.y2; y++)
                                floor[l.x1, y]++;
                    }
                    else
                    {
                        var dir = l.GetDirections(); 
                        int dx = dir.Item1; int dy = dir.Item2;
                        int len = l.GetLength(); 
                        int x = l.x1; int y = l.y1;
                        for (int i = 0; i<=len  ; i++)
                        {
                            floor[x, y]++;
                            x += dx; y += dy;
                        }
                    }
                }
                var overlaps = CountOverlaps();
                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Console.WriteLine(overlaps);
                Console.WriteLine($"Time (no I/O): {elapsedMs}ms");
            }
            public int CountOverlaps()
            {
                int sum = 0;
                for (int x = 0; x <= Line.max; x++)
                    for (int y = 0; y <= Line.max; y++)
                        if (floor[x, y] > 1) sum++;
                return sum;  
            }
        }
        public static void SolvePuzzle()
        {
            var floor = new Floor(new StreamReader("inputs/Puzzle5Input.txt"),false);
            Console.WriteLine(floor.CountOverlaps());
        }
        public static void SolvePuzzle2()
        {
            var floor = new Floor(new StreamReader("inputs/Puzzle5Input.txt"), true);
            Console.WriteLine(floor.CountOverlaps());
        }
    }
}
