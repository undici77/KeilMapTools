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

#ifndef _MAIN_H
#define _MAIN_H

#include "stdafx.h"

typedef struct
{
	std::string              architecture;
	boost::filesystem::path  map_file_path;
	boost::filesystem::path  output_file_path;
	std::string              thread_regex;
	size_t                   stack_oversizing;
} PARAMETERS;

#endif