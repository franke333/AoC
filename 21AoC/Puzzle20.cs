using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;


namespace AoC2021
{
    class Puzzle20
    {
        struct Point
        {
            public int x, y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public override bool Equals(object obj)
                => obj is Point p ? p.x == x && p.y == y : false;

            public override int GetHashCode()
                => (x << 16) + y;
        }

        static Dictionary<Point, byte> Enhance(Dictionary<Point, byte> map, byte[] magic, ref byte outOfRangePixel,int height,int width)
        {
            var newMap = new Dictionary<Point, byte>();
            byte _outOfRangePixel = outOfRangePixel;
            Func<Point, byte> lookUp = x => map.TryGetValue(x, out byte val) ? val : _outOfRangePixel;
            var range = new int[] { -1, 0, 1 };
            for (int j = 0; j < width+2; j++)
                for (int i = 0; i < height+2; i++)
                {
                    int val = 0;
                    foreach (var l in range)
                        foreach (var k in range)
                            val = val*2 + lookUp(new Point(i + k -1, j + l -1));
                    newMap.Add(new Point(i, j), magic[val]);
                }
            outOfRangePixel = outOfRangePixel == 0 ? magic[0] : magic[511];
            return newMap;
        }

        public static void SolvePuzzle()
        {
            StreamReader sr = new StreamReader("inputs/Puzzle20Input.txt");
            byte[] valToPixel = new byte[512];
            string line = sr.ReadLine();
            Func<char,byte> lambda =  x => (byte)(x == '#' ? 1 : 0) ;
            for (int i = 0; i < 512; i++)
                valToPixel[i] = lambda(line[i]);
            sr.ReadLine();
            var map = new Dictionary<Point, byte>();
            int row = 0;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                for (int i = 0; i < line.Length; i++)
                    map.Add(new Point(i, row), lambda(line[i]));
                row++;
            }
            int width = line.Length;
            byte outOfRangePixel = 0;
            for (int i = 0; i < 50; i++)
            {
                map = Enhance(map, valToPixel, ref outOfRangePixel, width + 2 * i, row + 2 * i);
                if(i==1)
                    Console.WriteLine($"part1: {map.Values.Sum(x => x)}");
            }
            Console.WriteLine($"part2: {map.Values.Sum(x => x)}");
            for (int j = 0; j < row+102; j++)
            {
                for (int i = 0; i < width+102; i++)
                {
                    Console.Write(map[new Point(i,j)] == 0 ? '.' : '#');
                }
                Console.WriteLine();
            }
        }
    }
}
