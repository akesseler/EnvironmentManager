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

using Plexdata.EnvironmentManager.Extensions;
using Plexdata.EnvironmentManager.Internals;
using Plexdata.EnvironmentManager.Models;
using Plexdata.LogWriter.Extensions;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Plexdata.EnvironmentManager.Dialogs
{
    public partial class VariableDialog : Form
    {
        public VariableDialog(EnvironmentVariableTarget scope)
           : base()
        {
            this.InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;
            this.Variable = new EnvironmentVariable(scope);
        }

        public VariableDialog(EnvironmentVariable variable)
           : base()
        {
            this.InitializeComponent();
            this.Icon = Properties.Resources.MainIcon;
            if (variable == null) { throw new ArgumentNullException(nameof(variable)); }
            this.Variable = (EnvironmentVariable)variable.Clone();
        }

        public EnvironmentVariable Variable { get; private set; }

        protected override void OnLoad(EventArgs args)
        {
            base.OnLoad(args);

            this.Text += this.Variable.IsCreated ? " (NEW)" : " (EDIT)";

            this.valScope.DataSource = Enum.GetValues(this.Variable.Scope.GetType());
            this.valScope.SelectedItem = this.Variable.Scope;
            this.valScope.Enabled = false;

            this.valLabel.Text = this.Variable.Label;
            this.valLabel.TextChanged += this.OnLabelTextChanged;
            this.valLabel.ReadOnly = this.Variable.IsDeleted;
            this.valLabel.BackColor = this.valLabel.ReadOnly ? SystemColors.Info : SystemColors.Window;

            this.valValue.Text = this.Variable.Value;
            this.valValue.TextChanged += this.OnValueTextChanged;
            this.valValue.ReadOnly = this.Variable.IsDeleted;
            this.valValue.BackColor = this.valValue.ReadOnly ? SystemColors.Info : SystemColors.Window;

            this.valShift.Lines = this.Variable.Shift;
            this.valShift.TextChanged += this.OnShiftTextChanged;
            this.valShift.ReadOnly = this.Variable.IsDeleted;
            this.valShift.BackColor = this.valShift.ReadOnly ? SystemColors.Info : SystemColors.Window;

            this.btnAccept.Enabled = this.CanEnableAccept();
            this.btnAccept.Click += this.OnAcceptClicked;
        }

        protected override void OnShown(EventArgs args)
        {
            using (new WaitCursor(this))
            {
                this.LoadSettings();
                base.OnShown(args);
            }
        }

        protected override void OnClosing(CancelEventArgs args)
        {
            base.OnClosing(args);
            this.SaveSettings();
        }

        private void OnLabelTextChanged(Object sender, EventArgs args)
        {
            if (!String.IsNullOrWhiteSpace(this.valLabel.Text))
            {
                this.labelError.SetError(this.valLabel, String.Empty);
            }
            else
            {
                this.labelError.SetIconPadding(this.valLabel, 2);
                this.labelError.SetIconAlignment(this.valLabel, ErrorIconAlignment.MiddleLeft);
                this.labelError.SetError(this.valLabel, "The label for an environment variable is required.!");
            }

            this.btnAccept.Enabled = this.CanEnableAccept();
        }

        private void OnValueTextChanged(Object sender, EventArgs args)
        {
            if (!String.IsNullOrWhiteSpace(this.valValue.Text))
            {
                this.valueError.SetError(this.valValue, String.Empty);
            }
            else
            {
                this.valueError.SetIconPadding(this.valValue, 2);
                this.valueError.SetIconAlignment(this.valValue, ErrorIconAlignment.TopLeft);
                this.valueError.SetError(this.valValue, "The value for an environment variable is required.!");
            }

            this.btnAccept.Enabled = this.CanEnableAccept();
        }

        private void OnShiftTextChanged(Object sender, EventArgs args)
        {
            this.btnAccept.Enabled = this.CanEnableAccept();
        }

        private void OnAcceptClicked(Object sender, EventArgs args)
        {
            if (String.IsNullOrWhiteSpace(this.valLabel.Text) || String.IsNullOrWhiteSpace(this.valValue.Text))
            {
                base.DialogResult = DialogResult.None;
                return;
            }

            this.Variable.Scope = (EnvironmentVariableTarget)this.valScope.SelectedItem;
            this.Variable.Label = this.valLabel.Text;
            this.Variable.Value = this.valValue.Text;

            this.Variable.Shift = this.valShift.Lines;
        }

        private Boolean CanEnableAccept()
        {
            return
                !this.Variable.IsReadonly &&
                !this.Variable.IsDeleted &&
                !String.IsNullOrWhiteSpace(this.valLabel.Text) &&
                !String.IsNullOrWhiteSpace(this.valValue.Text);
        }

        private void LoadSettings()
        {
            try
            {
                this.DesktopBounds = Program.ReadSettingsValue(this.GetType().Name, nameof(this.DesktopBounds))
                    .StringToBounds(this.StandardBounds(new Size(400, 400)));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Loading settings for variable dialog from configuration has failed.", exception);
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
                Program.Logger.Error("Saving settings for variable dialog into configuration has failed.", exception);
            }
        }
    }
}
