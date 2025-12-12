using Microsoft.Z3;
namespace _25AoC
{
    internal class Day12
    {
        public static void Execute()
        {
            StreamReader sr = new StreamReader("Inputs/Input12.txt");
            for (int i = 0; i < 30; i++) sr.ReadLine();
            int correct = 0; string? line;
            while((line = sr.ReadLine()) != null)
                if (line.Split(' ')[1..].Select(x => int.Parse(x)).Sum() <= (int.Parse(line.Split(' ')[0][0..2]) / 3) * (int.Parse(line.Split(' ')[0][3..5]) / 3))
                    correct++;
            Console.WriteLine(correct);
        }


    }
}
