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
	class CrossReferenceField
	{
		public readonly string Module;
		public readonly string Module_Reference;
		public readonly string Reference_Qualifier;
		public readonly string Symbol;

		public CrossReferenceField()
		{
			Module              = "";
			Module_Reference    = "";
			Reference_Qualifier = "";
			Symbol              = "";
		}

		public CrossReferenceField(CROSS_REFERENCE_FIELD field)
		{
			Module              = field.module;
			Module_Reference    = field.module_reference;
			Reference_Qualifier = field.reference_qualifier;
			Symbol              = field.symbol;
		}
	}

	class CrossReferenceDataManager : DataManager<CrossReferenceField>
	{
		static readonly Func<CrossReferenceField, string, bool> _Filter_Function = new Func<CrossReferenceField, string, bool>
		(
		    (CrossReferenceField field, string filter) =>
		{
			return ((field.Module.IndexOf(filter)              != -1) ||
			        (field.Module_Reference.IndexOf(filter)    != -1) ||
			        (field.Reference_Qualifier.IndexOf(filter) != -1) ||
			        (field.Symbol.IndexOf(filter)              != -1));
		}
		);

		public CrossReferenceDataManager(CROSS_REFERENCE_FIELD[] input_data_vector) :
			base (DataConverter<CROSS_REFERENCE_FIELD, CrossReferenceField>.Converter(input_data_vector),
			      _Filter_Function)
		{
		}
	}
}
