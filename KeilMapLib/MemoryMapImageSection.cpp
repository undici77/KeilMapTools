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
bool MemoryMapImageSection::Manage(std::string &file)
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
		MEMORY_MAP_IMAGE_LOAD_REGION load_region;
		if (!boost::regex_search(load_region_string, match_result, load_region_regex))
		{
			return (false);
		}

		load_region.name = match_result[1].str();
		load_region.data = match_result[2].str();

		boost::algorithm::trim(load_region.name);
		boost::algorithm::trim(load_region.data);

		if (!SplitSection(load_region_string, execution_region_regex, &execution_region_vector))
		{
			return (false);
		}

		for (std::string execution_region_string : execution_region_vector)
		{
			MEMORY_MAP_IMAGE_EXECUTION_REGION execution_region;
			if (!boost::regex_search(execution_region_string, match_result, execution_region_regex))
			{
				return (false);
			}

			execution_region.name = match_result[1].str();
			execution_region.data = match_result[2].str();

			boost::algorithm::trim(execution_region.name);
			boost::algorithm::trim(execution_region.data);

			fields_iterator = boost::sregex_token_iterator(execution_region_string.begin(), execution_region_string.end(), fields_regex, 0);
			while (fields_iterator != fields_end)
			{
				MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD fields;

				std::string fields_string = fields_iterator->str();
				if (boost::regex_search(fields_string, match_result, fields_regex))
				{
					try
					{
						if (match_result[1].matched &&
						    match_result[2].matched &&
						    match_result[3].matched)
						{
							fields.execution_address = match_result[1].str();
							fields.load_address      = match_result[2].str();
							fields.size              = match_result[3].str();
							fields.type              = "PAD";
							fields.attribute         = "";
							fields.id                = "";
							fields.entry_point       = false;
							fields.section_name      = "";
							fields.object            = "";
						}
						else
						{
							fields.execution_address = match_result[4].str();
							fields.load_address      = match_result[5].str();
							fields.size              = match_result[6].str();
							fields.type              = match_result[7].str();
							fields.attribute         = match_result[8].str();
							fields.id                = match_result[9].str();
							fields.entry_point       = match_result[10].matched;
							fields.section_name      = match_result[11].str();
							fields.object            = match_result[12].str();
						}

						boost::algorithm::trim(fields.execution_address);
						boost::algorithm::trim(fields.load_address);
						boost::algorithm::trim(fields.size);
						boost::algorithm::trim(fields.type);
						boost::algorithm::trim(fields.attribute);
						boost::algorithm::trim(fields.id);
						boost::algorithm::trim(fields.section_name);
						boost::algorithm::trim(fields.object);

						execution_region.fields.emplace_back(fields);
					}
					catch (...)
					{
					}
				}

				fields_iterator++;
			}

			load_region.execution_region.emplace_back(execution_region);
		}

		memory_map_image.load_region.emplace_back(load_region);
	}

	_Data.emplace_back(memory_map_image);

	return (true);
}
