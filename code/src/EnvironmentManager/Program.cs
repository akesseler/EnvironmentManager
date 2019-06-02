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

using Plexdata.ArgumentParser.Extensions;
using Plexdata.CfgParser.Converters;
using Plexdata.CfgParser.Entities;
using Plexdata.CfgParser.Processors;
using Plexdata.CfgParser.Settings;
using Plexdata.EnvironmentManager.Internals;
using Plexdata.EnvironmentManager.Models;
using Plexdata.LogWriter.Abstraction;
using Plexdata.LogWriter.Definitions;
using Plexdata.LogWriter.Extensions;
using Plexdata.LogWriter.Logging;
using Plexdata.LogWriter.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager
{
    static class Program
    {
        private static ILogger logger = null;
        private static ConfigContent settings = null;
        private static CommandArguments arguments = null;

        [STAThread]
        static void Main()
        {
            Application.ThreadException += Program.OnUnhandledThreadException;
            AppDomain.CurrentDomain.UnhandledException += Program.OnUnhandledDomainException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (Program.Arguments.IsHelp)
            {
                MessageBox.Show(Program.Arguments.Generate(70), "Help", MessageBoxButtons.OK, MessageBoxIcon.Question);
                return;
            }

            if (Program.Arguments.IsInstall)
            {
                Program.RunInstall();
                return;
            }

            if (Program.Arguments.IsRemove)
            {
                Program.RunRemove();
                return;
            }

            Application.Run(new MainForm());
        }

        internal static ILogger Logger
        {
            get
            {
                if (Program.logger == null)
                {
                    Program.logger = new PersistentLogger(Program.GetLoggerSettings());
                }

                return Program.logger;
            }
        }

        internal static ConfigContent Settings
        {
            get
            {
                if (Program.settings == null)
                {
                    Program.settings = ConfigReader.Read(Program.GetSettingsFilename());

                    if (!Program.settings.Header.IsValid)
                    {
                        Program.settings.Header = ConfigSettings.CreateDefaultHeader("Auto-generated configuration file.", true);
                    }
                }

                return Program.settings;
            }
        }

        internal static CommandArguments Arguments
        {
            get
            {
                if (Program.arguments == null)
                {
                    Program.arguments = new CommandArguments();
                    List<String> helper = new List<String>(Environment.GetCommandLineArgs());

                    if (helper.Count > 1)
                    {
                        helper.RemoveAt(0); // Remove filename of executing assembly (required).
                        Program.arguments.Process(helper);
                    }
                }

                return Program.arguments;
            }
        }

        internal static void SaveSettings()
        {
            if (Program.settings != null)
            {
                ConfigWriter.Write(Program.settings, Program.GetSettingsFilename(), true);
            }
        }

        internal static String ReadSettingsValue(String sectionName, String valueName)
        {
            if (String.IsNullOrWhiteSpace(sectionName) || String.IsNullOrWhiteSpace(valueName))
            {
                return String.Empty;
            }

            ConfigSection section = Program.Settings.Find(sectionName);

            if (section == null)
            {
                return String.Empty;
            }

            ConfigValue value = section.Find(valueName);

            if (value == null)
            {
                return String.Empty;

            }

            return value.Value;
        }

        internal static void SaveSettingsValue(String sectionName, String valueName, String valueData)
        {
            if (String.IsNullOrWhiteSpace(sectionName) || String.IsNullOrWhiteSpace(valueName))
            {
                return;
            }

            ConfigSection section = Program.Settings.Find(sectionName);

            if (section == null)
            {
                section = Program.Settings.Append(sectionName);
            }

            ConfigValue value = section.Find(valueName);

            if (value == null)
            {
                value = new ConfigValue(valueName);
            }

            value.Value = valueData ?? String.Empty;

            section[valueName] = value;
        }

        private static void RunInstall()
        {
            if (!PermissionCheck.IsRunAsAdmin)
            {
                MessageBox.Show("This option requires administrator privileges.", "Install", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (TaskScheduler.IsInstalled())
            {
                return;
            }

            if (!TaskScheduler.Create())
            {
                MessageBox.Show("Registration failed, task not created.", "Install", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void RunRemove()
        {
            if (!PermissionCheck.IsRunAsAdmin)
            {
                MessageBox.Show("This option requires administrator privileges.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!TaskScheduler.IsInstalled())
            {
                return;
            }

            if (!TaskScheduler.Delete())
            {
                MessageBox.Show("Unregistration failed, Task possibly not completely removed.", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static String GetSettingsFilename()
        {
            String filename = Path.ChangeExtension(
                Path.GetFileName(Application.ExecutablePath), "ini");

            String filepath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                typeof(Program).Namespace.Replace('.', Path.DirectorySeparatorChar));

            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

            String fullname = Path.Combine(filepath, filename);

            if (!File.Exists(fullname))
            {
                using (StreamWriter writer = File.CreateText(fullname))
                {
                    writer.Close();
                }
            }

            return fullname;
        }

        private static String GetLoggingFilename()
        {
            return Path.ChangeExtension(Program.GetSettingsFilename(), "log");
        }

        private static IPersistentLoggerSettings GetLoggerSettings()
        {
            ConfigSection section = Program.Settings.Find("Logging");

            if (section == null)
            {
                section = Program.Settings.Append(new ConfigSection("Logging"));
                section.Append(new ConfigValue("LogLevel", LogLevel.Trace.ToString()));
            }

            Object logLevel = LogLevel.Trace;
            if (!ValueConverter.TryConvert(section["LogLevel"].Value, typeof(LogLevel), out logLevel))
            {
                logLevel = LogLevel.Trace;
            }

            return new PersistentLoggerSettings()
            {
                ShowTime = true,
                LogTime = LogTime.Utc,
                LogType = LogType.Raw,
                LogLevel = (LogLevel)logLevel,
                Filename = Program.GetLoggingFilename(),
                IsQueuing = false,
                IsRolling = false,
                Threshold = 0,
            };
        }

        private static void OnUnhandledThreadException(Object sender, ThreadExceptionEventArgs args)
        {
            try
            {
                Exception error = args.Exception;
                Program.Logger.Critical("Unhandled Thread Exception", error);
                MessageBox.Show(error.Message, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Extreme Trouble", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Exit();
        }

        private static void OnUnhandledDomainException(Object sender, UnhandledExceptionEventArgs args)
        {
            try
            {
                Exception error = args.ExceptionObject as Exception;
                Program.Logger.Critical("Unhandled Domain Exception", error);
                MessageBox.Show(error.Message, "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString(), "Extreme Trouble", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Exit();
        }
    }
}
