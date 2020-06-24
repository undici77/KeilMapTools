using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class DataConverter<INPUT, OUTPUT>  where OUTPUT : new()
	{
		public static OUTPUT[] Converter(INPUT[] input)
		{
			List<OUTPUT> result;
		
			result = new List<OUTPUT>();

			foreach (INPUT field in input)
			{
				result.Add((OUTPUT)Activator.CreateInstance(typeof(OUTPUT), new object[] { field }));
			}

			return(result.ToArray());
		}
	}
}
