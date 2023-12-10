#pragma once

#include <string>
#include <vector>
#include <iostream>
#include <fstream>
#include <stack>

using std::string;

// helping macros
#pragma region Macros

#define LOG(x) std::cout << x << std::endl;
#define LOG2(x, y) std::cout << x << y << std::endl;
// with line of code
#define LOGLOC(x) std::cout << x << " at line: " << __LINE__ << std::endl;
// with line of code and file
#define LOGLOCF(x) std::cout << x << " at line: " << __LINE__ << " in file: " << __FILE__ << std::endl;
// assert with line of code and file
#define ASSERT(x)                                                                                       \
	if (!(x))                                                                                           \
	{                                                                                                   \
		std::cout << "Assertion failed at line: " << __LINE__ << " in file: " << __FILE__ << std::endl; \
		return;                                                                                         \
	}
// assert with line of code and file and custom message
#define ASSERTM(x, m)                                                                                                             \
	if (!(x))                                                                                                                     \
	{                                                                                                                             \
		std::cout << "Assertion failed at line: " << __LINE__ << " in file: " << __FILE__ << " with message: " << m << std::endl; \
		return;                                                                                                                   \
	}
// assert with line of code and file and custom message and return value
#define ASSERTMR(x, m, r)                                                                                                         \
	if (!(x))                                                                                                                     \
	{                                                                                                                             \
		std::cout << "Assertion failed at line: " << __LINE__ << " in file: " << __FILE__ << " with message: " << m << std::endl; \
		return r;                                                                                                                 \
	}

#pragma endregion
class Utility
{
public:
	static std::vector<string> ReadLines(const string &path)
	{
		std::vector<string> lines;
		std::ifstream file(path);
		if (file.is_open())
		{
			string line;
			while (getline(file, line))
			{
				lines.push_back(line);
			}
			file.close();
		}
		else
		{
			LOG("ERROR: Unable to open file")
		}
		return lines;
	}

	static std::vector<std::vector<string>> ReadWordsByLines(const string &path, const string &delimeter = " ", bool IgnoreEmpty = true)
	{
		std::vector<std::vector<string>> wordsByLines;
		std::ifstream file(path);

		if (file.is_open())
		{
			string line;
			while (getline(file, line))
			{
				std::vector<string> words;
				size_t pos = 0;
				while ((pos = line.find(delimeter)) != string::npos)
				{
					if (!IgnoreEmpty || line.substr(0, pos).length() > 0)
						words.push_back(line.substr(0, pos));
					line.erase(0, pos + delimeter.length());
				}
				if (!IgnoreEmpty || line.length() > 0)
					words.push_back(line);
				wordsByLines.push_back(words);
			}
			file.close();
		}
		else
		{
			std::cout << "Unable to open file";
		}
		return wordsByLines;
	}
};