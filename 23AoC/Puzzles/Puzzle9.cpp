#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle9 : IPuzzle
{
	class BC
	{
	public:
		std::vector<std::vector<int>> _bc;
		BC(int depth)
		{
			_bc.resize(depth);
			for (size_t i = 0; i < _bc.size(); i++)
				_bc[i].resize(i + 1, 1);
			for (int i = 0; i < depth; i++)
				for (int j = 1; j < i; j++)
					_bc[i][j] = _bc[i - 1][j - 1] + _bc[i - 1][j];
		}
		int operator()(int i, int j)
		{
			return _bc[i][j];
		}
	};
	void Solve() override
	{
		BC bc(30);
		long long result1 = 0, result2 = 0;
		for (auto &line : _inputWords)
		{
			int depth = line.size();
			for (int i = 0; i < depth; i++)
			{
				result1 += bc(depth, i) * std::stoll(line[i]) * (((depth + i) % 2) * 2 - 1);
				result2 += bc(depth, i + 1) * std::stoll(line[i]) * (((depth + i) % 2) * 2 - 1);
			}
		}
		LOG("Result1: " << result1);
		LOG("Result2: " << result2);
	}
	void SolveAdvanced() override
	{
		LOG("Result in base solution");
	}
};