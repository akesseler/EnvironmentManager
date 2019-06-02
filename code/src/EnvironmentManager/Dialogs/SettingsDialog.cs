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
using Plexdata.EnvironmentManager.Extensions;
using Plexdata.EnvironmentManager.Internals;
using Plexdata.LogWriter.Extensions;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Dialogs
{
    public partial class SettingsDialog : Form
    {
        public SettingsDialog()
        {
            this.InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;

            PermissionCheck.SetButtonShield(this.btnRemove, !PermissionCheck.IsRunAsAdmin);
            PermissionCheck.SetButtonShield(this.btnInstall, !PermissionCheck.IsRunAsAdmin);

            this.SetDoubleBuffered(this.tabContent);
            this.SetDoubleBuffered(this.tabRegistration);
            this.SetDoubleBuffered(this.tabArguments);
            this.SetDoubleBuffered(this.txtArguments); // Actually, it does not really has an effect. But why?
        }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            this.txtArguments.Text = Program.Arguments.Generate();
        }

        protected override void OnShown(EventArgs args)
        {
            using (new WaitCursor(this))
            {
                this.LoadSettings();
                base.OnShown(args);
                this.UpdateButtons();
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            this.SaveSettings();
        }

        private void OnInstallClicked(Object sender, EventArgs args)
        {
            using (new WaitCursor(this))
            {
                this.Enabled = false;

                try
                {
                    this.ExecuteCommand("--install", !PermissionCheck.IsRunAsAdmin);
                }
                catch (Exception exception)
                {
                    Program.Logger.Critical("Unexpected error while executing task create command.", exception);
                }
                finally
                {
                    this.Enabled = true;
                }

                this.UpdateButtons();
            }
        }

        private void OnRemoveClicked(Object sender, EventArgs args)
        {
            using (new WaitCursor(this))
            {
                this.Enabled = false;

                try
                {
                    this.ExecuteCommand("--remove", !PermissionCheck.IsRunAsAdmin);
                }
                catch (Exception exception)
                {
                    Program.Logger.Critical("Unexpected error while executing task remove command.", exception);
                }
                finally
                {
                    this.Enabled = true;
                }

                this.UpdateButtons();
            }
        }

        private void UpdateButtons()
        {
            try
            {
                Boolean installed = TaskScheduler.IsInstalled();

                this.btnRemove.Enabled = installed;
                this.btnInstall.Enabled = !installed;
            }
            catch (Exception exception)
            {
                Program.Logger.Critical("Unexpected error while updating buttons.", exception);

                this.btnRemove.Enabled = false;
                this.btnInstall.Enabled = false;
            }

            this.Update();
        }

        private Boolean ExecuteCommand(String arguments, Boolean elevated)
        {
            try
            {
                ProcessStartInfo info = new ProcessStartInfo
                {
                    Verb = elevated ? "runas" : String.Empty,
                    Arguments = arguments ?? String.Empty,
                    FileName = Assembly.GetExecutingAssembly().Location,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

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
                    Program.Logger.Warning("User rejected command execution in settings dialog.", new (String, Object)[] { ("Arguments", arguments), ("Elevated", elevated) });
                    return false;
                }
                else
                {
                    Program.Logger.Critical("Unexpected error while command execution in settings dialog.", exception);
                    throw exception;
                }
            }
        }

        private void SetDoubleBuffered(Control control)
        {
            typeof(Control).InvokeMember("DoubleBuffered",
                BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                null, control, new Object[] { true });
        }

        private void LoadSettings()
        {
            try
            {
                this.DesktopBounds = Program.ReadSettingsValue(this.GetType().Name, nameof(this.DesktopBounds))
                    .StringToBounds(this.StandardBounds(new Size(500, 350)));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Loading settings for settings dialog from configuration has failed.", exception);
            }

            this.EnsureScreenLocation();
        }

        private void SaveSettings()
        {
            try
            {
                Program.SaveSettingsValue(this.GetType().Name, nameof(this.DesktopBounds), this.DesktopBounds.BoundsToString());
                Program.SaveSettings();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Saving settings for settings dialog into configuration has failed.", exception);
            }
        }
    }
}




