#include "../Utility.h"
#include "../Puzzle.h"
#include <map>
#include <array>

class Puzzle2 : IPuzzle {
	std::map<std::string, int> maxes{
			{"red", 12},{"green", 13},{"blue", 14},
		};
	std::map<std::string, std::string> colors{
			{"red", "red"},{"red,", "red"},{"red;", "red"},
			{"green", "green"},{"green,", "green"},{"green;", "green"},
			{"blue", "blue"},{"blue,", "blue"},{"blue;", "blue"},
		};
		
	void Solve() override
	{
		int sum = 0;
		for(auto& line : _inputWords)
		{
			bool game_ok = true;
			for (size_t i = 2; i < line.size(); i+=2)
				if(std::stoi(line[i]) > maxes[colors[line[i + 1]]])
					game_ok = false;
			if(game_ok)
				sum += std::stoi(line[1].substr(0, line[1].size() - 1));
		}
		LOG2("Sum: ", sum);
	}

	void SolveAdvanced() override
	{
		int sum = 0;
		for(auto& line : _inputWords){
			std::array<int,3> a  = {0,0,0};
			for (size_t i = 2; i < line.size(); i+=2)
				a[maxes[colors[line[i + 1]]]-12] = std::max(a[maxes[colors[line[i + 1]]]-12], std::stoi(line[i]));
			sum += a[0] * a[1] * a[2];
		}
		LOG2("Sum: ", sum);

	}
};
