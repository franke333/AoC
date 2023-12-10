#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle10 : IPuzzle
{
	class Map
	{
	public:
		std::vector<std::vector<char>> _map;
		//.1.
		// 3x5
		//.7.
		std::map<char, std::vector<int>> _pipes{
			{'|', {1, 7}},
			{'-', {3, 5}},
			{'L', {1, 5}},
			{'7', {3, 7}},
			{'F', {5, 7}},
			{'J', {1, 3}},
			{'S', {1, 3, 5, 7}},
		};
		std::pair<int, int> _start;
		void CreateMap(const std::vector<string> &input)
		{
			_map.resize(input.size() * 3 + 2);
			for (size_t i = 0; i < _map.size(); i++)
				_map[i].resize(input[0].size() * 3 + 2, ' ');
			for (size_t i = 0; i < input.size(); i++)
				for (size_t j = 0; j < input[i].size(); j++)
					if (input[i][j] != '.')
					{
						if (input[i][j] == 'S')
							_start = {i * 3 + 2, j * 3 + 2};
						_map[i * 3 + 2][j * 3 + 2] = '.';
						for (auto &p : _pipes[input[i][j]])
							_map[i * 3 + p / 3 + 1][j * 3 + 1 + p % 3] = '.';
					}
		};

		void print()
		{
			for (size_t i = 0; i < _map.size(); i++)
			{
				for (size_t j = 0; j < _map[i].size(); j++)
					std::cout << _map[i][j];
				std::cout << std::endl;
			}
		}

		size_t TraverseMap()
		{
			size_t path = 0;
			std::stack<std::pair<int, int>> stack;
			stack.push(_start);
			while (!stack.empty())
			{
				auto [x, y] = stack.top();
				stack.pop();
				if (_map[x][y] == '.')
				{
					path++;
					_map[x][y] = '#';
					stack.push({x + 1, y});
					stack.push({x - 1, y});
					stack.push({x, y + 1});
					stack.push({x, y - 1});
				}
			}
			return (path - 2) / 6; //-5 from S tile, /3 from 3x3 tiles /2 from 2 directions , +3 for common end point
		}

		void FillMap()
		{
			std::stack<std::pair<int, int>> stack;
			int w = _map[0].size() - 1, h = _map.size() - 1;
			stack.push({1, 1});
			while (!stack.empty())
			{
				auto [x, y] = stack.top();
				stack.pop();
				if (x == 0 || x == h || y == 0 || y == w)
					continue;
				if (_map[x][y] == ' ' || _map[x][y] == '.')
				{
					_map[x][y] = 'O';
					stack.push({x + 1, y});
					stack.push({x - 1, y});
					stack.push({x, y + 1});
					stack.push({x, y - 1});
				}
			}
		}

		size_t EmptyTiles()
		{
			size_t tiles = 0;
			for (size_t i = 0; i < _map.size() / 3; i++)
				for (size_t j = 0; j < _map[i].size() / 3; j++)
				{
					bool empty = true;
					for (size_t k = 0; k < 3; k++)
						for (size_t l = 0; l < 3; l++)
							if (_map[i * 3 + k + 1][j * 3 + l + 1] != ' ' && _map[i * 3 + k + 1][j * 3 + l + 1] != '.')
								empty = false;
					tiles += empty;
				}
			return tiles;
		}
	};

	void Solve() override
	{
		auto map = Map();
		map.CreateMap(_inputLines);
		LOG("Path length: " << map.TraverseMap());
		map.FillMap();
		LOG("Empty tiles: " << map.EmptyTiles());
		// map.print();
	}
	void SolveAdvanced() override
	{
		LOG("in base solve")
	}
};
