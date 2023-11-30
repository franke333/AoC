#pragma once

class IPuzzle {
public:
		virtual void Solve() = 0;
		virtual void SolveAdvanced() = 0;
};

template<typename T>
IPuzzle* createInstance() { return new T; }

enum SOLUTIONENUM
{
	Basic = 1,
	Advanced = 2,
	Both = 3
};