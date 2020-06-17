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
#include "FunctionPointerSection.h"

/*****************************************************************************/
FunctionPointerSection::FunctionPointerSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
FunctionPointerSection::~FunctionPointerSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool FunctionPointerSection::Manage(std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::regex                                      fields_regex;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	FUNCTION_POINTER_FIELD                            function_pointer_field;
	std::string                                       fields_string;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup()    + RegexString("Image Stack Usage Information.\\s*Maximum stack usage for Image.") + // Regex[1]
	                                         RegexMapMultilineFieldsGroup() +                                                                                   // Regex[2]
	                                         RegexStringGroup("Untraceable function pointers."));                                                              // Regex[3]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 3, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fields_regex = RegexBuilder::Make(RegexString("^ \\* ")                              + RegexMapMultiFieldsGroup() + // Regex[1]
	                                  RegexString(" from ")                              + RegexMapSingleFieldGroup() + // Regex[2]
	                                  RegexString(" referenced (([\\d]*) times )?from ") + RegexMapSingleFieldGroup()); // Regex[3] Regex[4] Regex[5]

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fields_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fields_regex))
		{
			try
			{
				function_pointer_field.symbol = fields_match_result[1].str();
				function_pointer_field.module = fields_match_result[2].str();
				if (fields_match_result[4].matched)
				{
					function_pointer_field.counts = fields_match_result[4].str();
				}
				else
				{
					function_pointer_field.counts = "1";
				}
				function_pointer_field.module_reference = fields_match_result[5].str();

				boost::algorithm::trim(function_pointer_field.symbol);
				boost::algorithm::trim(function_pointer_field.module);
				boost::algorithm::trim(function_pointer_field.module_reference);

				_Data.emplace_back(function_pointer_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
