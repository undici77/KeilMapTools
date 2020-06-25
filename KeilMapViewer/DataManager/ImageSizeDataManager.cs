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
