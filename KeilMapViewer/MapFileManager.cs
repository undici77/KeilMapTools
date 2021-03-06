﻿// This file is part of KeilMapViewer.
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KeilMapViewer
{
	class MapFileManager
	{
		private readonly object _Filter_String_Lock;
		private string _Filter_String;
		public string FilterString
		{
			get
			{
				lock (_Filter_String_Lock)
				{
					return (_Filter_String);
				}
			}

			set
			{
				lock (_Filter_String_Lock)
				{
					_Worker_Thread_Update_Event.Set();
					_Filter_String = value;
				}
			}
		}

		private readonly object _Cross_Reference_Data_Lock;
		private CrossReferenceField[] _Cross_Reference_Data;
		public CrossReferenceField[] CrossReferenceSectionData
		{
			get
			{
				lock (_Cross_Reference_Data_Lock)
				{
					return (_Cross_Reference_Data);
				}
			}
		}

		private readonly object _Removed_Symbol_Data_Lock;
		private RemovedSymbolField[] _Removed_Symbol_Data;
		public RemovedSymbolField[] RemovedSymbolSectionData
		{
			get
			{
				lock (_Removed_Symbol_Data_Lock)
				{
					return (_Removed_Symbol_Data);
				}
			}
		}

		private readonly object _Maximum_Stack_Usage_Data_Lock;
		private MaximumStackUsageField[] _Maximum_Stack_Usage_Data;
		public MaximumStackUsageField[] MaximumStackUsageSectionData
		{
			get
			{
				lock (_Removed_Symbol_Data_Lock)
				{
					return (_Maximum_Stack_Usage_Data);
				}
			}
		}

		private readonly object _Stack_Usage_Data_Lock;
		private StackUsageField[] _Stack_Usage_Data;
		public StackUsageField[] StackUsageSectionData
		{
			get
			{
				lock (_Stack_Usage_Data_Lock)
				{
					return (_Stack_Usage_Data);
				}
			}
		}

		private readonly object _Mutually_Recursive_Data_Lock;
		private MutuallyRecursiveField[] _Mutually_Recursive_Data;
		public MutuallyRecursiveField[] MutuallyRecursiveSectionData
		{
			get
			{
				lock (_Mutually_Recursive_Data_Lock)
				{
					return (_Mutually_Recursive_Data);
				}
			}
		}

		private readonly object _Function_Pointer_Data_Lock;
		private FunctionPointerField[] _Function_Pointer_Data;
		public FunctionPointerField[] FunctionPointerSectionData
		{
			get
			{
				lock (_Function_Pointer_Data_Lock)
				{
					return (_Function_Pointer_Data);
				}
			}
		}

		private readonly object _Local_Symbol_Data_Lock;
		private LocalSymbolField[] _Local_Symbol_Data;
		public LocalSymbolField[] LocalSymbolSectionData
		{
			get
			{
				lock (_Local_Symbol_Data_Lock)
				{
					return (_Local_Symbol_Data);
				}
			}
		}
		private readonly object _Global_Symbol_Data_Lock;
		private GlobalSymbolField[] _Global_Symbol_Data;
		public GlobalSymbolField[] GlobalSymbolData
		{
			get
			{
				lock (_Global_Symbol_Data_Lock)
				{
					return (_Global_Symbol_Data);
				}
			}
		}

		private readonly object _Memory_Map_Image_Data_Lock;
		private MemoryMapImage _Memory_Map_Image_Data;
		public MemoryMapImage MemoryMapImageData
		{
			get
			{
				lock (_Memory_Map_Image_Data_Lock)
				{
					return (_Memory_Map_Image_Data);
				}
			}
		}

		private readonly object _Image_Component_Size_Data_Lock;
		private ImageComponentSizeField[] _Image_Component_Size_Data;
		public ImageComponentSizeField[] ImageComponentSizeData
		{
			get
			{
				lock (_Image_Component_Size_Data_Lock)
				{
					return (_Image_Component_Size_Data);
				}
			}
		}

		private readonly object _Image_Size_Data_Lock;
		private ImageSizeData _Image_Size_Data;
		public ImageSizeData ImageSizeData
		{
			get
			{
				lock (_Image_Size_Data_Lock)
				{
					return (_Image_Size_Data);
				}
			}
		}

		private string _Map_File;
		private bool _Map_File_Update;
		private Thread _Worker_Thread;
		private EventWaitHandle _Worker_Thread_Update_Event;
		private FileSystemWatcher _Watcher;

		public MapFileManager(string map_file)
		{
			_Map_File = map_file;

			_Filter_String_Lock = new object();
			_Cross_Reference_Data_Lock = new object();
			_Removed_Symbol_Data_Lock  = new object();
			_Maximum_Stack_Usage_Data_Lock	= new object();
			_Stack_Usage_Data_Lock = new object();
			_Mutually_Recursive_Data_Lock = new object();
			_Function_Pointer_Data_Lock	= new object();
			_Local_Symbol_Data_Lock	= new object();
			_Global_Symbol_Data_Lock = new object();
			_Memory_Map_Image_Data_Lock = new object();
			_Image_Component_Size_Data_Lock = new object();
			_Image_Size_Data_Lock = new object();

			_Filter_String = "";

			_Worker_Thread_Update_Event = new EventWaitHandle(false, EventResetMode.AutoReset);

			_Worker_Thread = new Thread(this.WorkerThread);
			_Worker_Thread.Start();
		}

		private void WorkerThread()
		{
			KeilMapLibClient              client;
			CrossReferenceDataManager     cross_reference_manager;
			RemovedSymbolDataManager      removed_symbol_data_manager;
			MaximumStackUsageDataManager  maximum_stack_usage_data_manager;
			StackUsageDataManager         stack_usage_data_manager;
			MutuallyRecursiveDataManager  mutually_recursive_data_manager;
			FunctionPointerDataManager	  function_pointer_data_manager;
			LocalSymbolDataManager		  local_symbol_data_manager;
			GlobalSymbolDataManager		  global_symbol_data_manager;
			MemoryMapImageDataManager     memory_map_image_data_manager;
			ImageComponentSizeDataManager image_component_size_data_manager;
			bool                          stack_info_ok;

			cross_reference_manager           = null;
			removed_symbol_data_manager       = null;
			maximum_stack_usage_data_manager  = null;
			stack_usage_data_manager          = null;
			mutually_recursive_data_manager   = null;
			function_pointer_data_manager     = null;
			local_symbol_data_manager         = null;
			global_symbol_data_manager        = null;
			memory_map_image_data_manager     = null;
			image_component_size_data_manager = null;

			_Watcher = new FileSystemWatcher();
			_Watcher.EnableRaisingEvents = false;
			_Watcher.NotifyFilter = NotifyFilters.LastWrite;

			_Watcher.Changed += new FileSystemEventHandler(FileSystemWatcher_OnChanged);

			_Watcher.Path = Path.GetDirectoryName(_Map_File);
			_Watcher.Filter = Path.GetFileName(_Map_File);
			_Watcher.EnableRaisingEvents = true;

			_Map_File_Update = true;

			stack_info_ok = false;
			try
			{
				while (true)
				{
					if (_Map_File_Update)
					{
						using (client = new KeilMapLibClient())
						{
							_Map_File_Update = false;
							if (!client.ReadFile(_Map_File))
							{
								App.Instance.BeginInvoke(App.Instance.ShowErrorDelegate, "Unable to read " + _Map_File, "MapFileManager");
							}

							Task image_size_data_task = Task.Run(() =>
							{
								using (IMAGE_SIZE_DATA data = client.GetImageSize())
								{
									ImageSizeData result = new ImageSizeData(data);

									lock (_Image_Size_Data_Lock)
									{
										_Image_Size_Data = result;
									}
									App.Instance.BeginInvoke(App.Instance.UpdateImageSizeDataDelegate);
								}
							});

							Task cross_reference_task = Task.Run(() =>
							{
								using (CROSS_REFERENCE_VECTOR vector = client.GetCrossReference())
								{
									cross_reference_manager = new CrossReferenceDataManager(vector.ToArray());
								}
							});

							Task removed_symbol_task = Task.Run(() =>
							{
								using (REMOVED_SYMBOL_VECTOR vector = client.GetRemovedSymbols())
								{
									removed_symbol_data_manager = new RemovedSymbolDataManager(vector.ToArray());
								}
							});

							Task maximum_stack_usage_task = Task.Run(() =>
							{
								using (MAXIMUM_STACK_USAGE_VECTOR vector = client.GetMaximumStackUsage())
								{
									maximum_stack_usage_data_manager = new MaximumStackUsageDataManager(vector.ToArray());
								}
							});

							Task stack_usage_task = Task.Run(() =>
							{
								using (STACK_USAGE_VECTOR vector = client.GetStackUsage())
								{
									stack_usage_data_manager = new StackUsageDataManager(vector.ToArray());
								}
							});

							Task mutually_recursive_task = Task.Run(() =>
							{
								using (MUTUALLY_RECURSIVE_VECTOR vector = client.GetMutualRecursive())
								{
									mutually_recursive_data_manager = new MutuallyRecursiveDataManager(vector.ToArray());
								}
							});

							Task function_pointer_task = Task.Run(() =>
							{
								using (FUNCTION_POINTER_VECTOR vector = client.GetFunctionPointer())
								{
									function_pointer_data_manager = new FunctionPointerDataManager(vector.ToArray());
								}
							});

							Task local_symbol_task = Task.Run(() =>
							{
								using (LOCAL_SYMBOL_VECTOR vector = client.GetLocalSymbols())
								{
									local_symbol_data_manager = new LocalSymbolDataManager(vector.ToArray());
								}
							});

							Task global_symbol_task = Task.Run(() =>
							{
								using (GLOBAL_SYMBOL_VECTOR vector = client.GetGlobalSymbols())
								{
									global_symbol_data_manager = new GlobalSymbolDataManager(vector.ToArray());
								}
							});

							Task memory_map_image_task = Task.Run(() =>
							{
								using (MEMORY_MAP_IMAGE data = client.GetMemoryMapImage())
								{
									memory_map_image_data_manager = new MemoryMapImageDataManager(data);
								}
							});

							Task image_component_size_task = Task.Run(() =>
							{
								using (IMAGE_COMPONENT_SIZE_VECTOR vector = client.GetImageComponentSize())
								{
									image_component_size_data_manager = new ImageComponentSizeDataManager(vector.ToArray());
								}
							});

							image_size_data_task.Wait();
							cross_reference_task.Wait();
							removed_symbol_task.Wait();
							maximum_stack_usage_task.Wait();
							stack_usage_task.Wait();
							mutually_recursive_task.Wait();
							function_pointer_task.Wait();
							local_symbol_task.Wait();
							global_symbol_task.Wait();
							memory_map_image_task.Wait();
							image_component_size_task.Wait();

							image_size_data_task = null;

							stack_info_ok = ((maximum_stack_usage_data_manager.Length > 0) ||
							                 (stack_usage_data_manager.Length > 0)         ||
							                 (mutually_recursive_data_manager.Length > 0)  ||
							                 (function_pointer_data_manager.Length > 0));
						}
					}

					while (!_Map_File_Update)
					{
						string filter_string;

						filter_string = FilterString;

						Task cross_reference_task = Task.Run(() =>
						{
							CrossReferenceField[] result = cross_reference_manager.Get(filter_string);
							lock (_Cross_Reference_Data_Lock)
							{
								_Cross_Reference_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateCrossReferenceDelegate);
						});

						Task removed_symbol_task = Task.Run(() =>
						{
							RemovedSymbolField[] result = removed_symbol_data_manager.Get(filter_string);
							lock (_Removed_Symbol_Data_Lock)
							{
								_Removed_Symbol_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateRemovedSymbolDelegate);
						});

						Task maximum_stack_usage_task = Task.Run(() =>
						{
							MaximumStackUsageField[] result = maximum_stack_usage_data_manager.Get(filter_string);
							lock (_Maximum_Stack_Usage_Data_Lock)
							{
								_Maximum_Stack_Usage_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateMaximumStackUsageDelegate, stack_info_ok);
						});

						Task stack_usage_task = Task.Run(() =>
						{
							StackUsageField[] result = stack_usage_data_manager.Get(filter_string);
							lock (_Stack_Usage_Data_Lock)
							{
								_Stack_Usage_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateStackUsageDelegate, stack_info_ok);
						});

						Task mutually_recursive_task = Task.Run(() =>
						{
							MutuallyRecursiveField[] result = mutually_recursive_data_manager.Get(filter_string);
							lock (_Mutually_Recursive_Data_Lock)
							{
								_Mutually_Recursive_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateMutuallyRecursiveDelegate, stack_info_ok);
						});

						Task function_pointer_task = Task.Run(() =>
						{
							FunctionPointerField[] result = function_pointer_data_manager.Get(filter_string);
							lock (_Function_Pointer_Data_Lock)
							{
								_Function_Pointer_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateFunctionPointerDelegate, stack_info_ok);
						});

						Task local_symbol_task = Task.Run(() =>
						{
							LocalSymbolField[] result = local_symbol_data_manager.Get(filter_string);
							lock (_Local_Symbol_Data_Lock)
							{
								_Local_Symbol_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateLocalSymbolDelegate);
						});

						Task global_symbol_task = Task.Run(() =>
						{
							GlobalSymbolField[] result = global_symbol_data_manager.Get(filter_string);
							lock (_Global_Symbol_Data_Lock)
							{
								_Global_Symbol_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateGlobalSymbolDelegate);
						});

						Task memory_map_image_task = Task.Run(() =>
						{
							MemoryMapImage result = memory_map_image_data_manager.Get(filter_string);
							lock (_Memory_Map_Image_Data_Lock)
							{
								_Memory_Map_Image_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateMemoryMapImageDelegate);
						});

						Task image_component_size_task = Task.Run(() =>
						{
							ImageComponentSizeField[] result = image_component_size_data_manager.Get(filter_string);
							lock (_Image_Component_Size_Data_Lock)
							{
								_Image_Component_Size_Data = result;
							}
							App.Instance.BeginInvoke(App.Instance.UpdateImageComponentSizeDelegate);
						});

						cross_reference_task.Wait();
						removed_symbol_task.Wait();
						maximum_stack_usage_task.Wait();
						stack_usage_task.Wait();
						mutually_recursive_task.Wait();
						function_pointer_task.Wait();
						local_symbol_task.Wait();
						global_symbol_task.Wait();
						memory_map_image_task.Wait();
						image_component_size_task.Wait();

						cross_reference_task      = null;
						removed_symbol_task		  = null;
						maximum_stack_usage_task  = null;
						stack_usage_task		  = null;
						mutually_recursive_task	  = null;
						function_pointer_task	  = null;
						local_symbol_task		  = null;
						global_symbol_task		  = null;
						memory_map_image_task	  = null;
						image_component_size_task = null;

						GC.Collect();

						_Worker_Thread_Update_Event.WaitOne();
						while (_Worker_Thread_Update_Event.WaitOne(100))
							;
					}
				}
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception ex)
			{
				App.Instance.BeginInvoke(App.Instance.ShowErrorDelegate, "Exception: " + ex.Message + ex.StackTrace, "MapFileManager");
			}
		}

		private void FileSystemWatcher_OnChanged(object source, FileSystemEventArgs e)
		{
			_Watcher.EnableRaisingEvents = false;

			Thread.Sleep(500);

			_Map_File_Update = true;
			_Worker_Thread_Update_Event.Set();
			_Watcher.EnableRaisingEvents = true;
		}

		public void Close()
		{
			try
			{
				_Worker_Thread.Abort();
				_Worker_Thread.Join();
			}
			catch
			{
			}
		}
	}
}



