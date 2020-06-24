using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class StackUsageField
	{
		public readonly string Function;
		public readonly string Size;

		public StackUsageField()
		{
			Function   = "";
			Size       = "";
		}

		public StackUsageField(STACK_USAGE_FIELD field)
		{
			Function   = field.function;
			try
			{
				Size = Convert.ToInt32(field.size, 16).ToString();
			}
			catch
			{
				Size = field.size;
			}
		}
	}

	class StackUsageDataManager : DataManager<StackUsageField>
	{
		static readonly Func<StackUsageField, string, bool> _Filter_Function = new Func<StackUsageField, string, bool>
		(
			(StackUsageField field, string filter) =>
			{
				return ((field.Function.IndexOf(filter)   != -1) ||
						(field.Size.IndexOf(filter)       != -1));
			}
		);

		public StackUsageDataManager(STACK_USAGE_FIELD[] input_data_vector) :
							         base (DataConverter<STACK_USAGE_FIELD, StackUsageField>.Converter(input_data_vector), 
										   _Filter_Function)
		{
		}
	}

}
