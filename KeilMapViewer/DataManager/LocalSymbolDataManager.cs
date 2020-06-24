using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class LocalSymbolField
	{
		public readonly string Symbolic_Name;
		public readonly string Address;
		public readonly string Type;
		public readonly string Size;
		public readonly string Object_Name;

		public LocalSymbolField()
		{
			Symbolic_Name = "";
			Address       = "";
			Type          = "";
			Size          = "";
			Object_Name   = "";
		}

		public LocalSymbolField(LOCAL_SYMBOL_FIELD field)
		{
			Symbolic_Name = field.symbolic_name;
			Address       = field.address;
			Type          = field.type;
			Size          = field.size;
			Object_Name   = field.object_name;
		}
	}

	class LocalSymbolDataManager : DataManager<LocalSymbolField>
	{
		static readonly Func<LocalSymbolField, string, bool> _Filter_Function = new Func<LocalSymbolField, string, bool>
		(
			(LocalSymbolField field, string filter) =>
			{
				return ((field.Symbolic_Name.IndexOf(filter) != -1) ||
						(field.Address.IndexOf(filter)       != -1) ||
						(field.Type.IndexOf(filter)          != -1) ||
						(field.Size.IndexOf(filter)          != -1) ||
						(field.Object_Name.IndexOf(filter)   != -1));
			}
		);

		public LocalSymbolDataManager(LOCAL_SYMBOL_FIELD[] input_data_vector) :
							           base (DataConverter<LOCAL_SYMBOL_FIELD, LocalSymbolField>.Converter(input_data_vector), 
										     _Filter_Function)
		{
		}
	}
}
