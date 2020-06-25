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
#include "Stm32Generator.h"

/*****************************************************************************/

class GeneratorFactory
{
public:
	static std::unique_ptr<GeneratorBase> Make(std::string architecture);

private:
	GeneratorFactory(void);

	static std::unique_ptr<GeneratorBase> MakeStm32Generator(void);

	static std::unordered_map <std::string, std::unique_ptr<GeneratorBase>(*)(void)> _Map;
};

