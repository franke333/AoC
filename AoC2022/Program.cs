using System;

namespace AoC2022
{

    class Program
    {
        static PuzzleType activePuzzle = PuzzleType.Calories;
        enum PuzzleType
        {
            Calories
        }
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            switch (activePuzzle)
            {
                case PuzzleType.Calories:
                    Puzzle1.SolvePuzzle();
                    break;
                default:
                    break;
            }
            watch.Stop();
            Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
        }
    }
}
