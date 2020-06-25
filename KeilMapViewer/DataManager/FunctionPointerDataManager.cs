// This file is part of KeilMapViewer.
//
// KeilMapViewer is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// KeilMapViewer is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with KeilMapViewer.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class FunctionPointerField
	{
		public readonly string Symbol;
		public readonly string Module;
		public readonly string Counts;
		public readonly string Module_Reference;

		public FunctionPointerField()
		{
			Symbol           = "";
			Module           = "";
			Counts           = "";
			Module_Reference = "";
		}

		public FunctionPointerField(FUNCTION_POINTER_FIELD field)
		{
			Symbol           = field.symbol;
			Module           = field.module;
			Counts           = field.counts;
			Module_Reference = field.module_reference;
		}
	}

	class FunctionPointerDataManager : DataManager<FunctionPointerField>
	{
		static readonly Func<FunctionPointerField, string, bool> _Filter_Function = new Func<FunctionPointerField, string, bool>
		(
		    (FunctionPointerField field, string filter) =>
		{
			return ((field.Symbol.IndexOf(filter)           != -1) ||
			        (field.Module.IndexOf(filter)           != -1) ||
			        (field.Counts.IndexOf(filter)           != -1) ||
			        (field.Module_Reference.IndexOf(filter) != -1));
		}
		);

		public FunctionPointerDataManager(FUNCTION_POINTER_FIELD[] input_data_vector) :
			base (DataConverter<FUNCTION_POINTER_FIELD, FunctionPointerField>.Converter(input_data_vector),
			      _Filter_Function)
		{
		}
	}
}
