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

namespace Plexdata.EnvironmentManager.Dialogs
{
    partial class SettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnInstall = new System.Windows.Forms.Button();
            this.lblRemove = new System.Windows.Forms.Label();
            this.lblInstall = new System.Windows.Forms.Label();
            this.tblRegistration = new System.Windows.Forms.TableLayoutPanel();
            this.tabContent = new System.Windows.Forms.TabControl();
            this.tabRegistration = new System.Windows.Forms.TabPage();
            this.tabArguments = new System.Windows.Forms.TabPage();
            this.txtArguments = new System.Windows.Forms.TextBox();
            this.tblRegistration.SuspendLayout();
            this.tabContent.SuspendLayout();
            this.tabRegistration.SuspendLayout();
            this.tabArguments.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(397, 277);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnAccept.Location = new System.Drawing.Point(316, 277);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "&OK";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(344, 104);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.OnRemoveClicked);
            // 
            // btnInstall
            // 
            this.btnInstall.Location = new System.Drawing.Point(344, 3);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(75, 23);
            this.btnInstall.TabIndex = 1;
            this.btnInstall.Text = "&Install";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.OnInstallClicked);
            // 
            // lblRemove
            // 
            this.lblRemove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemove.Location = new System.Drawing.Point(3, 101);
            this.lblRemove.Name = "lblRemove";
            this.lblRemove.Size = new System.Drawing.Size(335, 102);
            this.lblRemove.TabIndex = 2;
            this.lblRemove.Text = "Click button [Remove] to delete program registration for auto-launch on user logi" +
    "n.";
            // 
            // lblInstall
            // 
            this.lblInstall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInstall.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstall.Location = new System.Drawing.Point(3, 0);
            this.lblInstall.Name = "lblInstall";
            this.lblInstall.Size = new System.Drawing.Size(335, 101);
            this.lblInstall.TabIndex = 0;
            this.lblInstall.Text = "Click button [Install] to register program for auto-launch on user login.";
            // 
            // tblRegistration
            // 
            this.tblRegistration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tblRegistration.ColumnCount = 2;
            this.tblRegistration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblRegistration.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblRegistration.Controls.Add(this.btnRemove, 1, 1);
            this.tblRegistration.Controls.Add(this.lblInstall, 0, 0);
            this.tblRegistration.Controls.Add(this.btnInstall, 1, 0);
            this.tblRegistration.Controls.Add(this.lblRemove, 0, 1);
            this.tblRegistration.Location = new System.Drawing.Point(15, 15);
            this.tblRegistration.Margin = new System.Windows.Forms.Padding(10);
            this.tblRegistration.Name = "tblRegistration";
            this.tblRegistration.RowCount = 2;
            this.tblRegistration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRegistration.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblRegistration.Size = new System.Drawing.Size(422, 203);
            this.tblRegistration.TabIndex = 0;
            // 
            // tabContent
            // 
            this.tabContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabContent.Controls.Add(this.tabRegistration);
            this.tabContent.Controls.Add(this.tabArguments);
            this.tabContent.Location = new System.Drawing.Point(12, 12);
            this.tabContent.Name = "tabContent";
            this.tabContent.SelectedIndex = 0;
            this.tabContent.Size = new System.Drawing.Size(460, 259);
            this.tabContent.TabIndex = 0;
            // 
            // tabRegistration
            // 
            this.tabRegistration.Controls.Add(this.tblRegistration);
            this.tabRegistration.Location = new System.Drawing.Point(4, 22);
            this.tabRegistration.Name = "tabRegistration";
            this.tabRegistration.Padding = new System.Windows.Forms.Padding(5);
            this.tabRegistration.Size = new System.Drawing.Size(452, 233);
            this.tabRegistration.TabIndex = 0;
            this.tabRegistration.Text = "Registration";
            this.tabRegistration.UseVisualStyleBackColor = true;
            // 
            // tabArguments
            // 
            this.tabArguments.Controls.Add(this.txtArguments);
            this.tabArguments.Location = new System.Drawing.Point(4, 22);
            this.tabArguments.Name = "tabArguments";
            this.tabArguments.Padding = new System.Windows.Forms.Padding(5);
            this.tabArguments.Size = new System.Drawing.Size(452, 233);
            this.tabArguments.TabIndex = 1;
            this.tabArguments.Text = "Arguments";
            this.tabArguments.UseVisualStyleBackColor = true;
            // 
            // txtArguments
            // 
            this.txtArguments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtArguments.BackColor = System.Drawing.SystemColors.Window;
            this.txtArguments.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtArguments.Location = new System.Drawing.Point(15, 15);
            this.txtArguments.Margin = new System.Windows.Forms.Padding(10);
            this.txtArguments.Multiline = true;
            this.txtArguments.Name = "txtArguments";
            this.txtArguments.ReadOnly = true;
            this.txtArguments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtArguments.Size = new System.Drawing.Size(422, 203);
            this.txtArguments.TabIndex = 0;
            this.txtArguments.WordWrap = false;
            // 
            // SettingsDialog
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 312);
            this.Controls.Add(this.tabContent);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 300);
            this.Name = "SettingsDialog";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tblRegistration.ResumeLayout(false);
            this.tabContent.ResumeLayout(false);
            this.tabRegistration.ResumeLayout(false);
            this.tabArguments.ResumeLayout(false);
            this.tabArguments.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label lblInstall;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Label lblRemove;
        private System.Windows.Forms.TableLayoutPanel tblRegistration;
        private System.Windows.Forms.TabControl tabContent;
        private System.Windows.Forms.TabPage tabRegistration;
        private System.Windows.Forms.TabPage tabArguments;
        private System.Windows.Forms.TextBox txtArguments;
    }
}