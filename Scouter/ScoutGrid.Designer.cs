﻿namespace CM0102Patcher.Scouter
{
    partial class ScoutGrid
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScoutGrid));
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBoxShowIntrinstics = new System.Windows.Forms.CheckBox();
            this.buttonColumns = new System.Windows.Forms.Button();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonSaveToScoutFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.Location = new System.Drawing.Point(12, 26);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowCellToolTips = false;
            this.dataGridView.ShowEditingIcon = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(626, 276);
            this.dataGridView.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(650, 24);
            this.menuStrip.TabIndex = 1;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem.Text = "&Open...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // checkBoxShowIntrinstics
            // 
            this.checkBoxShowIntrinstics.AutoSize = true;
            this.checkBoxShowIntrinstics.Location = new System.Drawing.Point(541, 312);
            this.checkBoxShowIntrinstics.Name = "checkBoxShowIntrinstics";
            this.checkBoxShowIntrinstics.Size = new System.Drawing.Size(97, 17);
            this.checkBoxShowIntrinstics.TabIndex = 2;
            this.checkBoxShowIntrinstics.Text = "Show Intrinsics";
            this.checkBoxShowIntrinstics.UseVisualStyleBackColor = true;
            this.checkBoxShowIntrinstics.CheckedChanged += new System.EventHandler(this.checkBoxShowIntrinstics_CheckedChanged);
            // 
            // buttonColumns
            // 
            this.buttonColumns.Location = new System.Drawing.Point(12, 308);
            this.buttonColumns.Name = "buttonColumns";
            this.buttonColumns.Size = new System.Drawing.Size(75, 23);
            this.buttonColumns.TabIndex = 3;
            this.buttonColumns.Text = "Columns...";
            this.buttonColumns.UseVisualStyleBackColor = true;
            this.buttonColumns.Click += new System.EventHandler(this.buttonColumns_Click);
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(93, 308);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(75, 23);
            this.buttonFilter.TabIndex = 4;
            this.buttonFilter.Text = "Filter...";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.buttonFilter_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(174, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(433, 308);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(102, 23);
            this.buttonCopyToClipboard.TabIndex = 6;
            this.buttonCopyToClipboard.Text = "Copy to Clipboard";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // buttonSaveToScoutFile
            // 
            this.buttonSaveToScoutFile.Location = new System.Drawing.Point(275, 308);
            this.buttonSaveToScoutFile.Name = "buttonSaveToScoutFile";
            this.buttonSaveToScoutFile.Size = new System.Drawing.Size(152, 23);
            this.buttonSaveToScoutFile.TabIndex = 7;
            this.buttonSaveToScoutFile.Text = "Append/Save To Scout File (.pls)";
            this.buttonSaveToScoutFile.UseVisualStyleBackColor = true;
            this.buttonSaveToScoutFile.Click += new System.EventHandler(this.buttonSaveToScoutFile_Click);
            // 
            // ScoutGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 337);
            this.Controls.Add(this.buttonSaveToScoutFile);
            this.Controls.Add(this.buttonCopyToClipboard);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.buttonColumns);
            this.Controls.Add(this.checkBoxShowIntrinstics);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ScoutGrid";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Save Scouter";
            this.Resize += new System.EventHandler(this.ScoutGrid_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxShowIntrinstics;
        private System.Windows.Forms.Button buttonColumns;
        private System.Windows.Forms.Button buttonFilter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.Button buttonSaveToScoutFile;
    }
}