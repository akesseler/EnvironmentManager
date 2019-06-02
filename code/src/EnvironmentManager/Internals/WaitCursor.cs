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
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Internals
{
    internal class WaitCursor : IDisposable
    {
        private WaitCursor()
            : base()
        {
            this.Control = null;
            this.Cursor = Cursors.Default;
        }

        public WaitCursor(Control control)
            : this(control, false)
        {
        }

        public WaitCursor(Control control, Boolean highest)
            : this()
        {
            if (highest)
            {
                if (control != null)
                {
                    Control parent = control.Parent;

                    while (parent != null)
                    {
                        control = parent;
                        parent = control.Parent;
                    }
                }
            }

            this.Control = control;

            if (this.Control != null)
            {
                this.Cursor = this.Control.Cursor;
                this.Control.Cursor = Cursors.WaitCursor;
            }
        }

        public Control Control { get; private set; }

        public Cursor Cursor { get; private set; }

        public void Dispose()
        {
            if (this.Control != null)
            {
                this.Control.Cursor = Cursor;
            }
        }
    }

}
