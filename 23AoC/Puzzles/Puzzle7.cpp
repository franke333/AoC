#include "../Utility.h"
#include "../Puzzle.h"

#define MAX_HAND_VAL 371293

class Puzzle7 : IPuzzle {
	static size_t CardToVal(char c, bool advanced = false) {
		switch (c) {
		case 'A': return 12;
		case 'K': return 11;
		case 'Q': return 10;
		case 'J': return advanced ? 0 : 9;
		case 'T': return advanced ? 9 : 8;
		default: return c - (advanced ? '1' : '2');
		}
	}

	class Hand{
	private:
		size_t value = size_t(-1);
	public:
		size_t bid;
		string cards;
		
		size_t HandValue(bool advanced = false) {
			if (value != size_t(-1)) return value;
			value=0;
			size_t mult = MAX_HAND_VAL;
			for(char c : cards){
				mult /= 13;
				value += mult * CardToVal(c,advanced);
			}
			value += MAX_HAND_VAL*RankValue(advanced);
			return value;
		}
		size_t RankValue(bool advanced = false) {
			std::vector count(13, 0);
			for (auto c : cards) {
				count[CardToVal(c,advanced)]++;
			}
			std::sort(count.begin() + advanced, count.end());
			if(count[12] + count[0] == 5) return 6;
			if(count[12] + count[0] == 4) return 5;
			if(count[12] + count[0] == 3) return count[11] == 2 ? 4 : 3;
			if(count[12] + count[0] == 2) return count[11] == 2 ? 2 : 1;
			return 0;
		}
	};
	bool advanced = false;
	void Solve() override
	{
		std::vector<Hand> hands;
		for(auto& line : _inputWords){
			Hand h;
			h.bid = std::stoi(line[1]);
			h.cards = line[0];
			hands.push_back(h);
		}
		std::sort(hands.begin(), hands.end(), [&](Hand& a, Hand& b) {
			return a.HandValue(advanced) < b.HandValue(advanced);
		});
		size_t mult = 1;
		size_t winning = std::accumulate(hands.begin(), hands.end(), 0, [&](size_t a, const Hand& b) {
			return a + b.bid * mult++;
		});
		LOG(winning);
	}
	void SolveAdvanced() override
	{
		advanced = true;
		Solve();
	}
};
