#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle23 : IPuzzle {
	bool _advanced = false;
	void Solve() override
	{
		int size = _inputLines.size();
		// pos, steps, reverse
		std::stack<std::tuple<int,int,bool>> stack; 
		int start, end;
		for (int i = 0; i < size; i++)
		{
			if(_inputLines[0][i] == '.')
				start = i;
			if(_inputLines[size-1][i] == '.')
				end = size*(size-1)+i;
		}
		std::vector<std::vector<bool>> visited(size, std::vector<bool>(size, false));
		stack.push(std::make_tuple(start, 0, false));
		std::map<int,char> ok_slope{
			{-1, '<'},
			{1, '>'},
			{-size, '^'},
			{size, 'v'}
		};
		int longest = -1;
		while(!stack.empty()){
			auto [pos, steps, reverse] = stack.top();
			stack.pop();
			if(reverse){
				visited[pos/size][pos%size] = false;
				continue;
			}
			if(pos == end){
				longest = std::max(longest, steps);
				continue;
			}
			if(visited[pos/size][pos%size])
				continue;
			visited[pos/size][pos%size] = true;

			stack.push(std::make_tuple(pos, steps, true));
			
			for(int i : {-1,1,-size,size}){
				if(pos+i < 0 || pos+i >= size*size)
					continue;
				if(!_advanced){
					if(_inputLines[(pos+i)/size][(pos+i)%size] == '.' || _inputLines[(pos+i)/size][(pos+i)%size] == ok_slope[i])
						stack.push(std::make_tuple(pos+i, steps+1, false));
				}
				else if(_inputLines[(pos+i)/size][(pos+i)%size] != '#')
					stack.push(std::make_tuple(pos+i, steps+1, false));
			}
		}
		LOG("Longest path: " << longest);
		
		
	}

	void SolveAdvanced() override
	{

	}
};
