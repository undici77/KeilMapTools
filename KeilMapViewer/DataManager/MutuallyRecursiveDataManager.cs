using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class MutuallyRecursiveField
	{
		public readonly string Function;
		public readonly string Caller;

		public MutuallyRecursiveField()
		{
			Function = "";
			Caller   = "";
		}

		public MutuallyRecursiveField(MUTUALLY_RECURSIVE_FIELD field)
		{
			Function = field.function;
			Caller   = field.caller;
		}
	}

	class MutuallyRecursiveDataManager : DataManager<MutuallyRecursiveField>
	{
		static readonly Func<MutuallyRecursiveField, string, bool> _Filter_Function = new Func<MutuallyRecursiveField, string, bool>
		(
			(MutuallyRecursiveField field, string filter) =>
			{
				return ((field.Function.IndexOf(filter) != -1) ||
						(field.Caller.IndexOf(filter)   != -1));
			}
		);

		public MutuallyRecursiveDataManager(MUTUALLY_RECURSIVE_FIELD[] input_data_vector) :
							             base (DataConverter<MUTUALLY_RECURSIVE_FIELD, MutuallyRecursiveField>.Converter(input_data_vector), 
										       _Filter_Function)
		{
		}
	}
}
