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
	class ImageComponentSizeField
	{
		public readonly string Code;
		public readonly string Inline_Data;
		public readonly string Read_Only_Data;
		public readonly string Read_Write_Data;
		public readonly string Zero_Init_Data;
		public readonly string Debug_Data;
		public readonly string Object_Name;

		public ImageComponentSizeField()
		{
			Code            = "";
			Inline_Data     = "";
			Read_Only_Data  = "";
			Read_Write_Data = "";
			Zero_Init_Data  = "";
			Debug_Data      = "";
			Object_Name     = "";
		}

		public ImageComponentSizeField(IMAGE_COMPONENT_SIZE_FIELD field)
		{
			Code            = field.code;
			Inline_Data     = field.inline_data;
			Read_Only_Data  = field.read_only_data;
			Read_Write_Data = field.read_write_data;
			Zero_Init_Data  = field.zero_init_data;
			Debug_Data      = field.debug_data;
			Object_Name     = field.object_name;
		}
	}

	class ImageComponentSizeDataManager : DataManager<ImageComponentSizeField>
	{
		static readonly Func<ImageComponentSizeField, string, bool> _Filter_Function = new Func<ImageComponentSizeField, string, bool>
		(
		    (ImageComponentSizeField field, string filter) =>
		{
			return ((field.Code.IndexOf(filter)            != -1) ||
			        (field.Inline_Data.IndexOf(filter)     != -1) ||
			        (field.Read_Only_Data.IndexOf(filter)  != -1) ||
			        (field.Read_Write_Data.IndexOf(filter) != -1) ||
			        (field.Zero_Init_Data.IndexOf(filter)  != -1) ||
			        (field.Debug_Data.IndexOf(filter)      != -1) ||
			        (field.Object_Name.IndexOf(filter)     != -1));
		}
		);

		public ImageComponentSizeDataManager(IMAGE_COMPONENT_SIZE_FIELD[] input_data_vector) :
			base (DataConverter<IMAGE_COMPONENT_SIZE_FIELD, ImageComponentSizeField>.Converter(input_data_vector),
			      _Filter_Function)
		{
		}
	}
}
