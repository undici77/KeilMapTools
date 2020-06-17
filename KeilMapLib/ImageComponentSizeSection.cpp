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
#include "ImageComponentSizeSection.h"

/*****************************************************************************/
ImageComponentSizeSection::ImageComponentSizeSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
ImageComponentSizeSection::~ImageComponentSizeSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool ImageComponentSizeSection::Manage(std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::regex                                      begin_image_section_regex;
	boost::regex                                      end_image_section_regex;
	std::vector<std::string>                          image_section_vector;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	IMAGE_COMPONENT_SIZE_FIELD                        image_component_field;
	std::string                                       fields_string;
	boost::regex                                      fileds_section_regex;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup() + RegexString("Image component sizes")); // Regex[1]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 1, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	begin_image_section_regex = RegexBuilder::Make(RegexString("Code \\(inc\\. data\\)") + RegexString("[ ]*") +
	                                               RegexString("RO Data")                + RegexString("[ ]*") +
	                                               RegexString("RW Data")                + RegexString("[ ]*") +
	                                               RegexString("ZI Data")                + RegexString("[ ]*") +
	                                               RegexString("Debug")                  + RegexMapMultiFieldsGroup()); // Regex[1]

	end_image_section_regex = RegexBuilder::Make(RegexString(R"([\s]+([\-]{68,}))"));// Regex[1]

	fileds_section_regex = RegexBuilder::Make(RegexString("^[ ]*")   + RegexMapDecimalGroup()     +  // Regex[1]
	                                          RegexString("[ ]*")    + RegexMapDecimalGroup()     +  // Regex[2]
	                                          RegexString("[ ]*")    + RegexMapDecimalGroup()     +  // Regex[3]
	                                          RegexString("[ ]*")    + RegexMapDecimalGroup()     +  // Regex[4]
	                                          RegexString("[ ]*")    + RegexMapDecimalGroup()     +  // Regex[5]
	                                          RegexString("[ ]*")    + RegexMapDecimalGroup()     +  // Regex[6]
	                                          RegexString("[ ]*")    + RegexMapMultiFieldsGroup() +  // Regex[7]
	                                          RegexString("$"));

	if (!SplitSection(section_string, begin_image_section_regex, &image_section_vector))
	{
		return (false);
	}

	for (std::string image_section_string : image_section_vector)
	{
		section_string = GetSection(image_section_string, begin_image_section_regex, 1, end_image_section_regex, 1);

		fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fileds_section_regex, 0);
		while (fields_iterator != fields_end)
		{
			fields_string = fields_iterator->str();
			if (boost::regex_search(fields_string, fields_match_result, fileds_section_regex))
			{
				try
				{
					image_component_field.code            = fields_match_result[1].str();
					image_component_field.inline_data     = fields_match_result[2].str();
					image_component_field.read_only_data  = fields_match_result[3].str();
					image_component_field.read_write_data = fields_match_result[4].str();
					image_component_field.zero_init_data  = fields_match_result[5].str();
					image_component_field.debug_data      = fields_match_result[6].str();
					image_component_field.object_name     = fields_match_result[7].str();

					boost::algorithm::trim(image_component_field.code);
					boost::algorithm::trim(image_component_field.inline_data);
					boost::algorithm::trim(image_component_field.read_only_data);
					boost::algorithm::trim(image_component_field.read_write_data);
					boost::algorithm::trim(image_component_field.zero_init_data);
					boost::algorithm::trim(image_component_field.debug_data);
					boost::algorithm::trim(image_component_field.object_name);

					_Data.emplace_back(image_component_field);
				}
				catch (...)
				{
				}
			}

			fields_iterator++;
		}
	}

	return (true);
}
