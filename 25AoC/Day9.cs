using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day9
    {

        private static bool isInEdge(long x, long y, long ax, long ay, long bx, long by)
        {
            if(ax == bx)
            {
                return x == ax && y >= Math.Min(ay, by) && y <= Math.Max(ay, by);
            }
            else if(ay == by)
            {
                return y == ay && x >= Math.Min(ax, bx) && x <= Math.Max(ax, bx);
            }
            return false;
        }

        public static void Execute()
        {
            List<(long, long)> corners = new List<(long, long)>();
            StreamReader sr = new StreamReader("Inputs/Input9.txt");
            for (var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                var coords = line.Split(",").Select(x => long.Parse(x)).ToArray();
                corners.Add((coords[0], coords[1]));
            }
            long largestArea = 0;
            foreach (var (a,b) in corners) { 
                foreach(var (c,d) in corners) {
                    long area = 0;
                    area = (1+Math.Abs(a - c)) * (1+Math.Abs(b - d));
                    if (area > largestArea)
                        largestArea = area;
                }
            }
            Console.WriteLine(largestArea);
            largestArea = 0;
            long count = (corners.Count+1)/2 * corners.Count;
            long calced = 0;
            long minSize = corners.Min(a => Math.Min(a.Item1, a.Item2));
            long maxSize = corners.Max(a => Math.Max(a.Item1, a.Item2));
            long offset = minSize - 1;
            long size = maxSize - minSize + 3;
            bool[][] outside = new bool[size][];
            for (long i = 0; i < size; i++)
            {
                outside[i] = new bool[size];
            }
            Queue<(long,long)> q = new Queue<(long,long)>();
            q.Enqueue((0, 0));
            while (q.Count > 0)
            {
                var (x, y) = q.Dequeue();
                calced++;
                if(calced % 500_000 == 0)
                {
                    Console.WriteLine($"{100*calced/(float)(size*size)}%");
                }
                foreach (var (dx, dy) in new (long, long)[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
                {
                    long nx = x + dx;
                    long ny = y + dy;
                    if (nx < 0 || ny < 0 || nx >= size || ny >= size)
                        continue;
                    if (outside[nx][ny])
                        continue;
                    bool isOutside = true;
                    for (int i = 0; i < corners.Count; i++)
                    {
                        var (cx, cy) = corners[i];
                        var (ex, ey) = corners[(i + 1) % corners.Count];
                        if (isInEdge(nx + offset, ny + offset, cx, cy, ex, ey))
                        {
                            isOutside = false;
                            break;
                        }
                    }
                    if (isOutside)
                    {
                        outside[nx][ny] = true;
                        q.Enqueue((nx, ny));
                    }
                }
            }
            for (int i = 0; i < corners.Count; i++)
            {
                for (int j = i+1; j < corners.Count; j++)
                {
                    var (ax, ay) = corners[i];
                    var (bx, by) = corners[j];
                    ax -= offset;
                    ay -= offset;
                    bx -= offset;
                    by -= offset;
                    long area = (1 + Math.Abs(ax - bx)) * (1 + Math.Abs(ay - by));
                    if(area < largestArea)
                        continue;
                    bool isOutside = false;
                    long x = Math.Min(ax, bx);
                    while (x <= Math.Max(ax, bx))
                    {
                        if (outside[x][ay] || outside[x][by])
                        {
                            isOutside = true;
                            break;
                        }
                        x++;
                    }
                    long y = Math.Min(ay, by);
                    while (y <= Math.Max(ay, by))
                    {
                        if (outside[ax][y] || outside[bx][y])
                        {
                            isOutside = true;
                            break;
                        }
                        y++;
                    }
                    if (!isOutside)
                        largestArea = area;


                }
            }
            Console.WriteLine(largestArea);
        }
    }
}
