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
#include "RemovedSymbolsSection.h"

/*****************************************************************************/
RemovedSymbolSection::RemovedSymbolSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
RemovedSymbolSection::~RemovedSymbolSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool RemovedSymbolSection::Manage(const std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	REMOVED_SYMBOL_FIELD                              removed_symbol_field;
	std::string                                       fields_string;
	boost::regex                                      fileds_section_regex;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup() + RegexString("Removing Unused input sections from the image.")); // Group[1]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Group[1]

	section_string = GetSection(file, begin_section_regex, 1, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fileds_section_regex = RegexBuilder::Make(RegexString("[ ]+Removing ") + RegexMapSingleFieldGroup() +                       // Group[1]
	                                          RegexString("\\(")           + RegexMapSingleFieldGroup() + RegexString("\\)") +  // Group[2]
	                                          RegexString(", ")            + RegexString("\\(([\\d]+) bytes\\)"));              // Group[3]

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fileds_section_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fileds_section_regex))
		{
			try
			{
				removed_symbol_field.module = fields_match_result[1].str();
				removed_symbol_field.symbol = fields_match_result[2].str();
				removed_symbol_field.size   = fields_match_result[3].str();

				boost::algorithm::trim(removed_symbol_field.module);
				boost::algorithm::trim(removed_symbol_field.symbol);
				boost::algorithm::trim(removed_symbol_field.size);

				_Data.emplace_back(removed_symbol_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
