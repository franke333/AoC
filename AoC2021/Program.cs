using AoC2021;
using System;

namespace Puzzle1
{
    
    class Program
    {
        static PuzzleType activePuzzle = PuzzleType.Segment2;
        enum PuzzleType
        {
            ScanDepth,ScanDepth2,
            SubTravel, SubTravel2,
            Diagnose1,Diagnose2,
            Bingo1,Bingo2,
            Vents1, Vents2,
            Lanternfish1,LanternFish2,
            Crabs1,Crabs2,
            Segment1,Segment2

        }
        static void Main(string[] args)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            switch (activePuzzle)
            {
                case PuzzleType.ScanDepth:
                    Puzzle1.SolvePuzzle();
                    break;
                case PuzzleType.ScanDepth2:
                    Puzzle1.SolvePuzzle2();
                    break;
                case PuzzleType.SubTravel:
                    Puzzle2.SolvePuzzle();
                    break;
                case PuzzleType.SubTravel2:
                    Puzzle2.SolvePuzzle2();
                    break;
                case PuzzleType.Diagnose1:
                    Puzzle3.SolvePuzzle();
                    break;
                case PuzzleType.Diagnose2:
                    Puzzle3.SolvePuzzle2();
                    break;
                case PuzzleType.Bingo1:
                    Puzzle4.SolvePuzzle();
                    break;
                case PuzzleType.Bingo2:
                    Puzzle4.SolvePuzzle2();
                    break;
                case PuzzleType.Vents1:
                    Puzzle5.SolvePuzzle();
                    break;
                case PuzzleType.Vents2:
                    Puzzle5.SolvePuzzle2();
                    break;
                case PuzzleType.Lanternfish1:
                    Puzzle6.SolvePuzzle();
                    break;
                case PuzzleType.LanternFish2:
                    Puzzle6.SolvePuzzle2();
                    break;
                case PuzzleType.Crabs1:
                    Puzzle7.SolvePuzzle();
                    break;
                case PuzzleType.Crabs2:
                    Puzzle7.SolvePuzzle2();
                    break;
                case PuzzleType.Segment1:
                    Puzzle8.SolvePuzzle();
                    break;
                case PuzzleType.Segment2:
                    Puzzle8.SolvePuzzle2();
                    break;
                default:
                    break;
            }
            watch.Stop();
            Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
        }
    }
}
