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
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Internals
{
    internal static class PermissionCheck
    {
        private static Boolean? isRunAsAdmin = null;

        internal static Boolean IsRunAsAdmin
        {
            get
            {
                // NOTE: This state will not change over the process lifetime.

                if (!PermissionCheck.isRunAsAdmin.HasValue)
                {
                    PermissionCheck.isRunAsAdmin = PermissionCheck.IsUserAnAdmin();
                }

                return PermissionCheck.isRunAsAdmin.Value;
            }
        }

        internal static void SetButtonShield(Button button, Boolean visible)
        {
            const Int32 BCM_SETSHIELD = 0x0000160C;

            if (button != null)
            {
                // Important because otherwise shield is not shown!
                if (button.FlatStyle != FlatStyle.System)
                {
                    button.FlatStyle = FlatStyle.System;
                }

                HandleRef hWnd = new HandleRef(button, button.Handle);
                IntPtr lParam = visible ? new IntPtr(1) : new IntPtr(0);

                PermissionCheck.SendMessage(hWnd, BCM_SETSHIELD, IntPtr.Zero, lParam);
            }
        }

        #region Win32 related declaration and implementation section.

        // Windows 2000 Professional / Windows 2000 Server
        // Remarks are taken from the MSDN: "This function is a wrapper for CheckTokenMembership. 
        // It is recommended to call that function directly to determine Administrator group status 
        // rather than calling IsUserAnAdmin." 
        [DllImport("shell32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern Boolean IsUserAnAdmin();

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(HandleRef hWnd, Int32 message, IntPtr wParam, IntPtr lParam);

        #endregion // Win32 related declaration and implementation section.
    }
}
