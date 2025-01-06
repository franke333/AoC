#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle19 : IPuzzle
{
	class Rule
	{
	public:
		int id;
		// xmas, greater, than, goto
		std::vector<std::tuple<char, bool, int, int>> rules;
		int last_id;
		int GetNextRule(std::map<char, int> input)
		{
			for (auto [c, greater, than, goto_id] : rules)
			{
				if (greater)
				{
					if (input[c] > than)
					{
						return goto_id;
					}
				}
				else
				{
					if (input[c] < than)
					{
						return goto_id;
					}
				}
			}
			return last_id;
		}
	};

	static int getID(std::string s)
	{
		return std::accumulate(s.begin(), s.end(), 0, [](int a, char b)
							   { return a * 256 + b; });
	}

	void Solve() override
	{
		std::map<int, Rule> rules;
		bool rules_done = false;
		int good = 'A';
		int bad = 'R';
		size_t sum = 0;
		for (auto line : _inputLines)
		{
			LOG(line);
			if (line == "")
			{
				rules_done = true;
				continue;
			}
			if (!rules_done)
			{
				auto ssplit = Utility::Split(line.substr(0, line.size() - 1), "{");
				auto name = ssplit[0];
				auto rs = ssplit[1];
				Rule r;
				r.id = getID(name);
				for (auto rule : Utility::Split(rs, ","))
				{
					auto split = Utility::Split(rule, ":");
					if (split.size() == 1)
					{
						r.last_id = getID(split[0]);
						continue;
					}
					auto c = split[0];
					auto val = split[1];
					r.rules.push_back({c[0], c[1] == '>', std::stoi(c.substr(2)), getID(val)});
				}
				rules[r.id] = r;
			}
			else
			{
				std::map<char, int> part;
				for (auto r : Utility::Split(line.substr(1, line.size() - 2), ","))
				{
					auto split = Utility::Split(r, "=");
					auto c = split[0];
					auto val = split[1];
					part[c[0]] = std::stoi(val);
				}
				int id = getID("in");
				while (id != bad && id != good)
				{
					id = rules[id].GetNextRule(part);
				}
				if (id == good)
					sum += part['x'] + part['m'] + part['a'] + part['s'];
			}
		}
		LOG("Sum: " << sum);
	}

	void SolveAdvanced() override
	{
		std::map<int, Rule> rules;
		int good = 'A';
		int bad = 'R';
		std::map<char, std::set<int>> rules_ranges;
		for (auto c : "xmas")
			rules_ranges[c] = {4000};
		for (auto line : _inputLines)
		{
			LOG(line);
			if (line == "")
				break;
			auto ssplit = Utility::Split(line.substr(0, line.size() - 1), "{");
			auto name = ssplit[0];
			auto rs = ssplit[1];
			Rule r;
			r.id = getID(name);
			std::set<int> ids;
			for (auto rule : Utility::Split(rs, ","))
			{
				auto split = Utility::Split(rule, ":");
				if (split.size() == 1)
				{
					r.last_id = getID(split[0]);
					ids.insert(r.last_id);
					continue;
				}
				auto c = split[0];
				auto val = split[1];
				r.rules.push_back({c[0], c[1] == '>', std::stoi(c.substr(2)), getID(val)});
				ids.insert(getID(val));
			}
			if (ids.size() > 1)
				for (auto rule : Utility::Split(rs, ","))
				{
					auto split = Utility::Split(rule, ":");
					if (split.size() == 1)
						continue;
					auto c = split[0];
					auto val = split[1];
					rules_ranges[c[0]].insert(std::stoi(c.substr(2)) - (c[1] == '<'));
				}
			rules[r.id] = r;
		}
		size_t sum = 0;

		std::vector xv = {0};
		for (auto x : rules_ranges['x'])
			xv.push_back(x);
		
		#pragma omp parallel for reduction(+:sum) num_threads(12)
		for (int i = 0; i < xv.size() - 1; i++)
		{
			size_t x = xv[i+1];
			std::map<char, size_t> last_xmas;
			last_xmas['m'] = 0;
			for (size_t m : rules_ranges['m'])
			{
				last_xmas['a'] = 0;
				for (size_t a : rules_ranges['a'])
				{
					last_xmas['s'] = 0;
					for (size_t s : rules_ranges['s'])
					{
						std::map<char, int> part;
						part['x'] = x;
						part['m'] = m;
						part['a'] = a;
						part['s'] = s;
						int id = getID("in");
						while (id != bad && id != good)
							id = rules[id].GetNextRule(part);
						if (id == good)
						{
							size_t xmas = (x - xv[i]) * (m - last_xmas['m']) * (a - last_xmas['a']) * (s - last_xmas['s']);
							sum += xmas;
							//LOG("Found: " << x << "-" << last_xmas['x'] << " " << m << "-" << last_xmas['m'] << " " << a << "-" << last_xmas['a'] << " " << s << "-" << last_xmas['s'] << " := " << xmas);
						}
						last_xmas['s'] = s;
					}
					last_xmas['a'] = a;
				}
				last_xmas['m'] = m;
			}
			last_xmas['x'] = x;
			LOG("X: " << i+1 << "/" << rules_ranges['x'].size());
		}
		LOG("Sum: " << sum);
	}
};
