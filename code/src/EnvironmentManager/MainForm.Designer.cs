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

namespace Plexdata.EnvironmentManager
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.tbsMain = new System.Windows.Forms.ToolStrip();
            this.tbbExit = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbCreate = new System.Windows.Forms.ToolStripButton();
            this.tbbModify = new System.Windows.Forms.ToolStripButton();
            this.tbbDelete = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbElevate = new System.Windows.Forms.ToolStripButton();
            this.tbbDump = new System.Windows.Forms.ToolStripButton();
            this.tbbHide = new System.Windows.Forms.ToolStripButton();
            this.tbsSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tbbSettings = new System.Windows.Forms.ToolStripButton();
            this.stbMain = new System.Windows.Forms.StatusStrip();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmiExit = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiShow = new System.Windows.Forms.ToolStripMenuItem();
            this.cmiSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cmiHide = new System.Windows.Forms.ToolStripMenuItem();
            this.splContainer = new Plexdata.EnvironmentManager.Controls.SplitContainerEx();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this.lsvUser = new System.Windows.Forms.ListView();
            this.grpMachine = new System.Windows.Forms.GroupBox();
            this.lsvMachine = new System.Windows.Forms.ListView();
            this.tbsMain.SuspendLayout();
            this.notifyMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splContainer)).BeginInit();
            this.splContainer.Panel1.SuspendLayout();
            this.splContainer.Panel2.SuspendLayout();
            this.splContainer.SuspendLayout();
            this.grpUser.SuspendLayout();
            this.grpMachine.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbsMain
            // 
            this.tbsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbbExit,
            this.tbsSeparator1,
            this.tbbSave,
            this.tbsSeparator2,
            this.tbbCreate,
            this.tbbModify,
            this.tbbDelete,
            this.tbsSeparator3,
            this.tbbElevate,
            this.tbbDump,
            this.tbbHide,
            this.tbsSeparator4,
            this.tbbSettings});
            this.tbsMain.Location = new System.Drawing.Point(0, 0);
            this.tbsMain.Name = "tbsMain";
            this.tbsMain.Size = new System.Drawing.Size(660, 47);
            this.tbsMain.TabIndex = 2;
            this.tbsMain.Text = "toolStrip1";
            // 
            // tbbExit
            // 
            this.tbbExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbExit.Image = global::Plexdata.EnvironmentManager.Properties.Resources.ExitLargeIcon;
            this.tbbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbExit.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbExit.Name = "tbbExit";
            this.tbbExit.Padding = new System.Windows.Forms.Padding(3);
            this.tbbExit.Size = new System.Drawing.Size(42, 42);
            this.tbbExit.Text = "Exit";
            this.tbbExit.ToolTipText = "Close main window and exit application.";
            this.tbbExit.Click += new System.EventHandler(this.OnExitClicked);
            // 
            // tbsSeparator1
            // 
            this.tbsSeparator1.Name = "tbsSeparator1";
            this.tbsSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // tbbSave
            // 
            this.tbbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSave.Image = global::Plexdata.EnvironmentManager.Properties.Resources.SaveLargeIcon;
            this.tbbSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSave.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbSave.Name = "tbbSave";
            this.tbbSave.Padding = new System.Windows.Forms.Padding(3);
            this.tbbSave.Size = new System.Drawing.Size(42, 42);
            this.tbbSave.Text = "Save";
            this.tbbSave.ToolTipText = "Write all changes back to the system.";
            this.tbbSave.Click += new System.EventHandler(this.OnSaveClicked);
            // 
            // tbsSeparator2
            // 
            this.tbsSeparator2.Name = "tbsSeparator2";
            this.tbsSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // tbbCreate
            // 
            this.tbbCreate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbCreate.Image = global::Plexdata.EnvironmentManager.Properties.Resources.CreateLargeIcon;
            this.tbbCreate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbCreate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbCreate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbCreate.Name = "tbbCreate";
            this.tbbCreate.Padding = new System.Windows.Forms.Padding(3);
            this.tbbCreate.Size = new System.Drawing.Size(42, 42);
            this.tbbCreate.Text = "Create";
            this.tbbCreate.ToolTipText = "Create new environment variable.";
            this.tbbCreate.Click += new System.EventHandler(this.OnCreateClicked);
            // 
            // tbbModify
            // 
            this.tbbModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbModify.Image = global::Plexdata.EnvironmentManager.Properties.Resources.ModifyLargeIcon;
            this.tbbModify.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbModify.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbModify.Name = "tbbModify";
            this.tbbModify.Padding = new System.Windows.Forms.Padding(3);
            this.tbbModify.Size = new System.Drawing.Size(42, 42);
            this.tbbModify.Text = "Modify";
            this.tbbModify.ToolTipText = "Modify selected environment variable.";
            this.tbbModify.Click += new System.EventHandler(this.OnModifyClicked);
            // 
            // tbbDelete
            // 
            this.tbbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbDelete.Image = global::Plexdata.EnvironmentManager.Properties.Resources.DeleteLargeIcon;
            this.tbbDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbDelete.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbDelete.Name = "tbbDelete";
            this.tbbDelete.Padding = new System.Windows.Forms.Padding(3);
            this.tbbDelete.Size = new System.Drawing.Size(42, 42);
            this.tbbDelete.Text = "Delete";
            this.tbbDelete.ToolTipText = "Indicate selected environment variable as deleted.";
            this.tbbDelete.Click += new System.EventHandler(this.OnDeleteClicked);
            // 
            // tbsSeparator3
            // 
            this.tbsSeparator3.Name = "tbsSeparator3";
            this.tbsSeparator3.Size = new System.Drawing.Size(6, 47);
            // 
            // tbbElevate
            // 
            this.tbbElevate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbElevate.Image = global::Plexdata.EnvironmentManager.Properties.Resources.ShieldLargeIcon;
            this.tbbElevate.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbElevate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbElevate.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbElevate.Name = "tbbElevate";
            this.tbbElevate.Padding = new System.Windows.Forms.Padding(3);
            this.tbbElevate.Size = new System.Drawing.Size(42, 42);
            this.tbbElevate.Text = "Elevate";
            this.tbbElevate.ToolTipText = "Restart application in administrator mode.";
            this.tbbElevate.Click += new System.EventHandler(this.OnElevateClicked);
            // 
            // tbbDump
            // 
            this.tbbDump.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbDump.Image = global::Plexdata.EnvironmentManager.Properties.Resources.DumpLargeIcon;
            this.tbbDump.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbDump.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbDump.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbDump.Name = "tbbDump";
            this.tbbDump.Padding = new System.Windows.Forms.Padding(3);
            this.tbbDump.Size = new System.Drawing.Size(42, 42);
            this.tbbDump.Text = "Dump";
            this.tbbDump.ToolTipText = "Dump all environment variables.";
            this.tbbDump.Click += new System.EventHandler(this.OnDumpClicked);
            // 
            // tbbHide
            // 
            this.tbbHide.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbHide.Image = global::Plexdata.EnvironmentManager.Properties.Resources.FollowLargeIcon;
            this.tbbHide.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbHide.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbHide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbHide.Name = "tbbHide";
            this.tbbHide.Padding = new System.Windows.Forms.Padding(3);
            this.tbbHide.Size = new System.Drawing.Size(42, 42);
            this.tbbHide.Text = "Hide";
            this.tbbHide.ToolTipText = "Hide user interface and run in tray icon mode.";
            this.tbbHide.Click += new System.EventHandler(this.OnHideClicked);
            // 
            // tbsSeparator4
            // 
            this.tbsSeparator4.Name = "tbsSeparator4";
            this.tbsSeparator4.Size = new System.Drawing.Size(6, 47);
            // 
            // tbbSettings
            // 
            this.tbbSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbbSettings.Image = global::Plexdata.EnvironmentManager.Properties.Resources.SettingsLargeIcon;
            this.tbbSettings.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tbbSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbbSettings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 3);
            this.tbbSettings.Name = "tbbSettings";
            this.tbbSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbbSettings.Size = new System.Drawing.Size(42, 42);
            this.tbbSettings.Text = "Settings";
            this.tbbSettings.ToolTipText = "Modify application settings.";
            this.tbbSettings.Click += new System.EventHandler(this.OnSettingsClicked);
            // 
            // stbMain
            // 
            this.stbMain.Location = new System.Drawing.Point(0, 532);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(660, 22);
            this.stbMain.TabIndex = 3;
            this.stbMain.Text = "statusStrip1";
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyMenu;
            this.notifyIcon.Text = "Environment Manager";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OnNotifyDoubleClicked);
            // 
            // notifyMenu
            // 
            this.notifyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmiExit,
            this.cmiShow,
            this.cmiSeparator1,
            this.cmiHide});
            this.notifyMenu.Name = "notifyMenu";
            this.notifyMenu.Size = new System.Drawing.Size(104, 76);
            this.notifyMenu.Opening += new System.ComponentModel.CancelEventHandler(this.OnNotifyMenuOpening);
            // 
            // cmiExit
            // 
            this.cmiExit.Name = "cmiExit";
            this.cmiExit.Size = new System.Drawing.Size(103, 22);
            this.cmiExit.Text = "&Exit";
            this.cmiExit.Click += new System.EventHandler(this.OnNotifyExitClicked);
            // 
            // cmiShow
            // 
            this.cmiShow.Name = "cmiShow";
            this.cmiShow.Size = new System.Drawing.Size(103, 22);
            this.cmiShow.Text = "&Show";
            this.cmiShow.Click += new System.EventHandler(this.OnNotifyShowClicked);
            // 
            // cmiSeparator1
            // 
            this.cmiSeparator1.Name = "cmiSeparator1";
            this.cmiSeparator1.Size = new System.Drawing.Size(100, 6);
            // 
            // cmiHide
            // 
            this.cmiHide.Name = "cmiHide";
            this.cmiHide.Size = new System.Drawing.Size(103, 22);
            this.cmiHide.Text = "&Hide";
            this.cmiHide.Click += new System.EventHandler(this.OnNotifyHideClicked);
            // 
            // splContainer
            // 
            this.splContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splContainer.Location = new System.Drawing.Point(0, 47);
            this.splContainer.Name = "splContainer";
            this.splContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splContainer.Panel1
            // 
            this.splContainer.Panel1.Controls.Add(this.grpUser);
            this.splContainer.Panel1.Padding = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.splContainer.Panel1MinSize = 150;
            // 
            // splContainer.Panel2
            // 
            this.splContainer.Panel2.Controls.Add(this.grpMachine);
            this.splContainer.Panel2MinSize = 150;
            this.splContainer.Size = new System.Drawing.Size(660, 485);
            this.splContainer.SplitterDistance = 178;
            this.splContainer.SplitterWidth = 10;
            this.splContainer.TabIndex = 0;
            this.splContainer.TabStop = false;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this.lsvUser);
            this.grpUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpUser.Location = new System.Drawing.Point(0, 10);
            this.grpUser.Name = "grpUser";
            this.grpUser.Padding = new System.Windows.Forms.Padding(10);
            this.grpUser.Size = new System.Drawing.Size(660, 163);
            this.grpUser.TabIndex = 0;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "User Scope";
            // 
            // lsvUser
            // 
            this.lsvUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvUser.FullRowSelect = true;
            this.lsvUser.LabelWrap = false;
            this.lsvUser.Location = new System.Drawing.Point(10, 23);
            this.lsvUser.MultiSelect = false;
            this.lsvUser.Name = "lsvUser";
            this.lsvUser.ShowGroups = false;
            this.lsvUser.ShowItemToolTips = true;
            this.lsvUser.Size = new System.Drawing.Size(640, 130);
            this.lsvUser.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvUser.TabIndex = 0;
            this.lsvUser.UseCompatibleStateImageBehavior = false;
            this.lsvUser.View = System.Windows.Forms.View.Details;
            this.lsvUser.Click += new System.EventHandler(this.OnListViewEnter);
            this.lsvUser.Enter += new System.EventHandler(this.OnListViewEnter);
            // 
            // grpMachine
            // 
            this.grpMachine.Controls.Add(this.lsvMachine);
            this.grpMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpMachine.Location = new System.Drawing.Point(0, 0);
            this.grpMachine.Name = "grpMachine";
            this.grpMachine.Padding = new System.Windows.Forms.Padding(10);
            this.grpMachine.Size = new System.Drawing.Size(660, 297);
            this.grpMachine.TabIndex = 0;
            this.grpMachine.TabStop = false;
            this.grpMachine.Text = "Machine Scope";
            // 
            // lsvMachine
            // 
            this.lsvMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvMachine.FullRowSelect = true;
            this.lsvMachine.LabelWrap = false;
            this.lsvMachine.Location = new System.Drawing.Point(10, 23);
            this.lsvMachine.MultiSelect = false;
            this.lsvMachine.Name = "lsvMachine";
            this.lsvMachine.ShowGroups = false;
            this.lsvMachine.ShowItemToolTips = true;
            this.lsvMachine.Size = new System.Drawing.Size(640, 264);
            this.lsvMachine.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvMachine.TabIndex = 0;
            this.lsvMachine.UseCompatibleStateImageBehavior = false;
            this.lsvMachine.View = System.Windows.Forms.View.Details;
            this.lsvMachine.Click += new System.EventHandler(this.OnListViewEnter);
            this.lsvMachine.Enter += new System.EventHandler(this.OnListViewEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 554);
            this.Controls.Add(this.splContainer);
            this.Controls.Add(this.tbsMain);
            this.Controls.Add(this.stbMain);
            this.Name = "MainForm";
            this.Text = "Environment Manager";
            this.tbsMain.ResumeLayout(false);
            this.tbsMain.PerformLayout();
            this.notifyMenu.ResumeLayout(false);
            this.splContainer.Panel1.ResumeLayout(false);
            this.splContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splContainer)).EndInit();
            this.splContainer.ResumeLayout(false);
            this.grpUser.ResumeLayout(false);
            this.grpMachine.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvMachine;
        private System.Windows.Forms.ListView lsvUser;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.GroupBox grpMachine;
        private Controls.SplitContainerEx splContainer;
        private System.Windows.Forms.ToolStrip tbsMain;
        private System.Windows.Forms.ToolStripButton tbbExit;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator1;
        private System.Windows.Forms.ToolStripButton tbbSave;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator2;
        private System.Windows.Forms.ToolStripButton tbbCreate;
        private System.Windows.Forms.ToolStripButton tbbModify;
        private System.Windows.Forms.ToolStripButton tbbDelete;
        private System.Windows.Forms.ToolStripButton tbbElevate;
        private System.Windows.Forms.ToolStripButton tbbDump;
        private System.Windows.Forms.StatusStrip stbMain;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator4;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenu;
        private System.Windows.Forms.ToolStripMenuItem cmiExit;
        private System.Windows.Forms.ToolStripMenuItem cmiShow;
        private System.Windows.Forms.ToolStripSeparator cmiSeparator1;
        private System.Windows.Forms.ToolStripMenuItem cmiHide;
        private System.Windows.Forms.ToolStripButton tbbHide;
        private System.Windows.Forms.ToolStripSeparator tbsSeparator3;
        private System.Windows.Forms.ToolStripButton tbbSettings;
    }
}

