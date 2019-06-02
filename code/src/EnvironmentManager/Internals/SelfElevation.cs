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

using Plexdata.LogWriter.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Internals
{
    internal static class SelfElevation
    {
        public static Boolean Elevate(String parameters)
        {
            return Elevate(parameters, false);
        }

        public static Boolean Elevate(String parameters, Boolean wait)
        {
            try
            {
                // Be aware, starting a process with different window styles does not really 
                // work! Therefore, starting the sibling process with administrator privileges 
                // uses appropriated command line arguments instead.

                ProcessStartInfo info = new ProcessStartInfo
                {
                    Verb = "runas",
                    Arguments = parameters,
                    FileName = Application.ExecutablePath
                };

                if (wait)
                {
                    Process process = Process.Start(info);
                    process.WaitForExit();

                    // By definition the self elevated program 
                    // returns zero if execution was successful!
                    return process.ExitCode == 0;
                }
                else
                {
                    Process.Start(info);
                    return true;
                }
            }
            catch (Win32Exception exception)
            {
                const Int32 ERROR_CANCELLED = 1223;

                if (exception.NativeErrorCode == ERROR_CANCELLED)
                {
                    Program.Logger.Warning("User rejected self-elevation.", new (String, Object)[] { ("Parameters", parameters), ("Wait", wait) });
                    return false;
                }
                else
                {
                    Program.Logger.Critical("Unexpected error while self-elevation.", exception);
                    throw exception;
                }
            }
        }
    }
}
