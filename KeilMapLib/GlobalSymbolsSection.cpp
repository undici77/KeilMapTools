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
#include "GlobalSymbolsSection.h"

/*****************************************************************************/
GlobalSymbolsSection::GlobalSymbolsSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
GlobalSymbolsSection::~GlobalSymbolsSection()
/*****************************************************************************/
{
}

/*****************************************************************************/
bool GlobalSymbolsSection::Manage(std::string &file)
/*****************************************************************************/
{
	boost::regex                                      begin_section_regex;
	boost::regex                                      end_section_regex;
	std::string                                       section_string;
	boost::regex                                      fields_regex;
	boost::match_results<std::string::const_iterator> fields_match_result;
	boost::sregex_token_iterator                      fields_iterator;
	boost::sregex_token_iterator                      fields_end;
	GLOBAL_SYMBOL_FIELD                              global_symbols_field;
	std::string                                       fields_string;

	_Data.clear();

	begin_section_regex = RegexBuilder::Make(RegexMapBeginSectionGroup()    + RegexString("Image Symbol Table[\\s]*Local Symbols") + // Regex[1]
	                                         RegexMapMultilineFieldsGroup() +                                                        // Regex[2]
	                                         RegexStringGroup("[\\s]+Global Symbols[\\s]+Symbol Name"));                             // Regex[3]

	end_section_regex = RegexBuilder::Make(RegexMapEndSectionGroup()); // Regex[1]

	section_string = GetSection(file, begin_section_regex, 3, end_section_regex, 1);
	if (section_string.empty())
	{
		return (false);
	}

	fields_regex = RegexBuilder::Make(RegexString("^[ ]*") + RegexMapMultiFieldsGroup()                    +  // Regex[1]
	                                  RegexString("[ ]*")  + RegexMapHexGroup()                            +  // Regex[2]
	                                  RegexString("[ ]*")  + RegexMultiWordsGroup()                        +  // Regex[3]
	                                  RegexString("[ ]*")  + RegexMapDecimalGroup()                        +  // Regex[4]
	                                  RegexString("[ ]*")  + RegexMapMultiFieldsGroup()                    +  // Regex[5]
	                                  RegexString("$")     +
	                                  RegexString("|")     +
	                                  RegexString("^[ ]*") + RegexMapMultiFieldsGroup()                    +  // Regex[6]
	                                  RegexString("[ ]*")  + RegexString("(-) (Undefined Weak Reference)") +  // Regex[7] Regex[8]
	                                  RegexString("$"));

	fields_iterator = boost::sregex_token_iterator(section_string.begin(), section_string.end(), fields_regex, 0);
	while (fields_iterator != fields_end)
	{
		fields_string = fields_iterator->str();
		if (boost::regex_search(fields_string, fields_match_result, fields_regex))
		{
			try
			{
				if (fields_match_result[1].matched &&
				    fields_match_result[2].matched &&
				    fields_match_result[3].matched &&
				    fields_match_result[4].matched &&
				    fields_match_result[5].matched)
				{
					global_symbols_field.symbolic_name = fields_match_result[1].str();
					global_symbols_field.address       = fields_match_result[2].str();
					global_symbols_field.type          = fields_match_result[3].str();
					global_symbols_field.size          = fields_match_result[4].str();
					global_symbols_field.object_name   = fields_match_result[5].str();
				}
				else
				{
					global_symbols_field.symbolic_name = fields_match_result[6].str();
					global_symbols_field.address       = fields_match_result[7].str();
					global_symbols_field.type          = fields_match_result[8].str();
					global_symbols_field.size          = "0";
					global_symbols_field.object_name   = "-";
				}

				boost::algorithm::trim(global_symbols_field.symbolic_name);
				boost::algorithm::trim(global_symbols_field.address);
				boost::algorithm::trim(global_symbols_field.type);
				boost::algorithm::trim(global_symbols_field.size);
				boost::algorithm::trim(global_symbols_field.object_name);

				_Data.emplace_back(global_symbols_field);
			}
			catch (...)
			{
			}
		}

		fields_iterator++;
	}

	return (true);
}
