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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.Reflection;
using System.IO;

namespace KeilMapViewer
{
	static class App
	{
		[DllImport("winmm.dll", EntryPoint = "timeBeginPeriod", SetLastError = true)]
		private static extern uint TimeBeginPeriod(uint uMilliseconds);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("kernel32.dll")]
		static extern bool AttachConsole(int dwProcessId);
		private const int ATTACH_PARENT_PROCESS = -1;

		static MainForm _Main_Form;
		static public MainForm Instance
		{
			get
			{
				return (_Main_Form);
			}
		}

		static string _Version;
		static public string Version
		{
			get
			{
				return (_Version);
			}
		}

		static string _Name;
		static public string Name
		{
			get
			{
				return (_Name);
			}
		}

		static string _Path;
		static public string Path
		{
			get
			{
				return (_Path);
			}
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			bool not_created;
			string process_name;
			string ini_path;

			TimeBeginPeriod(1);

			process_name = System.IO.Directory.GetCurrentDirectory() + " - " + Application.ProductName;
			process_name = process_name.Replace("\\", "");
			process_name = process_name.Replace(".", "");
			process_name = process_name.Replace(":", "");


			Assembly assembly = Assembly.GetExecutingAssembly();
			FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
			_Version = fvi.FileVersion;
			_Name = fvi.ProductName;
			_Path = System.IO.Path.GetDirectoryName(fvi.FileName) + "\\";

			not_created = true;
			using (Mutex mutex = new Mutex(true, process_name, out not_created))
			{
				if (not_created)
				{
					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);

					_Main_Form = null;
					ini_path   = null;

					if (args.Length > 1)
					{
						if (args[0] == "ini")
						{
							ini_path = args[1];
							ini_path = ini_path.TrimStart('\"');
							ini_path = ini_path.TrimEnd('\"');
						}
					}

					_Main_Form = new MainForm(ini_path);
					Application.Run(_Main_Form);
				}
				else
				{
					Process current = Process.GetCurrentProcess();
					foreach (Process process in Process.GetProcessesByName(current.ProcessName))
					{
						if (process.Id != current.Id)
						{
							SetForegroundWindow(process.MainWindowHandle);
							break;
						}
					}
				}
			}
		}
	}
}
