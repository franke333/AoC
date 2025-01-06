#include <iostream>
#include "Utility.h"
#include "Puzzle.h"
#include <cassert>
#include <format>

#include "Stopwatch.h"
#include "AllIncludes.h"

#define PUZZLE 0   // 0 = Today, 1 = Puzzle 1, 2 = Puzzle 2, ..., 25 = Puzzle 25
#define SOLUTION 3 // 1 = Basic, 2 = Advanced, 3 = Both

IPuzzle *GetPuzzle(int puzzle, bool& byLines, bool& byWords);
int GetToday();

int main(int argc, char *argv[])
{
	// parse argc
	int puzzleDay = argc > 1 ? std::stoi(argv[1]) : PUZZLE;
	puzzleDay = puzzleDay == 0 ? GetToday() : puzzleDay;
	int solution = argc > 2 ? std::stoi(argv[2]) : SOLUTION;
	
	bool inputByLines = true, inputByWords = true;
	auto puzzle = GetPuzzle(puzzleDay,inputByLines,inputByWords);
	ASSERTMR(puzzle != nullptr, std::format("No puzzle of number {} found!", PUZZLE), -1);
	puzzle->LoadInput(std::format("Inputs/Input{}.txt", puzzleDay), inputByLines, inputByWords);

	if (solution & SOLUTIONENUM::Basic)
	{

		LOG("Basic solution:");
		auto sw = Stopwatch();
		puzzle->Solve();
		auto time = sw.elapsed();
		LOG("Elapsed time: " << time << "ms");
	}
	if (solution & SOLUTIONENUM::Advanced)
	{
		LOG("Advanced solution:");
		auto sw = Stopwatch();
		puzzle->SolveAdvanced();
		auto time = sw.elapsed();
		LOG("Elapsed time: " << time << "ms");
	}

	delete puzzle;
	return 0;
}

IPuzzle *GetPuzzle(int puzzle, bool& byLines, bool& byWords)
{
	switch (puzzle)
	{
	case 1:
		return createInstance<Puzzle1>();
	case 2:
		return createInstance<Puzzle2>();
	case 3:
		return createInstance<Puzzle3>();
	case 4:
		return createInstance<Puzzle4>();
	case 5:
		return createInstance<Puzzle5>();
	case 6:
		return createInstance<Puzzle6>();
	case 7:
		return createInstance<Puzzle7>();
	case 8:
		return createInstance<Puzzle8>();
	case 9:
		return createInstance<Puzzle9>();
	case 10:
		return createInstance<Puzzle10>();
	case 11:
		return createInstance<Puzzle11>();
	case 12:
		return createInstance<Puzzle12>();
	case 13:
		return createInstance<Puzzle13>();
	case 14:
		return createInstance<Puzzle14>();
	case 15:
		return createInstance<Puzzle15>();
	case 16:
		return createInstance<Puzzle16>();
	case 17:
		return createInstance<Puzzle17>();
	case 18:
		return createInstance<Puzzle18>();
	case 19:
		return createInstance<Puzzle19>();
	case 20:
		return createInstance<Puzzle20>();
	case 21:
		return createInstance<Puzzle21>();
	case 22:
		return createInstance<Puzzle22>();
	case 23:
		return createInstance<Puzzle23>();
	case 24:
		return createInstance<Puzzle24>();
	case 25:
		return createInstance<Puzzle25>();
	default:
		break;
	}
	return nullptr;
}

int GetToday()
{
	time_t now = time(0);
	tm *ltm = localtime(&now);
	return ltm->tm_mday;
}