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
#include <boost\algorithm\string\trim.hpp>
#include "ImageSizeSection.h"

/*****************************************************************************/
ImageSizeSection::ImageSizeSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
ImageSizeSection::~ImageSizeSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool ImageSizeSection::Manage(const std::string &file)
/*****************************************************************************/
{
	boost::regex                                      section_regex;
	boost::match_results<std::string::const_iterator> match_result;
	IMAGE_SIZE_DATA                                   image_size_data;

	_Data.clear();

	section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup() +                                                                                            // Group[1]
	                                   RegexString("Total RO  Size \\(Code \\+ RO Data\\)")                                      + RegexMapMultiFieldsGroup() + // Group[2]
	                                   RegexString("\\s[ ]*") + RegexString("Total RW  Size \\(RW Data \\+ ZI Data\\)")          + RegexMapMultiFieldsGroup() + // Group[3]
	                                   RegexString("\\s[ ]*") + RegexString("Total ROM Size \\(Code \\+ RO Data \\+ RW Data\\)") + RegexMapMultiFieldsGroup()); // Group[4]

	if (boost::regex_search(file, match_result, section_regex))
	{
		try
		{
			image_size_data.total_read_only_size  = match_result[2].str();
			image_size_data.total_read_write_size = match_result[3].str();
			image_size_data.total_rom_size        = match_result[4].str();

			boost::algorithm::trim(image_size_data.total_read_only_size);
			boost::algorithm::trim(image_size_data.total_read_write_size);
			boost::algorithm::trim(image_size_data.total_rom_size);

			_Data.emplace_back(image_size_data);
		}
		catch (...)
		{
		}
	}

	return (true);
}
