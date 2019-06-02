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

using Plexdata.EnvironmentManager.Dialogs;
using Plexdata.EnvironmentManager.Extensions;
using Plexdata.EnvironmentManager.Internals;
using Plexdata.EnvironmentManager.Models;
using Plexdata.EnvironmentManager.Serializers;
using Plexdata.LogWriter.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager
{
    // TODO: Consider sorting by column, see below how:
    // https://support.microsoft.com/de-de/help/319401/how-to-sort-a-listview-control-by-a-column-in-visual-c

    public partial class MainForm : Form
    {
        private ListView activeListView = null;
        private Font defaultFont = new Font("Consolas", 11.25f);

        public MainForm()
        {
            this.InitializeComponent();

            this.notifyIcon.Visible = Program.Arguments.IsMinimized;

            this.SetDoubleBuffered(this.lsvUser);
            this.lsvUser.DoubleClick += this.OnListViewDoubleClick;
            this.lsvUser.KeyDown += this.OnListViewKeyDown;

            this.SetDoubleBuffered(this.lsvMachine);
            this.lsvMachine.DoubleClick += this.OnListViewDoubleClick;
            this.lsvMachine.KeyDown += this.OnListViewKeyDown;

            this.Icon = Properties.Resources.MainIcon;
            this.notifyIcon.Icon = Properties.Resources.MainIcon;

            if (PermissionCheck.IsRunAsAdmin)
            {
                this.Text += " (ADMIN)";
                this.notifyIcon.Text += " (ADMIN)";
            }
#if !DEBUG
            this.tbbDump.Visible = false;
#endif
        }

        #region Overwritten protected methods

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);
            this.PerformReload();
        }

        protected override void OnShown(EventArgs args)
        {
            this.LoadSettings();
            base.OnShown(args);
        }

        protected override void SetVisibleCore(Boolean value)
        {
            base.SetVisibleCore(this.notifyIcon.Visible ? false : value);
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            this.SaveSettings();
        }

        #endregion

        #region List view events

        private void OnListViewKeyDown(Object sender, KeyEventArgs args)
        {
            switch (args.KeyCode)
            {
                case Keys.Insert:
                    args.Handled = true;
                    this.HandleCreate();
                    break;
                case Keys.F2:
                case Keys.Enter:
                    args.Handled = true;
                    this.HandleModify();
                    break;
                case Keys.Delete:
                    args.Handled = true;
                    this.HandleDelete();
                    break;
            }
        }

        private void OnListViewDoubleClick(Object sender, EventArgs args)
        {
            this.HandleModify();
        }

        private void OnListViewEnter(Object sender, EventArgs args)
        {
            this.activeListView = sender as ListView;
            this.EnableButtons();
        }

        #endregion

        #region Tool bar events

        private void OnCreateClicked(Object sender, EventArgs args)
        {
            this.HandleCreate();
        }

        private void OnDeleteClicked(Object sender, EventArgs args)
        {
            this.HandleDelete();
        }

        private void OnModifyClicked(Object sender, EventArgs args)
        {
            this.HandleModify();
        }

        private void OnElevateClicked(Object sender, EventArgs args)
        {
            String message = "Do you want to start program in administration mode?";

            if (this.CheckModified())
            {
                message += String.Format("{0}{0}Be aware, all made changes are lost!", Environment.NewLine);
            }

            if (DialogResult.Yes == MessageBox.Show(this, message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2))
            {
                if (SelfElevation.Elevate(String.Empty))
                {
                    this.Close();
                    Application.Exit();
                }
            }
        }

        private void OnDumpClicked(Object sender, EventArgs args)
        {
            using (new WaitCursor(this))
            {
                foreach (ListViewItem item in this.lsvUser.Items)
                {
                    Program.Logger.Trace(((EnvironmentVariable)item.Tag).ToString());
                }

                foreach (ListViewItem item in this.lsvMachine.Items)
                {
                    Program.Logger.Trace(((EnvironmentVariable)item.Tag).ToString());
                }
            }
        }

        private void OnSaveClicked(Object sender, EventArgs args)
        {
            String message = "Possibly dangerous operation! Do you really want to apply the environment changes you made?";

            if (DialogResult.Yes == MessageBox.Show(this, message, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2))
            {
                IList<Exception> exceptions = new List<Exception>();

                using (new WaitCursor(this))
                {
                    if (!EnvironmentSerializer.Save(this.GetModifiedVariables(this.lsvUser), ref exceptions))
                    {
                        Program.Logger.Fatal("Error occurred while serializing user scope environment.");
                    }

                    this.SetupListView(this.lsvUser, EnvironmentSerializer.Load(EnvironmentVariableTarget.User));

                    if (PermissionCheck.IsRunAsAdmin)
                    {
                        if (!EnvironmentSerializer.Save(this.GetModifiedVariables(this.lsvMachine), ref exceptions))
                        {
                            Program.Logger.Fatal("Error occurred while serializing machine scope environment.");
                        }

                        this.SetupListView(this.lsvMachine, EnvironmentSerializer.Load(EnvironmentVariableTarget.Machine));
                    }

                    foreach (Exception exception in exceptions)
                    {
                        Program.Logger.Error(exception);
                    }

                    Program.SaveSettings();
                }
            }
        }

        private void OnExitClicked(Object sender, EventArgs args)
        {
            this.Close();
            Application.Exit();
        }

        private void OnHideClicked(Object sender, EventArgs args)
        {
            this.notifyIcon.Visible = true;
            this.Hide();
        }

        private void OnSettingsClicked(Object sender, EventArgs args)
        {
            SettingsDialog dialog = new SettingsDialog();

            dialog.ShowDialog(this);

            if (dialog.DialogResult == DialogResult.OK)
            {
                // TODO: Something useful later on...
            }
        }

        #endregion

        #region Notify icon events

        private void OnNotifyDoubleClicked(Object sender, MouseEventArgs args)
        {
            this.OnNotifyShowClicked(sender, args);
        }

        private void OnNotifyMenuOpening(Object sender, CancelEventArgs args)
        {
            Point point = Control.MousePosition;

            using (new WaitCursor(this))
            {
                this.notifyMenu.Items.Clear();

                if (!this.Visible)
                {
                    IEnumerable<EnvironmentVariable> variables =
                        EnvironmentSerializer.Load(EnvironmentVariableTarget.User)
                        .Concat(EnvironmentSerializer.Load(EnvironmentVariableTarget.Machine))
                        .Where(x => x.Shift != null && x.Shift.Length > 0);

                    foreach (EnvironmentVariable variable in variables)
                    {
                        ToolStripMenuItem strip = new ToolStripMenuItem(variable.Label)
                        {
                            Tag = variable.Scope,
                        };

                        foreach (String shift in variable.Shift)
                        {
                            ToolStripMenuItem child = new ToolStripMenuItem(shift)
                            {
                                Checked = String.Compare(shift, variable.Value) == 0
                            };

                            child.Click += this.OnNotifyItemClicked;
                            strip.DropDownItems.Add(child);
                        }

                        this.notifyMenu.Items.Add(strip);
                    }

                    if (variables.Any())
                    {
                        this.notifyMenu.Items.Add(this.cmiSeparator1);
                    }

                    this.notifyMenu.Items.Add(this.cmiShow);
                }
                else
                {
                    this.notifyMenu.Items.Add(this.cmiHide);
                }

                this.notifyMenu.Items.Add(this.cmiExit);
            }

            this.notifyMenu.Show(point, ToolStripDropDownDirection.AboveLeft);
        }

        private void OnNotifyItemClicked(Object sender, EventArgs args)
        {
            if (!(sender is ToolStripMenuItem affected) || affected.OwnerItem == null)
            {
                return;
            }

            if (!Enum.IsDefined(typeof(EnvironmentVariableTarget), affected.OwnerItem.Tag))
            {
                return;
            }

            EnvironmentVariableTarget target = (EnvironmentVariableTarget)affected.OwnerItem.Tag;
            String label = affected.OwnerItem.Text;
            String value = affected.Text;

            try
            {
                if (target == EnvironmentVariableTarget.Machine && !PermissionCheck.IsRunAsAdmin)
                {
                    String message = "Administrator privileges required to change this environment variable!";
                    MessageBox.Show(message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Environment.SetEnvironmentVariable(label, value, target);
            }
            catch (Exception exception)
            {

                String message = "Could not change environment variable.";
                Program.Logger.Error(message, exception, new (String, Object)[] { ("Label", label), ("Value", value), ("Target", target.ToString()) });
                MessageBox.Show($"{message}{Environment.NewLine}{Environment.NewLine}{exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnNotifyShowClicked(Object sender, EventArgs args)
        {
            this.notifyIcon.Visible = false;
            this.PerformReload();
            this.Show();
        }

        private void OnNotifyHideClicked(Object sender, EventArgs args)
        {
            this.OnHideClicked(sender, args);
        }

        private void OnNotifyExitClicked(Object sender, EventArgs args)
        {
            this.OnExitClicked(sender, args);
        }

        #endregion

        #region Other private methods

        private void PerformReload()
        {
            using (new WaitCursor(this))
            {
                this.SetupListView(this.lsvUser, EnvironmentSerializer.Load(EnvironmentVariableTarget.User));
                this.SetupListView(this.lsvMachine, EnvironmentSerializer.Load(EnvironmentVariableTarget.Machine));

                if (PermissionCheck.IsRunAsAdmin)
                {
                    this.tbbElevate.Visible = false;
                    this.tbsSeparator4.Visible = false;
                }

                this.tbbSave.Enabled = this.CheckModified();

                if (this.lsvUser.Items.Count > 0)
                {
                    this.activeListView = this.lsvUser;
                }
                else if (this.lsvMachine.Items.Count > 0)
                {
                    this.activeListView = this.lsvMachine;
                }

                if (this.activeListView != null && this.activeListView.Items.Count > 0)
                {
                    this.activeListView.Items[0].Selected = true;
                }

                this.EnableButtons();
            }
        }

        private ListViewItem CreateEntry(EnvironmentVariable variable)
        {
            return new ListViewItem(new String[] { variable.Label, variable.Value })
            {
                Tag = variable,
                ToolTipText = variable.GetTooltip(),
                Font = this.GetItemFont(variable),
                ForeColor = this.GetForeColor(variable),
                BackColor = this.GetBackColor(variable)
            };
        }

        private void SetupListView(ListView affected, IEnumerable<EnvironmentVariable> variables)
        {
            affected.Clear();
            affected.Columns.Clear();

            this.EnsureColumns(affected);

            foreach (EnvironmentVariable current in variables)
            {
                affected.Items.Add(this.CreateEntry(current));
            }

            this.ResizeColumns(affected);
        }

        private void AppendListView(ListView affected, EnvironmentVariable variable)
        {
            this.EnsureColumns(affected);

            ListViewItem current = this.CreateEntry(variable);

            affected.Items.Add(current);

            current.Selected = true;
            current.EnsureVisible();

            this.ResizeColumns(affected);
        }

        private void UpdateListView(ListViewItem affected, EnvironmentVariable variable)
        {
            affected.SubItems[0].Text = variable.Label;
            affected.SubItems[1].Text = variable.Value;
            affected.Tag = variable;
            affected.ToolTipText = variable.GetTooltip();
            affected.Font = this.GetItemFont(variable);
            affected.ForeColor = this.GetForeColor(variable);
            affected.BackColor = this.GetBackColor(variable);
        }

        private void HandleCreate()
        {
            if (this.activeListView == null)
            {
                return;
            }

            if (this.activeListView == this.lsvMachine && !PermissionCheck.IsRunAsAdmin)
            {
                return;
            }

            EnvironmentVariableTarget scope = this.activeListView == this.lsvMachine ? EnvironmentVariableTarget.Machine : EnvironmentVariableTarget.User;

            VariableDialog dialog = new VariableDialog(scope);

            dialog.ShowDialog(this);

            if (dialog.DialogResult == DialogResult.OK)
            {
                this.AppendListView(this.activeListView, dialog.Variable);
            }

            this.tbbSave.Enabled = this.CheckModified();
        }

        private void HandleModify()
        {
            if (this.activeListView == null)
            {
                return;
            }

            if (this.activeListView == this.lsvMachine && !PermissionCheck.IsRunAsAdmin)
            {
                return;
            }

            if (this.activeListView.SelectedItems == null || this.activeListView.SelectedItems.Count < 1)
            {
                return;
            }

            if (this.activeListView.SelectedItems[0].Tag is EnvironmentVariable current)
            {
                VariableDialog dialog = new VariableDialog(current);

                dialog.ShowDialog(this);

                if (dialog.DialogResult == DialogResult.OK)
                {
                    this.UpdateListView(this.activeListView.SelectedItems[0], dialog.Variable);
                }
            }

            this.tbbSave.Enabled = this.CheckModified();
        }

        private void HandleDelete()
        {
            if (this.activeListView == null)
            {
                return;
            }

            if (this.activeListView == this.lsvMachine && !PermissionCheck.IsRunAsAdmin)
            {
                return;
            }

            if (this.activeListView.SelectedItems == null || this.activeListView.SelectedItems.Count < 1)
            {
                return;
            }

            ListViewItem selected = this.activeListView.SelectedItems[0];

            if (selected.Tag is EnvironmentVariable current)
            {
                if (current.IsCreated)
                {
                    selected.Remove();
                }
                else
                {
                    current.TryChangeStage(EnvironmentVariable.Stages.Deleted);

                    selected.ToolTipText = current.GetTooltip();
                    selected.Font = this.GetItemFont(current);
                    selected.ForeColor = this.GetForeColor(current);
                    selected.BackColor = this.GetBackColor(current);
                }
            }

            this.tbbSave.Enabled = this.CheckModified();
        }

        private Color GetForeColor(EnvironmentVariable variable)
        {
            switch (variable.Stage)
            {
                case EnvironmentVariable.Stages.Created:
                    return Color.Black;
                case EnvironmentVariable.Stages.Changed:
                    return Color.Black;
                case EnvironmentVariable.Stages.Deleted:
                    return Color.WhiteSmoke;
                case EnvironmentVariable.Stages.Nothing:
                default:
                    return Color.Black;
            }
        }

        private Color GetBackColor(EnvironmentVariable variable)
        {
            switch (variable.Stage)
            {
                case EnvironmentVariable.Stages.Created:
                    return Color.Lime;
                case EnvironmentVariable.Stages.Changed:
                    return Color.Khaki;
                case EnvironmentVariable.Stages.Deleted:
                    return Color.Crimson;
                case EnvironmentVariable.Stages.Nothing:
                default:
                    return Color.White;
            }
        }

        private Font GetItemFont(EnvironmentVariable variable)
        {
            switch (variable.Stage)
            {
                case EnvironmentVariable.Stages.Created:
                case EnvironmentVariable.Stages.Changed:
                case EnvironmentVariable.Stages.Deleted:
                case EnvironmentVariable.Stages.Nothing:
                default:
                    return this.defaultFont;
            }
        }

        private void EnableButtons()
        {
            if (this.activeListView == this.lsvMachine)
            {
                Boolean enabled = this.CheckSelected(this.lsvMachine) && PermissionCheck.IsRunAsAdmin;

                this.tbbCreate.Enabled = PermissionCheck.IsRunAsAdmin;
                this.tbbModify.Enabled = enabled;
                this.tbbDelete.Enabled = enabled;
            }
            else
            {
                Boolean enabled = this.CheckSelected(this.lsvUser);

                this.tbbCreate.Enabled = true;
                this.tbbModify.Enabled = enabled;
                this.tbbDelete.Enabled = enabled;
            }
        }

        private void EnsureColumns(ListView affected)
        {
            if (affected.Columns.Count < 1)
            {
                affected.Columns.AddRange(
                    new ColumnHeader[]
                    {
                        new ColumnHeader() { Text = nameof(EnvironmentVariable.Label) },
                        new ColumnHeader() { Text = nameof(EnvironmentVariable.Value) },
                    });
            }
        }

        private void ResizeColumns(ListView affected)
        {
            foreach (ColumnHeader column in affected.Columns)
            {
                column.Width = -2;
            }
        }

        private Boolean CheckSelected(ListView affected)
        {
            return affected.Items.Count > 0 && affected.Items
                .Cast<ListViewItem>()
                .Any(x => x.Selected);
        }

        private Boolean CheckModified()
        {
            return this.CountModified(this.lsvUser) > 0 || this.CountModified(this.lsvMachine) > 0;
        }

        private Int32 CountModified(ListView affected)
        {
            return affected.Items
                .Cast<ListViewItem>()
                .Select(x => x.Tag as EnvironmentVariable)
                .Where(x => x.IsModified)
                .Count();
        }

        private IEnumerable<EnvironmentVariable> GetModifiedVariables(ListView affected)
        {
            return affected.Items
                .Cast<ListViewItem>()
                .Where(x => (x.Tag as EnvironmentVariable).IsModified)
                .Select(x => (x.Tag as EnvironmentVariable))
                .AsEnumerable();
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
                // Keep this order of settings assignments!

                this.DesktopBounds = Program.ReadSettingsValue(this.GetType().Name, nameof(this.DesktopBounds))
                    .StringToBounds(this.StandardBounds(new Size(700, 600)));

                Int32 minimumDistance = this.splContainer.Panel1.Top + this.splContainer.Panel1MinSize;
                Int32 maximumDistance = this.splContainer.Panel2.Bottom - this.splContainer.Panel2MinSize;
                Int32 defaultDistance = this.splContainer.SplitterDistance;

                this.splContainer.SplitterDistance = Program.ReadSettingsValue(this.GetType().Name, nameof(this.splContainer.SplitterDistance))
                    .StringToInteger(minimumDistance, maximumDistance, defaultDistance);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Loading settings for main window from configuration has failed.", exception);
            }

            this.EnsureScreenLocation();
        }

        private void SaveSettings()
        {
            try
            {
                Program.SaveSettingsValue(this.GetType().Name, nameof(this.splContainer.SplitterDistance), this.splContainer.SplitterDistance.IntegerToString());
                Program.SaveSettingsValue(this.GetType().Name, nameof(this.DesktopBounds), this.DesktopBounds.BoundsToString());
                Program.SaveSettings();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Saving settings for main window into configuration has failed.", exception);
            }
        }

        #endregion
    }
}
