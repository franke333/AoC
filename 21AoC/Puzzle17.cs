using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics.CodeAnalysis;

namespace AoC2021
{
    class Puzzle17
    {
        static int xMin= 135123456, xMax= 155123456, yMin=-102123456, yMax=-78123456;
        static bool WillHit(int vx,int vy)
        {
            int x = 0, y = 0;
            while(x <= xMax && y >= yMin)
            {
                if (x >= xMin && y <= yMax)
                    return true;
                x += vx;
                y += vy;
                if (vx > 0) vx--;
                vy--;
            }
            return false;
        }
       
        public static void SolvePuzzle()
        {
            int ymin = 134;
            int sum = 0;
            for (int i = 0; i <ymin; i++)
                sum += i;
            Console.WriteLine($"part1 {sum} ");
            sum = 0;
            for (int x = 0; x <= xMax; x++)
            {
                for (int y = yMin; y < -yMin; y++)
                    if (WillHit(x, y))
                        sum++;
                Console.WriteLine($"{((double)x)/xMax}%");
            }
            Console.WriteLine($"part2 {sum}");
        }
    }
}
