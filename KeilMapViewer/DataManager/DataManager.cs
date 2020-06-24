using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class DataManager<INPUT> : DataFilter<INPUT>
	{
		IEnumerable<INPUT> _Vector;

		public DataManager(IEnumerable<INPUT> input_data_vector,
						  Func<INPUT, string, bool> filter_function) : base(filter_function)
		{
			_Vector = input_data_vector;		
		}

		virtual public INPUT[] Get(string filter_string)
		{
			List<INPUT> result;

			result = new List<INPUT>();

			foreach (INPUT field in _Vector)
			{
				if (Filter(field, filter_string))
				{
					result.Add(field);	
				}
			}

			return(result.ToArray());
		}
	}
}
