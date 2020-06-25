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

#ifndef _KEIL_MAP_LIB_H
#define _KEIL_MAP_LIB_H

/*****************************************************************************/

#include "KeilMapLibStructs.h"
#include <iostream>
#include <vector>

/*****************************************************************************/

#ifdef LIB_EXPORTS
#define LIB_API __declspec(dllexport)
#else
#define LIB_API __declspec(dllimport)
#endif

/*****************************************************************************/

class LIB_API KeilMapLib
{
public:
	KeilMapLib(void);
	~KeilMapLib(void);

	std::vector<CROSS_REFERENCE_FIELD>      GetCrossReference(std::string &file);
	std::vector<FUNCTION_POINTER_FIELD>     GetFunctionPointer(std::string &file);
	std::vector<GLOBAL_SYMBOL_FIELD>        GetGlobalSymbols(std::string &file);
	std::vector<IMAGE_COMPONENT_SIZE_FIELD> GetImageComponentSize(std::string &file);
	IMAGE_SIZE_DATA                         GetImageSize(std::string &file);
	std::vector<LOCAL_SYMBOL_FIELD>         GetLocalSymbols(std::string &file);
	std::vector<MAXIMUM_STACK_USAGE_FIELD>  GetMaximumStackUsage(std::string &file);
	MEMORY_MAP_IMAGE                        GetMemoryMapImage(std::string &file);
	std::vector<MUTUALLY_RECURSIVE_FIELD>   GetMutualRecursive(std::string &file);
	std::vector<REMOVED_SYMBOL_FIELD>       GetRemovedSymbols(std::string &file);
	std::vector<STACK_USAGE_FIELD>          GetStackUsage(std::string &file);
};

#endif
