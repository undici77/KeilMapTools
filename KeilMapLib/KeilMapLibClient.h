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

#ifndef _KEIL_MAP_LIB_INTERFACE_H
#define _KEIL_MAP_LIB_INTERFACE_H

/*****************************************************************************/

#include "KeilMapLibStructs.h"
#include <iostream>
#include <vector>

class KeilMapLibClient
{
public:
	KeilMapLibClient(void);
	~KeilMapLibClient(void);

	bool                                    ReadFile(const std::string file_path);

	std::vector<CROSS_REFERENCE_FIELD>      GetCrossReference(void);
	std::vector<FUNCTION_POINTER_FIELD>     GetFunctionPointer(void);
	std::vector<GLOBAL_SYMBOL_FIELD>        GetGlobalSymbols(void);
	std::vector<IMAGE_COMPONENT_SIZE_FIELD> GetImageComponentSize(void);
	IMAGE_SIZE_DATA                         GetImageSize(void);
	std::vector<LOCAL_SYMBOL_FIELD>         GetLocalSymbols(void);
	std::vector<MAXIMUM_STACK_USAGE_FIELD>  GetMaximumStackUsage(void);
	MEMORY_MAP_IMAGE                        GetMemoryMapImage(void);
	std::vector<MUTUALLY_RECURSIVE_FIELD>   GetMutualRecursive(void);
	std::vector<REMOVED_SYMBOL_FIELD>       GetRemovedSymbols(void);
	std::vector<STACK_USAGE_FIELD>          GetStackUsage(void);

private:
	std::string _File;
};

#endif
