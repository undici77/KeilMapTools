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
#include "Global.h"
#include "SwigInterface.h"
#include "RegexBuilder.h"
#include "CrossReferencesSection.h"
#include "RemovedSymbolsSection.h"
#include "MaximumStackUsageSection.h"
#include "StackUsageSection.h"
#include "MutuallyRecursiveSection.h"
#include "FunctionPointerSection.h"
#include "LocalSymbolsSection.h"
#include "GlobalSymbolsSection.h"
#include "MemoryMapImageSection.h"
#include "ImageComponentSizeSection.h"
#include "ImageSizeSection.h"

/*****************************************************************************/
std::vector<CROSS_REFERENCE_FIELD> SwigInterface::GetCrossReference(std::string &file)
/*****************************************************************************/
{
	CrossReferencesSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<FUNCTION_POINTER_FIELD> SwigInterface::GetFunctionPointer(std::string &file)
/*****************************************************************************/
{
	FunctionPointerSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<GLOBAL_SYMBOL_FIELD> SwigInterface::GetGlobalSymbols(std::string &file)
/*****************************************************************************/
{
	GlobalSymbolsSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<IMAGE_COMPONENT_SIZE_FIELD> SwigInterface::GetImageComponentSize(std::string &file)
/*****************************************************************************/
{
	ImageComponentSizeSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<IMAGE_SIZE_DATA> SwigInterface::GetImageSize(std::string &file)
/*****************************************************************************/
{
	ImageSizeSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<LOCAL_SYMBOL_FIELD> SwigInterface::GetLocalSymbols(std::string &file)
/*****************************************************************************/
{
	LocalSymbolsSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<MAXIMUM_STACK_USAGE_FIELD> SwigInterface::GetMaximumStackUsage(std::string &file)
/*****************************************************************************/
{
	MaximumStackUsageSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
MEMORY_MAP_IMAGE SwigInterface::GetMemoryMapImage(std::string &file)
/*****************************************************************************/
{
	MemoryMapImageSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()[0]));
}

/*****************************************************************************/
std::vector<MUTUALLY_RECURSIVE_FIELD> SwigInterface::GetMutualRecursive(std::string &file)
/*****************************************************************************/
{
	MutuallyRecursiveSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<REMOVED_SYMBOL_FIELD> SwigInterface::GetRemovedSymbols(std::string &file)
/*****************************************************************************/
{
	RemovedSymbolSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}

/*****************************************************************************/
std::vector<STACK_USAGE_FIELD> SwigInterface::GetStackUsage(std::string &file)
/*****************************************************************************/
{
	StackUsageSection manager;

	manager.Manage(file);
	return (std::move(manager.Get()));
}
