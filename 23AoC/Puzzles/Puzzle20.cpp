#include "../Utility.h"
#include "../Puzzle.h"

#include <queue>
#include <numeric>

class Module
{
public:
	static size_t presses;
	static size_t counters[2];
	static std::map<string, Module *> modules;
	static std::queue<std::pair<Module *, bool>> eventqueue;
	static size_t done;
	virtual void ReceivePulse(Module *from, bool high){};
	virtual void Init(){};
	std::vector<Module *> outputs;
	std::vector<Module *> inputs;
	string name;

	static void ProcessEvents()
	{
		presses++;
		while (!eventqueue.empty())
		{
			auto [module, high] = eventqueue.front();
			eventqueue.pop();
			for (auto &output : module->outputs)
			{
				// LOG(module->name << " -" << (high ? "high" : "low") << "-> " << output->name);
				output->ReceivePulse(module, high);
				counters[high]++;
			}
		}
	}
};

class FFMod : public Module
{
public:
	bool state = false;
	void ReceivePulse(Module *from, bool high) override
	{
		if (!high)
		{
			state = !state;
			eventqueue.push({this, state});
		}
	}
	void Init() override
	{
		state = false;
	}
};

class FinalMod : public Module
{
public:
	void ReceivePulse(Module *from, bool high) override
	{
		if (high)
			return;
		LOG("Received pulse");
		Module::done++;
	}
	void Init() override
	{
		Module::done = 0;
	}
};

class ConjMod : public Module
{
public:
	static std::vector<size_t> counters;
	std::map<Module *, bool> states;
	bool printed = false;
	size_t incomes = 0;
	void ReceivePulse(Module *from, bool high) override
	{
		if (states[from] != high)
		{
			states[from] = high;
			incomes += high ? 1 : -1;
		}
		if (!printed && incomes == 0 && outputs[0]->name == "hb")
		{
			counters.push_back(Module::presses);
			printed = true;
		}
		eventqueue.push({this, incomes != inputs.size()});
	}
	void Init() override
	{
		for (auto &module : inputs)
			states[module] = false;
	}
};

class BroadMod : public Module
{
public:
	void ReceivePulse(Module *from, bool high) override
	{
		eventqueue.push({this, high});
	}
	void Init() override{};
};

size_t Module::counters[2] = {0, 0};
std::map<string, Module *> Module::modules;
std::queue<std::pair<Module *, bool>> Module::eventqueue;
size_t Module::done = 0;
size_t Module::presses = 0;
std::vector<size_t> ConjMod::counters;

class Puzzle20 : IPuzzle
{

	void Solve() override
	{
		Module::counters[0] = 0;
		Module::counters[1] = 0;
		for (auto &line : _inputWords)
		{
			Module *m;
			int pref = true;
			switch (line[0][0])
			{
			case '%':
				m = new FFMod();
				break;
			case '&':
				m = new ConjMod();
				break;
			default:
				m = new BroadMod();
				pref = false;
				break;
			}
			m->name = line[0].substr(pref);
			Module::modules[m->name] = m;
		}
		Module::modules["output"] = new Module();
		Module::modules["output"]->name = "output";
		Module::modules["rx"] = new FinalMod();
		Module::modules["rx"]->name = "rx";


		for (auto &line : _inputWords)
		{
			bool pref = line[0][0] == '%' || line[0][0] == '&';
			Module *m = Module::modules[line[0].substr(pref)];
			for (size_t i = 2; i < line.size(); i++)
			{
				bool post = (i != line.size() - 1);
				if (!Module::modules.contains(line[i].substr(0, line[i].length() - post)))
					Module::modules[line[i].substr(0, line[i].length() - post)] = new Module();
				Module *m2 = Module::modules[line[i].substr(0, line[i].length() - post)];
				m->outputs.push_back(m2);
				m2->inputs.push_back(m);
			}
		}

		for (auto &[name, module] : Module::modules)
			module->Init();
			
		for (size_t i = 0; i < 1000; i++)
		{
			Module::modules["broadcaster"]->ReceivePulse(nullptr, false);
			Module::counters[0]++;
			Module::ProcessEvents();
		}
		LOG(Module::counters[0] * Module::counters[1]);
	}
	void SolveAdvanced() override
	{
		if (Module::modules.size() == 0)
		{
			LOG("please run Solve() first");
			return;
		}
		for (size_t i = 0; i < 4000; i++)
		{
			Module::modules["broadcaster"]->ReceivePulse(nullptr, false);
			Module::ProcessEvents();
		}
		LOG(std::accumulate(ConjMod::counters.begin(), ConjMod::counters.end(), (size_t)1, [](size_t a, size_t b)
							{ return std::lcm(a, b); }));
	}
};
