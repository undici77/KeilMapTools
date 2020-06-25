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

#pragma once

#include "stdafx.h"
#include "Main.h"

/*****************************************************************************/

#define OS_RTX_DEFAULT_STACK_SIZE         96

/*****************************************************************************/

class GeneratorBase
{
public:
	virtual bool Read(PARAMETERS &parameters, std::string *error);
	virtual void Generate(PARAMETERS &parameters);
	virtual bool WriteRequest(void);
	virtual bool Write(PARAMETERS &parameters);

protected:
	virtual size_t CalculareStackSize(size_t value, size_t stdio_stack_size, size_t stack_oversize);
	std::string Pad(const char *input, size_t length);

	std::vector<MAXIMUM_STACK_USAGE_FIELD> _Maximum_Stack_Usage;
	std::vector<STACK_USAGE_FIELD>         _Stack_Usage;
	std::vector<std::string>               _Header_File;
	std::vector<std::string>               _Original_Header_File;
};

