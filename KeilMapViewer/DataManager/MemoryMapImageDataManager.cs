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
	class MemoryMapImageObjectField
	{
		public readonly string Execution_Address;
		public readonly string Load_Address;
		public readonly string Size;
		public readonly string Type;
		public readonly string Attribute;
		public readonly string Id;
		public readonly bool   Entry_Point;
		public readonly string Section_Name;
		public readonly string Object_Name;

		public MemoryMapImageObjectField()
		{
			Execution_Address = "";
			Load_Address      = "";
			Size              = "";
			Type              = "";
			Attribute         = "";
			Id                = "";
			Entry_Point       = false;
			Section_Name      = "";
			Object_Name       = "";
		}

		public MemoryMapImageObjectField(MEMORY_MAP_IMAGE_OBJECT_FIELD field)
		{
			Execution_Address = field.execution_address;
			Load_Address      = field.load_address;
			Size              = Convert.ToInt32(field.size, 16).ToString();
			Type              = field.type;
			Attribute         = field.attribute;
			Id                = field.id;
			Entry_Point       = field.entry_point;
			Section_Name      = field.section_name;
			Object_Name       = field.object_name;
		}
	};

	class MemoryMapImageExecutionRegionField
	{
		public string                          Name;
		public string                          Data;
		public List<MemoryMapImageObjectField> Fields;

		public MemoryMapImageExecutionRegionField()
		{
			Name   = "";
			Data   = "";
			Fields = new List<MemoryMapImageObjectField>();
		}

		public MemoryMapImageExecutionRegionField(MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD field)
		{
			Name = field.name;
			Data = field.data;
			Fields = new List<MemoryMapImageObjectField>(field.fields.Count);
			foreach (MEMORY_MAP_IMAGE_OBJECT_FIELD f in field.fields)
			{
				Fields.Add(new MemoryMapImageObjectField(f));
			}
		}
	};

	class MemoryMapImageLoadRegionField
	{
		public string                                   Name;
		public string                                   Data;
		public List<MemoryMapImageExecutionRegionField> Execution_Region;

		public MemoryMapImageLoadRegionField()
		{
			Name             = "";
			Data             = "";
			Execution_Region = new List<MemoryMapImageExecutionRegionField>();
		}

		public MemoryMapImageLoadRegionField(MEMORY_MAP_IMAGE_LOAD_REGION_FIELD field)
		{
			Name             = field.name;
			Data             = field.data;
			Execution_Region = new List<MemoryMapImageExecutionRegionField>(field.execution_region.Count);
			foreach (MEMORY_MAP_IMAGE_EXECUTION_REGION_FIELD f in field.execution_region)
			{
				Execution_Region.Add(new MemoryMapImageExecutionRegionField(f));
			}
		}
	};

	class MemoryMapImage
	{
		public string                              Entry_Point;
		public List<MemoryMapImageLoadRegionField> Load_Region;

		public MemoryMapImage()
		{
			Entry_Point = "";
			Load_Region = new List<MemoryMapImageLoadRegionField>();
		}

		public MemoryMapImage(MEMORY_MAP_IMAGE field)
		{
			Entry_Point = field.entry_point;
			Load_Region = new List<MemoryMapImageLoadRegionField>(field.load_region.Count);
			foreach (MEMORY_MAP_IMAGE_LOAD_REGION_FIELD f in field.load_region)
			{
				Load_Region.Add(new MemoryMapImageLoadRegionField(f));
			}
		}
	};

	class MemoryMapImageDataManager : DataFilter<MemoryMapImageObjectField>
	{
		static readonly Func<MemoryMapImageObjectField, string, bool> _Filter_Function = new Func<MemoryMapImageObjectField, string, bool>
		(
		    (MemoryMapImageObjectField field, string filter) =>
		{
			return ((field.Execution_Address.IndexOf(filter) != -1) ||
			        (field.Load_Address.IndexOf(filter)      != -1) ||
			        (field.Size.IndexOf(filter)              != -1) ||
			        (field.Type.IndexOf(filter)              != -1) ||
			        (field.Attribute.IndexOf(filter)         != -1) ||
			        (field.Id.IndexOf(filter)                != -1) ||
			        (field.Section_Name.IndexOf(filter)      != -1) ||
			        (field.Object_Name.IndexOf(filter)       != -1));
		}
		);

		private MemoryMapImage _Input_Data;

		public MemoryMapImageDataManager(MEMORY_MAP_IMAGE input_data) :
			base (_Filter_Function)
		{
			_Input_Data = new MemoryMapImage(input_data);
		}

		public MemoryMapImage Get(string filter_string)
		{
			MemoryMapImage result = new MemoryMapImage();

			result.Entry_Point = _Input_Data.Entry_Point;
			foreach (MemoryMapImageLoadRegionField load_region in _Input_Data.Load_Region)
			{
				MemoryMapImageLoadRegionField result_load_region;

				result_load_region      = new MemoryMapImageLoadRegionField();
				result_load_region.Name = load_region.Name;
				result_load_region.Data = load_region.Data;
				foreach (MemoryMapImageExecutionRegionField execution_region in load_region.Execution_Region)
				{
					MemoryMapImageExecutionRegionField result_execution_region;

					result_execution_region      = new MemoryMapImageExecutionRegionField();
					result_execution_region.Name = execution_region.Name;
					result_execution_region.Data = execution_region.Data;
					foreach (MemoryMapImageObjectField field in execution_region.Fields)
					{
						if (Filter(field, filter_string))
						{
							result_execution_region.Fields.Add(field);
						}
					}

					if (result_execution_region.Fields.Count > 0)
					{
						result_load_region.Execution_Region.Add(result_execution_region);
					}
				}

				if (result_load_region.Execution_Region.Count > 0)
				{
					result.Load_Region.Add(result_load_region);
				}
			}

			return (result);
		}
	}
}
