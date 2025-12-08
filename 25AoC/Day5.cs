using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day5
    {

        public static void Execute()
        {
            List<Tuple<long,long>> ranges = new();
            StreamReader sr = new StreamReader("inputs/Input5.txt");

            Dictionary<long,int> left = new(), right = new();
            List<long> points = new();

            for (string? line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                if (!line.Contains("-"))
                    break;
                var parts = line.Split('-');
                
                
                points.Add(long.Parse(parts[0]));
                points.Add(long.Parse(parts[1]));
                if (!left.ContainsKey(long.Parse(parts[0])))
                    left.Add(long.Parse(parts[0]), 0);
                left[long.Parse(parts[0])]++;
                if (!right.ContainsKey(long.Parse(parts[1])))
                    right.Add(long.Parse(parts[1]), 0);
                right[long.Parse(parts[1])]++;
                ranges.Add(new Tuple<long, long>(long.Parse(parts[0]), long.Parse(parts[1])));
                
            }
            long fresh = 0;
            for (string? line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                long num = long.Parse(line);
                foreach(var range in ranges)
                {
                    if (num >= range.Item1 && num <= range.Item2)
                    {
                        fresh++;
                        break;
                    }
                }
            }
            Console.WriteLine(fresh);
            fresh = 0;
            points = points.Distinct().ToList();
            points.Sort();
            long lastVal = 0;
            int inside = 0;
            foreach (long p in points)
            {
                if (left.ContainsKey(p))
                {
                    if(inside == 0)
                        lastVal = p;
                    inside += left[p];
                }
                if(right.ContainsKey(p))
                {
                    inside -= right[p];
                    if (inside == 0)
                        fresh += (p - lastVal) + 1;
                }

                Console.WriteLine($"Point {p} inside {inside}");
            }
            Console.WriteLine(fresh);
            Console.WriteLine(inside);

        }
    }
}
