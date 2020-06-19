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
#include "KeilMapLibInterface.h"
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
KeilMapLibInterface::KeilMapLibInterface(void)
/*****************************************************************************/
{
}

/*****************************************************************************/
KeilMapLibInterface::~KeilMapLibInterface(void)
/*****************************************************************************/
{
}

/*****************************************************************************/
std::vector<CROSS_REFERENCE_FIELD> KeilMapLibInterface::GetCrossReference(std::string file)
/*****************************************************************************/
{
	CrossReferencesSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<CROSS_REFERENCE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<FUNCTION_POINTER_FIELD> KeilMapLibInterface::GetFunctionPointer(std::string file)
/*****************************************************************************/
{
	FunctionPointerSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<FUNCTION_POINTER_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<GLOBAL_SYMBOL_FIELD> KeilMapLibInterface::GetGlobalSymbols(std::string file)
/*****************************************************************************/
{
	GlobalSymbolsSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<GLOBAL_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<IMAGE_COMPONENT_SIZE_FIELD> KeilMapLibInterface::GetImageComponentSize(std::string file)
/*****************************************************************************/
{
	ImageComponentSizeSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<IMAGE_COMPONENT_SIZE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
IMAGE_SIZE_DATA KeilMapLibInterface::GetImageSize(std::string file)
/*****************************************************************************/
{
	ImageSizeSection manager;
	IMAGE_SIZE_DATA  result;

	try
	{
		manager.Manage(file);

		if (manager.Get().size() > 0)
		{
			result = manager.Get().at(0);
		}
		else
		{
			result.total_read_only_size  = "";
			result.total_read_write_size = "";
			result.total_rom_size        = "";
		}
	}
	catch (...)
	{
		result.total_read_only_size  = "";
		result.total_read_write_size = "";
    	result.total_rom_size        = "";
	}

	return (std::move(result));
}

/*****************************************************************************/
std::vector<LOCAL_SYMBOL_FIELD> KeilMapLibInterface::GetLocalSymbols(std::string file)
/*****************************************************************************/
{
	LocalSymbolsSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<LOCAL_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<MAXIMUM_STACK_USAGE_FIELD> KeilMapLibInterface::GetMaximumStackUsage(std::string file)
/*****************************************************************************/
{
	MaximumStackUsageSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<MAXIMUM_STACK_USAGE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
MEMORY_MAP_IMAGE KeilMapLibInterface::GetMemoryMapImage(std::string file)
/*****************************************************************************/
{
	MemoryMapImageSection manager;
	MEMORY_MAP_IMAGE      result;

	try
	{
	    manager.Manage(file);

	    if (manager.Get().size() > 0)
	    {
		    result = manager.Get().at(0);
	    }
	    else
	    {
		    result.entry_point = "";
	    }
    }
	catch (...)
	{
	    result.entry_point = "";
	}

    return (std::move(result));
}

/*****************************************************************************/
std::vector<MUTUALLY_RECURSIVE_FIELD> KeilMapLibInterface::GetMutualRecursive(std::string file)
/*****************************************************************************/
{
	MutuallyRecursiveSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<MUTUALLY_RECURSIVE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<REMOVED_SYMBOL_FIELD> KeilMapLibInterface::GetRemovedSymbols(std::string file)
/*****************************************************************************/
{
	RemovedSymbolSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<REMOVED_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<STACK_USAGE_FIELD> KeilMapLibInterface::GetStackUsage(std::string file)
/*****************************************************************************/
{
	StackUsageSection manager;

	try
	{
		manager.Manage(file);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<STACK_USAGE_FIELD> error;
		return (std::move(error));
	}
}

