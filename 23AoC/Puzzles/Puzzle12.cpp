#include "../Utility.h"
#include "../Puzzle.h"

#include <thread>
#include <atomic>
#include <numeric>
#include <unordered_map>

#define cache_t std::unordered_map<std::tuple<int,int,int>,size_t>

namespace std {
    template <>
    struct hash<std::tuple<int, int, int>> {
        size_t operator()(const std::tuple<int, int, int>& t) const {
            size_t hash_value = 0;
            // Combine the hash values of the tuple elements
            hash_value ^= std::hash<int>()(std::get<0>(t)) + 0x9e3779b9 + (hash_value << 6) + (hash_value >> 2);
            hash_value ^= std::hash<int>()(std::get<1>(t)) + 0x9e3779b9 + (hash_value << 6) + (hash_value >> 2);
            hash_value ^= std::hash<int>()(std::get<2>(t)) + 0x9e3779b9 + (hash_value << 6) + (hash_value >> 2);
            return hash_value;
        }
    };
}

class Puzzle12 : IPuzzle {
	size_t GetArrangments(const string& input, uint16_t pos, int streak,const std::vector<int>& remains,int minLength, int readInputOver, cache_t& cache){
		if(pos == input.size()*readInputOver + readInputOver - 1){
			if(remains.empty() && streak == 0)
				return 1;
			if(remains.size() == 1 && remains[0] == streak)
				return 1;
			return 0;
		}

		if((int)input.size()*readInputOver+readInputOver-pos+streak+3 < minLength)
			return 0;
		
		if(cache.find(std::make_tuple(pos,streak,remains.size())) != cache.end()){
			return cache[std::make_tuple(pos,streak,remains.size())];
		}

		char currSymbol = (pos+1) % (input.size()+1) ? input[pos % (input.size()+1)] : '?' ;
		size_t sum = 0;
		if(currSymbol != '.'){
			if(!remains.empty() && streak + 1 <= remains[0])
				sum += GetArrangments(input, pos+1, streak+1, remains, minLength, readInputOver, cache);
		}
		if(currSymbol != '#'){
			if(!remains.empty() && streak > 0 && streak == remains[0])
				sum += GetArrangments(input, pos+1, 0, std::vector<int>(remains.begin()+1, remains.end()),minLength - remains[0]-1,readInputOver, cache);
			if(streak == 0)
				sum += GetArrangments(input, pos+1, 0, remains,minLength,readInputOver, cache);
		}
		cache[std::make_tuple(pos,streak,remains.size())] = sum;
		return sum;
	}
	void Solve() override
	{
		size_t sum = 0;
		for(auto& line : _inputWords){
			auto remains = std::vector<int>();
			for(auto& n : Utility::Split(line[1], ","))
				remains.push_back(std::stoi(n));
			cache_t cache;
			auto r = GetArrangments(line[0], 0, 0, remains,std::accumulate(remains.begin(),remains.end(),0) + remains.size() - 1,1,cache);
			sum += r;
		}
		LOG("Sum: " << sum);
	}

	void SolveAdvanced() override {
		size_t sum = 0;
		for (auto& line : _inputWords) {
			auto remains = std::vector<int>();
			for (int i = 0; i < 5; i++)
				for (auto& n : Utility::Split(line[1], ","))
					remains.push_back(std::stoi(n));
			int len = std::accumulate(remains.begin(), remains.end(), 0) + remains.size() - 1;
			cache_t cache;
			sum += GetArrangments(line[0], 0, 0, remains,len, 5, cache);
		}
		LOG("Sum: " << sum);
	}
};