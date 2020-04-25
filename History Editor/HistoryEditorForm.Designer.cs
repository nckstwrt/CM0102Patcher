namespace CM0102Patcher
{
    partial class HistoryEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryEditorForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageNationComp = new System.Windows.Forms.TabPage();
            this.dataGridViewNationComp = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.listBoxNationComps = new System.Windows.Forms.ListBox();
            this.tabPageClubComps = new System.Windows.Forms.TabPage();
            this.dataGridViewClubComp = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.listBoxClubComps = new System.Windows.Forms.ListBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxIndexFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageNationComp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNationComp)).BeginInit();
            this.tabPageClubComps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubComp)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNationComp);
            this.tabControl.Controls.Add(this.tabPageClubComps);
            this.tabControl.Location = new System.Drawing.Point(12, 65);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(754, 262);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageNationComp
            // 
            this.tabPageNationComp.Controls.Add(this.dataGridViewNationComp);
            this.tabPageNationComp.Controls.Add(this.listBoxNationComps);
            this.tabPageNationComp.Location = new System.Drawing.Point(4, 22);
            this.tabPageNationComp.Name = "tabPageNationComp";
            this.tabPageNationComp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageNationComp.Size = new System.Drawing.Size(746, 236);
            this.tabPageNationComp.TabIndex = 0;
            this.tabPageNationComp.Text = "International Comp";
            this.tabPageNationComp.UseVisualStyleBackColor = true;
            // 
            // dataGridViewNationComp
            // 
            this.dataGridViewNationComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewNationComp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5});
            this.dataGridViewNationComp.Location = new System.Drawing.Point(213, 6);
            this.dataGridViewNationComp.Name = "dataGridViewNationComp";
            this.dataGridViewNationComp.Size = new System.Drawing.Size(526, 193);
            this.dataGridViewNationComp.TabIndex = 3;
            this.dataGridViewNationComp.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComp_CellEnter);
            this.dataGridViewNationComp.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComp_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.FillWeight = 65F;
            this.dataGridViewTextBoxColumn1.HeaderText = "Season";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 65;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewTextBoxColumn2.HeaderText = "Winners";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewTextBoxColumn3.HeaderText = "Runners up";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewTextBoxColumn4.HeaderText = "Third place";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewTextBoxColumn5.HeaderText = "Hosts";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // listBoxNationComps
            // 
            this.listBoxNationComps.FormattingEnabled = true;
            this.listBoxNationComps.Location = new System.Drawing.Point(6, 6);
            this.listBoxNationComps.Name = "listBoxNationComps";
            this.listBoxNationComps.Size = new System.Drawing.Size(201, 225);
            this.listBoxNationComps.TabIndex = 0;
            this.listBoxNationComps.SelectedIndexChanged += new System.EventHandler(this.listBoxNationComps_SelectedIndexChanged);
            // 
            // tabPageClubComps
            // 
            this.tabPageClubComps.Controls.Add(this.dataGridViewClubComp);
            this.tabPageClubComps.Controls.Add(this.listBoxClubComps);
            this.tabPageClubComps.Location = new System.Drawing.Point(4, 22);
            this.tabPageClubComps.Name = "tabPageClubComps";
            this.tabPageClubComps.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageClubComps.Size = new System.Drawing.Size(746, 236);
            this.tabPageClubComps.TabIndex = 1;
            this.tabPageClubComps.Text = "Club Comp";
            this.tabPageClubComps.UseVisualStyleBackColor = true;
            // 
            // dataGridViewClubComp
            // 
            this.dataGridViewClubComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewClubComp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewComboBoxColumn1,
            this.dataGridViewComboBoxColumn2,
            this.dataGridViewComboBoxColumn3,
            this.dataGridViewComboBoxColumn4});
            this.dataGridViewClubComp.Location = new System.Drawing.Point(213, 6);
            this.dataGridViewClubComp.Name = "dataGridViewClubComp";
            this.dataGridViewClubComp.Size = new System.Drawing.Size(526, 193);
            this.dataGridViewClubComp.TabIndex = 4;
            this.dataGridViewClubComp.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComp_CellEnter);
            this.dataGridViewClubComp.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComp_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.FillWeight = 65F;
            this.dataGridViewTextBoxColumn6.HeaderText = "Season";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 65;
            // 
            // dataGridViewComboBoxColumn1
            // 
            this.dataGridViewComboBoxColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn1.HeaderText = "Winners";
            this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
            this.dataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn2
            // 
            this.dataGridViewComboBoxColumn2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn2.HeaderText = "Runners up";
            this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
            this.dataGridViewComboBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn3
            // 
            this.dataGridViewComboBoxColumn3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn3.HeaderText = "Third place";
            this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
            this.dataGridViewComboBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // dataGridViewComboBoxColumn4
            // 
            this.dataGridViewComboBoxColumn4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn4.HeaderText = "Hosts";
            this.dataGridViewComboBoxColumn4.Name = "dataGridViewComboBoxColumn4";
            this.dataGridViewComboBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // listBoxClubComps
            // 
            this.listBoxClubComps.FormattingEnabled = true;
            this.listBoxClubComps.Location = new System.Drawing.Point(6, 6);
            this.listBoxClubComps.Name = "listBoxClubComps";
            this.listBoxClubComps.Size = new System.Drawing.Size(201, 225);
            this.listBoxClubComps.TabIndex = 1;
            this.listBoxClubComps.SelectedIndexChanged += new System.EventHandler(this.listBoxClubComps_SelectedIndexChanged);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(314, 17);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 1;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxIndexFile
            // 
            this.textBoxIndexFile.Location = new System.Drawing.Point(6, 19);
            this.textBoxIndexFile.Name = "textBoxIndexFile";
            this.textBoxIndexFile.Size = new System.Drawing.Size(302, 20);
            this.textBoxIndexFile.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonLoad);
            this.groupBox1.Controls.Add(this.buttonBrowse);
            this.groupBox1.Controls.Add(this.textBoxIndexFile);
            this.groupBox1.Location = new System.Drawing.Point(12, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 54);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Index File";
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(395, 17);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 3;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(351, 334);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 4;
            this.buttonSave.Text = "Save";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // HistoryEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 369);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryEditorForm";
            this.Text = "History Editor";
            this.Load += new System.EventHandler(this.HistoryEditorForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageNationComp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNationComp)).EndInit();
            this.tabPageClubComps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubComp)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageNationComp;
        private System.Windows.Forms.TabPage tabPageClubComps;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.TextBox textBoxIndexFile;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ListBox listBoxNationComps;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ListBox listBoxClubComps;
        private System.Windows.Forms.DataGridView dataGridViewNationComp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridView dataGridViewClubComp;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn4;
    }
}