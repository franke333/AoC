#include "../Utility.h"
#include "../Puzzle.h"

class Puzzle21 : IPuzzle {
	void Solve() override
	{
		std::unordered_set<int> seen;
		size_t size = _inputLines.size();
		std::queue<std::pair<int, int>> q;
		for (size_t i = 0; i < size; i++)
			for (size_t j = 0; j < size; j++)
				if(_inputLines[i][j] == 'S')
					q.push({ i*size+j,64 });
		seen.insert(q.front().first);
		int even = q.front().first % 2 == 0;
		while(!q.empty()){
			auto [pos,steps] = q.front();
			q.pop();
			if(steps == 0)
				break;
			int x = pos % size;
			int y = pos / size;
			for(auto d : {1,-1}){
				if(_inputLines[y][x+d] == '.' && !seen.contains(pos+d)){
					seen.insert(pos+d);
					q.push({pos+d,steps-1});
				}
				if(_inputLines[y+d][x] == '.' && !seen.contains(pos+d*size)){
					seen.insert(pos+d*size);
					q.push({pos+d*size,steps-1});
				}
			}
		}
		LOG(std::accumulate(seen.begin(),seen.end(),0,[=](int a, int b){return a + (b % 2 != even);}));
			
		
	}
	void SolveAdvanced() override
	{
		std::unordered_set<int> seen;
		int size = _inputLines.size();
		std::queue<std::pair<int, int>> q;
		for (int i = 0; i < size; i++)
			for (int j = 0; j < size; j++)
				if(_inputLines[i][j] == 'S')
					q.push({ i*size+j,0 });
		seen.insert(q.front().first);
		while(!q.empty()){
			auto [pos,steps] = q.front();
			q.pop();
			int x = pos % size;
			int y = pos / size;
			for(auto d : {1,-1}){
				if(x+d >= 0 && x+d < size && _inputLines[y][x+d] == '.' && !seen.contains(pos+d)){
					seen.insert(pos+d);
					q.push({pos+d,steps+1});
				}
				if(y+d >= 0 && y+d < size && _inputLines[y+d][x] == '.' && !seen.contains(pos+d*size)){
					seen.insert(pos+d*size);
					q.push({pos+d*size,steps+1});
				}
			}
		}
		int steps_total = 10;
		size_t gardens_per_cell = std::accumulate(seen.begin(),seen.end(),0,[=](int a, int b){return a + (b % 2 != steps_total % 2);});
		size_t cells_per_side = (steps_total-size/2)/size;
		size_t full_cells = 2*cells_per_side*(cells_per_side+1)+1;
		size_t remainder_side = (steps_total-size/2) % size;
		size_t remainder_corner = (steps_total) % size;

		size_t sum = 0;


		// pos, steps, count
		std::vector<std::tuple<size_t,size_t,size_t>> starts = {
			{0,remainder_corner-2,cells_per_side-1},
			{size-1,remainder_corner-2,cells_per_side-1},
			{size*(size-1),remainder_corner-2,cells_per_side-1},
			{size*size-1,remainder_corner-2,cells_per_side-1},
			
			{size/2,remainder_side-1,1},
			{(size/2)*size,remainder_side-1,1},
			{(size/2)*size+size-1,remainder_side-1,1},
			{size*size-size/2-1,remainder_side-1,1},
		};
		for(auto start : starts){
			auto [pos,ssteps,ccount] = start;
			seen.clear();
			q.push({ pos,ssteps});
			seen.insert(q.front().first);
			while(!q.empty()){
				auto [pos,steps] = q.front();
				q.pop();
				int x = pos % size;
				int y = pos / size;
				if(steps == 0)
					break;
				for(auto d : {1,-1}){
					if(x+d >= 0 && x+d < size && _inputLines[y][x+d] == '.' && !seen.contains(pos+d)){
						seen.insert(pos+d);
						q.push({pos+d,steps-1});
					}
					if(y+d >= 0 && y+d < size && _inputLines[y+d][x] == '.' && !seen.contains(pos+d*size)){
						seen.insert(pos+d*size);
						q.push({pos+d*size,steps-1});
					}
				}
			}
			sum += ccount*std::accumulate(seen.begin(),seen.end(),0,[](int a, int b){return a + (b % 2 == 0);});
		}
		sum += full_cells*gardens_per_cell;
		LOG(sum);
	}
	
};
