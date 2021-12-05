using System;

namespace Puzzle1
{
    
    class Program
    {
        static PuzzleType activePuzzle = PuzzleType.Vents2;
        enum PuzzleType
        {
            ScanDepth,ScanDepth2,
            SubTravel, SubTravel2,
            Diagnose1,Diagnose2,
            Bingo1,Bingo2,
            Vents1, Vents2,


        }
        static void Main(string[] args)
        {
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
                default:
                    break;
            }
        }
    }
}
