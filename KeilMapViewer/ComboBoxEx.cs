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
