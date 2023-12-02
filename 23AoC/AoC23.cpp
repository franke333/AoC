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
		LOG("Elapsed time: " << sw.elapsed() << "ms");
	}
	if (solution & SOLUTIONENUM::Advanced)
	{
		LOG("Advanced solution:");
		auto sw = Stopwatch();
		puzzle->SolveAdvanced();
		LOG("Elapsed time: " << sw.elapsed() << "ms");
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