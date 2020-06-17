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
#include "GeneratorFactory.h"

/*****************************************************************************/

std::unordered_map <std::string, std::unique_ptr<GeneratorBase>(*)(void)> GeneratorFactory::_Map
{
	std::pair<std::string, std::unique_ptr<GeneratorBase>(*)(void)>("STM32", MakeStm32Generator),
};

/*****************************************************************************/
GeneratorFactory::GeneratorFactory()
/*****************************************************************************/
{
}

/*****************************************************************************/
std::unique_ptr<GeneratorBase> GeneratorFactory::Make(std::string architecture)
/*****************************************************************************/
{
	std::unique_ptr<GeneratorBase>(*factory)(void);

	if (_Map.find(architecture) != _Map.end())
	{
		factory = _Map[architecture];
		return (factory());
	}
	else
	{
		return (NULL);
	}
}

/*****************************************************************************/
std::unique_ptr<GeneratorBase> GeneratorFactory::MakeStm32Generator(void)
/*****************************************************************************/
{
	return (std::make_unique<Stm32Generator>());
}

