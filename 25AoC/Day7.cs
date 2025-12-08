using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25AoC
{
    internal class Day7
    {
        public static void Execute()
        {
            StreamReader sr = new StreamReader("inputs/Input7.txt");
            string? line;
            List<HashSet<int>> splitters = new();
            Dictionary<int, long> beams = new();
            
            line = sr.ReadLine();
            beams[line.IndexOf('S')] = 1;
            for (line = sr.ReadLine(); line != null; line = sr.ReadLine())
                splitters.Add(line.Select((c,i) => (c,i)).Where(t => t.c == '^').Select(t => t.i).ToHashSet());

            int splits = 0;
            for (int i = 0; i < splitters.Count; i++)
            {
                Dictionary<int, long> nextBeam = new();
                foreach (var beam in beams)
                    if (splitters[i].Contains(beam.Key))
                    {
                        nextBeam[beam.Key - 1] = nextBeam.GetValueOrDefault(beam.Key - 1, 0) + beam.Value;
                        nextBeam[beam.Key + 1] = nextBeam.GetValueOrDefault(beam.Key + 1, 0) + beam.Value;
                        splits++;
                    }
                    else
                        nextBeam[beam.Key] = nextBeam.GetValueOrDefault(beam.Key, 0) + beam.Value;
                beams = nextBeam;

            }
            Console.WriteLine(splits + " " + beams.Values.Sum());
        }
    }
}
