#pragma once

class IPuzzle
{
public:
	virtual void Solve() = 0;
	virtual void SolveAdvanced() = 0;
	virtual ~IPuzzle(){};

	void LoadInput(const std::string &path, bool byLines = true, bool byWords = true)
	{
		if (byLines)
			_inputLines = Utility::ReadLines(path);
		if (byWords)
			_inputWords = Utility::ReadWordsByLines(path);
		return;
	}
protected:
	std::vector<std::string> _inputLines;
	std::vector<std::vector<std::string>> _inputWords;
};

template <typename T>
IPuzzle *createInstance() { return (IPuzzle *)(new T); }

enum SOLUTIONENUM
{
	Basic = 1,
	Advanced = 2,
	Both = 3
};