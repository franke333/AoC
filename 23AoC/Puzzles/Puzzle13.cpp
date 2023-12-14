#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle13 : IPuzzle
{
	class Map
	{
	public:
		std::vector<uint32_t> rows, cols;
		Map(const std::vector<string> &input)
		{
			rows = std::vector<uint32_t>(input.size());
			cols = std::vector<uint32_t>(input[0].size());
			for (size_t i = 0; i < input.size(); i++)
				for (size_t j = 0; j < input[i].size(); j++)
					if (input[i][j] == '#')
					{
						rows[i] |= 1 << j;
						cols[j] |= 1 << i;
					}
		}
		long symmetry()
		{
			for (auto &dir : {rows, cols})
				for (int i = 0; i < (int)dir.size() - 1; i++)
				{
					bool ok = true;
					for (int d = 0; d < std::min(i + 1, (int)dir.size() - i - 1); d++)
					{
						if (dir[i - d] != dir[i + 1 + d])
						{
							ok = false;
							break;
						}
					}
					if (ok)
						return (i + 1) * (dir == rows ? 100 : 1);
				}

			return 0;
		}

		bool isOneBitDifferent(int num1, int num2)
		{
			int xorResult = num1 ^ num2;

			int count = 0;
			while (xorResult > 0)
			{
				count += xorResult & 1;
				xorResult >>= 1;
			}

			return count == 1;
		}

		long smudgeSymmetry()
		{
			for (auto &dir : {rows, cols})
				for (int i = 0; i < (int)dir.size() - 1; i++)
				{
					bool ok = true;
					bool smudge = false;
					for (int d = 0; d < std::min(i + 1, (int)dir.size() - i - 1); d++)
					{
						if (dir[i - d] != dir[i + 1 + d] && smudge)
						{
							ok = false;
							break;
						}
						if (dir[i - d] != dir[i + 1 + d])
						{
							if (isOneBitDifferent(dir[i - d], dir[i + 1 + d]))
							{
								smudge = true;
							}
							else
							{
								ok = false;
								break;
							}
						}
					}
					if (ok && smudge)
					{
						return (i + 1) * (dir == rows ? 100 : 1);
					}
				}

			return 0;
		}
	};
	void Solve() override
	{
		int start = 0;
		long syms = 0;
		for (size_t i = 0; i < _inputLines.size(); i++)
		{
			if (_inputLines[i].empty())
			{
				syms += Map(std::vector<string>(_inputLines.begin() + start, _inputLines.begin() + i)).symmetry();
				start = i + 1;
			}
		}
		syms += Map(std::vector<string>(_inputLines.begin() + start, _inputLines.end())).symmetry();
		LOG(syms);
	}
	void SolveAdvanced() override
	{
		int start = 0;
		long syms = 0;
		for (size_t i = 0; i < _inputLines.size(); i++)
		{
			if (_inputLines[i].empty())
			{
				syms += Map(std::vector<string>(_inputLines.begin() + start, _inputLines.begin() + i)).smudgeSymmetry();
				start = i + 1;
			}
		}
		syms += Map(std::vector<string>(_inputLines.begin() + start, _inputLines.end())).smudgeSymmetry();
		LOG(syms);
	}
};
