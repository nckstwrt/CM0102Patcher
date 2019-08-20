namespace CM0102Patcher
{
    partial class PatcherForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatcherForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelFilename = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.labelGameStartYear = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxGameSpeed = new System.Windows.Forms.ComboBox();
            this.numericGameStartYear = new System.Windows.Forms.NumericUpDown();
            this.buttonApply = new System.Windows.Forms.Button();
            this.checkBoxEnableColouredAtts = new System.Windows.Forms.CheckBox();
            this.checkBoxDisableUnprotectedContracts = new System.Windows.Forms.CheckBox();
            this.checkBoxHideNonPublicBids = new System.Windows.Forms.CheckBox();
            this.buttonTools = new System.Windows.Forms.Button();
            this.checkBoxCDRemoval = new System.Windows.Forms.CheckBox();
            this.numericCurrencyInflation = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxDisableSplashScreen = new System.Windows.Forms.CheckBox();
            this.checkBox7Subs = new System.Windows.Forms.CheckBox();
            this.checkBoxShowStarPlayers = new System.Windows.Forms.CheckBox();
            this.checkBoxChangeStartYear = new System.Windows.Forms.CheckBox();
            this.checkBoxAllowCloseWindow = new System.Windows.Forms.CheckBox();
            this.checkBoxIdleSensitivity = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxMakeExecutablePortable = new System.Windows.Forms.CheckBox();
            this.checkBoxRestrictTactics = new System.Windows.Forms.CheckBox();
            this.comboBoxReplacementLeagues = new System.Windows.Forms.ComboBox();
            this.checkBoxReplaceWelshPremier = new System.Windows.Forms.CheckBox();
            this.checkBoxRemove3NonEULimit = new System.Windows.Forms.CheckBox();
            this.checkBoxManageAnyTeam = new System.Windows.Forms.CheckBox();
            this.checkBoxNewRegenCode = new System.Windows.Forms.CheckBox();
            this.checkBoxJobsAbroadBoost = new System.Windows.Forms.CheckBox();
            this.checkBoxChangeResolution1280s800 = new System.Windows.Forms.CheckBox();
            this.checkBoxRegenFixes = new System.Windows.Forms.CheckBox();
            this.checkBoxForceLoadAllPlayers = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdateNames = new System.Windows.Forms.CheckBox();
            this.checkBoxRemoveCDChecks = new System.Windows.Forms.CheckBox();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonRestore = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericGameStartYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrencyInflation)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelFilename);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(9, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(348, 45);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Executable to patch";
            // 
            // labelFilename
            // 
            this.labelFilename.AutoEllipsis = true;
            this.labelFilename.Location = new System.Drawing.Point(7, 20);
            this.labelFilename.Name = "labelFilename";
            this.labelFilename.Size = new System.Drawing.Size(329, 18);
            this.labelFilename.TabIndex = 0;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBrowse.Location = new System.Drawing.Point(363, 18);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(93, 25);
            this.buttonBrowse.TabIndex = 0;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // labelGameStartYear
            // 
            this.labelGameStartYear.AutoSize = true;
            this.labelGameStartYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGameStartYear.Location = new System.Drawing.Point(12, 97);
            this.labelGameStartYear.Name = "labelGameStartYear";
            this.labelGameStartYear.Size = new System.Drawing.Size(103, 16);
            this.labelGameStartYear.TabIndex = 1;
            this.labelGameStartYear.Text = "Game start year";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(232, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Game speed";
            // 
            // comboBoxGameSpeed
            // 
            this.comboBoxGameSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxGameSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxGameSpeed.FormattingEnabled = true;
            this.comboBoxGameSpeed.Location = new System.Drawing.Point(345, 61);
            this.comboBoxGameSpeed.Name = "comboBoxGameSpeed";
            this.comboBoxGameSpeed.Size = new System.Drawing.Size(96, 24);
            this.comboBoxGameSpeed.TabIndex = 4;
            // 
            // numericGameStartYear
            // 
            this.numericGameStartYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericGameStartYear.Location = new System.Drawing.Point(123, 95);
            this.numericGameStartYear.Maximum = new decimal(new int[] {
            2100,
            0,
            0,
            0});
            this.numericGameStartYear.Minimum = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.numericGameStartYear.Name = "numericGameStartYear";
            this.numericGameStartYear.Size = new System.Drawing.Size(89, 22);
            this.numericGameStartYear.TabIndex = 6;
            this.numericGameStartYear.Value = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            // 
            // buttonApply
            // 
            this.buttonApply.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonApply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApply.Location = new System.Drawing.Point(381, 445);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 31);
            this.buttonApply.TabIndex = 7;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // checkBoxEnableColouredAtts
            // 
            this.checkBoxEnableColouredAtts.AutoSize = true;
            this.checkBoxEnableColouredAtts.Checked = true;
            this.checkBoxEnableColouredAtts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxEnableColouredAtts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxEnableColouredAtts.Location = new System.Drawing.Point(16, 147);
            this.checkBoxEnableColouredAtts.Name = "checkBoxEnableColouredAtts";
            this.checkBoxEnableColouredAtts.Size = new System.Drawing.Size(183, 20);
            this.checkBoxEnableColouredAtts.TabIndex = 8;
            this.checkBoxEnableColouredAtts.Text = "Enable coloured attributes";
            this.toolTips.SetToolTip(this.checkBoxEnableColouredAtts, "Enables coloured attributes for all players (dark red = 20, etc)");
            this.checkBoxEnableColouredAtts.UseVisualStyleBackColor = true;
            // 
            // checkBoxDisableUnprotectedContracts
            // 
            this.checkBoxDisableUnprotectedContracts.AutoSize = true;
            this.checkBoxDisableUnprotectedContracts.Checked = true;
            this.checkBoxDisableUnprotectedContracts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisableUnprotectedContracts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDisableUnprotectedContracts.Location = new System.Drawing.Point(228, 147);
            this.checkBoxDisableUnprotectedContracts.Name = "checkBoxDisableUnprotectedContracts";
            this.checkBoxDisableUnprotectedContracts.Size = new System.Drawing.Size(205, 20);
            this.checkBoxDisableUnprotectedContracts.TabIndex = 9;
            this.checkBoxDisableUnprotectedContracts.Text = "Disable unprotected contracts";
            this.checkBoxDisableUnprotectedContracts.UseVisualStyleBackColor = true;
            // 
            // checkBoxHideNonPublicBids
            // 
            this.checkBoxHideNonPublicBids.AutoSize = true;
            this.checkBoxHideNonPublicBids.Checked = true;
            this.checkBoxHideNonPublicBids.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxHideNonPublicBids.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxHideNonPublicBids.Location = new System.Drawing.Point(16, 199);
            this.checkBoxHideNonPublicBids.Name = "checkBoxHideNonPublicBids";
            this.checkBoxHideNonPublicBids.Size = new System.Drawing.Size(150, 20);
            this.checkBoxHideNonPublicBids.TabIndex = 10;
            this.checkBoxHideNonPublicBids.Text = "Hide non-public bids";
            this.checkBoxHideNonPublicBids.UseVisualStyleBackColor = true;
            // 
            // buttonTools
            // 
            this.buttonTools.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTools.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTools.Location = new System.Drawing.Point(9, 445);
            this.buttonTools.Name = "buttonTools";
            this.buttonTools.Size = new System.Drawing.Size(75, 31);
            this.buttonTools.TabIndex = 11;
            this.buttonTools.Text = "Tools...";
            this.buttonTools.UseVisualStyleBackColor = true;
            this.buttonTools.Click += new System.EventHandler(this.buttonTools_Click);
            // 
            // checkBoxCDRemoval
            // 
            this.checkBoxCDRemoval.AutoSize = true;
            this.checkBoxCDRemoval.Checked = true;
            this.checkBoxCDRemoval.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCDRemoval.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxCDRemoval.Location = new System.Drawing.Point(228, 173);
            this.checkBoxCDRemoval.Name = "checkBoxCDRemoval";
            this.checkBoxCDRemoval.Size = new System.Drawing.Size(208, 20);
            this.checkBoxCDRemoval.TabIndex = 12;
            this.checkBoxCDRemoval.Text = "Disable CD removal message";
            this.toolTips.SetToolTip(this.checkBoxCDRemoval, "Stops the \"You can now remove the CD\" messages from appearing");
            this.checkBoxCDRemoval.UseVisualStyleBackColor = true;
            // 
            // numericCurrencyInflation
            // 
            this.numericCurrencyInflation.DecimalPlaces = 2;
            this.numericCurrencyInflation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericCurrencyInflation.Increment = new decimal(new int[] {
            25,
            0,
            0,
            131072});
            this.numericCurrencyInflation.Location = new System.Drawing.Point(345, 95);
            this.numericCurrencyInflation.Name = "numericCurrencyInflation";
            this.numericCurrencyInflation.Size = new System.Drawing.Size(96, 22);
            this.numericCurrencyInflation.TabIndex = 15;
            this.numericCurrencyInflation.Value = new decimal(new int[] {
            25,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(233, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Currency Inflation";
            // 
            // checkBoxDisableSplashScreen
            // 
            this.checkBoxDisableSplashScreen.AutoSize = true;
            this.checkBoxDisableSplashScreen.Checked = true;
            this.checkBoxDisableSplashScreen.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDisableSplashScreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxDisableSplashScreen.Location = new System.Drawing.Point(228, 199);
            this.checkBoxDisableSplashScreen.Name = "checkBoxDisableSplashScreen";
            this.checkBoxDisableSplashScreen.Size = new System.Drawing.Size(161, 20);
            this.checkBoxDisableSplashScreen.TabIndex = 16;
            this.checkBoxDisableSplashScreen.Text = "Disable splash screen";
            this.toolTips.SetToolTip(this.checkBoxDisableSplashScreen, "Stops the Sigames screen from appearing every time you load the executable");
            this.checkBoxDisableSplashScreen.UseVisualStyleBackColor = true;
            // 
            // checkBox7Subs
            // 
            this.checkBox7Subs.AutoSize = true;
            this.checkBox7Subs.Checked = true;
            this.checkBox7Subs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox7Subs.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox7Subs.Location = new System.Drawing.Point(16, 225);
            this.checkBox7Subs.Name = "checkBox7Subs";
            this.checkBox7Subs.Size = new System.Drawing.Size(146, 20);
            this.checkBox7Subs.TabIndex = 13;
            this.checkBox7Subs.Text = "Enable 7 substitutes";
            this.toolTips.SetToolTip(this.checkBox7Subs, "Enables 7 substitutes for England, Italy and France");
            this.checkBox7Subs.UseVisualStyleBackColor = true;
            // 
            // checkBoxShowStarPlayers
            // 
            this.checkBoxShowStarPlayers.AutoSize = true;
            this.checkBoxShowStarPlayers.Checked = true;
            this.checkBoxShowStarPlayers.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxShowStarPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxShowStarPlayers.Location = new System.Drawing.Point(16, 251);
            this.checkBoxShowStarPlayers.Name = "checkBoxShowStarPlayers";
            this.checkBoxShowStarPlayers.Size = new System.Drawing.Size(133, 20);
            this.checkBoxShowStarPlayers.TabIndex = 17;
            this.checkBoxShowStarPlayers.Text = "Show star players";
            this.toolTips.SetToolTip(this.checkBoxShowStarPlayers, "Adds a little star to your top 3 players when looking at your team");
            this.checkBoxShowStarPlayers.UseVisualStyleBackColor = true;
            // 
            // checkBoxChangeStartYear
            // 
            this.checkBoxChangeStartYear.AutoSize = true;
            this.checkBoxChangeStartYear.Checked = true;
            this.checkBoxChangeStartYear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxChangeStartYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxChangeStartYear.Location = new System.Drawing.Point(16, 61);
            this.checkBoxChangeStartYear.Name = "checkBoxChangeStartYear";
            this.checkBoxChangeStartYear.Size = new System.Drawing.Size(132, 20);
            this.checkBoxChangeStartYear.TabIndex = 18;
            this.checkBoxChangeStartYear.Text = "Change start year";
            this.checkBoxChangeStartYear.UseVisualStyleBackColor = true;
            this.checkBoxChangeStartYear.CheckedChanged += new System.EventHandler(this.checkBoxChangeStartYear_CheckedChanged);
            // 
            // checkBoxAllowCloseWindow
            // 
            this.checkBoxAllowCloseWindow.AutoSize = true;
            this.checkBoxAllowCloseWindow.Checked = true;
            this.checkBoxAllowCloseWindow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAllowCloseWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxAllowCloseWindow.Location = new System.Drawing.Point(228, 225);
            this.checkBoxAllowCloseWindow.Name = "checkBoxAllowCloseWindow";
            this.checkBoxAllowCloseWindow.Size = new System.Drawing.Size(190, 20);
            this.checkBoxAllowCloseWindow.TabIndex = 19;
            this.checkBoxAllowCloseWindow.Text = "Allow closing game window";
            this.checkBoxAllowCloseWindow.UseVisualStyleBackColor = true;
            // 
            // checkBoxIdleSensitivity
            // 
            this.checkBoxIdleSensitivity.AutoSize = true;
            this.checkBoxIdleSensitivity.Checked = true;
            this.checkBoxIdleSensitivity.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIdleSensitivity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxIdleSensitivity.Location = new System.Drawing.Point(16, 173);
            this.checkBoxIdleSensitivity.Name = "checkBoxIdleSensitivity";
            this.checkBoxIdleSensitivity.Size = new System.Drawing.Size(156, 20);
            this.checkBoxIdleSensitivity.TabIndex = 20;
            this.checkBoxIdleSensitivity.Text = "Enable idle sensitivity";
            this.toolTips.SetToolTip(this.checkBoxIdleSensitivity, "Stops the CPU from using 100% when the game is idle and not processing");
            this.checkBoxIdleSensitivity.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(9, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 81);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBoxMakeExecutablePortable);
            this.groupBox3.Controls.Add(this.checkBoxRestrictTactics);
            this.groupBox3.Controls.Add(this.comboBoxReplacementLeagues);
            this.groupBox3.Controls.Add(this.checkBoxReplaceWelshPremier);
            this.groupBox3.Controls.Add(this.checkBoxRemove3NonEULimit);
            this.groupBox3.Controls.Add(this.checkBoxManageAnyTeam);
            this.groupBox3.Controls.Add(this.checkBoxNewRegenCode);
            this.groupBox3.Controls.Add(this.checkBoxJobsAbroadBoost);
            this.groupBox3.Controls.Add(this.checkBoxChangeResolution1280s800);
            this.groupBox3.Controls.Add(this.checkBoxRegenFixes);
            this.groupBox3.Controls.Add(this.checkBoxForceLoadAllPlayers);
            this.groupBox3.Controls.Add(this.checkBoxUpdateNames);
            this.groupBox3.Location = new System.Drawing.Point(9, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(447, 304);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // checkBoxMakeExecutablePortable
            // 
            this.checkBoxMakeExecutablePortable.AutoSize = true;
            this.checkBoxMakeExecutablePortable.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxMakeExecutablePortable.Location = new System.Drawing.Point(7, 276);
            this.checkBoxMakeExecutablePortable.Name = "checkBoxMakeExecutablePortable";
            this.checkBoxMakeExecutablePortable.Size = new System.Drawing.Size(183, 20);
            this.checkBoxMakeExecutablePortable.TabIndex = 34;
            this.checkBoxMakeExecutablePortable.Text = "Make executable portable";
            this.toolTips.SetToolTip(this.checkBoxMakeExecutablePortable, "This attempts to remove the need for XP compatibility and running as Administrato" +
        "r. So you can run the exe from anywhere.");
            this.checkBoxMakeExecutablePortable.UseVisualStyleBackColor = true;
            // 
            // checkBoxRestrictTactics
            // 
            this.checkBoxRestrictTactics.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.checkBoxRestrictTactics.AutoSize = true;
            this.checkBoxRestrictTactics.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRestrictTactics.Location = new System.Drawing.Point(219, 250);
            this.checkBoxRestrictTactics.Name = "checkBoxRestrictTactics";
            this.checkBoxRestrictTactics.Size = new System.Drawing.Size(218, 20);
            this.checkBoxRestrictTactics.TabIndex = 33;
            this.checkBoxRestrictTactics.Text = "Restrict player tactics + scouters";
            this.toolTips.SetToolTip(this.checkBoxRestrictTactics, "This stops you from loading tactics and from adjusting WibWob. It also stops your" +
        " save games from loading into CMScout and other scouters. Keeps you honest! :)");
            this.checkBoxRestrictTactics.UseVisualStyleBackColor = true;
            // 
            // comboBoxReplacementLeagues
            // 
            this.comboBoxReplacementLeagues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxReplacementLeagues.FormattingEnabled = true;
            this.comboBoxReplacementLeagues.Location = new System.Drawing.Point(219, 197);
            this.comboBoxReplacementLeagues.Name = "comboBoxReplacementLeagues";
            this.comboBoxReplacementLeagues.Size = new System.Drawing.Size(222, 21);
            this.comboBoxReplacementLeagues.TabIndex = 32;
            // 
            // checkBoxReplaceWelshPremier
            // 
            this.checkBoxReplaceWelshPremier.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.checkBoxReplaceWelshPremier.AutoSize = true;
            this.checkBoxReplaceWelshPremier.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxReplaceWelshPremier.Location = new System.Drawing.Point(7, 198);
            this.checkBoxReplaceWelshPremier.Name = "checkBoxReplaceWelshPremier";
            this.checkBoxReplaceWelshPremier.Size = new System.Drawing.Size(197, 20);
            this.checkBoxReplaceWelshPremier.TabIndex = 31;
            this.checkBoxReplaceWelshPremier.Text = "Replace Welsh premier with:";
            this.toolTips.SetToolTip(this.checkBoxReplaceWelshPremier, "Replaces the Welsh League with the Northern Premier League");
            this.checkBoxReplaceWelshPremier.UseVisualStyleBackColor = true;
            this.checkBoxReplaceWelshPremier.CheckedChanged += new System.EventHandler(this.checkBoxAddNorthernLeague_CheckedChanged);
            // 
            // checkBoxRemove3NonEULimit
            // 
            this.checkBoxRemove3NonEULimit.AutoSize = true;
            this.checkBoxRemove3NonEULimit.Checked = true;
            this.checkBoxRemove3NonEULimit.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRemove3NonEULimit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemove3NonEULimit.Location = new System.Drawing.Point(219, 146);
            this.checkBoxRemove3NonEULimit.Name = "checkBoxRemove3NonEULimit";
            this.checkBoxRemove3NonEULimit.Size = new System.Drawing.Size(200, 20);
            this.checkBoxRemove3NonEULimit.TabIndex = 30;
            this.checkBoxRemove3NonEULimit.Text = "Remove 3 foreign player limit";
            this.toolTips.SetToolTip(this.checkBoxRemove3NonEULimit, "Removes the 3 non-EU player limit from England, Denmark and Sweden");
            this.checkBoxRemove3NonEULimit.UseVisualStyleBackColor = true;
            // 
            // checkBoxManageAnyTeam
            // 
            this.checkBoxManageAnyTeam.AutoSize = true;
            this.checkBoxManageAnyTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxManageAnyTeam.Location = new System.Drawing.Point(7, 250);
            this.checkBoxManageAnyTeam.Name = "checkBoxManageAnyTeam";
            this.checkBoxManageAnyTeam.Size = new System.Drawing.Size(135, 20);
            this.checkBoxManageAnyTeam.TabIndex = 29;
            this.checkBoxManageAnyTeam.Text = "Manage any team";
            this.toolTips.SetToolTip(this.checkBoxManageAnyTeam, "Allows human managers to apply for jobs at clubs in inactive leagues. The Apply b" +
        "utton will now always be displayed on a club\'s screen.");
            this.checkBoxManageAnyTeam.UseVisualStyleBackColor = true;
            // 
            // checkBoxNewRegenCode
            // 
            this.checkBoxNewRegenCode.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.checkBoxNewRegenCode.AutoSize = true;
            this.checkBoxNewRegenCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxNewRegenCode.Location = new System.Drawing.Point(219, 224);
            this.checkBoxNewRegenCode.Name = "checkBoxNewRegenCode";
            this.checkBoxNewRegenCode.Size = new System.Drawing.Size(180, 20);
            this.checkBoxNewRegenCode.TabIndex = 27;
            this.checkBoxNewRegenCode.Text = "Add Tapani\'s regen code";
            this.toolTips.SetToolTip(this.checkBoxNewRegenCode, "Tapani\'s Regen code ensures Regens do not get ever increasing Positioning ability" +
        ".\r\nPreferred by those that play long games (5+ years)");
            this.checkBoxNewRegenCode.UseVisualStyleBackColor = true;
            // 
            // checkBoxJobsAbroadBoost
            // 
            this.checkBoxJobsAbroadBoost.AutoSize = true;
            this.checkBoxJobsAbroadBoost.Checked = true;
            this.checkBoxJobsAbroadBoost.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxJobsAbroadBoost.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxJobsAbroadBoost.Location = new System.Drawing.Point(7, 146);
            this.checkBoxJobsAbroadBoost.Name = "checkBoxJobsAbroadBoost";
            this.checkBoxJobsAbroadBoost.Size = new System.Drawing.Size(183, 20);
            this.checkBoxJobsAbroadBoost.TabIndex = 26;
            this.checkBoxJobsAbroadBoost.Text = "Obtain jobs abroad easier";
            this.toolTips.SetToolTip(this.checkBoxJobsAbroadBoost, "Changes to allow managers to obtain jobs abroad easier");
            this.checkBoxJobsAbroadBoost.UseVisualStyleBackColor = true;
            // 
            // checkBoxChangeResolution1280s800
            // 
            this.checkBoxChangeResolution1280s800.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.checkBoxChangeResolution1280s800.AutoSize = true;
            this.checkBoxChangeResolution1280s800.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxChangeResolution1280s800.Location = new System.Drawing.Point(7, 172);
            this.checkBoxChangeResolution1280s800.Name = "checkBoxChangeResolution1280s800";
            this.checkBoxChangeResolution1280s800.Size = new System.Drawing.Size(207, 20);
            this.checkBoxChangeResolution1280s800.TabIndex = 24;
            this.checkBoxChangeResolution1280s800.Text = "Change resolution to 1280x800";
            this.checkBoxChangeResolution1280s800.UseVisualStyleBackColor = true;
            // 
            // checkBoxRegenFixes
            // 
            this.checkBoxRegenFixes.AutoSize = true;
            this.checkBoxRegenFixes.Checked = true;
            this.checkBoxRegenFixes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRegenFixes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRegenFixes.Location = new System.Drawing.Point(219, 120);
            this.checkBoxRegenFixes.Name = "checkBoxRegenFixes";
            this.checkBoxRegenFixes.Size = new System.Drawing.Size(98, 20);
            this.checkBoxRegenFixes.TabIndex = 23;
            this.checkBoxRegenFixes.Text = "Regen fixes";
            this.toolTips.SetToolTip(this.checkBoxRegenFixes, "Forces only teams with less than 45 players to get regens. Also fixes the issue o" +
        "f older regens to small nations.");
            this.checkBoxRegenFixes.UseVisualStyleBackColor = true;
            // 
            // checkBoxForceLoadAllPlayers
            // 
            this.checkBoxForceLoadAllPlayers.AutoSize = true;
            this.checkBoxForceLoadAllPlayers.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxForceLoadAllPlayers.Location = new System.Drawing.Point(7, 224);
            this.checkBoxForceLoadAllPlayers.Name = "checkBoxForceLoadAllPlayers";
            this.checkBoxForceLoadAllPlayers.Size = new System.Drawing.Size(157, 20);
            this.checkBoxForceLoadAllPlayers.TabIndex = 23;
            this.checkBoxForceLoadAllPlayers.Text = "Force load all players";
            this.toolTips.SetToolTip(this.checkBoxForceLoadAllPlayers, resources.GetString("checkBoxForceLoadAllPlayers.ToolTip"));
            this.checkBoxForceLoadAllPlayers.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpdateNames
            // 
            this.checkBoxUpdateNames.AccessibleRole = System.Windows.Forms.AccessibleRole.Cursor;
            this.checkBoxUpdateNames.AutoSize = true;
            this.checkBoxUpdateNames.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUpdateNames.Location = new System.Drawing.Point(219, 172);
            this.checkBoxUpdateNames.Name = "checkBoxUpdateNames";
            this.checkBoxUpdateNames.Size = new System.Drawing.Size(227, 20);
            this.checkBoxUpdateNames.TabIndex = 28;
            this.checkBoxUpdateNames.Text = "Update names + transfer windows";
            this.toolTips.SetToolTip(this.checkBoxUpdateNames, "Update some older names:\r\ni.e.\r\nHolland -> Netherlands\r\nEuropean Champions Cup ->" +
        " UEFA Champions League\r\netc\r\n\r\nTransfer windows will also be updated\r\n\r\nCURRENTL" +
        "Y UNFINISHED!!!");
            this.checkBoxUpdateNames.UseVisualStyleBackColor = true;
            // 
            // checkBoxRemoveCDChecks
            // 
            this.checkBoxRemoveCDChecks.AutoSize = true;
            this.checkBoxRemoveCDChecks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRemoveCDChecks.Location = new System.Drawing.Point(294, 451);
            this.checkBoxRemoveCDChecks.Name = "checkBoxRemoveCDChecks";
            this.checkBoxRemoveCDChecks.Size = new System.Drawing.Size(69, 20);
            this.checkBoxRemoveCDChecks.TabIndex = 23;
            this.checkBoxRemoveCDChecks.Text = "NO CD";
            this.checkBoxRemoveCDChecks.UseVisualStyleBackColor = true;
            this.checkBoxRemoveCDChecks.Visible = false;
            // 
            // buttonSave
            // 
            this.buttonSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSave.Location = new System.Drawing.Point(135, 445);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(66, 31);
            this.buttonSave.TabIndex = 26;
            this.buttonSave.Text = "Save";
            this.toolTips.SetToolTip(this.buttonSave, "Saves a restore point of the Data, Pictures and cm0102.exe to restore later if re" +
        "quired");
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonRestore
            // 
            this.buttonRestore.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRestore.Location = new System.Drawing.Point(211, 445);
            this.buttonRestore.Name = "buttonRestore";
            this.buttonRestore.Size = new System.Drawing.Size(66, 31);
            this.buttonRestore.TabIndex = 27;
            this.buttonRestore.Text = "Restore";
            this.toolTips.SetToolTip(this.buttonRestore, "Restores a previously saved restore point to replace the Data, Pictures and cm010" +
        "2.exe");
            this.buttonRestore.UseVisualStyleBackColor = true;
            this.buttonRestore.Click += new System.EventHandler(this.buttonRestore_Click);
            // 
            // buttonAbout
            // 
            this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbout.Location = new System.Drawing.Point(95, 445);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(30, 31);
            this.buttonAbout.TabIndex = 23;
            this.buttonAbout.Text = "?";
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            // 
            // PatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(465, 484);
            this.Controls.Add(this.buttonRestore);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.checkBoxIdleSensitivity);
            this.Controls.Add(this.checkBoxAllowCloseWindow);
            this.Controls.Add(this.checkBoxChangeStartYear);
            this.Controls.Add(this.checkBoxShowStarPlayers);
            this.Controls.Add(this.checkBoxDisableSplashScreen);
            this.Controls.Add(this.numericCurrencyInflation);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.checkBoxRemoveCDChecks);
            this.Controls.Add(this.checkBox7Subs);
            this.Controls.Add(this.checkBoxCDRemoval);
            this.Controls.Add(this.buttonTools);
            this.Controls.Add(this.checkBoxHideNonPublicBids);
            this.Controls.Add(this.checkBoxDisableUnprotectedContracts);
            this.Controls.Add(this.checkBoxEnableColouredAtts);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.numericGameStartYear);
            this.Controls.Add(this.comboBoxGameSpeed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelGameStartYear);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "PatcherForm";
            this.Text = "Nick\'s CM0102Patcher v1.17";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PatcherForm_KeyPress);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericGameStartYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericCurrencyInflation)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labelFilename;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label labelGameStartYear;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxGameSpeed;
        private System.Windows.Forms.NumericUpDown numericGameStartYear;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.CheckBox checkBoxEnableColouredAtts;
        private System.Windows.Forms.CheckBox checkBoxDisableUnprotectedContracts;
        private System.Windows.Forms.CheckBox checkBoxHideNonPublicBids;
        private System.Windows.Forms.Button buttonTools;
        private System.Windows.Forms.CheckBox checkBoxCDRemoval;
        private System.Windows.Forms.NumericUpDown numericCurrencyInflation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxDisableSplashScreen;
        private System.Windows.Forms.CheckBox checkBox7Subs;
        private System.Windows.Forms.CheckBox checkBoxShowStarPlayers;
        private System.Windows.Forms.CheckBox checkBoxChangeStartYear;
        private System.Windows.Forms.CheckBox checkBoxAllowCloseWindow;
        private System.Windows.Forms.CheckBox checkBoxIdleSensitivity;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxRemoveCDChecks;
        private System.Windows.Forms.CheckBox checkBoxForceLoadAllPlayers;
        private System.Windows.Forms.CheckBox checkBoxRegenFixes;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.CheckBox checkBoxChangeResolution1280s800;
        private System.Windows.Forms.CheckBox checkBoxJobsAbroadBoost;
        private System.Windows.Forms.CheckBox checkBoxNewRegenCode;
        private System.Windows.Forms.CheckBox checkBoxUpdateNames;
        private System.Windows.Forms.CheckBox checkBoxManageAnyTeam;
        private System.Windows.Forms.CheckBox checkBoxRemove3NonEULimit;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonRestore;
        private System.Windows.Forms.CheckBox checkBoxReplaceWelshPremier;
        private System.Windows.Forms.ComboBox comboBoxReplacementLeagues;
        private System.Windows.Forms.CheckBox checkBoxRestrictTactics;
        private System.Windows.Forms.CheckBox checkBoxMakeExecutablePortable;
    }
}

