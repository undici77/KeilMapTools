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

#include <fstream>
#include <boost/filesystem.hpp>

#include "Global.h"
#include "KeilMapLibClient.h"
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
KeilMapLibClient::KeilMapLibClient(void)
/*****************************************************************************/
{
}

/*****************************************************************************/
KeilMapLibClient::~KeilMapLibClient(void)
/*****************************************************************************/
{
}

/*****************************************************************************/
bool KeilMapLibClient::ReadFile(const std::string file_path)
/*****************************************************************************/
{
	std::ifstream input_file_stream;

	try
	{
		if (!boost::filesystem::exists(file_path))
		{
			return (false);
		}

		input_file_stream = std::ifstream(file_path);
		_File             = std::string(std::istreambuf_iterator<char>(input_file_stream.rdbuf()), std::istreambuf_iterator<char>());

		return (true);
	}
	catch (...)
	{

	}

	return (false);
}

/*****************************************************************************/
std::vector<CROSS_REFERENCE_FIELD> KeilMapLibClient::GetCrossReference(void)
/*****************************************************************************/
{
	CrossReferencesSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<CROSS_REFERENCE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<FUNCTION_POINTER_FIELD> KeilMapLibClient::GetFunctionPointer(void)
/*****************************************************************************/
{
	FunctionPointerSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<FUNCTION_POINTER_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<GLOBAL_SYMBOL_FIELD> KeilMapLibClient::GetGlobalSymbols(void)
/*****************************************************************************/
{
	GlobalSymbolsSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<GLOBAL_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<IMAGE_COMPONENT_SIZE_FIELD> KeilMapLibClient::GetImageComponentSize(void)
/*****************************************************************************/
{
	ImageComponentSizeSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<IMAGE_COMPONENT_SIZE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
IMAGE_SIZE_DATA KeilMapLibClient::GetImageSize(void)
/*****************************************************************************/
{
	ImageSizeSection manager;
	IMAGE_SIZE_DATA  result;

	try
	{
		manager.Manage(_File);

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
std::vector<LOCAL_SYMBOL_FIELD> KeilMapLibClient::GetLocalSymbols(void)
/*****************************************************************************/
{
	LocalSymbolsSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<LOCAL_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<MAXIMUM_STACK_USAGE_FIELD> KeilMapLibClient::GetMaximumStackUsage(void)
/*****************************************************************************/
{
	MaximumStackUsageSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<MAXIMUM_STACK_USAGE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
MEMORY_MAP_IMAGE KeilMapLibClient::GetMemoryMapImage(void)
/*****************************************************************************/
{
	MemoryMapImageSection manager;
	MEMORY_MAP_IMAGE      result;

	try
	{
		manager.Manage(_File);

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
std::vector<MUTUALLY_RECURSIVE_FIELD> KeilMapLibClient::GetMutualRecursive(void)
/*****************************************************************************/
{
	MutuallyRecursiveSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<MUTUALLY_RECURSIVE_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<REMOVED_SYMBOL_FIELD> KeilMapLibClient::GetRemovedSymbols(void)
/*****************************************************************************/
{
	RemovedSymbolSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<REMOVED_SYMBOL_FIELD> error;
		return (std::move(error));
	}
}

/*****************************************************************************/
std::vector<STACK_USAGE_FIELD> KeilMapLibClient::GetStackUsage(void)
/*****************************************************************************/
{
	StackUsageSection manager;

	try
	{
		manager.Manage(_File);
		return (std::move(manager.Get()));
	}
	catch (...)
	{
		std::vector<STACK_USAGE_FIELD> error;
		return (std::move(error));
	}
}

