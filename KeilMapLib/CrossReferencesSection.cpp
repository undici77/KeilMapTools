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
#include "CrossReferencesSection.h"

/*****************************************************************************/
CrossReferencesSection::CrossReferencesSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
CrossReferencesSection::~CrossReferencesSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool CrossReferencesSection::Manage(const std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	CROSS_REFERENCE_FIELD                             cross_reference_field;
	std::string                                       fields_string;
	boost::regex                                      fileds_section_regex;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup() + RegexString("Section Cross References")); // Regex[1]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 1, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fileds_section_regex = RegexBuilder::Make(RegexString("[\\s]*")             + RegexMapMultiFieldsGroup() + // Regex[1]
	                                          RegexString(" (refers (.*?)to) ") +                              // Regex[2] Regex[3]
	                                          RegexMapSingleFieldGroup()        + RegexString(" for ")       + // Regex[4]
	                                          RegexMapMultiFieldsGroup());                                     // Regex[5]

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fileds_section_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fileds_section_regex))
		{
			try
			{
				cross_reference_field.module              = fields_match_result[1].str();
				cross_reference_field.reference_qualifier = fields_match_result[3].str();
				cross_reference_field.module_reference    = fields_match_result[4].str();
				cross_reference_field.symbol              = fields_match_result[5].str();

				boost::algorithm::trim(cross_reference_field.module);
				boost::algorithm::trim(cross_reference_field.reference_qualifier);
				boost::algorithm::trim(cross_reference_field.module_reference);
				boost::algorithm::trim(cross_reference_field.symbol);

				_Data.emplace_back(cross_reference_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
