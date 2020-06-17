// This file is part of KeilRtxStackSize.
//
// KeilRtxStackSize is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilRtxStackSize is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilRtxStackSize.  If not, see <https://www.gnu.org/licenses/>.

#pragma once

#include <iostream>
#include <string>
#include <stdexcept>

#include <boost/format.hpp>

#include "DebugBreak.h"

/*****************************************************************************/

#define ASSERT(condition) do \
	{ \
		if (!(condition)) \
		{ \
			debug_break(); \
			HandleError((#condition), (__FILE__), (__LINE__)); \
		} \
	} while(0)

#define STATIC_ASSERT(condition) static_assert(condition, "STATIC_ASSERT(" #condition ")");

/*****************************************************************************/

class HandleErrorException : public std::runtime_error
{
	public:
		HandleErrorException(const char *what, const char *expression, const char *file, int line) : std::runtime_error(what)
		{
			_Expression = expression;
			_File       = file;
			_Line       = line;
		}

		virtual ~HandleErrorException() throw() { };

		std::string GetExpression(void)
		{
			return (_Expression);
		};

		std::string GetFile(void)
		{
			return (_File);
		};

		int GetLine(void)
		{
			return (_Line);
		};

	private:
		std::string _Expression;
		std::string _File;
		int         _Line;
};

class HandleError
{
	public:
		HandleError(const char *expression, const char *file, int line)
		{
			std::string what;
			what = (boost::format("ASSERT(%s) in %s at line %u is false") % expression % file % line).str();
			std::cout << what << std::endl;

			throw HandleErrorException(what.c_str(), expression, file, line);
		}
};
