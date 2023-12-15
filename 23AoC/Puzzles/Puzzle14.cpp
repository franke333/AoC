#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle14 : IPuzzle
{
	void Solve() override
	{
		std::vector<uint32_t> rocks;
		std::vector<int> holes(_inputLines[0].size(), 0);
		for (size_t i = 0; i < _inputLines.size(); ++i)
			for (size_t j = 0; j < _inputLines[i].size(); ++j)
				if (_inputLines[i][j] == '#')
					holes[j] = i + 1;
				else if (_inputLines[i][j] == 'O')
					rocks.push_back(holes[j]++);
		LOG(std::accumulate(rocks.begin(), rocks.end(), 0, [&](int a, int b)
							{ return a + _inputLines.size() - b; }));
	}

	// --------ADVANCED------------

	size_t totalLoad(const std::vector<std::vector<char>> &map)
	{
		size_t size = map[0].size();
		long count = 0;
		for (size_t i = 0; i < _inputLines.size(); ++i)
			for (size_t j = 0; j < _inputLines[i].size(); ++j)
				if (map[i][j] == 'O')
					count += size - i;
		return count;
	}

	bool hasRepeatingPattern(const std::vector<size_t> &sequence, int patternLength)
	{
		int n = sequence.size();
		for (int j = 0; j < patternLength; j++)
			if (sequence[n - j - 2 * patternLength] != sequence[n - j - patternLength])
				return false;
		return true;
	}

	int findRepeatingPattern(const std::vector<size_t> &sequence)
	{
		for (int patternLength = 2; patternLength <= 100; patternLength++)
			if (hasRepeatingPattern(sequence, patternLength))
				return patternLength;
		return -1;
	}

	void SolveAdvanced() override
	{
		size_t size = _inputLines[0].size();
		std::vector<std::vector<char>> map(_inputLines.size(), std::vector<char>(_inputLines[0].size(), '.'));
		for (size_t i = 0; i < _inputLines.size(); ++i)
			for (size_t j = 0; j < _inputLines[i].size(); ++j)
				map[i][j] = _inputLines[i][j];
		std::vector<size_t> totals;
		for (size_t c = 0; c < 300; c++)
		{
			// N
			std::vector<size_t> holes(_inputLines.size(), 0);
			for (size_t i = 0; i < _inputLines.size(); ++i)
				for (size_t j = 0; j < _inputLines[i].size(); ++j)
					if (map[i][j] == '#')
						holes[j] = i + 1;
					else if (map[i][j] == 'O')
					{
						map[i][j] = '.';
						map[holes[j]++][j] = 'O';
					}
			// W
			holes = std::vector<size_t>(_inputLines.size(), 0);
			for (size_t i = 0; i < _inputLines.size(); ++i)
				for (size_t j = 0; j < _inputLines[i].size(); ++j)
					if (map[j][i] == '#')
						holes[j] = i + 1;
					else if (map[j][i] == 'O')
					{
						map[j][i] = '.';
						map[j][holes[j]++] = 'O';
					}
			// S
			holes = std::vector<size_t>(_inputLines.size(), size - 1);
			for (size_t i = 0; i < _inputLines.size(); ++i)
				for (size_t j = 0; j < _inputLines[i].size(); ++j)
					if (map[size - i - 1][j] == '#')
						holes[j] = size - i - 2;
					else if (map[size - i - 1][j] == 'O')
					{
						map[size - i - 1][j] = '.';
						map[holes[j]--][j] = 'O';
					}
			// E
			holes = std::vector<size_t>(_inputLines.size(), size - 1);
			for (size_t i = 0; i < _inputLines.size(); ++i)
				for (size_t j = 0; j < _inputLines[i].size(); ++j)
					if (map[j][size - i - 1] == '#')
						holes[j] = size - i - 2;
					else if (map[j][size - i - 1] == 'O')
					{
						map[j][size - i - 1] = '.';
						map[j][holes[j]--] = 'O';
					}
			totals.push_back(totalLoad(map));
		}
		auto cycleLength = findRepeatingPattern(totals);
		auto start = totals.size() - cycleLength;
		LOG(totals[start + ((size_t)1e9 - 1 - start) % cycleLength]);
	}
};
