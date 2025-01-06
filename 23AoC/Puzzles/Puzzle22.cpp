#include "../Utility.h"
#include "../Puzzle.h"

#include <set>

class Puzzle22 : IPuzzle {
	class Brick{
	public:
		int x, y, z;
		int dx,dy,dz;
		int id;
		std::set<int> bricks_below;
		std::set<int> bricks_above;
		Brick(int x, int y, int z, int x2, int y2, int z2, int id) :
		 x(x), y(y), z(z),
		 dx(x2-x), dy(y2-y), dz(z2-z),
		 id(id) {}
	};
	class World{
	public:

		std::vector<Brick> bricks;
		std::vector<std::vector<std::vector<int>>> world; //indices of bricks

		void LoadInput(const std::vector<string>& input){
			for(auto& line : input){
				auto ep = Utility::Split(line, "~");
				auto ep1 = Utility::Split(ep[0], ",");
				auto ep2 = Utility::Split(ep[1], ",");
				bricks.push_back(Brick(std::stoi(ep1[0]),std::stoi(ep1[1]),std::stoi(ep1[2]),
									   std::stoi(ep2[0]),std::stoi(ep2[1]),std::stoi(ep2[2]), bricks.size()));
			}
		}
		bool CanBePlaced(const Brick& b,int dz){
			for(int x = b.x; x <= b.x+b.dx; x++)
				for(int y = b.y; y <= b.y + b.dy; y++)
					for(int z = b.z; z <= b.z+b.dz; z++)
						if(z-dz == 0 || world[x][y][z-dz] != -1)
							return false;
			return true;
		}

		void PlaceBrick(Brick& b,int id){
			int dz = 0;
			while(CanBePlaced(b,dz))
				dz++;
			b.z -= dz-1;
			for(int x = b.x; x <= b.x+b.dx; x++)
				for(int y = b.y; y <= b.y + b.dy; y++)
					for(int z = b.z; z <= b.z+b.dz; z++)
						world[x][y][z] = id;
		}

		static bool CompareBricks(const Brick& a, const Brick& b) {
			return a.z < b.z;
		}

		void InitWorld(){
			int maxX = 0, maxY = 0, maxZ = 0;
			for(auto& brick : bricks){
				maxX = std::max(maxX, brick.x + brick.dx);
				maxY = std::max(maxY, brick.y + brick.dy);
				maxZ = std::max(maxZ, brick.z + brick.dz);
			}
			world.resize(maxX+1);
			for(auto& x : world){
				x.resize(maxY+1);
				for(auto& y : x){
					y.resize(maxZ+2,-1);
				}
			}

			std::sort(bricks.begin(),bricks.end(),CompareBricks);
			for (size_t i = 0; i < bricks.size(); i++)
				bricks[i].id = i;
			

			for(size_t i = 0; i< bricks.size(); i++)
				PlaceBrick(bricks[i],i);

			for(auto& b : bricks)
				for(int x = b.x; x <= b.x+b.dx; x++)
					for(int y = b.y; y <= b.y + b.dy; y++)
						for(int z = b.z; z <= b.z+b.dz; z++){
							if(world[x][y][z-1] != -1 && world[x][y][z-1] != b.id)
								b.bricks_below.insert(world[x][y][z-1]);
							if(world[x][y][z+1] != -1 && world[x][y][z+1] != b.id)
								b.bricks_above.insert(world[x][y][z+1]);
						}
			
			
			int count = 0;
			size_t fallen = 0;
			for(auto& b : bricks){
				bool mandatory = false;
				for(auto& ind : b.bricks_above)
					if(bricks[ind].bricks_below.size() == 1){
						mandatory = true;
						break;
					}
				if(mandatory){
					auto bricks_fallen = std::set<int>();
					bricks_fallen.insert(b.id);
					for (int i = b.id+1; i < (int)bricks.size(); i++)
					{
						std::set<int> setDifference;
						std::set_difference(bricks[i].bricks_below.begin(), bricks[i].bricks_below.end(), bricks_fallen.begin(), bricks_fallen.end(), std::inserter(setDifference, setDifference.begin()));
						if(setDifference.size() == 0 && bricks[i].bricks_below.size() > 0){
							bricks_fallen.insert(i);
						}
					}
					fallen += bricks_fallen.size()-1;
					
				}
				count += !mandatory;
			}
			LOG(count);
			LOG(fallen);
		}
	};
	void Solve() override
	{
		World w = World();
		w.LoadInput(_inputLines);
		w.InitWorld();
	}
	void SolveAdvanced() override
	{

	}
};
