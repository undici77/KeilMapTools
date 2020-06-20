using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCs
{
	class Program
	{
		static void Main(string[] args)
		{
			KeilMapLibClient kli = new KeilMapLibClient();

			kli.ReadFile(@"C:\Projects\Application.C.map");
			CROSS_REFERENCE_VECTOR cr = kli.GetCrossReference();
			FUNCTION_POINTER_VECTOR fp = kli.GetFunctionPointer();
			GLOBAL_SYMBOL_VECTOR gs = kli.GetGlobalSymbols();
			IMAGE_COMPONENT_SIZE_VECTOR ics = kli.GetImageComponentSize();
			IMAGE_SIZE_DATA isd = kli.GetImageSize();
			MAXIMUM_STACK_USAGE_VECTOR msu = kli.GetMaximumStackUsage();
			MEMORY_MAP_IMAGE mmi =  kli.GetMemoryMapImage();
			MUTUALLY_RECURSIVE_VECTOR mr = kli.GetMutualRecursive();
			LOCAL_SYMBOL_VECTOR ls = kli.GetLocalSymbols();
			REMOVED_SYMBOL_VECTOR rs = kli.GetRemovedSymbols();
			STACK_USAGE_VECTOR su = kli.GetStackUsage();
		}
	}
}
