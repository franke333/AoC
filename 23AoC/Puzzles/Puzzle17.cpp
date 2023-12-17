#include "../Utility.h"
#include "../Puzzle.h"

#include <set>
#include <unordered_set>

class Puzzle17 : IPuzzle
{
	class Node
	{
	public:
		int heat;
		int x, y;
		bool vertical;
		size_t hash() const
		{
			return (x + 1000 * y) * 2 + vertical;
		}
	};
	struct LessHeat
	{
		bool operator()(const Node &a, const Node &b) const
		{
			return a.heat < b.heat;
		}
	};

	int max = 3, min = 0;

	void Solve() override
	{
		std::multiset<Node, LessHeat> nodes;
		std::unordered_set<size_t> visited;
		nodes.insert({0, 0, 0, true});
		nodes.insert({0, 0, 0, false});
		int size = _inputLines.size();
		Node node;
		while (!nodes.empty())
		{
			node = *nodes.begin();
			nodes.erase(nodes.begin());

			if (node.x == size - 1 && node.y == size - 1)
				break;
			if (visited.find(node.hash()) != visited.end())
				continue;
			visited.insert(node.hash());
			for (int j = -1; j < 2; j += 2)
			{
				auto x = node.x;
				auto y = node.y;
				int extraHeat = 0;
				for (int i = 1; i <= max; i++)
				{
					if (node.vertical)
						y += j;
					else
						x += j;
					if (x >= size || y >= size || x < 0 || y < 0)
						break;
					extraHeat += _inputLines[y][x] - '0';
					if (i >= min)
						nodes.insert({node.heat + extraHeat, x, y, !node.vertical});
				}
			}
		}
		LOG(node.heat);
	}
	void SolveAdvanced() override
	{
		min = 4;
		max = 10;
		Solve();
	}
};
