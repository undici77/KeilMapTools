using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class ImageSizeData
	{
		public readonly string Total_Read_Only_Size;
		public readonly string Total_Read_Write_Size;
		public readonly string Total_Rom_Size;

		public ImageSizeData()
		{
			Total_Read_Only_Size  = "";
			Total_Read_Write_Size = "";
			Total_Rom_Size        = "";
		}

		public ImageSizeData(IMAGE_SIZE_DATA field)
		{
			Total_Read_Only_Size  = field.total_read_only_size;
			Total_Read_Write_Size = field.total_read_write_size;
			Total_Rom_Size        = field.total_rom_size;
		}
	}
}
