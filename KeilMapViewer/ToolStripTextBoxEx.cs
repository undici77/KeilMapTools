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

sing System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class ToolStripTextBoxEx : ToolStripTextBox
{

	public ToolStripTextBoxEx() : base()
	{
		if (this.Control != null)
		{
			this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
		}
	}

	public ToolStripTextBoxEx(string name) : base(name)
	{
		if (this.Control != null)
		{
			this.Control.HandleCreated += new EventHandler(OnControlHandleCreated);
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			if (this.Control != null)
			{
				this.Control.HandleCreated -= new EventHandler(OnControlHandleCreated);
			}
		}

		base.Dispose(disposing);
	}

	void OnControlHandleCreated(object sender, EventArgs e)
	{
		UpdateCue();
	}

	private static uint ECM_FIRST = 0x1500;
	private static uint EM_SETCUEBANNER = ECM_FIRST + 1;

	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
	private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, String lParam);

	private string _CueText = String.Empty;

	public string CueText
	{
		get
		{
			return _CueText;
		}
		set
		{
			if (value == null)
			{
				value = String.Empty;
			}

			if (!_CueText.Equals(value, StringComparison.CurrentCulture))
			{
				_CueText = value;
				UpdateCue();
				OnCueTextChanged(EventArgs.Empty);
			}
		}
	}

	public event EventHandler _CueTextChanged;

	protected virtual void OnCueTextChanged(EventArgs e)
	{
		EventHandler handler = _CueTextChanged;
		if (handler != null)
		{
			handler(this, e);
		}
	}

	private bool _ShowCueTextWithFocus = false;

	public bool ShowCueTextWithFocus
	{
		get
		{
			return _ShowCueTextWithFocus;
		}
		set
		{
			if (_ShowCueTextWithFocus != value)
			{
				_ShowCueTextWithFocus = value;
				UpdateCue();
				OnShowCueTextWithFocusChanged(EventArgs.Empty);
			}
		}
	}

	public event EventHandler _ShowCueTextWithFocusChanged;

	[EditorBrowsable(EditorBrowsableState.Advanced)]
	protected virtual void OnShowCueTextWithFocusChanged(EventArgs e)
	{
		EventHandler handler = _ShowCueTextWithFocusChanged;
		if (handler != null)
		{
			handler(this, e);
		}
	}

	private void UpdateCue()
	{
		if ((this.Control != null) && (this.Control.IsHandleCreated))
		{
			SendMessage(new HandleRef(this.Control, this.Control.Handle), EM_SETCUEBANNER, (_ShowCueTextWithFocus) ? new IntPtr(1) : IntPtr.Zero, _CueText);
		}
	}

	public override Size GetPreferredSize(Size constrainingSize)
	{
		Int32 width;
		Int32 spring_box_count;
		Size size;

		if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
		{
			return (DefaultSize);
		}

		width = Owner.DisplayRectangle.Width;

		if (Owner.OverflowButton.Visible)
		{
			width = width - Owner.OverflowButton.Width - Owner.OverflowButton.Margin.Horizontal;
		}

		spring_box_count = 0;

		foreach (ToolStripItem item in Owner.Items)
		{
			if (!item.IsOnOverflow)
			{
				if (item is ToolStripTextBoxEx)
				{
					spring_box_count++;
					width -= item.Margin.Horizontal;
				}
				else
				{
					width = width - item.Width - item.Margin.Horizontal;
				}
			}
		}

		if (spring_box_count > 1)
		{
			width /= spring_box_count;
		}

		if (width < DefaultSize.Width)
		{
			width = DefaultSize.Width;
		}

		size = base.GetPreferredSize(constrainingSize);
		size.Width = width - 12;

		return (size);
	}

}
