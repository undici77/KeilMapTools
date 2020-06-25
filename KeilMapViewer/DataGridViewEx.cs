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
using System.ComponentModel;

[Browsable(true)]
class DataGridViewEx : DataGridView
{
	private SortOrder             _Sort_Order;
	private DataGridViewColumn    _Sorted_Column;
	private List<DataGridViewRow> _Rows_Selected;

	public DataGridViewEx() : base()
	{
		SortCompare += CustomSortCompare;
	}

	private void CustomSortCompare(object sender, DataGridViewSortCompareEventArgs e)
	{
		string cell_1;
		uint cell_value_1;
		string cell_2;
		uint cell_value_2;

		cell_1 = e.CellValue1.ToString();
		cell_2 = e.CellValue2.ToString();

		if ((uint.TryParse(cell_1, out cell_value_1)) &&
		    (uint.TryParse(cell_2, out cell_value_2)))
		{
			e.SortResult = cell_value_1.CompareTo(cell_value_2);
		}
		else
		{
			e.SortResult = cell_1.CompareTo(cell_2);
		}

		e.Handled = true;
	}

	public void SaveData()
	{
		_Sort_Order = SortOrder;
		_Sorted_Column = SortedColumn;

		_Rows_Selected = new List<DataGridViewRow>();
		foreach (DataGridViewRow row in Rows)
		{
			if (row.Selected)
			{
				_Rows_Selected.Add(row);
			}
		}
	}

	public void RestoreData()
	{
		ListSortDirection direction;
		bool              found;
		bool              ok;
		DataGridViewRow   sel_row;
		int               sel_row_id;
		int               cell_id;

		if (_Sorted_Column != null)
		{
			if (_Sort_Order == SortOrder.Ascending)
			{
				direction = ListSortDirection.Ascending;
			}
			else
			{
				direction = ListSortDirection.Descending;
			}
			Sort(Columns[_Sorted_Column.Name.ToString()], direction);
			Columns[_Sorted_Column.Name.ToString()].HeaderCell.SortGlyphDirection = _Sort_Order;
		}

		if (_Rows_Selected.Count > 0)
		{
			foreach (DataGridViewRow row in Rows)
			{
				found = false;
				sel_row_id = 0;
				while (!found && (sel_row_id < _Rows_Selected.Count))
				{
					ok = true;
					sel_row = _Rows_Selected[sel_row_id];
					cell_id = 0;
					do
					{
						if (!row.Cells[cell_id].Value.Equals(sel_row.Cells[cell_id].Value))
						{
							ok = false;
						}
						cell_id++;
					}
					while (ok && (cell_id < row.Cells.Count));

					found = ok;

					sel_row_id++;
				}

				if (found)
				{
					Rows[row.Index].Selected = true;
				}
				else if (MultiSelect)
				{
					Rows[row.Index].Selected = false;
				}

				if (found)
				{
					if (Rows[row.Index].Selected)
					{
						if (row.Index >= DisplayedRowCount(false))
						{
							FirstDisplayedScrollingRowIndex = (row.Index - DisplayedRowCount(false) + 1);
						}
						else
						{
							FirstDisplayedScrollingRowIndex = 0;
						}
					}
				}
			}
		}
	}
}
