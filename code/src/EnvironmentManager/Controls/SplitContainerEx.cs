/*
 * MIT License
 * 
 * Copyright (c) 2019 plexdata.de
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Plexdata.EnvironmentManager.Controls
{
    // Find a quite nice example of a splitter based on a TableLayoutPanel... 
    // http://stackoverflow.com/questions/5033690/add-button-controls-to-splitcontainer-splitter/5046984#5046984

    public class SplitContainerEx : SplitContainer
    {
        public SplitContainerEx()
            : base()
        {
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            base.TabStop = false;
        }

        protected override void OnKeyUp(KeyEventArgs args)
        {
            base.OnKeyUp(args);
            // Needed to disable painted focus rectangle...
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs args)
        {
            try
            {
                Size size = new Size(4, 40); // Gripper dimension...
                Rectangle rect = this.SplitterRectangle;
                VisualStyleRenderer renderer = null;

                if (this.Orientation == Orientation.Vertical)
                {
                    rect.Y = (rect.Bottom - (rect.Top + size.Height)) / 2;
                    rect.Height = size.Height;
                    rect.Width = size.Width;
                    rect.X += Math.Max((this.SplitterWidth - rect.Width) / 2, 0) - 1;

                    if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.Gripper.Normal))
                    {
                        renderer = new VisualStyleRenderer(VisualStyleElement.Rebar.Gripper.Normal);
                    }
                }
                else
                {
                    rect.X = (rect.Right - (rect.Left + size.Height)) / 2;
                    rect.Height = size.Width;
                    rect.Width = size.Height;
                    rect.Y += Math.Max((this.SplitterWidth - rect.Height) / 2, 0) - 1;

                    if (VisualStyleRenderer.IsElementDefined(VisualStyleElement.Rebar.GripperVertical.Normal))
                    {
                        renderer = new VisualStyleRenderer(VisualStyleElement.Rebar.GripperVertical.Normal);
                    }
                }

                if (renderer != null)
                {
                    renderer.DrawBackground(args.Graphics, rect, args.ClipRectangle);
                }

                if (base.Focused && base.TabStop)
                {
                    ControlPaint.DrawFocusRectangle(args.Graphics,
                        Rectangle.Inflate(this.SplitterRectangle, -1, -1),
                        this.ForeColor, this.BackColor);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
        }

        protected override void OnDoubleClick(EventArgs args)
        {
            base.OnDoubleClick(args);
            if (this.Orientation == Orientation.Vertical)
            {
                this.SplitterDistance = this.ClientSize.Width / 2;
            }
            else
            {
                this.SplitterDistance = this.ClientSize.Height / 2;
            }
            this.Refresh();
        }
    }
}

