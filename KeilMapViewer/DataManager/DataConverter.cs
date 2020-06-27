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
	class DataConverter<INPUT, OUTPUT>  where OUTPUT : new ()
	{
		public static OUTPUT[] Converter(INPUT[] input)
		{
			List<OUTPUT> result;

			GC.KeepAlive(input);

			result = new List<OUTPUT>();

			foreach (INPUT field in input)
			{
				result.Add((OUTPUT)Activator.CreateInstance(typeof(OUTPUT), new object[] { field }));
			}

			return (result.ToArray());
		}
	}
}
