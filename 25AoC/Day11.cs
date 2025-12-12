using Microsoft.Z3;
namespace _25AoC
{
    internal class Day11
    {
        public static void Execute()
        {
            StreamReader sr = new StreamReader("Inputs/Input11.txt");
            Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
            string? line;
            while ((line = sr.ReadLine()) != null)
                map[line.Split(' ')[0][..^1]] = line.Split(' ')[1..].ToList();
            HashSet<string> visited = new HashSet<string>();
            Queue<string> toVisit = new Queue<string>();
            toVisit.Enqueue("svr");
            map["out"] = new List<string>();
            while (toVisit.Count > 0)
            {
                string current = toVisit.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                foreach (string neighbor in map[current])
                    toVisit.Enqueue(neighbor);
            }
            map = map.Where(kvp => visited.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Where(v => visited.Contains(v)).ToList());
            Dictionary<string, int> inputs = new();
            Dictionary<string, (long, long, long, long)> paths = new(); // x, dac, fft, dac+fft
            foreach (string node in map.Keys)
                inputs[node] = map.Values.Where(l => l.Contains(node)).Count();
            foreach (string node in map.Keys)
                paths[node] = (0,0,0,0);
            paths["svr"] = (1,0,0,0);
            paths["out"] = (0,0,0,0);
            HashSet<string> completed = new HashSet<string>();
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (string node in map.Keys)
                {
                    if (completed.Contains(node))
                        continue;
                    if (inputs[node] == 0)
                    {
                        foreach (string neighbor in map[node])
                        {
                            if(node == "dac")
                            {
                                var (x,dac,fft,dacfft) = paths[node];
                                var (nx,ndac,nfft,ndacfft) = paths[neighbor];
                                paths[neighbor] = (nx, x+dac+ndac,nfft,fft+dacfft+ndacfft);
                            }
                            if (node == "fft")
                            {
                                var (x, dac, fft, dacfft) = paths[node];
                                var (nx, ndac, nfft, ndacfft) = paths[neighbor];
                                paths[neighbor] = (nx, ndac, nfft + x + fft, ndacfft + dac + dacfft);
                            }
                            else
                            {
                                var (x, dac, fft, dacfft) = paths[node];
                                var (nx, ndac, nfft, ndacfft) = paths[neighbor];
                                paths[neighbor] = (nx + x, ndac + dac, nfft + fft, ndacfft + dacfft);
                            }
                        }
                        completed.Add(node);
                        changed = true;
                        foreach (string neighbor in map[node])
                            inputs[neighbor]--;
                    }
                }
            }
            Console.WriteLine(paths["out"]);
        }

        public static void ExecuteOld()
        {
            StreamReader sr = new StreamReader("Inputs/Input11.txt");
            Dictionary<string,List<string>> map = new Dictionary<string, List<string>>();
            string? line;
            while ((line = sr.ReadLine()) != null)
                map[line.Split(' ')[0][..^1]] = line.Split(' ')[1..].ToList();
            HashSet<string> visited = new HashSet<string>();
            Queue<string> toVisit = new Queue<string>();
            toVisit.Enqueue("you");
            map["out"] = new List<string>();
            while (toVisit.Count > 0)
            {
                string current = toVisit.Dequeue();
                if (visited.Contains(current))
                    continue;
                visited.Add(current);
                foreach (string neighbor in map[current])
                    toVisit.Enqueue(neighbor);
            }
            map = map.Where(kvp => visited.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Where(v => visited.Contains(v)).ToList());
            Dictionary<string, int> inputs = new(), paths = new();
            foreach (string node in map.Keys)
                inputs[node] = map.Values.Where(l => l.Contains(node)).Count();
            foreach (string node in map.Keys)
                paths[node] = 0;
            paths["you"] = 1;
            paths["out"] = 0;
            HashSet<string> completed = new HashSet<string>();
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (string node in map.Keys)
                {
                    if (completed.Contains(node))
                        continue;
                    if (inputs[node] == 0)
                    {
                        foreach (string neighbor in map[node])
                            paths[neighbor] += paths[node];
                        completed.Add(node);
                        changed = true;
                        foreach (string neighbor in map[node])
                            inputs[neighbor]--;
                    }
                }
            }
            Console.WriteLine(paths["out"]);
        }


    }
}
