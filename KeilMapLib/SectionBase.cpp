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

#pragma once

#include "stdafx.h"
#include "SectionBase.h"

/*****************************************************************************/
std::string SectionBase::GetSection(const std::string &file, const boost::regex &begin_section_regex, int begin_group_id, const boost::regex &end_section_regex, int end_group_id)
/*****************************************************************************/
{
	boost::match_results<std::string::const_iterator> section_begin_match_result;
	boost::match_results<std::string::const_iterator> section_end_match_result;
	std::string                                       section_string;
	size_t                                            index;

	if (!boost::regex_search(file, section_begin_match_result, begin_section_regex))
	{
		return ("");
	}

	index = std::distance(file.cbegin(), section_begin_match_result[begin_group_id].end());
	section_string = file.substr(index);

	if (!boost::regex_search(section_string, section_end_match_result, end_section_regex))
	{
		return ("");
	}

	index = std::distance(section_string.cbegin(), section_end_match_result[end_group_id].begin());
	section_string = section_string.substr(0, index);

	return (std::move(section_string));
}

/*****************************************************************************/
bool SectionBase::SplitSection(const std::string &section, const boost::regex &split_regex, std::vector<std::string> *splitted_section)
/*****************************************************************************/
{
	size_t                                             begin_index;
	size_t                                             end_index;
	boost::sregex_token_iterator                       iterator;
	boost::sregex_token_iterator                       end;
	boost::match_results<std::string::const_iterator>  match;
	std::string                                        chunk;

	ASSERT(splitted_section != NULL);

	splitted_section->clear();

	iterator = boost::sregex_token_iterator(section.begin(), section.end(), split_regex, 0);

	if (iterator == end)
	{
		return (false);
	}

	do
	{
		begin_index = std::distance(section.cbegin(), iterator->begin());

		iterator++;

		if (iterator == end)
		{
			end_index = std::distance(section.cbegin(), section.cend());
		}
		else
		{
			end_index = std::distance(section.cbegin(), iterator->begin());
		}

		chunk = section.substr(begin_index, (end_index - begin_index));
		splitted_section->emplace_back(chunk);
	}
	while (iterator != end);

	return (true);
}

