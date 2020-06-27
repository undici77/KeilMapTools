// This file is part of KeilMapLib.
//
// KeilMapLib is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilMapLib is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilMapLib.  If not, see <https://www.gnu.org/licenses/>.

#include "stdafx.h"
#include "GeneratorBase.h"

/*****************************************************************************/

#define OS_RTX_DEFAULT_STACK_SIZE         96

/*****************************************************************************/
bool GeneratorBase::Read(PARAMETERS &parameters, std::string *error)
/*****************************************************************************/
{
	std::ifstream                          input_file_stream;
	std::string                            data;
	KeilMapLib                             keil_map;
	std::vector<MAXIMUM_STACK_USAGE_FIELD> maximum_stack_usage;

	ASSERT(error != NULL);

	if (!boost::filesystem::exists(parameters.map_file_path))
	{
		*error = "file " + parameters.map_file_path.string() + " not found";
		return (false);
	}

	input_file_stream    = std::ifstream(parameters.map_file_path.string());
	data                 = std::string(std::istreambuf_iterator<char>(input_file_stream.rdbuf()), std::istreambuf_iterator<char>());
	_Maximum_Stack_Usage = keil_map.GetMaximumStackUsage(data);
	if (_Maximum_Stack_Usage.size() == 0)
	{
		*error = "section maximum stack usage not found, add '--info=stack' in linker misc controls";
		return (false);
	}

	_Stack_Usage = keil_map.GetStackUsage(data);

	input_file_stream = std::ifstream(parameters.output_file_path.string());
	while (std::getline(input_file_stream, data))
	{
		_Original_Header_File.emplace_back(data);
	}

	return (true);
}

/*****************************************************************************/
void GeneratorBase::Generate(PARAMETERS &parameters)
/*****************************************************************************/
{
	ASSERT(false);
}

/*****************************************************************************/
bool GeneratorBase::WriteRequest(void)
/*****************************************************************************/
{
	std::vector<std::string>::const_iterator current_line;
	size_t                                   current_line_comment_found;
	std::vector<std::string>::const_iterator new_line;
	size_t                                   new_line_comment_found;

	current_line = _Original_Header_File.cbegin();
	new_line     = _Header_File.cbegin();

	while ((current_line != _Original_Header_File.end()) &&
	       (new_line     != _Header_File.end()))
	{
		if (*current_line != *new_line)
		{
			current_line_comment_found = current_line->find("//");
			new_line_comment_found     = current_line->find("//");

			if ((current_line_comment_found  == std::string::npos) &&
			    (new_line_comment_found      == std::string::npos))
			{
				return (true);
			}
		}

		current_line++;
		new_line++;
	}

	return ((current_line != _Original_Header_File.end()) || (new_line != _Header_File.end()));
}

/*****************************************************************************/
bool GeneratorBase::Write(PARAMETERS &parameters)
/*****************************************************************************/
{
	std::ofstream output_file_stream;

	output_file_stream.open (parameters.output_file_path.string(), std::ios::trunc);
	if (output_file_stream.fail())
	{
		return (false);
	}

	for (std::string line : _Header_File)
	{
		output_file_stream << line << std::endl;
	}

	output_file_stream.close();

	return (true);
}

/*****************************************************************************/
size_t GeneratorBase::CalculareStackSize(size_t value, size_t stdio_stack_size, size_t stack_oversize)
/*****************************************************************************/
{
	// Adding 4% of free stack
	value = ((value * 104) / 100);
	// Adding stdio usage (architecture dependency) and stack needed from KEIL RTX
	value += stdio_stack_size + OS_RTX_DEFAULT_STACK_SIZE;
	// Take align of 64 bytes in order to reduce project rebuild
	value = (((value + (64 - 1)) / 64) * 64) + stack_oversize;

	return (value);
}

/*****************************************************************************/
std::string GeneratorBase::Pad(const char *input, size_t length)
/*****************************************************************************/
{
	std::ostringstream ss;
	ss << std::left << std::setfill(' ') << std::setw(length) << input;
	return (std::move(ss.str()));
}
