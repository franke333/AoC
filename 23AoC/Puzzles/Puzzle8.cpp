#include "../Utility.h"
#include "../Puzzle.h"

#define ABCL = 'Z'-'A'+1



class Puzzle8 : IPuzzle {
	class Node{
	public:
		Node* L,* R;
		bool end = false;
		string name;
	};
	std::map<string,Node*> nodes;
	void AddNode(const std::vector<string>& s){
		Node* n;
		if(nodes.find(s[0]) == nodes.end()){
			nodes[s[0]] = new Node();
			nodes[s[0]]->end = s[0][2] == 'Z';
			nodes[s[0]]->name = s[0];
		}
		n = nodes[s[0]];
		if(nodes.find(s[1]) == nodes.end()){
			nodes[s[1]] = new Node();
			nodes[s[1]]->end = s[1][2] == 'Z';
			nodes[s[1]]->name = s[1];

		}
		if(nodes.find(s[2]) == nodes.end()){
			nodes[s[2]] = new Node();
			nodes[s[2]]->end = s[2][2] == 'Z';
			nodes[s[2]]->name = s[2];
		}
		n->L =  nodes[s[1]];
		n->R =  nodes[s[2]];
	}
	void Solve() override
	{
		string instr = _inputWords[0][0];
		size_t i = 0;
		for (size_t i = 2; i < _inputWords.size(); i++)
			AddNode({ _inputWords[i][0],_inputWords[i][2].substr(1,3),_inputWords[i][3].substr(0,3)});
		Node* curr = nodes["AAA"];
		while(curr != nodes["ZZZ"]){
			curr = instr[i % instr.length()] == 'L' ? curr->L : curr->R;
			i++;
		}
		LOG("Result: " << i);
	}

	void SolveAdvanced() override
	{
		string instr = _inputWords[0][0];
		nodes.clear();
		std::unordered_set<Node*> currs;
		for (size_t i = 2; i < _inputWords.size(); i++){
			AddNode({ _inputWords[i][0],_inputWords[i][2].substr(1,3),_inputWords[i][3].substr(0,3)});
			if(_inputWords[i][0][2] == 'A')
				currs.insert(nodes[_inputWords[i][0]]);
		}
		std::vector<size_t> cycles;
		for(auto& start : currs){
			Node* curr = start;
			size_t i = 0;
			while(!curr->end){
				curr = (instr[i % instr.length()] == 'L' ? curr->L : curr->R);
				i++;
			}
			cycles.push_back(i);
		}
		size_t lcm = cycles[0];
		for(size_t i = 1; i < cycles.size(); i++){
			lcm = std::lcm(lcm,cycles[i]);
		}
		LOG("Result: " << lcm);
		
	}
};
