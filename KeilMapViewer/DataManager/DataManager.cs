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
	class DataManager<INPUT> : DataFilter<INPUT>
	{
		INPUT[] _Vector;

		public DataManager(INPUT[] input_data_vector,
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

			return (result.ToArray());
		}

		virtual public int Length
		{
			get
			{
				return (_Vector.Length);
			}
		}
	}
}
