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
	class RemovedSymbolField
	{
		public readonly string Module;
		public readonly string Symbol;
		public readonly string Size;

		public RemovedSymbolField()
		{
			Module  = "";
			Symbol  = "";
			Size    = "";
		}

		public RemovedSymbolField(REMOVED_SYMBOL_FIELD field)
		{
			Module  = field.module;
			Symbol  = field.symbol;
			Size    = field.size;
		}
	}

	class RemovedSymbolDataManager : DataManager<RemovedSymbolField>
	{
		static readonly Func<RemovedSymbolField, string, bool> _Filter_Function = new Func<RemovedSymbolField, string, bool>
		(
		    (RemovedSymbolField field, string filter) =>
		{
			return ((field.Module.IndexOf(filter) != -1) ||
			        (field.Symbol.IndexOf(filter) != -1) ||
			        (field.Size.IndexOf(filter)   != -1));
		}
		);

		public RemovedSymbolDataManager(REMOVED_SYMBOL_FIELD[] input_data_vector) :
			base (DataConverter<REMOVED_SYMBOL_FIELD, RemovedSymbolField>.Converter(input_data_vector),
			      _Filter_Function)
		{
		}
	}
}
