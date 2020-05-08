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
            this.numericNationUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonNationShiftYears = new System.Windows.Forms.Button();
            this.buttonNationDeleteRow = new System.Windows.Forms.Button();
            this.dataGridViewNationComp = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.listBoxNationComps = new System.Windows.Forms.ListBox();
            this.tabPageClubComps = new System.Windows.Forms.TabPage();
            this.numericClubUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonClubShiftYears = new System.Windows.Forms.Button();
            this.buttonClubDeleteRow = new System.Windows.Forms.Button();
            this.dataGridViewClubComp = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn4 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.listBoxClubComps = new System.Windows.Forms.ListBox();
            this.tabPageStaffComp = new System.Windows.Forms.TabPage();
            this.numericStaffCompUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonStaffCompShiftYears = new System.Windows.Forms.Button();
            this.buttonStaffDeleteRow = new System.Windows.Forms.Button();
            this.dataGridViewStaffComp = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn5 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn6 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn7 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.listBoxStaffComps = new System.Windows.Forms.ListBox();
            this.tabPageStaffHistory = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxSearchStaff = new System.Windows.Forms.TextBox();
            this.numericStaffHistoryUpDown = new System.Windows.Forms.NumericUpDown();
            this.buttonStaffHistoryShiftYears = new System.Windows.Forms.Button();
            this.buttonStaffHistoryDeleteRow = new System.Windows.Forms.Button();
            this.dataGridViewStaffHistory = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn8 = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dataGridViewComboBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewComboBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.listBoxStaff = new System.Windows.Forms.ListBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxIndexFile = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonYearShifter = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageNationComp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericNationUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNationComp)).BeginInit();
            this.tabPageClubComps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericClubUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubComp)).BeginInit();
            this.tabPageStaffComp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStaffCompUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffComp)).BeginInit();
            this.tabPageStaffHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStaffHistoryUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffHistory)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageNationComp);
            this.tabControl.Controls.Add(this.tabPageClubComps);
            this.tabControl.Controls.Add(this.tabPageStaffComp);
            this.tabControl.Controls.Add(this.tabPageStaffHistory);
            this.tabControl.Location = new System.Drawing.Point(12, 65);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(754, 262);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageNationComp
            // 
            this.tabPageNationComp.Controls.Add(this.numericNationUpDown);
            this.tabPageNationComp.Controls.Add(this.buttonNationShiftYears);
            this.tabPageNationComp.Controls.Add(this.buttonNationDeleteRow);
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
            // numericNationUpDown
            // 
            this.numericNationUpDown.Location = new System.Drawing.Point(501, 207);
            this.numericNationUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericNationUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericNationUpDown.Name = "numericNationUpDown";
            this.numericNationUpDown.Size = new System.Drawing.Size(75, 20);
            this.numericNationUpDown.TabIndex = 6;
            this.numericNationUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonNationShiftYears
            // 
            this.buttonNationShiftYears.Location = new System.Drawing.Point(583, 205);
            this.buttonNationShiftYears.Name = "buttonNationShiftYears";
            this.buttonNationShiftYears.Size = new System.Drawing.Size(75, 23);
            this.buttonNationShiftYears.TabIndex = 5;
            this.buttonNationShiftYears.Text = "Shift Years";
            this.buttonNationShiftYears.UseVisualStyleBackColor = true;
            this.buttonNationShiftYears.Click += new System.EventHandler(this.buttonNationShiftYears_Click);
            // 
            // buttonNationDeleteRow
            // 
            this.buttonNationDeleteRow.Location = new System.Drawing.Point(664, 205);
            this.buttonNationDeleteRow.Name = "buttonNationDeleteRow";
            this.buttonNationDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.buttonNationDeleteRow.TabIndex = 4;
            this.buttonNationDeleteRow.Text = "Delete Row";
            this.buttonNationDeleteRow.UseVisualStyleBackColor = true;
            this.buttonNationDeleteRow.Click += new System.EventHandler(this.buttonNationDeleteRow_Click);
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
            this.tabPageClubComps.Controls.Add(this.numericClubUpDown);
            this.tabPageClubComps.Controls.Add(this.buttonClubShiftYears);
            this.tabPageClubComps.Controls.Add(this.buttonClubDeleteRow);
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
            // numericClubUpDown
            // 
            this.numericClubUpDown.Location = new System.Drawing.Point(501, 207);
            this.numericClubUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericClubUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericClubUpDown.Name = "numericClubUpDown";
            this.numericClubUpDown.Size = new System.Drawing.Size(75, 20);
            this.numericClubUpDown.TabIndex = 9;
            this.numericClubUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonClubShiftYears
            // 
            this.buttonClubShiftYears.Location = new System.Drawing.Point(583, 205);
            this.buttonClubShiftYears.Name = "buttonClubShiftYears";
            this.buttonClubShiftYears.Size = new System.Drawing.Size(75, 23);
            this.buttonClubShiftYears.TabIndex = 8;
            this.buttonClubShiftYears.Text = "Shift Years";
            this.buttonClubShiftYears.UseVisualStyleBackColor = true;
            this.buttonClubShiftYears.Click += new System.EventHandler(this.buttonClubShiftYears_Click);
            // 
            // buttonClubDeleteRow
            // 
            this.buttonClubDeleteRow.Location = new System.Drawing.Point(664, 205);
            this.buttonClubDeleteRow.Name = "buttonClubDeleteRow";
            this.buttonClubDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.buttonClubDeleteRow.TabIndex = 7;
            this.buttonClubDeleteRow.Text = "Delete Row";
            this.buttonClubDeleteRow.UseVisualStyleBackColor = true;
            this.buttonClubDeleteRow.Click += new System.EventHandler(this.buttonClubDeleteRow_Click);
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
            // tabPageStaffComp
            // 
            this.tabPageStaffComp.Controls.Add(this.numericStaffCompUpDown);
            this.tabPageStaffComp.Controls.Add(this.buttonStaffCompShiftYears);
            this.tabPageStaffComp.Controls.Add(this.buttonStaffDeleteRow);
            this.tabPageStaffComp.Controls.Add(this.dataGridViewStaffComp);
            this.tabPageStaffComp.Controls.Add(this.listBoxStaffComps);
            this.tabPageStaffComp.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaffComp.Name = "tabPageStaffComp";
            this.tabPageStaffComp.Size = new System.Drawing.Size(746, 236);
            this.tabPageStaffComp.TabIndex = 2;
            this.tabPageStaffComp.Text = "Staff Comp";
            this.tabPageStaffComp.UseVisualStyleBackColor = true;
            // 
            // numericStaffCompUpDown
            // 
            this.numericStaffCompUpDown.Location = new System.Drawing.Point(501, 207);
            this.numericStaffCompUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericStaffCompUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericStaffCompUpDown.Name = "numericStaffCompUpDown";
            this.numericStaffCompUpDown.Size = new System.Drawing.Size(75, 20);
            this.numericStaffCompUpDown.TabIndex = 11;
            this.numericStaffCompUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonStaffCompShiftYears
            // 
            this.buttonStaffCompShiftYears.Location = new System.Drawing.Point(583, 205);
            this.buttonStaffCompShiftYears.Name = "buttonStaffCompShiftYears";
            this.buttonStaffCompShiftYears.Size = new System.Drawing.Size(75, 23);
            this.buttonStaffCompShiftYears.TabIndex = 10;
            this.buttonStaffCompShiftYears.Text = "Shift Years";
            this.buttonStaffCompShiftYears.UseVisualStyleBackColor = true;
            this.buttonStaffCompShiftYears.Click += new System.EventHandler(this.buttonStaffCompShiftYears_Click);
            // 
            // buttonStaffDeleteRow
            // 
            this.buttonStaffDeleteRow.Location = new System.Drawing.Point(664, 205);
            this.buttonStaffDeleteRow.Name = "buttonStaffDeleteRow";
            this.buttonStaffDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.buttonStaffDeleteRow.TabIndex = 9;
            this.buttonStaffDeleteRow.Text = "Delete Row";
            this.buttonStaffDeleteRow.UseVisualStyleBackColor = true;
            this.buttonStaffDeleteRow.Click += new System.EventHandler(this.buttonStaffDeleteRow_Click);
            // 
            // dataGridViewStaffComp
            // 
            this.dataGridViewStaffComp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaffComp.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewComboBoxColumn5,
            this.dataGridViewComboBoxColumn6,
            this.dataGridViewComboBoxColumn7});
            this.dataGridViewStaffComp.Location = new System.Drawing.Point(213, 6);
            this.dataGridViewStaffComp.Name = "dataGridViewStaffComp";
            this.dataGridViewStaffComp.Size = new System.Drawing.Size(526, 193);
            this.dataGridViewStaffComp.TabIndex = 8;
            this.dataGridViewStaffComp.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComp_CellEnter);
            this.dataGridViewStaffComp.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComp_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 65F;
            this.dataGridViewTextBoxColumn7.HeaderText = "Season";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 65;
            // 
            // dataGridViewComboBoxColumn5
            // 
            this.dataGridViewComboBoxColumn5.FillWeight = 130F;
            this.dataGridViewComboBoxColumn5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn5.HeaderText = "Winners";
            this.dataGridViewComboBoxColumn5.Name = "dataGridViewComboBoxColumn5";
            this.dataGridViewComboBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn5.Width = 130;
            // 
            // dataGridViewComboBoxColumn6
            // 
            this.dataGridViewComboBoxColumn6.FillWeight = 130F;
            this.dataGridViewComboBoxColumn6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn6.HeaderText = "Runners up";
            this.dataGridViewComboBoxColumn6.Name = "dataGridViewComboBoxColumn6";
            this.dataGridViewComboBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn6.Width = 130;
            // 
            // dataGridViewComboBoxColumn7
            // 
            this.dataGridViewComboBoxColumn7.FillWeight = 130F;
            this.dataGridViewComboBoxColumn7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn7.HeaderText = "Third place";
            this.dataGridViewComboBoxColumn7.Name = "dataGridViewComboBoxColumn7";
            this.dataGridViewComboBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn7.Width = 130;
            // 
            // listBoxStaffComps
            // 
            this.listBoxStaffComps.FormattingEnabled = true;
            this.listBoxStaffComps.Location = new System.Drawing.Point(6, 6);
            this.listBoxStaffComps.Name = "listBoxStaffComps";
            this.listBoxStaffComps.Size = new System.Drawing.Size(201, 225);
            this.listBoxStaffComps.TabIndex = 7;
            this.listBoxStaffComps.SelectedIndexChanged += new System.EventHandler(this.listBoxStaffComps_SelectedIndexChanged);
            // 
            // tabPageStaffHistory
            // 
            this.tabPageStaffHistory.Controls.Add(this.label1);
            this.tabPageStaffHistory.Controls.Add(this.textBoxSearchStaff);
            this.tabPageStaffHistory.Controls.Add(this.numericStaffHistoryUpDown);
            this.tabPageStaffHistory.Controls.Add(this.buttonStaffHistoryShiftYears);
            this.tabPageStaffHistory.Controls.Add(this.buttonStaffHistoryDeleteRow);
            this.tabPageStaffHistory.Controls.Add(this.dataGridViewStaffHistory);
            this.tabPageStaffHistory.Controls.Add(this.listBoxStaff);
            this.tabPageStaffHistory.Location = new System.Drawing.Point(4, 22);
            this.tabPageStaffHistory.Name = "tabPageStaffHistory";
            this.tabPageStaffHistory.Size = new System.Drawing.Size(746, 236);
            this.tabPageStaffHistory.TabIndex = 3;
            this.tabPageStaffHistory.Text = "Staff History";
            this.tabPageStaffHistory.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 211);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "<--- Type player name here";
            // 
            // textBoxSearchStaff
            // 
            this.textBoxSearchStaff.Location = new System.Drawing.Point(7, 208);
            this.textBoxSearchStaff.Name = "textBoxSearchStaff";
            this.textBoxSearchStaff.Size = new System.Drawing.Size(201, 20);
            this.textBoxSearchStaff.TabIndex = 17;
            this.textBoxSearchStaff.TextChanged += new System.EventHandler(this.textBoxSearchStaff_TextChanged);
            // 
            // numericStaffHistoryUpDown
            // 
            this.numericStaffHistoryUpDown.Location = new System.Drawing.Point(502, 210);
            this.numericStaffHistoryUpDown.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericStaffHistoryUpDown.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
            this.numericStaffHistoryUpDown.Name = "numericStaffHistoryUpDown";
            this.numericStaffHistoryUpDown.Size = new System.Drawing.Size(75, 20);
            this.numericStaffHistoryUpDown.TabIndex = 16;
            this.numericStaffHistoryUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonStaffHistoryShiftYears
            // 
            this.buttonStaffHistoryShiftYears.Location = new System.Drawing.Point(584, 208);
            this.buttonStaffHistoryShiftYears.Name = "buttonStaffHistoryShiftYears";
            this.buttonStaffHistoryShiftYears.Size = new System.Drawing.Size(75, 23);
            this.buttonStaffHistoryShiftYears.TabIndex = 15;
            this.buttonStaffHistoryShiftYears.Text = "Shift Years";
            this.buttonStaffHistoryShiftYears.UseVisualStyleBackColor = true;
            this.buttonStaffHistoryShiftYears.Click += new System.EventHandler(this.buttonStaffHistoryShiftYears_Click);
            // 
            // buttonStaffHistoryDeleteRow
            // 
            this.buttonStaffHistoryDeleteRow.Location = new System.Drawing.Point(665, 208);
            this.buttonStaffHistoryDeleteRow.Name = "buttonStaffHistoryDeleteRow";
            this.buttonStaffHistoryDeleteRow.Size = new System.Drawing.Size(75, 23);
            this.buttonStaffHistoryDeleteRow.TabIndex = 14;
            this.buttonStaffHistoryDeleteRow.Text = "Delete Row";
            this.buttonStaffHistoryDeleteRow.UseVisualStyleBackColor = true;
            this.buttonStaffHistoryDeleteRow.Click += new System.EventHandler(this.buttonStaffHistoryDeleteRow_Click);
            // 
            // dataGridViewStaffHistory
            // 
            this.dataGridViewStaffHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewStaffHistory.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewComboBoxColumn8,
            this.dataGridViewComboBoxColumn9,
            this.dataGridViewComboBoxColumn10,
            this.Column1});
            this.dataGridViewStaffHistory.Location = new System.Drawing.Point(214, 6);
            this.dataGridViewStaffHistory.Name = "dataGridViewStaffHistory";
            this.dataGridViewStaffHistory.Size = new System.Drawing.Size(526, 199);
            this.dataGridViewStaffHistory.TabIndex = 13;
            this.dataGridViewStaffHistory.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewComp_CellEnter);
            this.dataGridViewStaffHistory.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewComp_CurrentCellDirtyStateChanged);
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.FillWeight = 65F;
            this.dataGridViewTextBoxColumn8.HeaderText = "Season";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 65;
            // 
            // dataGridViewComboBoxColumn8
            // 
            this.dataGridViewComboBoxColumn8.FillWeight = 150F;
            this.dataGridViewComboBoxColumn8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewComboBoxColumn8.HeaderText = "Club";
            this.dataGridViewComboBoxColumn8.Name = "dataGridViewComboBoxColumn8";
            this.dataGridViewComboBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewComboBoxColumn8.Width = 150;
            // 
            // dataGridViewComboBoxColumn9
            // 
            this.dataGridViewComboBoxColumn9.FillWeight = 80F;
            this.dataGridViewComboBoxColumn9.HeaderText = "Apps";
            this.dataGridViewComboBoxColumn9.Name = "dataGridViewComboBoxColumn9";
            this.dataGridViewComboBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn9.Width = 80;
            // 
            // dataGridViewComboBoxColumn10
            // 
            this.dataGridViewComboBoxColumn10.FillWeight = 80F;
            this.dataGridViewComboBoxColumn10.HeaderText = "Goals";
            this.dataGridViewComboBoxColumn10.Name = "dataGridViewComboBoxColumn10";
            this.dataGridViewComboBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewComboBoxColumn10.Width = 80;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 70F;
            this.Column1.HeaderText = "Loan";
            this.Column1.Name = "Column1";
            this.Column1.Width = 70;
            // 
            // listBoxStaff
            // 
            this.listBoxStaff.FormattingEnabled = true;
            this.listBoxStaff.Location = new System.Drawing.Point(7, 6);
            this.listBoxStaff.Name = "listBoxStaff";
            this.listBoxStaff.Size = new System.Drawing.Size(201, 199);
            this.listBoxStaff.TabIndex = 12;
            this.listBoxStaff.SelectedIndexChanged += new System.EventHandler(this.listBoxStaff_SelectedIndexChanged);
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonYearShifter);
            this.groupBox2.Location = new System.Drawing.Point(639, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(123, 54);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Year Shifter";
            // 
            // buttonYearShifter
            // 
            this.buttonYearShifter.Location = new System.Drawing.Point(6, 19);
            this.buttonYearShifter.Name = "buttonYearShifter";
            this.buttonYearShifter.Size = new System.Drawing.Size(107, 23);
            this.buttonYearShifter.TabIndex = 0;
            this.buttonYearShifter.Text = "Year Shifter Tool";
            this.buttonYearShifter.UseVisualStyleBackColor = true;
            this.buttonYearShifter.Click += new System.EventHandler(this.buttonYearShifter_Click);
            // 
            // HistoryEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 369);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryEditorForm";
            this.Text = "History Editor";
            this.Load += new System.EventHandler(this.HistoryEditorForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageNationComp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericNationUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewNationComp)).EndInit();
            this.tabPageClubComps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericClubUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewClubComp)).EndInit();
            this.tabPageStaffComp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericStaffCompUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffComp)).EndInit();
            this.tabPageStaffHistory.ResumeLayout(false);
            this.tabPageStaffHistory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericStaffHistoryUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewStaffHistory)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
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
        private System.Windows.Forms.NumericUpDown numericNationUpDown;
        private System.Windows.Forms.Button buttonNationShiftYears;
        private System.Windows.Forms.Button buttonNationDeleteRow;
        private System.Windows.Forms.NumericUpDown numericClubUpDown;
        private System.Windows.Forms.Button buttonClubShiftYears;
        private System.Windows.Forms.Button buttonClubDeleteRow;
        private System.Windows.Forms.TabPage tabPageStaffComp;
        private System.Windows.Forms.NumericUpDown numericStaffCompUpDown;
        private System.Windows.Forms.Button buttonStaffCompShiftYears;
        private System.Windows.Forms.Button buttonStaffDeleteRow;
        private System.Windows.Forms.DataGridView dataGridViewStaffComp;
        private System.Windows.Forms.ListBox listBoxStaffComps;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn5;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn6;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn7;
        private System.Windows.Forms.TabPage tabPageStaffHistory;
        private System.Windows.Forms.NumericUpDown numericStaffHistoryUpDown;
        private System.Windows.Forms.Button buttonStaffHistoryShiftYears;
        private System.Windows.Forms.Button buttonStaffHistoryDeleteRow;
        private System.Windows.Forms.DataGridView dataGridViewStaffHistory;
        private System.Windows.Forms.ListBox listBoxStaff;
        private System.Windows.Forms.TextBox textBoxSearchStaff;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewComboBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonYearShifter;
    }
}