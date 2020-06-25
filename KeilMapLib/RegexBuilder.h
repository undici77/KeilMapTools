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

#include <string>
#include <boost/regex.hpp>

/*****************************************************************************/

class RegexObject
{
public:
	std::string String(void) const
	{
		return (_Regex_String);
	}

	RegexObject &operator+(const RegexObject &regex_object)
	{
		_Regex_String.append(regex_object.String());

		return (*this);
	}

protected:
	std::string _Regex_String;
};

class RegexString : public RegexObject
{
public:
	RegexString(void) = delete;

	RegexString(std::string &regex_string)
	{
		_Regex_String = regex_string;
	}

	RegexString(const char *regex_string)
	{
		_Regex_String = std::string(regex_string);
	}
};

class RegexStringGroup : public RegexObject
{
public:
	RegexStringGroup(void) = delete;

	RegexStringGroup(std::string &regex_string)
	{
		_Regex_String = "(" + regex_string + ")";
	}

	RegexStringGroup(const char *regex_string)
	{
		_Regex_String = "(" + std::string(regex_string) + ")";
	}
};

class RegexSingleWordGroup : public RegexObject
{
public:
	RegexSingleWordGroup(void)
	{
		_Regex_String = R"(([a-zA-Z]+?))";
	}
};

class RegexMultiWordsGroup : public RegexObject
{
public:
	RegexMultiWordsGroup(void)
	{
		_Regex_String = R"(([a-zA-Z ]+?))";
	}
};

class RegexMapHexGroup : public RegexObject
{
public:
	RegexMapHexGroup(void)
	{
		_Regex_String = R"((0x[0-9a-fA-F]+|0X[0-9a-fA-F]+|unknown))";
	}
};

class RegexMapDecimalGroup : public RegexObject
{
public:
	RegexMapDecimalGroup(void)
	{
		_Regex_String = R"(([\d)]+))";
	}
};

class RegexMapSingleFieldGroup : public RegexObject
{
public:
	RegexMapSingleFieldGroup(void)
	{
		Format(std::string(""));
	}

	RegexMapSingleFieldGroup(std::string &regex_extra)
	{
		Format(regex_extra);
	}

	RegexMapSingleFieldGroup(const char *regex_extra)
	{
		Format(std::string(regex_extra));
	}

private:
	void Format(std::string &regex_extra)
	{
		_Regex_String = R"(([\w\(\)\_\.\:\+\-\*\\\!\~\=\<\>\$\&\|\^\%\[\]\/\,)" + regex_extra + R"(]+))";
	}
};

class RegexMapMultiFieldsGroup : public RegexObject
{
public:
	RegexMapMultiFieldsGroup(void)
	{
		RegexMapSingleFieldGroup base(R"( )");

		_Regex_String = base.String();
	}
};

class RegexMapMultilineFieldsGroup : public RegexObject
{
public:
	RegexMapMultilineFieldsGroup(void)
	{
		RegexMapSingleFieldGroup base(R"(\s)");

		_Regex_String = base.String();
	}
};

class RegexMapBeginSectionGroup : public RegexObject
{
public:
	RegexMapBeginSectionGroup(void)
	{
		_Regex_String = R"(([\=]{78,}[\s]+))";
	}
};

class RegexMapEndSectionGroup : public RegexObject
{
public:
	RegexMapEndSectionGroup(void)
	{
		_Regex_String = R"([\s]+([\=]{78,}))";
	}
};

/*****************************************************************************/

class RegexBuilder
{
public:
	static boost::regex Make(RegexObject &regex_object)
	{
		return (boost::regex(regex_object.String()));
	};
};
