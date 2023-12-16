#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle16 : IPuzzle
{

	std::pair<int, int> getD(int i)
	{
		return {(i%2)*(2-i),((i+1)%2)*(-1+i)};
	}
	size_t spos = 0;
	int sdir = 1;
	bool print = true;
	size_t result = 0;
	void Solve() override
	{
		size_t size = _inputLines[0].length();
		std::unordered_set<size_t> positions;
		std::stack<std::pair<size_t, int>> stack;
		stack.push({this->spos, this->sdir});
		while (!stack.empty())
		{
			auto [pos, dir] = stack.top();
			stack.pop();
			auto x = pos % size, y = pos / size;
			bool pop = false;
			while (!(pop))
			{
				switch (_inputLines[y][x])
				{
				case '/':
					dir = (dir + 1) % 2 + 2 * (dir / 2);
					break;
				case '\\':
					dir = (dir + 1) % 2 + 2 * (1 - dir / 2);
					break;
				case '-':
					if (dir == 0 || dir == 2)
					{
						if (positions.find(y * size + x) == positions.end())
						{
							stack.push({y * size + x, 1});
							stack.push({y * size + x, 3});
						}
						pop = true;
					}
					break;
				case '|':

					if (dir == 1 || dir == 3)
					{
						if (positions.find(y * size + x) == positions.end())
						{
							stack.push({y * size + x, 0});
							stack.push({y * size + x, 2});
						}
						pop = true;
					}
					break;
				default:
					break;
				}
				positions.insert(y * size + x);
				if ((dir == 0 && y == 0) || (dir == 1 && x == size - 1) || (dir == 2 && y == size - 1) || (dir == 3 && x == 0))
					break;
				auto [dx, dy] = getD(dir);
				x += dx;
				y += dy;
			}
		}
		if (print)
			LOG("Result " << positions.size());
		this->result = std::max(this->result, positions.size());
	}
	void SolveAdvanced() override
	{
		size_t size = _inputLines[0].length();
		this->print = false;
		for (size_t i = 0; i < size; i++)
		{
			this->spos = size * size - 1 - i;
			this->sdir = 0;
			Solve();
			this->spos = i * size;
			this->sdir = 1;
			Solve();
			this->spos = i;
			this->sdir = 2;
			Solve();
			this->spos = size - 1 + i * size;
			this->sdir = 3;
			Solve();
		}
		LOG("Result " << this->result);
	}
};
