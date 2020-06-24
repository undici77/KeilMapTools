// This file is part of KeilRtxStackSize.
//
// KeilRtxStackSize is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilRtxStackSize is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilRtxStackSize.  If not, see <https://www.gnu.org/licenses/>.

#pragma once

#include "targetver.h"

#define WIN32_LEAN_AND_MEAN
// Windows Header Files:
#include <windows.h>

#include <stdio.h>
#include <tchar.h>

#include <iostream>
#include <fstream>
#include <unordered_map>
#include <algorithm>
#include <string>
#include <iomanip>
#include <iterator>
#include <exception>

#include <boost/filesystem/path.hpp>
#include <boost/program_options.hpp>
#include <boost/format.hpp>
#include <boost/filesystem.hpp>
#include <boost/filesystem/fstream.hpp>
#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/ini_parser.hpp>
#include <boost/algorithm/string.hpp>
#include <boost/algorithm/string/replace.hpp>
#include <boost/filesystem/path.hpp>
#include <boost/filesystem/operations.hpp>
#include <boost/regex.hpp>

#include "Global.h"
#include "../KeilMapLib/KeilMapLib.h"

extern const std::string App_Name;
extern const std::string App_Version;
extern const std::string App_Author;
