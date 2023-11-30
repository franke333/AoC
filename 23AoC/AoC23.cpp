#include <iostream>
#include "Utility.h"
#include "Puzzle.h"
#include <cassert>
#include <format>

#include "AllIncludes.h"



#define PUZZLE 1 // 1 = Puzzle 1, 2 = Puzzle 2, ..., 25 = Puzzle 25
#define SOLUTION 1 // 1 = Basic, 2 = Advanced, 3 = Both


IPuzzle* GetPuzzle(int puzzle);

int main()
{
	auto puzzle = GetPuzzle(PUZZLE);
	ASSERTMR(puzzle != nullptr, std::format("No puzzle of number {} found!", PUZZLE),-1);

	if(SOLUTION & SOLUTIONENUM::Basic)
		puzzle->Solve();
	if (SOLUTION & SOLUTIONENUM::Advanced)
		puzzle->SolveAdvanced();

	delete puzzle;
	return 0;
}

IPuzzle* GetPuzzle(int puzzle) {
	switch (puzzle)
	{
	case 1:
		return createInstance<Puzzle1>();
	default:
		break;
	}
	return nullptr;
}

