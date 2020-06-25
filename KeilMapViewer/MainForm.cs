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
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace KeilMapViewer
{
	public partial class MainForm : Form
	{
		private string _Ini_File_Path;
		private string _Ini_File_Name;
		private IniFile _Ini_File;
		private string _Map_File_Name;

		private ToolStripTextBoxEx _Filter_Text_Box;
		private MapFileManager _Map_File_Manager;

		public MainForm()
		{
			Init(null);
		}

		public MainForm(string ini_file_path)
		{
			Init(ini_file_path);
		}

		private void Init(string ini_file_path)
		{
			InitializeComponent();
			InitializeDelegates();

			_Ini_File_Path = "";

			_Ini_File = new IniFile();
			if (String.IsNullOrEmpty(ini_file_path))
			{
				_Ini_File_Name = "KeilMapTools.ini";
			}
			else
			{
				_Ini_File_Path = ini_file_path;
				_Ini_File_Name = Path.Combine(ini_file_path, ("KeilMapTools.ini"));
			}

			_Filter_Text_Box = new ToolStripTextBoxEx();
			MainFromMenuStrip.Items.Add(_Filter_Text_Box);
			_Filter_Text_Box.TextChanged += FilterTextBox_TextChanged;
			_Filter_Text_Box.CueText = "Filter";
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			OpenFileDialog dialog;
			string buffer;
			int width;
			int height;
			int id;
			Uri map_path;
			Uri current_path;

			_Ini_File.Load(_Ini_File_Name);

			current_path = new Uri(System.IO.Directory.GetCurrentDirectory() + "\\");

			_Map_File_Name = Uri.UnescapeDataString(_Ini_File.GetKeyValue("Map", "FilePath"));
			if (String.IsNullOrEmpty(_Map_File_Name))
			{
				if (String.IsNullOrEmpty(_Ini_File_Path))
				{
					map_path = new Uri(Path.Combine(System.IO.Directory.GetCurrentDirectory() + "\\", ("Application.map")));
					_Map_File_Name = map_path.AbsolutePath;
				}
				else
				{
					map_path = new Uri(Path.Combine(_Ini_File_Path + "\\", ("Application.map")));
					_Map_File_Name = map_path.AbsolutePath;
				}
			}

			if (!File.Exists(_Map_File_Name))
			{
				dialog = new OpenFileDialog();
				dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

				dialog.Filter = "map files (*.map)|*.map";
				if (dialog.ShowDialog(this) != DialogResult.OK)
				{
					Close();

					return;
				}

				_Map_File_Name = dialog.FileName;
			}

			try
			{
				System.IO.File.ReadAllLines(_Map_File_Name);
			}
			catch
			{
				MessageBox.Show(this, "Unable to open " + _Map_File_Name, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				Close();

				return;
			}

			_Map_File_Manager = new MapFileManager(_Map_File_Name);

			try
			{
				map_path = new Uri(current_path, _Map_File_Name);
				_Ini_File.SetKeyValue("Map", "FilePath", current_path.MakeRelativeUri(map_path).ToString());
			}
			catch
			{
			}

			buffer = _Ini_File.GetKeyValue("Window", "Width");
			try
			{
				width = int.Parse(buffer);
			}
			catch
			{
				width = 1000;
			}

			buffer = _Ini_File.GetKeyValue("Window", "Height");
			try
			{
				height = int.Parse(buffer);
			}
			catch
			{
				height = 600;
			}

			this.Size = new System.Drawing.Size(width, height);

			buffer = _Ini_File.GetKeyValue("Tab", "Selected");
			try
			{
				id = int.Parse(buffer);
			}
			catch
			{
				id = 0;
			}

			MainTabs.SelectedIndex = id;

			this.Text = App.Name + " " + App.Version + " - " + (new Uri(current_path, _Map_File_Name).AbsolutePath);
		}

		private void FilterTextBox_TextChanged(object sender, EventArgs e)
		{
			_Map_File_Manager.FilterString = _Filter_Text_Box.Text;
		}

		private void MainForm_SizeChanged(object sender, EventArgs e)
		{
			_Ini_File.SetKeyValue("Window", "Width", this.Size.Width.ToString());
			_Ini_File.SetKeyValue("Window", "Height", this.Size.Height.ToString());
		}

		private void InfoTabs_SelectedIndexChanged(object sender, EventArgs e)
		{
			_Ini_File.SetKeyValue("Tab", "Selected", MainTabs.SelectedIndex.ToString());
		}

		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog dialog;
			Uri map_path;
			Uri current_path;

			dialog = new OpenFileDialog();
			dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

			dialog.Filter = "map files (*.map)|*.map";
			if (dialog.ShowDialog(this) != DialogResult.OK)
			{
				return;
			}

			current_path   = new Uri(System.IO.Directory.GetCurrentDirectory() + "\\");
			_Map_File_Name = dialog.FileName;

			_Map_File_Manager.Close();
			_Map_File_Manager = new MapFileManager(_Map_File_Name);

			map_path = new Uri(_Map_File_Name);
			_Ini_File.SetKeyValue("Map", "FilePath", current_path.MakeRelativeUri(map_path).ToString());

			this.Text = App.Name + " " + App.Version + " - " + _Map_File_Name;
		}

		private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				_Map_File_Manager.Close();
			}
			catch
			{
			}

			try
			{
				_Ini_File.Save(_Ini_File_Name);
			}
			catch
			{
			}
		}

		private void MemoryMapImageLoadRegionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			App.Instance.BeginInvoke(App.Instance.UpdateMemoryMapImageDelegate);
		}

		private void MemoryMapImageExecutionRegionComboBox_SelectionChangeCommitted(object sender, EventArgs e)
		{
			App.Instance.BeginInvoke(App.Instance.UpdateMemoryMapImageDelegate);
		}
	}
}

