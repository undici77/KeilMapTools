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
#include "MutuallyRecursiveSection.h"

/*****************************************************************************/
MutuallyRecursiveSection::MutuallyRecursiveSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
MutuallyRecursiveSection::~MutuallyRecursiveSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool MutuallyRecursiveSection::Manage(std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	MUTUALLY_RECURSIVE_FIELD                          mutually_recursive_field;
	std::string                                       fields_string;
	boost::regex                                      fileds_section_regex;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup()    + RegexString("[\\s]*Image Stack Usage Information.\\s*Maximum stack usage for Image.") + // Regex[1]
	                                         RegexMapMultilineFieldsGroup() +                                                                                         // Regex[2]
	                                         RegexStringGroup("Mutually recursive functions."));                                                                      // Regex[3]

	end_section_regex = RegexBuilder::Make(RegexStringGroup("\\sUntraceable function pointers.\\s")); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 3, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fileds_section_regex = RegexBuilder::Make(RegexString("^ \\* ")      + RegexMapMultiFieldsGroup() + // Regex[1]
	                                          RegexString(" => ")        + RegexMapMultiFieldsGroup()); // Regex[2]

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fileds_section_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fileds_section_regex))
		{
			try
			{
				mutually_recursive_field.function = fields_match_result[1].str();
				mutually_recursive_field.caller   = fields_match_result[2].str();

				boost::algorithm::trim(mutually_recursive_field.function);
				boost::algorithm::trim(mutually_recursive_field.caller);

				_Data.emplace_back(mutually_recursive_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
