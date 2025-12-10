using Microsoft.Z3;
namespace _25AoC
{
    internal class Day10
    {

        public static void Execute()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            StreamReader sr = new StreamReader("Inputs/Input10.txt");
            long sum = 0;
            long sumAdv = 0;
            for (string? line = sr.ReadLine(); line != null; line = sr.ReadLine())
            {
                List<List<int>> buttons = new();
                foreach(string butt in line.Split(' ')[1..^1])
                    buttons.Add(butt[1..^1].Split(',').Select(x => int.Parse(x)).ToList());
                // Part 1
                bool[] target = line.Split(' ')[0][1..^1].Select(x => x== '#').ToArray();
                int leastPushes = 999;
                for (int i = 0; i < (1 << buttons.Count); i++)
                {
                    int pushes = 0;
                    int[] toggles = new int[target.Length];
                    for (int j = 0; j < buttons.Count; j++)
                        if((i & (1 << j)) != 0)
                        {
                            pushes++;
                            foreach (var but in buttons[j])
                                toggles[but] += 1;
                        }
                    if (pushes >= leastPushes)
                        continue;
                    bool match = true;
                    for (int k = 0; k < target.Length; k++)
                        if ((toggles[k] % 2 == 1) != target[k])
                            match = false;
                    if (match)
                        leastPushes = pushes;
                }
                sum += leastPushes;

                // Part 2
                int[] targetAdv = line.Split(' ')[^1][1..^1].Split(',').Select(x => int.Parse(x)).ToArray();
                int leastPushesAdv = 999;
                using (var ctx = new Context())
                {
                    Solver solver = ctx.MkSolver();
                    IntExpr[] buttonConst = new IntExpr[buttons.Count];
                    for (int i = 0; i < buttons.Count; i++)
                        solver.Add(ctx.MkGe(buttonConst[i] = ctx.MkIntConst($"b{i}"), ctx.MkInt(0)));
                    for (int i = 0; i < targetAdv.Length; i++)
                        solver.Add(ctx.MkEq(ctx.MkAdd(buttons.Select((b, idx) => (b, idx)).Where(x => x.b.Contains(i)).Select(x => buttonConst[x.idx])), ctx.MkInt(targetAdv[i])));
                    while (solver.Check() == Status.SATISFIABLE)
                    {
                        Model model = solver.Model;
                        int thisPushes = buttonConst.Sum(bc => ((IntNum)model.Evaluate(bc)).Int);
                        if(thisPushes < leastPushesAdv)
                            leastPushesAdv = thisPushes;
                        solver.Add(ctx.MkNot(ctx.MkEq(ctx.MkAdd(buttonConst), ctx.MkInt(thisPushes))));
                    }
                }
                sumAdv += leastPushesAdv;
            }
            stopwatch.Stop();
            Console.WriteLine(sum);
            Console.WriteLine(sumAdv);
            Console.WriteLine($"Time: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
