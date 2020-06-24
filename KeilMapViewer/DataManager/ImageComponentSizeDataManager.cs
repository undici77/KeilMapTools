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
