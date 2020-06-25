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

// stdafx.cpp : source file that includes just the standard includes
// KeilRtxStackSize.pch will be the pre-compiled header

#pragma once

#include "stdafx.h"
#include "GeneratorBase.h"

#define STM32_STDIO_DEFAULT_STACK_SIZE         (128 + 32)

class Stm32Generator : public GeneratorBase
{
public:
	Stm32Generator();
	~Stm32Generator();

	void Generate(PARAMETERS &parameters);
};

