using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace AoC2021
{
    class Puzzle13
    {
        class Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object other)
                => x == ((Point)other).x && y == ((Point)other).y;

            public override int GetHashCode()
             => x * 10000 + y;

            public bool IsSameAs(Point p) => x == p.x && y == p.y;
        }
        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle13Input.txt");
            string line;
            HashSet<Point> points = new HashSet<Point>();
            while((line=sr.ReadLine())!= ""){
                var vals = line.Split(',');
                points.Add(new Point(int.Parse(vals[0]), int.Parse(vals[1])));
            }
            void flipX(int x)
            {
                List<Point> toFlip = new List<Point>();
                foreach(var p in points)
                    if (p.x > x)
                        toFlip.Add(p);
                foreach(var p in toFlip)
                {
                    points.Remove(p);
                    points.Add(new Point(x + x - p.x, p.y));
    
                }
            }
            void flipY(int y)
            {
                List<Point> toFlip = new List<Point>();
                foreach (var p in points)
                    if (p.y > y)
                        toFlip.Add(p);
                foreach (var p in toFlip)
                {
                    points.Remove(p);
                    points.Add(new Point(p.x, y + y - p.y));

                }
            }
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine().Substring(11);
                var v = line.Split('=');
                if (v[0][0] == 'x')
                    flipX(int.Parse(v[1]));
                else
                    flipY(int.Parse(v[1]));
                Console.WriteLine(points.Count);
            }
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if(points.Contains(new Point(j,i)))
                        Console.Write('#');
                    else
                        Console.Write('.');
                }
                Console.WriteLine();
            }

        }
    }
}
