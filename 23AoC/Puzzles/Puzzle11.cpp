#include "../Utility.h"
#include "../Puzzle.h"

#include <unordered_set>
#include <map>

#define Point std::pair<int, int>

class Puzzle11 : IPuzzle
{
	void Solve() override
	{
		std::vector<Point> galaxies;
		std::unordered_set<int> rows, columns;

		for (int i = 0; i < (int)_inputLines.size(); i++)
			for (int j = 0; j < (int)_inputLines[i].size(); j++)
				if (_inputLines[i][j] == '#')
				{
					galaxies.push_back({i, j});
					rows.insert(i);
					columns.insert(j);
				}
		std::map<int, int> rowMap, columnMap;
		int expansion = 0;
		for (int i = 0; i < (int)_inputLines.size(); i++)
		{
			if (rows.contains(i))
				rowMap[i] = i + expansion;
			else
				expansion++;
		}
		expansion = 0;
		for (int i = 0; i < (int)_inputLines[0].size(); i++)
		{
			if (columns.contains(i))
				columnMap[i] = i + expansion;
			else
				expansion++;
		}
		size_t length = 0;
		for (int i = 0; i < (int)galaxies.size(); i++)
		{
			auto &[x, y] = galaxies[i];
			for (int j = i + 1; j < (int)galaxies.size(); j++)
			{
				auto &[x2, y2] = galaxies[j];
				length += std::abs(rowMap[x2] - rowMap[x]) + std::abs(columnMap[y2] - columnMap[y]);
			}
		}
		LOG(length);
	}
	void SolveAdvanced() override
	{
		std::vector<Point> galaxies;
		std::unordered_set<int> rows, columns;

		for (int i = 0; i < (int)_inputLines.size(); i++)
			for (int j = 0; j < (int)_inputLines[i].size(); j++)
				if (_inputLines[i][j] == '#')
				{
					galaxies.push_back({i, j});
					rows.insert(i);
					columns.insert(j);
				}
		std::map<int, int> rowMap, columnMap;
		int expansion = 0;
		for (int i = 0; i < (int)_inputLines.size(); i++)
		{
			if (rows.contains(i))
				rowMap[i] = i + expansion;
			else
				expansion += 1000000 - 1;
		}
		expansion = 0;
		for (int i = 0; i < (int)_inputLines[0].size(); i++)
		{
			if (columns.contains(i))
				columnMap[i] = i + expansion;
			else
				expansion += 1000000 - 1;
		}
		size_t length = 0;
		for (int i = 0; i < (int)galaxies.size(); i++)
		{
			auto &[x, y] = galaxies[i];
			for (int j = i + 1; j < (int)galaxies.size(); j++)
			{
				auto &[x2, y2] = galaxies[j];
				length += std::abs(rowMap[x2] - rowMap[x]) + std::abs(columnMap[y2] - columnMap[y]);
			}
		}
		LOG(length);
	}
};
