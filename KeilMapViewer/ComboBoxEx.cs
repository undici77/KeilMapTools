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
using System.Windows.Forms;

namespace KeilMapViewer
{
	class ComboBoxEx : ComboBox
	{
		object _Selected_Item;

		public void SaveData()
		{
			_Selected_Item = SelectedItem;
		}

		public void RestoreData()
		{
			try
			{
				if (_Selected_Item == null)
				{
					SelectedIndex = 0;
				}
				else
				{
					int index;
					index = FindString((string)_Selected_Item);
					if (index == -1)
					{
						SelectedIndex = 0;
					}
					else
					{
						SelectedIndex = index;
					}
				}
			}
			catch
			{
			}
		}
	}
}
