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
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Internals
{
    internal static class TaskScheduler
    {
        public static Boolean IsInstalled()
        {
            return TaskScheduler.ExecuteCommand(TaskScheduler.GetQueryTaskCommandArguments());
        }

        public static Boolean Create()
        {
            return TaskScheduler.ExecuteCommand(TaskScheduler.GetCreateTaskCommandArguments(), !PermissionCheck.IsRunAsAdmin);
        }

        public static Boolean Delete()
        {
            // Not really nice, because of UAC window is shown three times.

            Boolean result = false;
            Boolean elevated = !PermissionCheck.IsRunAsAdmin;

            // Might be unsuccessful, e.g. is not running.
            TaskScheduler.ExecuteCommand(TaskScheduler.GetFinishTaskCommandArguments(), elevated);

            result = TaskScheduler.ExecuteCommand(TaskScheduler.GetRemoveTaskCommandArguments(), elevated);

            // Might be unsuccessful, e.g. is not running.
            TaskScheduler.ExecuteCommand(TaskScheduler.GetRemoveRootCommandArguments(), elevated);

            return result;
        }

        private static Boolean ExecuteCommand(String arguments)
        {
            return TaskScheduler.ExecuteCommand(arguments, false);
        }

        private static Boolean ExecuteCommand(String arguments, Boolean elevated)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    Verb = elevated ? "runas" : String.Empty,
                    Arguments = arguments ?? String.Empty,
                    FileName = TaskScheduler.GetTaskSchedulerExecutable(),
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Debug.WriteLine(($"{info.Verb} {info.FileName} {info.Arguments}").Trim());

                using (Process process = Process.Start(info))
                {
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
            }
            catch (Win32Exception exception)
            {
                const Int32 ERROR_CANCELLED = 1223;

                if (exception.NativeErrorCode == ERROR_CANCELLED)
                {
                    Program.Logger.Warning("User rejected command execution in task scheduler.", new (String, Object)[] { ("Arguments", arguments), ("Elevated", elevated) });
                    return false;
                }
                else
                {
                    Program.Logger.Critical("Unexpected error while command execution in task scheduler.", exception);
                    throw exception;
                }
            }
        }

        private static String GetTaskSchedulerExecutable()
        {
            return Environment.ExpandEnvironmentVariables(
                $"\"%SystemRoot%\\System32\\schtasks.exe\""
            );
        }

        private static String GetTaskRootName()
        {
            return $"\\Plexdata";
        }

        private static String GetTaskFullName()
        {
            return $"{TaskScheduler.GetTaskRootName()}\\{Application.ProductName} (Tray Icon Runner)";
        }

        private static String GetTaskFullPath()
        {
            return $"'{Assembly.GetExecutingAssembly().Location}' --tray-icon";
        }

        public static String GetQueryTaskCommandArguments()
        {
            return Environment.ExpandEnvironmentVariables(
                $"/QUERY " +
                $"/TN \"{TaskScheduler.GetTaskFullName()}\""
            );
        }

        public static String GetCreateTaskCommandArguments()
        {
            return Environment.ExpandEnvironmentVariables(
                $"/CREATE " +
                $"/TN \"{TaskScheduler.GetTaskFullName()}\" " +
                $"/TR \"{TaskScheduler.GetTaskFullPath()}\" " +
                $"/SC ONLOGON " +
                $"/RL HIGHEST " +
                $"/RU %USERDOMAIN%\\%USERNAME%"
            );
        }

        public static String GetFinishTaskCommandArguments()
        {
            return Environment.ExpandEnvironmentVariables(
                $"/END " +
                $"/TN \"{TaskScheduler.GetTaskFullName()}\""
            );
        }

        public static String GetRemoveRootCommandArguments()
        {
            return Environment.ExpandEnvironmentVariables(
                $"/DELETE " +
                $"/TN \"{TaskScheduler.GetTaskRootName()}\" " +
                $"/F"
            );
        }

        public static String GetRemoveTaskCommandArguments()
        {
            return Environment.ExpandEnvironmentVariables(
                $"/DELETE " +
                $"/TN \"{TaskScheduler.GetTaskFullName()}\" " +
                $"/F"
            );
        }
    }
}
