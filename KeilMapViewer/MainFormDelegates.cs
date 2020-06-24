using System;
using System.IO;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KeilMapViewer
{
	public delegate void UPDATE_UI_DELEGATE();
	public delegate void SHOW_ERROR(string text, string caption);

	public partial class MainForm : Form
	{
		public UPDATE_UI_DELEGATE UpdateCrossReferenceDelegate
		{
			get
			{
				return (_Update_Cross_Reference_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Cross_Reference_Delegate;

		public UPDATE_UI_DELEGATE UpdateRemovedSymbolDelegate
		{
			get
			{
				return (_Update_Removed_Symbol_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Removed_Symbol_Delegate;

		public UPDATE_UI_DELEGATE UpdateMaximumStackUsageDelegate
		{
			get
			{
				return (_Update_Maximum_Stack_Usage_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Maximum_Stack_Usage_Delegate;

		public UPDATE_UI_DELEGATE UpdateStackUsageDelegate
		{
			get
			{
				return (_Update_Stack_Usage_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Stack_Usage_Delegate;

		public UPDATE_UI_DELEGATE UpdateMutuallyRecursiveDelegate
		{
			get
			{
				return (_Update_Mutually_Recursive_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Mutually_Recursive_Delegate;

		public UPDATE_UI_DELEGATE UpdateFunctionPointerDelegate
		{
			get
			{
				return (_Update_Function_Pointer_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Function_Pointer_Delegate;

		public UPDATE_UI_DELEGATE UpdateLocalSymbolDelegate
		{
			get
			{
				return (_Update_Local_Symbol_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Local_Symbol_Delegate;

		public UPDATE_UI_DELEGATE UpdateGlobalSymbolDelegate
		{
			get
			{
				return (_Update_Global_Symbol_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Global_Symbol_Delegate;

		public UPDATE_UI_DELEGATE UpdateMemoryMapImageDelegate
		{
			get
			{
				return (_Update_Memory_Map_Image_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Memory_Map_Image_Delegate;

		public UPDATE_UI_DELEGATE UpdateImageComponentSizeDelegate
		{
			get
			{
				return (_Update_Image_Component_Size_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Image_Component_Size_Delegate;

		public UPDATE_UI_DELEGATE UpdateImageSizeDataDelegate
		{
			get
			{
				return (_Update_Image_Size_Data_Delegate);
			}
		}
		private UPDATE_UI_DELEGATE _Update_Image_Size_Data_Delegate;

		public SHOW_ERROR ShowErrorDelegate
		{
			get
			{
				return (_Show_Error_Delegate);
			}
		}
		private SHOW_ERROR _Show_Error_Delegate;

		private void InitializeDelegates()
		{

			_Update_Cross_Reference_Delegate      = new UPDATE_UI_DELEGATE(UpdateCrossReference);
			_Update_Removed_Symbol_Delegate       = new UPDATE_UI_DELEGATE(UpdateRemovedSymbol);
			_Update_Maximum_Stack_Usage_Delegate  = new UPDATE_UI_DELEGATE(UpdateMaximumStackUsage);
			_Update_Stack_Usage_Delegate          = new UPDATE_UI_DELEGATE(UpdateStackUsage);
			_Update_Mutually_Recursive_Delegate   = new UPDATE_UI_DELEGATE(UpdateMutuallyRecursive);
			_Update_Function_Pointer_Delegate     = new UPDATE_UI_DELEGATE(UpdateFunctionPointer);
			_Update_Local_Symbol_Delegate         = new UPDATE_UI_DELEGATE(UpdateLocalSymbol);
			_Update_Global_Symbol_Delegate        = new UPDATE_UI_DELEGATE(UpdateGlobalSymbol);
			_Update_Memory_Map_Image_Delegate     = new UPDATE_UI_DELEGATE(UpdateMemoryMapImage);
			_Update_Image_Component_Size_Delegate = new UPDATE_UI_DELEGATE(UpdateImageComponentSize);
			_Update_Image_Size_Data_Delegate      = new UPDATE_UI_DELEGATE(UpdateImageSizeData);
			_Show_Error_Delegate                  = new SHOW_ERROR(ShowError);
		}

		private void UpdateCrossReference()
		{
			CrossReferenceField[] data;
			string                tooltip_text;

			CrossReferenceDataGridView.SaveData();
			CrossReferenceDataGridView.Rows.Clear();
			CrossReferenceDataGridView.SuspendLayout();

			data = _Map_File_Manager.CrossReferenceSectionData;
			foreach (CrossReferenceField field in data)
			{
				tooltip_text = "REFERENCE QUALIFIER: " + field.Reference_Qualifier;
				CrossReferenceDataGridView.Rows.Add(field.Module, field.Module_Reference, field.Symbol);
				foreach (DataGridViewCell cell in CrossReferenceDataGridView.Rows[CrossReferenceDataGridView.RowCount - 1].Cells)
				{
					cell.ToolTipText = tooltip_text;
				}
			}

			CrossReferenceDataGridView.RestoreData();
			CrossReferenceDataGridView.ResumeLayout();
		}

		private void UpdateRemovedSymbol()
		{
			RemovedSymbolField[] data;

			RemovedSymbolDataGridView.SaveData();
			RemovedSymbolDataGridView.Rows.Clear();
			RemovedSymbolDataGridView.SuspendLayout();

			data = _Map_File_Manager.RemovedSymbolSectionData;
			foreach (RemovedSymbolField field in data)
			{
				RemovedSymbolDataGridView.Rows.Add(field.Module, field.Size, field.Symbol);
			}

			RemovedSymbolDataGridView.RestoreData();
			RemovedSymbolDataGridView.ResumeLayout();
		}

		private void UpdateMaximumStackUsage()
		{
			MaximumStackUsageField[] data;
			string                   tooltip_text;

			MaximumStackUsageDataGridView.SaveData();
			MaximumStackUsageDataGridView.Rows.Clear();
			MaximumStackUsageDataGridView.SuspendLayout();

			data = _Map_File_Manager.MaximumStackUsageSectionData;
			foreach (MaximumStackUsageField field in data)
			{
				tooltip_text = "CALL CHAIN: " + field.Call_Chain;
				MaximumStackUsageDataGridView.Rows.Add(field.Function, field.Size);
				foreach (DataGridViewCell cell in MaximumStackUsageDataGridView.Rows[MaximumStackUsageDataGridView.RowCount - 1].Cells)
				{
					cell.ToolTipText = tooltip_text;
				}
			}

			MaximumStackUsageDataGridView.RestoreData();
			MaximumStackUsageDataGridView.ResumeLayout();
		}

		private void UpdateStackUsage()
		{
			StackUsageField[] data;

			StackUsageDataGridView.SaveData();
			StackUsageDataGridView.Rows.Clear();
			StackUsageDataGridView.SuspendLayout();

			data = _Map_File_Manager.StackUsageSectionData;
			foreach (StackUsageField field in data)
			{
				StackUsageDataGridView.Rows.Add(field.Function, field.Size);
			}

			StackUsageDataGridView.RestoreData();
			StackUsageDataGridView.ResumeLayout();
		}

		private void UpdateMutuallyRecursive()
		{
			MutuallyRecursiveField[] data;

			MutuallyRecursiveDataGridView.SaveData();
			MutuallyRecursiveDataGridView.Rows.Clear();
			MutuallyRecursiveDataGridView.SuspendLayout();

			data = _Map_File_Manager.MutuallyRecursiveSectionData;
			foreach (MutuallyRecursiveField field in data)
			{
				MutuallyRecursiveDataGridView.Rows.Add(field.Function, field.Caller);
			}

			MutuallyRecursiveDataGridView.RestoreData();
			MutuallyRecursiveDataGridView.ResumeLayout();
		}

		private void UpdateFunctionPointer()
		{
			FunctionPointerField[] data;

			FunctionPointerDataGridView.SaveData();
			FunctionPointerDataGridView.Rows.Clear();
			FunctionPointerDataGridView.SuspendLayout();

			data = _Map_File_Manager.FunctionPointerSectionData;
			foreach (FunctionPointerField field in data)
			{
				FunctionPointerDataGridView.Rows.Add(field.Symbol, field.Module, field.Counts, field.Module_Reference);
			}

			FunctionPointerDataGridView.RestoreData();
			FunctionPointerDataGridView.ResumeLayout();
		}

		private void UpdateLocalSymbol()
		{
			LocalSymbolField[] data;

			LocalSymbolDataGridView.SaveData();
			LocalSymbolDataGridView.Rows.Clear();
			LocalSymbolDataGridView.SuspendLayout();

			data = _Map_File_Manager.LocalSymbolSectionData;
			foreach (LocalSymbolField field in data)
			{
				LocalSymbolDataGridView.Rows.Add(field.Symbolic_Name, field.Address, field.Type, field.Size, field.Object_Name);
			}

			LocalSymbolDataGridView.RestoreData();
			LocalSymbolDataGridView.ResumeLayout();
		}

		private void UpdateGlobalSymbol()
		{
			GlobalSymbolField[] data;

			GlobalSymbolDataGridView.SaveData();
			GlobalSymbolDataGridView.Rows.Clear();
			GlobalSymbolDataGridView.SuspendLayout();

			data = _Map_File_Manager.GlobalSymbolData;
			foreach (GlobalSymbolField field in data)
			{
				GlobalSymbolDataGridView.Rows.Add(field.Symbolic_Name, field.Address, field.Type, field.Size, field.Object_Name);
			}

			GlobalSymbolDataGridView.RestoreData();
			GlobalSymbolDataGridView.ResumeLayout();
		}

		private void UpdateMemoryMapImage()
		{
			MemoryMapImage data;
			int            load_region_index;
			int            execution_region_index;

			MemoryMapImageDataGridView.SaveData();
			MemoryMapImageDataGridView.Rows.Clear();
			MemoryMapImageDataGridView.SuspendLayout();

			data = _Map_File_Manager.MemoryMapImageData;

			MemoryMapImageEntryPointAddressLabel.Text = data.Entry_Point;

			MemoryMapImageLoadRegionComboBox.SaveData();
			MemoryMapImageLoadRegionComboBox.Items.Clear();
			if (data.Load_Region.Count > 0)
			{
				MemoryMapImageLoadRegionComboBox.Items.Add("All Load Regions");
				foreach (MemoryMapImageLoadRegionField load_region in data.Load_Region)
				{
					MemoryMapImageLoadRegionComboBox.Items.Add(load_region.Name);
				}
			}
			MemoryMapImageLoadRegionComboBox.RestoreData();
			load_region_index = MemoryMapImageLoadRegionComboBox.SelectedIndex;

			MemoryMapImageExecutionRegionComboBox.SaveData();
			MemoryMapImageExecutionRegionComboBox.Items.Clear();
			if (data.Load_Region.Count > 0)
			{
				if (load_region_index > 0)
				{
					MemoryMapImageExecutionRegionComboBox.Items.Add("All Execution Regions");
					if (data.Load_Region[load_region_index - 1].Execution_Region.Count > 0)
					{
						foreach (MemoryMapImageExecutionRegionField execution_region in data.Load_Region[load_region_index - 1].Execution_Region)
						{
							MemoryMapImageExecutionRegionComboBox.Items.Add(execution_region.Name);
						}
					}
				}
				else
				{
					MemoryMapImageExecutionRegionComboBox.Items.Add("All Execution Regions");
				}
			}
			MemoryMapImageExecutionRegionComboBox.RestoreData();
			execution_region_index = MemoryMapImageExecutionRegionComboBox.SelectedIndex;
			if (load_region_index >= 0)
			{
				if ((load_region_index != 0) && (execution_region_index != 0))
				{
					MemoryMapImageLoadRegionField      load_region;
					MemoryMapImageExecutionRegionField execution_region;
					string                             tooltip_text;
				
					load_region      = data.Load_Region[load_region_index - 1]; 
					execution_region = load_region.Execution_Region[execution_region_index - 1]; 
					tooltip_text     = "LOAD REGION: " + load_region.Name + " " + load_region.Data + "\r\n" + 
								       "EXEC REGION: " + execution_region.Name + " " + execution_region.Data;

					foreach (MemoryMapImageObjectField field in execution_region.Fields)
					{

								MemoryMapImageDataGridView.Rows.Add(field.Section_Name, field.Object_Name, field.Execution_Address, field.Load_Address, 
																	 field.Size, field.Type, field.Attribute, field.Id, field.Entry_Point);
						foreach (DataGridViewCell cell in MemoryMapImageDataGridView.Rows[MemoryMapImageDataGridView.RowCount - 1].Cells)
						{
							cell.ToolTipText = tooltip_text;
						}
					}
				}
				else if (load_region_index != 0)
				{
					MemoryMapImageLoadRegionField load_region;
					string                        tooltip_text;
				
					load_region = data.Load_Region[load_region_index - 1]; 
					foreach (MemoryMapImageExecutionRegionField execution_region in load_region.Execution_Region)
					{
						foreach (MemoryMapImageObjectField field in execution_region.Fields)
						{
							MemoryMapImageDataGridView.Rows.Add(field.Section_Name, field.Object_Name, field.Execution_Address, field.Load_Address, 
																 field.Size, field.Type, field.Attribute, field.Id, field.Entry_Point);
							tooltip_text = "LOAD REGION: " + load_region.Name + " " + load_region.Data + "\r\n" + 
							               "EXEC REGION: " + execution_region.Name + " " + execution_region.Data;
							foreach (DataGridViewCell cell in MemoryMapImageDataGridView.Rows[MemoryMapImageDataGridView.RowCount - 1].Cells)
							{
								cell.ToolTipText = tooltip_text;
							}
						}
					}
				}
				else
				{
					string tooltip_text;

					foreach (MemoryMapImageLoadRegionField load_region in data.Load_Region)
					{
						foreach (MemoryMapImageExecutionRegionField execution_region in load_region.Execution_Region)
						{
							tooltip_text = "LOAD REGION: " + load_region.Name + " " + load_region.Data + "\r\n" + 
							               "EXEC REGION: " + execution_region.Name + " " + execution_region.Data;
							foreach (MemoryMapImageObjectField field in execution_region.Fields)
							{
								MemoryMapImageDataGridView.Rows.Add(field.Section_Name, field.Object_Name, field.Execution_Address, field.Load_Address, 
																	 field.Size, field.Type, field.Attribute, field.Id, field.Entry_Point);
								foreach (DataGridViewCell cell in MemoryMapImageDataGridView.Rows[MemoryMapImageDataGridView.RowCount - 1].Cells)
								{
									cell.ToolTipText = tooltip_text;
								}
							}
						}
					}
				}
			}

			MemoryMapImageDataGridView.RestoreData();
			MemoryMapImageDataGridView.ResumeLayout();
		}

		private void UpdateImageComponentSize()
		{
			ImageComponentSizeField[] data;

			ImageComponentSizeDataGridView.SaveData();
			ImageComponentSizeDataGridView.Rows.Clear();
			ImageComponentSizeDataGridView.SuspendLayout();

			data = _Map_File_Manager.ImageComponentSizeData;
			foreach (ImageComponentSizeField field in data)
			{
				ImageComponentSizeDataGridView.Rows.Add(field.Object_Name, field.Code, field.Inline_Data, field.Read_Only_Data, field.Read_Write_Data, field.Zero_Init_Data, field.Debug_Data);
			}

			ImageComponentSizeDataGridView.RestoreData();
			ImageComponentSizeDataGridView.ResumeLayout();
		}

		private void UpdateImageSizeData()
		{
			TotalROLabel.Text = "RO " + _Map_File_Manager.ImageSizeData.Total_Read_Only_Size;
			TotalROLabel.ToolTipText = "Total RO  Size (Code + RO Data)";

			TotalRWLabel.Text = "RW " + _Map_File_Manager.ImageSizeData.Total_Read_Write_Size;
			TotalRWLabel.ToolTipText = "Total RW  Size (RW Data + ZI Data)";

			TotalROMLabel.Text = "ROM " + _Map_File_Manager.ImageSizeData.Total_Rom_Size;
			TotalROMLabel.ToolTipText = "Total ROM Size (Code + RO Data + RW Data)";
		}

		private void ShowError(string text, string caption)
		{
			MessageBox.Show(this, text, caption,  MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
