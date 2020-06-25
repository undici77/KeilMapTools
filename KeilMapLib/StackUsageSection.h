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

#include "KeilMapLib.h"
#include "SectionBase.h"
#include "SectionData.h"
#include "RegexBuilder.h"

class StackUsageSection : public SectionBase, public SectionData<STACK_USAGE_FIELD>
{
public:
	StackUsageSection();
	~StackUsageSection();

	bool Manage(const std::string &file);
};

