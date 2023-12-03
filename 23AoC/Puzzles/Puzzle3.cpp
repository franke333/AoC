#include "../Utility.h"
#include "../Puzzle.h"

#include <unordered_set>

class Puzzle3 : IPuzzle
{
	void ProcessInput(std::unordered_set<size_t> &symbol_positions,
					  std::vector<std::tuple<int, size_t>> &numbers_at_positions, bool gears = false)
	{
		size_t width = _inputLines[0].size() + 2;
		for (size_t i = 0; i < _inputLines.size(); i++)
		{
			bool is_number = false;
			int num = 0;
			for (size_t j = 0; j < _inputLines[i].size(); j++)
			{
				if (_inputLines[i][j] >= '0' && _inputLines[i][j] <= '9')
				{
					is_number = true;
					num = num * 10 + _inputLines[i][j] - '0';
				}
				else if (is_number)
				{
					numbers_at_positions.push_back({num, (i + 1) * width + j + 1 - std::to_string(num).size()});
					is_number = false;
					num = 0;
				}

				if ((!gears && _inputLines[i][j] != '.') || (gears && _inputLines[i][j] == '*'))
					symbol_positions.insert((i + 1) * width + j + 1);
			}
			if (is_number)
				numbers_at_positions.push_back({num, (i + 1) * width + _inputLines[i].size() + 1 - std::to_string(num).size()});
		}
	}

	bool IsNextToSymbol(std::tuple<int, size_t> &num_at_pos, std::unordered_set<size_t> &symbol_positions)
	{
		auto &[num, pos] = num_at_pos;
		int width = _inputLines[0].size() + 2;
		size_t num_size = std::to_string(num).size();
		for (size_t i = 0; i < num_size + 2; i++)
			if (symbol_positions.contains(pos - 1 - width + i) || symbol_positions.contains(pos - 1 + width + i))
				return true;
		return symbol_positions.contains(pos - 1) || symbol_positions.contains(pos + num_size);
	}

	void Solve() override
	{
		std::unordered_set<size_t> symbol_pos;
		std::vector<std::tuple<int, size_t>> numbers_at_pos;

		ProcessInput(symbol_pos, numbers_at_pos);

		long sum = 0;
		for (auto &num_at_pos : numbers_at_pos)
			if (IsNextToSymbol(num_at_pos, symbol_pos))
				sum += std::get<0>(num_at_pos);

		LOG2("Sum: ", sum);
	}

	void AssignNumToGears(std::tuple<int, size_t> &num_at_pos, std::map<size_t, std::vector<int>> &symbol_positions)
	{
		auto &[num, pos] = num_at_pos;
		int width = _inputLines[0].size() + 2;
		size_t num_size = std::to_string(num).size();
		for (size_t i = 0; i < num_size + 2; i++)
			if (symbol_positions.contains(pos - 1 - width + i))
				symbol_positions[pos - 1 - width + i].push_back(num);
			else if (symbol_positions.contains(pos - 1 + width + i))
				symbol_positions[pos - 1 + width + i].push_back(num);
		if (symbol_positions.contains(pos - 1))
			symbol_positions[pos - 1].push_back(num);
		else if (symbol_positions.contains(pos + num_size))
			symbol_positions[pos + num_size].push_back(num);
		return;
	}

	void SolveAdvanced() override
	{
		std::unordered_set<size_t> gears_pos;
		std::vector<std::tuple<int, size_t>> numbers_at_pos;

		ProcessInput(gears_pos, numbers_at_pos, true);
		std::map<size_t, std::vector<int>> gears_numbers;
		for (auto &gear : gears_pos)
			gears_numbers[gear] = {};
		long sum = 0;
		for (auto &num_at_pos : numbers_at_pos)
			AssignNumToGears(num_at_pos, gears_numbers);

		for (auto &[_, numbers] : gears_numbers)
			if (numbers.size() == 2)
				sum += numbers[0] * numbers[1];

		LOG2("Sum: ", sum);
	}
};