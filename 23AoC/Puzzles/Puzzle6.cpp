#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle6 : IPuzzle
{
	void Solve() override
	{
		long ways = 1;
		for (size_t i = 1; i < _inputWords[0].size(); i++)
		{
			long time = std::stol(_inputWords[0][i]), dist = std::stol(_inputWords[1][i]);
			int w = 0;
			while (w * (time - w) <= dist)
				w++;
			ways *= time + 1 - 2 * w;
		}
		LOG("Result: " << ways)
	}
	void SolveAdvanced() override
	{
		string t, d;
		for (size_t i = 1; i < _inputWords[0].size(); i++)
		{
			t += _inputWords[0][i];
			d += _inputWords[1][i];
		}
		size_t time = std::stoull(t), dist = std::stoull(d);
		size_t w = time / 2, step = w / 2;
		while (!(w * (time - w) > dist && (w - 1) * (time - w + 1) <= dist))
		{
			w += w * (time - w) > dist ? -step : step;
			step /= 2;
		}
		LOG("Result: " << time + 1 - 2 * w)
	}
};
