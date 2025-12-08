using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day8
    {
        public static void Execute()
        {
            StreamReader sr = new StreamReader("Inputs/Input8.txt");
            List<(long, long, long)> lights = new();
            for(var line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                var coords = line.Split(",").Select(x => long.Parse(x)).ToArray();
                lights.Add((coords[0], coords[1], coords[2]));
            }
            List<(int, int, double)> distances = new(); //x,y,distance
            for(int i = 0; i < lights.Count; i++)
            {
                for(int j = i + 1; j < lights.Count; j++)
                {
                    var dx = lights[i].Item1 - lights[j].Item1;
                    var dy = lights[i].Item2 - lights[j].Item2;
                    var dz = lights[i].Item3 - lights[j].Item3;
                    var distance = Math.Sqrt(dx * dx + dy * dy + dz * dz);
                    distances.Add((i, j, distance));
                }
            }
            distances = distances.OrderBy(x => x.Item3).ToList().Take(1000).ToList();
            bool[] inCirc = new bool[distances.Count];
            List<int> circSizes = new();
            for(int i = 0; i < lights.Count; i++)
            {
                if (inCirc[i]) 
                    continue;
                var count = 0;
                Queue<int> toCheck = new();
                toCheck.Enqueue(i);
                while (toCheck.Count > 0)
                {
                    int ii = toCheck.Dequeue();
                    if (inCirc[ii]) continue;
                    for (int j = 0; j < lights.Count; j++)
                    {
                        if (j == ii || inCirc[j])
                            continue;
                        var ind = j > ii ? distances.FindIndex(k => k.Item1 == ii && k.Item2 == j) :
                            distances.FindIndex(k => k.Item1 == j && k.Item2 == ii);
                        if (ind !=-1)
                            toCheck.Enqueue(j);
                    }
                    inCirc[ii] = true;
                    count++;
                }
                circSizes.Add(count);
            }
            circSizes = circSizes.OrderByDescending(x => x).ToList();
            Console.WriteLine(circSizes[0] * circSizes[1] * circSizes[2]);
            Console.WriteLine($"{circSizes[0]}, {circSizes[1]}, {circSizes[2]}");
            Console.WriteLine(circSizes.Sum());
        }
    }
}
