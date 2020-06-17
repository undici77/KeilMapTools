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
#include "MaximumStackUsageSection.h"

/*****************************************************************************/
MaximumStackUsageSection::MaximumStackUsageSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
MaximumStackUsageSection::~MaximumStackUsageSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool MaximumStackUsageSection::Manage(std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	MAXIMUM_STACK_USAGE_FIELD                         maximum_stack_usage_field;
	std::string                                       fields_string;
	boost::regex                                      fileds_section_regex;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup() + RegexString("Image Stack Usage Information.")); // Regex[1]


	end_section_regex = RegexBuilder::Make(RegexStringGroup("\\sStack Usage for functions.\\s")); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 1, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fileds_section_regex = RegexBuilder::Make(RegexString("Maximum Stack Usage for ")                         + RegexMapMultiFieldsGroup() + // Regex[1]
	                                          RegexString(" ")                                                + RegexMapHexGroup()         + // Regex[2]
	                                          RegexString(" bytes.\\sCall chain for maximum stack usage:\\s") + RegexMapMultiFieldsGroup()); // Regex[3]

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fileds_section_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fileds_section_regex))
		{
			try
			{
				maximum_stack_usage_field.function   = fields_match_result[1].str();
				maximum_stack_usage_field.size       = fields_match_result[2].str();
				maximum_stack_usage_field.call_chain = fields_match_result[3].str();

				boost::algorithm::trim(maximum_stack_usage_field.function);
				boost::algorithm::trim(maximum_stack_usage_field.size);
				boost::algorithm::trim(maximum_stack_usage_field.call_chain);

				_Data.emplace_back(maximum_stack_usage_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
