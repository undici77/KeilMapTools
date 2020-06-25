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

#include <boost/regex.hpp>
#include <vector>
#include "Global.h"

class SectionBase
{
public:
	virtual bool Manage(const std::string &file) = 0;

protected:
	std::string GetSection(const std::string &file, const boost::regex &begin_section_regex, int begin_group_id, const boost::regex &end_section_regex, int end_group_id);
	bool SplitSection(const std::string &section, const boost::regex &split_regex, std::vector<std::string> *splitted_section);
};

