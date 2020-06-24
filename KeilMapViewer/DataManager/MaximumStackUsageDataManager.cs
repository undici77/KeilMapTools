using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class MaximumStackUsageField
	{
		public readonly string Function;
		public readonly string Size;
		public readonly string Call_Chain;

		public MaximumStackUsageField()
		{
			Function   = "";
			Size       = "";
			Call_Chain = "";
		}

		public MaximumStackUsageField(MAXIMUM_STACK_USAGE_FIELD field)
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
			Call_Chain = field.call_chain;
		}
	}

	class MaximumStackUsageDataManager : DataManager<MaximumStackUsageField>
	{
		static readonly Func<MaximumStackUsageField, string, bool> _Filter_Function = new Func<MaximumStackUsageField, string, bool>
		(
			(MaximumStackUsageField field, string filter) =>
			{
				return ((field.Function.IndexOf(filter)   != -1) ||
						(field.Size.IndexOf(filter)       != -1));
			}
		);

		public MaximumStackUsageDataManager(MAXIMUM_STACK_USAGE_FIELD[] input_data_vector) :
							                base (DataConverter<MAXIMUM_STACK_USAGE_FIELD, MaximumStackUsageField>.Converter(input_data_vector), 
										          _Filter_Function)
		{
		}
	}
}
