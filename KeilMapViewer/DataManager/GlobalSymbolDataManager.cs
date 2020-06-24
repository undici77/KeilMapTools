using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class GlobalSymbolField
	{
		public readonly string Symbolic_Name;
		public readonly string Address;
		public readonly string Type;
		public readonly string Size;
		public readonly string Object_Name;

		public GlobalSymbolField()
		{
			Symbolic_Name = "";
			Address       = "";
			Type          = "";
			Size          = "";
			Object_Name   = "";
		}

		public GlobalSymbolField(GLOBAL_SYMBOL_FIELD field)
		{
			Symbolic_Name = field.symbolic_name;
			Address       = field.address;
			Type          = field.type;
			Size          = field.size;
			Object_Name   = field.object_name;
		}
	}

	class GlobalSymbolDataManager : DataManager<GlobalSymbolField>
	{
		static readonly Func<GlobalSymbolField, string, bool> _Filter_Function = new Func<GlobalSymbolField, string, bool>
		(
			(GlobalSymbolField field, string filter) =>
			{
				return ((field.Symbolic_Name.IndexOf(filter) != -1) ||
						(field.Address.IndexOf(filter)       != -1) ||
						(field.Type.IndexOf(filter)          != -1) ||
						(field.Size.IndexOf(filter)          != -1) ||
						(field.Object_Name.IndexOf(filter)   != -1));
			}
		);

		public GlobalSymbolDataManager(GLOBAL_SYMBOL_FIELD[] input_data_vector) :
							           base (DataConverter<GLOBAL_SYMBOL_FIELD, GlobalSymbolField>.Converter(input_data_vector), 
										     _Filter_Function)
		{
		}
	}
}
