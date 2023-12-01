#include <iostream>
#include "Utility.h"
#include "Puzzle.h"
#include <cassert>
#include <format>

#include "Stopwatch.h"
#include "AllIncludes.h"

#define PUZZLE 0   // 0 = Today, 1 = Puzzle 1, 2 = Puzzle 2, ..., 25 = Puzzle 25
#define SOLUTION 3 // 1 = Basic, 2 = Advanced, 3 = Both

IPuzzle *GetPuzzle(int puzzle);

int main(int argc, char *argv[])
{
	// parse argc
	int puzzleDay = argc > 1 ? std::stoi(argv[1]) : PUZZLE;
	int solution = argc > 2 ? std::stoi(argv[2]) : SOLUTION;

	auto puzzle = GetPuzzle(puzzleDay);
	ASSERTMR(puzzle != nullptr, std::format("No puzzle of number {} found!", PUZZLE), -1);

	

	if (solution & SOLUTIONENUM::Basic)
	{
		
		LOG("Basic solution:");
		auto sw = Stopwatch();
		puzzle->Solve();
		LOG("Elapsed time: " <<  sw.elapsed() << "ms" );
	}
	if (solution & SOLUTIONENUM::Advanced)
	{
		LOG("Advanced solution:");
		auto sw = Stopwatch();
		puzzle->SolveAdvanced();
		LOG("Elapsed time: " <<  sw.elapsed() << "ms" );
	}

	delete puzzle;
	return 0;
}

IPuzzle *GetPuzzle(int puzzle)
{
	if (puzzle == 0)
	{
		// get todays date
		auto now = std::chrono::system_clock::now();
		auto in_time_t = std::chrono::system_clock::to_time_t(now);
		std::tm buf;
		localtime_s(&buf, &in_time_t);
		puzzle = buf.tm_mday;
	}

	switch (puzzle)
	{
	case 1:
		return createInstance<Puzzle1>();
	case 2:
		// return createInstance<Puzzle2>();
	default:
		break;
	}
	return nullptr;
}
