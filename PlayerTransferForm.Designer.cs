
namespace CM0102Patcher
{
    partial class PlayerTransferForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerTransferForm));
            this.textBoxIndexFile = new System.Windows.Forms.TextBox();
            this.listBoxTransfers = new System.Windows.Forms.ListBox();
            this.labelAllPlayers = new System.Windows.Forms.Label();
            this.labelAllTeams = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPlayerFilter = new System.Windows.Forms.TextBox();
            this.textBoxTeamFilter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.vScrollBarPlayers = new System.Windows.Forms.VScrollBar();
            this.vScrollBarTeams = new System.Windows.Forms.VScrollBar();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonTransfer = new System.Windows.Forms.Button();
            this.buttonApplyChanges = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonCopyAndPaste = new System.Windows.Forms.Button();
            this.buttonclear = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.listBoxTeams = new CM0102Patcher.NoScrollBarListBox();
            this.listBoxPlayers = new CM0102Patcher.NoScrollBarListBox();
            this.buttonLoan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxIndexFile
            // 
            this.textBoxIndexFile.Location = new System.Drawing.Point(12, 26);
            this.textBoxIndexFile.Name = "textBoxIndexFile";
            this.textBoxIndexFile.Size = new System.Drawing.Size(377, 20);
            this.textBoxIndexFile.TabIndex = 0;
            // 
            // listBoxTransfers
            // 
            this.listBoxTransfers.FormattingEnabled = true;
            this.listBoxTransfers.Location = new System.Drawing.Point(1130, 69);
            this.listBoxTransfers.Name = "listBoxTransfers";
            this.listBoxTransfers.Size = new System.Drawing.Size(530, 342);
            this.listBoxTransfers.TabIndex = 3;
            // 
            // labelAllPlayers
            // 
            this.labelAllPlayers.AutoSize = true;
            this.labelAllPlayers.Location = new System.Drawing.Point(13, 53);
            this.labelAllPlayers.Name = "labelAllPlayers";
            this.labelAllPlayers.Size = new System.Drawing.Size(55, 13);
            this.labelAllPlayers.TabIndex = 4;
            this.labelAllPlayers.Text = "All Players";
            // 
            // labelAllTeams
            // 
            this.labelAllTeams.AutoSize = true;
            this.labelAllTeams.Location = new System.Drawing.Point(565, 53);
            this.labelAllTeams.Name = "labelAllTeams";
            this.labelAllTeams.Size = new System.Drawing.Size(108, 13);
            this.labelAllTeams.TabIndex = 5;
            this.labelAllTeams.Text = "Team To Transfer To";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1127, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Transfers";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 426);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Player Filter";
            // 
            // textBoxPlayerFilter
            // 
            this.textBoxPlayerFilter.Location = new System.Drawing.Point(76, 423);
            this.textBoxPlayerFilter.Name = "textBoxPlayerFilter";
            this.textBoxPlayerFilter.Size = new System.Drawing.Size(406, 20);
            this.textBoxPlayerFilter.TabIndex = 8;
            this.textBoxPlayerFilter.TextChanged += new System.EventHandler(this.textBoxPlayerFilter_TextChanged);
            // 
            // textBoxTeamFilter
            // 
            this.textBoxTeamFilter.Location = new System.Drawing.Point(630, 426);
            this.textBoxTeamFilter.Name = "textBoxTeamFilter";
            this.textBoxTeamFilter.Size = new System.Drawing.Size(340, 20);
            this.textBoxTeamFilter.TabIndex = 10;
            this.textBoxTeamFilter.TextChanged += new System.EventHandler(this.textBoxTeamFilter_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(565, 429);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Team Filter";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(395, 26);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
            this.buttonBrowse.TabIndex = 11;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(109, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Select Data To Load:";
            // 
            // vScrollBarPlayers
            // 
            this.vScrollBarPlayers.Location = new System.Drawing.Point(483, 71);
            this.vScrollBarPlayers.Name = "vScrollBarPlayers";
            this.vScrollBarPlayers.Size = new System.Drawing.Size(17, 342);
            this.vScrollBarPlayers.TabIndex = 13;
            // 
            // vScrollBarTeams
            // 
            this.vScrollBarTeams.Location = new System.Drawing.Point(971, 71);
            this.vScrollBarTeams.Name = "vScrollBarTeams";
            this.vScrollBarTeams.Size = new System.Drawing.Size(17, 342);
            this.vScrollBarTeams.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(501, 221);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "----------------->";
            // 
            // buttonTransfer
            // 
            this.buttonTransfer.Location = new System.Drawing.Point(1007, 173);
            this.buttonTransfer.Name = "buttonTransfer";
            this.buttonTransfer.Size = new System.Drawing.Size(108, 23);
            this.buttonTransfer.TabIndex = 16;
            this.buttonTransfer.Text = "-- TRANSFER -->";
            this.buttonTransfer.UseVisualStyleBackColor = true;
            this.buttonTransfer.Click += new System.EventHandler(this.buttonTransfer_Click);
            // 
            // buttonApplyChanges
            // 
            this.buttonApplyChanges.Location = new System.Drawing.Point(1504, 477);
            this.buttonApplyChanges.Name = "buttonApplyChanges";
            this.buttonApplyChanges.Size = new System.Drawing.Size(156, 23);
            this.buttonApplyChanges.TabIndex = 17;
            this.buttonApplyChanges.Text = "Apply Transfer Changes";
            this.buttonApplyChanges.UseVisualStyleBackColor = true;
            this.buttonApplyChanges.Click += new System.EventHandler(this.buttonApplyChanges_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.Location = new System.Drawing.Point(1383, 424);
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.Size = new System.Drawing.Size(89, 23);
            this.buttonImport.TabIndex = 18;
            this.buttonImport.Text = "Import";
            this.buttonImport.UseVisualStyleBackColor = true;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(1573, 422);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(89, 23);
            this.buttonExport.TabIndex = 19;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // buttonCopyAndPaste
            // 
            this.buttonCopyAndPaste.Location = new System.Drawing.Point(1478, 422);
            this.buttonCopyAndPaste.Name = "buttonCopyAndPaste";
            this.buttonCopyAndPaste.Size = new System.Drawing.Size(89, 23);
            this.buttonCopyAndPaste.TabIndex = 20;
            this.buttonCopyAndPaste.Text = "Copy && Paste";
            this.buttonCopyAndPaste.UseVisualStyleBackColor = true;
            this.buttonCopyAndPaste.Click += new System.EventHandler(this.buttonCopyAndPaste_Click);
            // 
            // buttonclear
            // 
            this.buttonclear.Location = new System.Drawing.Point(1130, 424);
            this.buttonclear.Name = "buttonclear";
            this.buttonclear.Size = new System.Drawing.Size(89, 23);
            this.buttonclear.TabIndex = 21;
            this.buttonclear.Text = "Clear";
            this.buttonclear.UseVisualStyleBackColor = true;
            this.buttonclear.Click += new System.EventHandler(this.buttonclear_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(1225, 424);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(89, 23);
            this.buttonDelete.TabIndex = 22;
            this.buttonDelete.Text = "Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // listBoxTeams
            // 
            this.listBoxTeams.ExternalScrollBar = null;
            this.listBoxTeams.FormattingEnabled = true;
            this.listBoxTeams.Location = new System.Drawing.Point(568, 71);
            this.listBoxTeams.Name = "listBoxTeams";
            this.listBoxTeams.ShowScrollbar = false;
            this.listBoxTeams.Size = new System.Drawing.Size(402, 342);
            this.listBoxTeams.TabIndex = 2;
            // 
            // listBoxPlayers
            // 
            this.listBoxPlayers.ExternalScrollBar = null;
            this.listBoxPlayers.FormattingEnabled = true;
            this.listBoxPlayers.Location = new System.Drawing.Point(12, 71);
            this.listBoxPlayers.Name = "listBoxPlayers";
            this.listBoxPlayers.ShowScrollbar = false;
            this.listBoxPlayers.Size = new System.Drawing.Size(470, 342);
            this.listBoxPlayers.TabIndex = 1;
            // 
            // buttonLoan
            // 
            this.buttonLoan.Location = new System.Drawing.Point(1007, 245);
            this.buttonLoan.Name = "buttonLoan";
            this.buttonLoan.Size = new System.Drawing.Size(108, 23);
            this.buttonLoan.TabIndex = 23;
            this.buttonLoan.Text = "---- LOAN ---->";
            this.buttonLoan.UseVisualStyleBackColor = true;
            this.buttonLoan.Click += new System.EventHandler(this.buttonLoan_Click);
            // 
            // PlayerTransferForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1686, 514);
            this.Controls.Add(this.buttonLoan);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonclear);
            this.Controls.Add(this.buttonCopyAndPaste);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonApplyChanges);
            this.Controls.Add(this.buttonTransfer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.vScrollBarTeams);
            this.Controls.Add(this.vScrollBarPlayers);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxTeamFilter);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxPlayerFilter);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelAllTeams);
            this.Controls.Add(this.labelAllPlayers);
            this.Controls.Add(this.listBoxTransfers);
            this.Controls.Add(this.listBoxTeams);
            this.Controls.Add(this.listBoxPlayers);
            this.Controls.Add(this.textBoxIndexFile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PlayerTransferForm";
            this.Text = "Player Transfer Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxIndexFile;
        private NoScrollBarListBox listBoxPlayers;
        private NoScrollBarListBox listBoxTeams;
        private System.Windows.Forms.ListBox listBoxTransfers;
        private System.Windows.Forms.Label labelAllPlayers;
        private System.Windows.Forms.Label labelAllTeams;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPlayerFilter;
        private System.Windows.Forms.TextBox textBoxTeamFilter;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.VScrollBar vScrollBarPlayers;
        private System.Windows.Forms.VScrollBar vScrollBarTeams;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonTransfer;
        private System.Windows.Forms.Button buttonApplyChanges;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonCopyAndPaste;
        private System.Windows.Forms.Button buttonclear;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonLoan;
    }
}