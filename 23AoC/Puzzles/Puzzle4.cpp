#include "../Utility.h"
#include "../Puzzle.h"
#include <numeric>

class Puzzle4 : IPuzzle
{
	int GetMatches(const std::vector<std::string> &line, size_t delimeter)
	{
		std::unordered_set<int> numbers;
		for (size_t i = 2; i < delimeter; i++)
			numbers.insert(std::stoi(line[i]));
		return std::count_if(line.begin() + delimeter + 1, line.end(), [&](const std::string &s)
							 { return numbers.contains(std::stoi(s)); });
	}
	void Solve() override
	{
		size_t delimeter = std::find(_inputWords[0].begin(), _inputWords[0].end(), "|") - _inputWords[0].begin();

		LOG("Sum: " << std::accumulate(_inputWords.begin(), _inputWords.end(), 0ul, [&](const size_t &sum, const std::vector<std::string> &line)
									   { return sum + ((1 << GetMatches(line, delimeter)) >> 1); }));
	}
	void SolveAdvanced() override
	{
		size_t delimeter = std::find(_inputWords[0].begin(), _inputWords[0].end(), "|") - _inputWords[0].begin();

		std::vector<size_t> cards(_inputWords.size(), 0);
		for (int i = _inputWords.size() - 1; i >= 0; i--)
			cards[i] = std::accumulate(cards.begin() + i, cards.begin() + i + GetMatches(_inputWords[i], delimeter) + 1, 0ul) + 1;
		LOG("Sum: " << std::accumulate(cards.begin(), cards.end(), 0ul));
	}
};