#include "../Utility.h"
#include "../Puzzle.h"
#include <algorithm>

#define SeedRange std::tuple<size_t, size_t>

class Puzzle5 : IPuzzle
{
	class Range
	{
	public:
		size_t dest_start, source_start, length;
		Range(const std::vector<std::string> &l)
		{
			this->dest_start = std::stoull(l[0]);
			this->source_start = std::stoull(l[1]);
			this->length = std::stoull(l[2]);
		}

		bool InRange(size_t i) const { return i >= source_start && i < source_start + length; }

		bool RangesCollide(SeedRange r) const
		{
			auto [s, l] = r;
			return InRange(s) || InRange(s + l - 1) || (s < source_start && s + l > source_start + length);
		}

		size_t Map(size_t i) const { return i - source_start + dest_start; }

		std::vector<SeedRange> Map(std::vector<SeedRange> &r) const
		{
			std::vector<SeedRange> c;
			for (size_t i = 0; i < r.size(); i++)
			{
				const auto [s, e] = r[i];
				if (RangesCollide({s, e}))
				{
					r.erase(r.begin() + i);
					i--;
					if (!InRange(s))
					{
						r.push_back({s, source_start - s});
					}
					if (!InRange(s + e - 1))
					{
						r.push_back({source_start + length, s + e - source_start - length});
					}
					c.push_back({Map(std::max(s, source_start)),
								 std::min(s + e, source_start + length) - std::max(s, source_start)});
				}
			}
			return c;
		}
	};

	class Almanac
	{
	public:
		std::vector<std::vector<Range>> maps;
		std::vector<SeedRange> seeds;
		Almanac(const std::vector<std::vector<std::string>> &l, bool loadSeedUnique = true)
		{
			if (loadSeedUnique)
				for (size_t i = 1; i < l[0].size(); i++)
					seeds.push_back({std::stoull(l[0][i]), 1});
			else
				for (size_t i = 1; i < l[0].size(); i += 2)
					seeds.push_back({std::stoull(l[0][i]), std::stoull(l[0][i + 1])});

			for (size_t i = 1; i < l.size(); i++)
			{
				if (l[i].size() == 0)
				{
					maps.push_back(std::vector<Range>());
					i++;
					continue;
				}
				maps[maps.size() - 1].push_back(Range(l[i]));
			}
		}
		size_t Map(const size_t i, const std::vector<Range> &map) const
		{
			for (const auto &r : map)
				if (r.InRange(i))
					return r.Map(i);
			return i;
		}
		std::vector<SeedRange> GetLocations() const
		{
			auto c = seeds;
			for (auto &map : maps)
			{
				std::vector<SeedRange> c2;
				for (auto &r : map)
					for (auto &r2 : r.Map(c))
						c2.push_back(r2);
				for (auto &r : c2)
					c.push_back(r);
			}
			return c;
		}
	};
	void Solve() override
	{
		auto a = Almanac(_inputWords).GetLocations();

		size_t m = std::get<0>(*std::min_element(a.begin(), a.end(),
												 [](const auto &tuple1, const auto &tuple2)
												 { return std::get<0>(tuple1) < std::get<0>(tuple2); }));
		LOG("Locations: " << m);
	}
	void SolveAdvanced() override
	{
		auto a = Almanac(_inputWords, false).GetLocations();

		size_t m = std::get<0>(*std::min_element(a.begin(), a.end(),
												 [](const auto &tuple1, const auto &tuple2)
												 { return std::get<0>(tuple1) < std::get<0>(tuple2); }));
		LOG("Locations: " << m);
	}
};
