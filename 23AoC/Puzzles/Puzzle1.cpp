#include "../Utility.h"
#include "../Puzzle.h"
#include <filesystem>
#include <map>

class Puzzle1 : IPuzzle {
private:
	std::vector<string> numbers{
		"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
	};
	int GetNumberAtPosition(const string& line, int pos) {
		int num = 0;
		if(line[pos] >= '0' && line[pos] <= '9')
			return line[pos] - '0';
		for (int j = 0; j < numbers.size(); j++)
			if (line.substr(pos, numbers[j].size()) == numbers[j])
				return j;
		return 0;
	}

public:
	void Solve() override
	{
		string path = "Inputs/Input1.txt";
		auto lines = Utility::ReadLines(path);
		int sum = 0;
		for (auto& line : lines)
		{
			for (int i = 0; i < line.size(); i++)
				if (line[i] >= '0' && line[i] <= '9') {
					sum += 10 * (line[i] - '0');
					break;
				}
			for (int i = line.size()-1; i > -1; i--)
				if (line[i] >= '0' && line[i] <= '9') {
					sum += (line[i] - '0');
					break;
				}
		}
		LOG2("Sum: ", sum);
		return;
	
	}
	void SolveAdvanced() override
	{
		
		string path = "Inputs/Input1.txt";
		auto lines = Utility::ReadLines(path);
		int sum = 0;
		for (auto& line : lines)
		{
			for (int i = 0; i < line.size(); i++) {
				if (int val = GetNumberAtPosition(line, i))
				{
					sum += 10 * val;
					break;
				}
			}
			for (int i = line.size() - 1; i > -1; i--) {
				if (int val = GetNumberAtPosition(line, i))
				{
					sum += val;
					break;
				}
			}
		}
		LOG2("Sum: ", sum);
		return;
	}
};
