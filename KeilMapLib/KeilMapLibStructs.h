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

#ifndef _KEIL_MAP_LIB_STRUCTS_H
#define _KEIL_MAP_LIB_STRUCTS_H

/*****************************************************************************/

#include <iostream>
#include <vector>

/*****************************************************************************/

typedef struct
{
	std::string module;
	std::string module_reference;
	std::string reference_qualifier;
	std::string symbol;
} CROSS_REFERENCE_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string symbol;
	std::string module;
	std::string counts;
	std::string module_reference;
} FUNCTION_POINTER_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string symbolic_name;
	std::string address;
	std::string type;
	std::string size;
	std::string object_name;
} GLOBAL_SYMBOL_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string code;
	std::string inline_data;
	std::string read_only_data;
	std::string read_write_data;
	std::string zero_init_data;
	std::string debug_data;
	std::string object_name;
} IMAGE_COMPONENT_SIZE_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string total_read_only_size;
	std::string total_read_write_size;
	std::string total_rom_size;
} IMAGE_SIZE_DATA;

/*****************************************************************************/

typedef struct
{
	std::string symbolic_name;
	std::string address;
	std::string type;
	std::string size;
	std::string object_name;
} LOCAL_SYMBOL_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string function;
	std::string size;
	std::string call_chain;
} MAXIMUM_STACK_USAGE_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string execution_address;
	std::string load_address;
	std::string size;
	std::string type;
	std::string attribute;
	std::string id;
	bool        entry_point;
	std::string section_name;
	std::string object_name;
} MEMORY_MAP_IMAGE_OBJECT_FIELD;

typedef struct
{
	std::string                                name;
	std::string                                data;
	std::vector<MEMORY_MAP_IMAGE_OBJECT_FIELD> fields;
} MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD;

typedef struct
{
	std::string                                          name;
	std::string                                          data;
	std::vector<MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD> execution_region;
} MEMORY_MAP_IMAGE_LOAD_REGION_FIELD;

typedef struct
{
	std::string                                     entry_point;
	std::vector<MEMORY_MAP_IMAGE_LOAD_REGION_FIELD> load_region;
} MEMORY_MAP_IMAGE;

/*****************************************************************************/

typedef struct
{
	std::string function;
	std::string caller;
} MUTUALLY_RECURSIVE_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string module;
	std::string symbol;
	std::string size;
} REMOVED_SYMBOL_FIELD;

/*****************************************************************************/

typedef struct
{
	std::string function;
	std::string size;
} STACK_USAGE_FIELD;

#endif