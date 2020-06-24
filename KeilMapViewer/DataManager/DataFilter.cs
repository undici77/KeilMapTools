using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class DataFilter<T>
	{
		Func<T, string, bool> _Filter_Function;

		public DataFilter(Func<T, string, bool> filter_function)
		{
			_Filter_Function = filter_function; 
		}

		protected bool Filter(T field, string pattern)
		{
			return(_Filter_Function(field, pattern));
		}
	}
}
