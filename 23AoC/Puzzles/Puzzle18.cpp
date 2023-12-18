#include "../Utility.h"
#include "../Puzzle.h"

#include <map>

class Puzzle18 : IPuzzle {
	char _lastThree[3] = {0,0,0};
	int _index = 0;
	int _lastSum = 0;
	std::vector<std::pair<int,int>> _history;
	void Add(std::unordered_map<int,std::set<int>>& m, int x, int y, char c, bool add = true){
		if(c != 'X'){
			_lastSum *= 2;
			_lastSum += c - _lastThree[_index]*8;
			_lastThree[_index++] = c;
			_index %= 3;
			_history.push_back({x,y});

			if((_lastSum == 'U'*4 + 'R'*2 + 'U') || (_lastSum == 'D'*4 + 'L'*2 + 'D') || (_lastSum == 'D'*4 + 'R'*2 + 'U') || (_lastSum == 'U'*4 + 'L'*2 + 'D')){
				auto& [x1,y1] = _history[_history.size()-2];
				m[y1].erase(x1);
			}
			if((_lastSum == 'U'*4 + 'L'*2 + 'U') || (_lastSum == 'D'*4 + 'R'*2 + 'D') || (_lastSum == 'D'*4 + 'R'*2 + 'U') || (_lastSum == 'U'*4 + 'L'*2 + 'D')){
				auto& [x1,y1] = _history[_history.size()-3];
				m[y1].erase(x1);
			}
		}
		if(!add){
			return;
		}

		if(m.find(y) == m.end()){
			m[y] = std::set<int>();
		}
		m[y].insert(x);
	}
	void Solve() override
	{
		int x=0,y=0;
		std::unordered_map<int,std::set<int>> map;
		for(auto& lines : _inputWords){
			int dir = 1;
			switch (lines[0][0])
			{
			case 'L':
				dir = -1;
			case 'R':
				x += dir * std::stoi(lines[1]);
				Add(map,x,y,lines[0][0]);
				break;
			case 'U':
				dir = -1;
			case 'D':
				for (int i = 0; i < std::stoi(lines[1]); i++)
				{
					y += dir;
					Add(map,x,y,'X');
				}
				Add(map,x,y,lines[0][0]);
				break;
			default:
				break;
			}
		}
		auto [hx,hy] = _history[0];
		Add(map,hx,hy,_inputWords[0][0][0],false);
		Add(map,hx,hy,_inputWords[1][0][0],false);
		size_t sum = 0;
		for(auto& [y, set] : map){
			int width = -1;
			for(auto& x : set){
				if(width == -1){
					width = x;
				}
				else{
					sum += x - width + 1;
					width = -1;
				}
			}
		}
		LOG(sum);


		
	}

	std::pair<int,char> get_instr(string s){
		std::map<int,char> m{
			{0,'R'},
			{1,'D'},
			{2,'L'},
			{3,'U'}
		};
		return std::make_pair(std::stoi(s.substr(2,5),nullptr,16),m[s[7] - '0']);
	}

	void SolveAdvanced() override
	{
		//clean class
		_lastThree[0] = 0;
		_lastThree[1] = 0;
		_lastThree[2] = 0;
		_index = 0;
		_lastSum = 0;
		_history.clear();

		// 
		int x=0,y=0;
		std::unordered_map<int,std::set<int>> map;
		for(auto& lines : _inputWords){
			int dir = 1;
			auto [steps,instr] = get_instr(lines[2]);
			switch (instr)
			{
			case 'L':
				dir = -1;
			case 'R':
				x += dir * steps;
				Add(map,x,y,instr);
				break;
			case 'U':
				dir = -1;
			case 'D':
				for (int i = 0; i < steps; i++)
				{
					y += dir;
					Add(map,x,y,'X');
				}
				Add(map,x,y,instr);
				break;
			default:
				break;
			}
		}
		auto [hx,hy] = _history[0];
		Add(map,hx,hy,get_instr(_inputWords[0][2]).second,false);
		Add(map,hx,hy,get_instr(_inputWords[1][2]).second,false);
		size_t sum = 0;
		for(auto& [y, set] : map){
			int width = -1;
			for(auto& x : set){
				if(width == -1){
					width = x;
				}
				else{
					sum += x - width + 1;
					width = -1;
				}
			}
		}
		LOG(sum);


	}
};
