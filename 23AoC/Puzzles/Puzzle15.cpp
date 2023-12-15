#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle15 : IPuzzle
{

	void Solve() override
	{
		size_t sum = 0;
		unsigned char hash = 0;
		for (auto &c : _inputLines[0])
		{
			if (c == ',')
			{
				sum += hash;
				hash = 0;
				continue;
			}
			hash += c;
			hash *= 17;
		}
		LOG("Solution: " << sum + hash)
	}
	void SolveAdvanced() override
	{
		std::vector<std::vector<std::pair<string, char>>> map(256);
		unsigned char hash = 0;
		size_t string_start = 0;
		for (size_t i = 0; i < _inputLines[0].size(); i++)
		{
			auto &c = _inputLines[0][i];
			if (c == '=')
			{
				auto ptr = std::find_if(map[hash].begin(), map[hash].end(), [&](auto &p)
										{ return p.first == _inputLines[0].substr(string_start, i - string_start); });
				if (ptr != map[hash].end())
					ptr->second = _inputLines[0][i + 1];
				else
					map[hash].push_back(std::make_pair(_inputLines[0].substr(string_start, i - string_start), _inputLines[0][i + 1]));
				hash = 0;
				i += 2;
				string_start = i + 1;
				continue;
			}
			if (c == '-')
			{
				auto ptr = std::find_if(map[hash].begin(), map[hash].end(), [&](auto &p)
										{ return p.first == _inputLines[0].substr(string_start, i - string_start); });
				if (ptr != map[hash].end())
					map[hash].erase(ptr);
				hash = 0;
				i += 1;
				string_start = i + 1;
				continue;
			}
			hash += c;
			hash *= 17;
		}
		size_t sum = 0;
		for (size_t box = 0; box < 256; box++)
		{
			if (map[box].size() == 0)
				continue;
			for (size_t i = 0; i < map[box].size(); i++)
				sum += (box + 1) * (i + 1) * (map[box][i].second - '0');
		}
		LOG("Solution: " << sum)
	}
};
