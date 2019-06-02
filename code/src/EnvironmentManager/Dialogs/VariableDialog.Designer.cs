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
    partial class VariableDialog
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.valScope = new System.Windows.Forms.ComboBox();
            this.lblScope = new System.Windows.Forms.Label();
            this.valLabel = new System.Windows.Forms.TextBox();
            this.valValue = new System.Windows.Forms.TextBox();
            this.lblLabel = new System.Windows.Forms.Label();
            this.lblValue = new System.Windows.Forms.Label();
            this.labelError = new System.Windows.Forms.ErrorProvider(this.components);
            this.valueError = new System.Windows.Forms.ErrorProvider(this.components);
            this.valShift = new System.Windows.Forms.TextBox();
            this.lblShift = new System.Windows.Forms.Label();
            this.tabLayout = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.labelError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueError)).BeginInit();
            this.tabLayout.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(297, 327);
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
            this.btnAccept.Location = new System.Drawing.Point(216, 327);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 1;
            this.btnAccept.Text = "&OK";
            this.btnAccept.UseVisualStyleBackColor = true;
            // 
            // valScope
            // 
            this.valScope.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.valScope.FormattingEnabled = true;
            this.valScope.Location = new System.Drawing.Point(63, 3);
            this.valScope.Name = "valScope";
            this.valScope.Size = new System.Drawing.Size(294, 21);
            this.valScope.TabIndex = 1;
            // 
            // lblScope
            // 
            this.lblScope.AutoSize = true;
            this.lblScope.Location = new System.Drawing.Point(3, 0);
            this.lblScope.Name = "lblScope";
            this.lblScope.Size = new System.Drawing.Size(41, 13);
            this.lblScope.TabIndex = 0;
            this.lblScope.Text = "&Scope:";
            // 
            // valLabel
            // 
            this.valLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valLabel.Location = new System.Drawing.Point(63, 30);
            this.valLabel.Name = "valLabel";
            this.valLabel.Size = new System.Drawing.Size(294, 20);
            this.valLabel.TabIndex = 3;
            // 
            // valValue
            // 
            this.valValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valValue.Location = new System.Drawing.Point(63, 56);
            this.valValue.Multiline = true;
            this.valValue.Name = "valValue";
            this.valValue.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.valValue.Size = new System.Drawing.Size(294, 122);
            this.valValue.TabIndex = 5;
            this.valValue.WordWrap = false;
            // 
            // lblLabel
            // 
            this.lblLabel.AutoSize = true;
            this.lblLabel.Location = new System.Drawing.Point(3, 27);
            this.lblLabel.Name = "lblLabel";
            this.lblLabel.Size = new System.Drawing.Size(33, 13);
            this.lblLabel.TabIndex = 2;
            this.lblLabel.Text = "&Label";
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(3, 53);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(34, 13);
            this.lblValue.TabIndex = 4;
            this.lblValue.Text = "&Value";
            // 
            // labelError
            // 
            this.labelError.ContainerControl = this;
            // 
            // valueError
            // 
            this.valueError.ContainerControl = this;
            // 
            // valShift
            // 
            this.valShift.AcceptsReturn = true;
            this.valShift.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.valShift.Location = new System.Drawing.Point(63, 184);
            this.valShift.Multiline = true;
            this.valShift.Name = "valShift";
            this.valShift.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.valShift.Size = new System.Drawing.Size(294, 122);
            this.valShift.TabIndex = 7;
            this.valShift.WordWrap = false;
            // 
            // lblShift
            // 
            this.lblShift.AutoSize = true;
            this.lblShift.Location = new System.Drawing.Point(3, 181);
            this.lblShift.Name = "lblShift";
            this.lblShift.Size = new System.Drawing.Size(28, 13);
            this.lblShift.TabIndex = 6;
            this.lblShift.Text = "S&hift";
            // 
            // tabLayout
            // 
            this.tabLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabLayout.ColumnCount = 2;
            this.tabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tabLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tabLayout.Controls.Add(this.valScope, 1, 0);
            this.tabLayout.Controls.Add(this.valValue, 1, 2);
            this.tabLayout.Controls.Add(this.valShift, 1, 3);
            this.tabLayout.Controls.Add(this.lblScope, 0, 0);
            this.tabLayout.Controls.Add(this.valLabel, 1, 1);
            this.tabLayout.Controls.Add(this.lblShift, 0, 3);
            this.tabLayout.Controls.Add(this.lblLabel, 0, 1);
            this.tabLayout.Controls.Add(this.lblValue, 0, 2);
            this.tabLayout.Location = new System.Drawing.Point(12, 12);
            this.tabLayout.Name = "tabLayout";
            this.tabLayout.RowCount = 4;
            this.tabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tabLayout.Size = new System.Drawing.Size(360, 309);
            this.tabLayout.TabIndex = 0;
            // 
            // VariableDialog
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(384, 362);
            this.Controls.Add(this.tabLayout);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.btnCancel);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 400);
            this.Name = "VariableDialog";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Environment Variable";
            ((System.ComponentModel.ISupportInitialize)(this.labelError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.valueError)).EndInit();
            this.tabLayout.ResumeLayout(false);
            this.tabLayout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.ComboBox valScope;
        private System.Windows.Forms.Label lblScope;
        private System.Windows.Forms.TextBox valLabel;
        private System.Windows.Forms.TextBox valValue;
        private System.Windows.Forms.Label lblLabel;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.ErrorProvider labelError;
        private System.Windows.Forms.ErrorProvider valueError;
        private System.Windows.Forms.TextBox valShift;
        private System.Windows.Forms.TableLayoutPanel tabLayout;
        private System.Windows.Forms.Label lblShift;
    }
}