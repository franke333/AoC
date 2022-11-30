using System;

namespace AoC2022
{

    class Program
    {
        static PuzzleType activePuzzle = ;
        enum PuzzleType
        {

        }
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            switch (activePuzzle)
            {
                default:
                    break;
            }
            watch.Stop();
            Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
        }
    }
}
