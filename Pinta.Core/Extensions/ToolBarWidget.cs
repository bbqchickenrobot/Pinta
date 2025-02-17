// 
// ToolBarWidget.cs
//  
// Author:
//       Cameron White <cameronwhite91@gmail.com>
// 
// Copyright (c) 2020 Cameron White
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using Gtk;

namespace Pinta.Core
{
	/// <summary>
	/// Generic tool item implementation that can contain an arbitrary widget.
	/// </summary>
	public class ToolBarWidget<WidgetT> : ToolItem where WidgetT : Gtk.Widget
	{
		public WidgetT Widget { get; private init; }

		public ToolBarWidget (WidgetT widget)
		{
			Widget = widget;
			Add (Widget);
			ShowAll ();

			// After a spin button is edited, return focus to the canvas so that
			// tools can handle subsequent key events.
			if (widget is SpinButton spin_button) {
				spin_button.ValueChanged += (o, e) => {
					if (PintaCore.Workspace.HasOpenDocuments)
						PintaCore.Workspace.ActiveWorkspace.Canvas.GrabFocus ();
				};
			}
		}
	}
}
