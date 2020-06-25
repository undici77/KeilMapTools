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
#include <boost/lexical_cast.hpp>
#include <boost/algorithm/string/trim.hpp>
#include "MemoryMapImageSection.h"

/*****************************************************************************/
MemoryMapImageSection::MemoryMapImageSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
MemoryMapImageSection::~MemoryMapImageSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool MemoryMapImageSection::Manage(const std::string &file)
/*****************************************************************************/
{
	boost::regex                                                   begin_section_regex;
	boost::regex                                                   end_section_regex;
	std::string                                                    section_string;
	MEMORY_MAP_IMAGE                                               memory_map_image;
	boost::regex                                                   enrty_point_regex;
	boost::regex                                                   load_region_regex;
	std::vector<std::string>                                       load_region_vector;
	boost::regex                                                   execution_region_regex;
	std::vector<std::string>                                       execution_region_vector;
	boost::match_results<std::string::const_iterator>              match_result;
	boost::regex                                                   fields_regex;
	boost::sregex_token_iterator                                   fields_iterator;
	boost::sregex_token_iterator                                   fields_end;
	std::string                                                    fields_string;


	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup()                 +  // Regex[1]
	                                         RegexStringGroup("Memory Map of the image"));  // Regex[2]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 2, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	enrty_point_regex = RegexBuilder::Make(RegexString("  Image Entry point[ ]*:[ ]*") + RegexMapHexGroup()); // Regex[1]
	if (!boost::regex_search(section_string, match_result, enrty_point_regex))
	{
		return (false);
	}

	memory_map_image.entry_point = match_result[1].str();

	load_region_regex = RegexBuilder::Make(RegexString("Load Region ") + RegexMapSingleFieldGroup() + // Regex[1];
	                                       RegexMapMultiFieldsGroup());                               // Regex[2];

	execution_region_regex = RegexBuilder::Make(RegexString("Execution Region ") + RegexMapSingleFieldGroup() + // Regex[1];
	                                            RegexMapMultiFieldsGroup());                                    // Regex[2];

	fields_regex = RegexBuilder::Make(RegexString("^[ ]*")   + RegexMapHexGroup()                            +  // Regex[1]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[2]
	                                  RegexString("[ ]*")    + RegexMapHexGroup()                            +  // Regex[3]
	                                  RegexString("[ ]*PAD") +
	                                  RegexString("$")       +
	                                  RegexString("|")       +
	                                  RegexString("^[ ]*")   + RegexMapHexGroup()                            +  // Regex[4]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[5]
	                                  RegexString("[ ]*")    + RegexMapHexGroup()                            +  // Regex[6]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[7]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[8]
	                                  RegexString("[ ]*")    + RegexMapDecimalGroup()                        +  // Regex[9]
	                                  RegexString("[ ]*")    + RegexStringGroup("[\\*]?")                    +  // Regex[10]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[11]
	                                  RegexString("[ ]*")    + RegexMapSingleFieldGroup()                    +  // Regex[12]
	                                  RegexString("$"));

	if (!SplitSection(section_string, load_region_regex, &load_region_vector))
	{
		return (false);
	}

	for (std::string load_region_string : load_region_vector)
	{
		MEMORY_MAP_IMAGE_LOAD_REGION_FIELD load_region_field;
		if (!boost::regex_search(load_region_string, match_result, load_region_regex))
		{
			return (false);
		}

		load_region_field.name = match_result[1].str();
		load_region_field.data = match_result[2].str();

		boost::algorithm::trim(load_region_field.name);
		boost::algorithm::trim(load_region_field.data);

		if (!SplitSection(load_region_string, execution_region_regex, &execution_region_vector))
		{
			return (false);
		}

		for (std::string execution_region_string : execution_region_vector)
		{
			MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD execution_region_field;
			if (!boost::regex_search(execution_region_string, match_result, execution_region_regex))
			{
				return (false);
			}

			execution_region_field.name = match_result[1].str();
			execution_region_field.data = match_result[2].str();

			boost::algorithm::trim(execution_region_field.name);
			boost::algorithm::trim(execution_region_field.data);

			fields_iterator = boost::sregex_token_iterator(execution_region_string.begin(), execution_region_string.end(), fields_regex, 0);
			while (fields_iterator != fields_end)
			{
				MEMORY_MAP_IMAGE_OBJECT_FIELD field;

				std::string fields_string = fields_iterator->str();
				if (boost::regex_search(fields_string, match_result, fields_regex))
				{
					try
					{
						if (match_result[1].matched &&
						    match_result[2].matched &&
						    match_result[3].matched)
						{
							field.execution_address = match_result[1].str();
							field.load_address      = match_result[2].str();
							field.size              = match_result[3].str();
							field.type              = "PAD";
							field.attribute         = "-";
							field.id                = "-";
							field.entry_point       = false;
							field.section_name      = "-";
							field.object_name       = "-";
						}
						else
						{
							field.execution_address = match_result[4].str();
							field.load_address      = match_result[5].str();
							field.size              = match_result[6].str();
							field.type              = match_result[7].str();
							field.attribute         = match_result[8].str();
							field.id                = match_result[9].str();
							field.entry_point       = (match_result[10].str() == "*");
							field.section_name      = match_result[11].str();
							field.object_name       = match_result[12].str();
						}

						boost::algorithm::trim(field.execution_address);
						boost::algorithm::trim(field.load_address);
						boost::algorithm::trim(field.size);
						boost::algorithm::trim(field.type);
						boost::algorithm::trim(field.attribute);
						boost::algorithm::trim(field.id);
						boost::algorithm::trim(field.section_name);
						boost::algorithm::trim(field.object_name);

						execution_region_field.fields.emplace_back(field);
					}
					catch (...)
					{
					}
				}

				fields_iterator++;
			}

			load_region_field.execution_region.emplace_back(execution_region_field);
		}

		memory_map_image.load_region.emplace_back(load_region_field);
	}

	_Data.emplace_back(memory_map_image);

	return (true);
}
