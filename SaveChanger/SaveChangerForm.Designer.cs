namespace CM0102Patcher
{
    partial class SaveChangerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveChangerForm));
            this.buttonInputSelectFile = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOutputSelectFile = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxSaveCompressed = new System.Windows.Forms.CheckBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.checkBoxCapReputation = new System.Windows.Forms.CheckBox();
            this.checkBoxContractStartDates = new System.Windows.Forms.CheckBox();
            this.checkBoxAddSuperStars = new System.Windows.Forms.CheckBox();
            this.checkBoxLowerStats = new System.Windows.Forms.CheckBox();
            this.checkBoxReduceCrazyWages = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInputSelectFile
            // 
            this.buttonInputSelectFile.Location = new System.Drawing.Point(300, 20);
            this.buttonInputSelectFile.Name = "buttonInputSelectFile";
            this.buttonInputSelectFile.Size = new System.Drawing.Size(95, 22);
            this.buttonInputSelectFile.TabIndex = 1;
            this.buttonInputSelectFile.Text = "Select File...";
            this.buttonInputSelectFile.UseVisualStyleBackColor = true;
            this.buttonInputSelectFile.Click += new System.EventHandler(this.buttonInputSelectFile_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(7, 21);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(287, 20);
            this.textBoxInput.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonInputSelectFile);
            this.groupBox1.Controls.Add(this.textBoxInput);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(406, 56);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Save File";
            // 
            // buttonOutputSelectFile
            // 
            this.buttonOutputSelectFile.Location = new System.Drawing.Point(300, 20);
            this.buttonOutputSelectFile.Name = "buttonOutputSelectFile";
            this.buttonOutputSelectFile.Size = new System.Drawing.Size(95, 22);
            this.buttonOutputSelectFile.TabIndex = 1;
            this.buttonOutputSelectFile.Text = "Select File...";
            this.buttonOutputSelectFile.UseVisualStyleBackColor = true;
            this.buttonOutputSelectFile.Click += new System.EventHandler(this.buttonOutputSelectFile_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(7, 21);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(287, 20);
            this.textBoxOutput.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxSaveCompressed);
            this.groupBox2.Controls.Add(this.buttonOutputSelectFile);
            this.groupBox2.Controls.Add(this.textBoxOutput);
            this.groupBox2.Location = new System.Drawing.Point(12, 182);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(406, 70);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output Save File";
            // 
            // checkBoxSaveCompressed
            // 
            this.checkBoxSaveCompressed.AutoSize = true;
            this.checkBoxSaveCompressed.Checked = true;
            this.checkBoxSaveCompressed.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveCompressed.Location = new System.Drawing.Point(7, 47);
            this.checkBoxSaveCompressed.Name = "checkBoxSaveCompressed";
            this.checkBoxSaveCompressed.Size = new System.Drawing.Size(112, 17);
            this.checkBoxSaveCompressed.TabIndex = 2;
            this.checkBoxSaveCompressed.Text = "Save Compressed";
            this.checkBoxSaveCompressed.UseVisualStyleBackColor = true;
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(180, 262);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 26);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // groupBox
            // 
            this.groupBox.Controls.Add(this.checkBoxReduceCrazyWages);
            this.groupBox.Controls.Add(this.checkBoxCapReputation);
            this.groupBox.Controls.Add(this.checkBoxContractStartDates);
            this.groupBox.Controls.Add(this.checkBoxAddSuperStars);
            this.groupBox.Controls.Add(this.checkBoxLowerStats);
            this.groupBox.Location = new System.Drawing.Point(12, 75);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(406, 101);
            this.groupBox.TabIndex = 4;
            this.groupBox.TabStop = false;
            this.groupBox.Text = "Options";
            // 
            // checkBoxCapReputation
            // 
            this.checkBoxCapReputation.AutoSize = true;
            this.checkBoxCapReputation.Location = new System.Drawing.Point(218, 53);
            this.checkBoxCapReputation.Name = "checkBoxCapReputation";
            this.checkBoxCapReputation.Size = new System.Drawing.Size(176, 17);
            this.checkBoxCapReputation.TabIndex = 3;
            this.checkBoxCapReputation.Text = "Cap Current Reputation to 5000";
            this.checkBoxCapReputation.UseVisualStyleBackColor = true;
            // 
            // checkBoxContractStartDates
            // 
            this.checkBoxContractStartDates.AutoSize = true;
            this.checkBoxContractStartDates.Location = new System.Drawing.Point(7, 53);
            this.checkBoxContractStartDates.Name = "checkBoxContractStartDates";
            this.checkBoxContractStartDates.Size = new System.Drawing.Size(203, 17);
            this.checkBoxContractStartDates.TabIndex = 2;
            this.checkBoxContractStartDates.Text = "Set Contract Start Dates Back 1 Year";
            this.checkBoxContractStartDates.UseVisualStyleBackColor = true;
            // 
            // checkBoxAddSuperStars
            // 
            this.checkBoxAddSuperStars.AutoSize = true;
            this.checkBoxAddSuperStars.Location = new System.Drawing.Point(218, 30);
            this.checkBoxAddSuperStars.Name = "checkBoxAddSuperStars";
            this.checkBoxAddSuperStars.Size = new System.Drawing.Size(179, 17);
            this.checkBoxAddSuperStars.TabIndex = 1;
            this.checkBoxAddSuperStars.Text = "Add Some CM0102 Super Stars!";
            this.checkBoxAddSuperStars.UseVisualStyleBackColor = true;
            // 
            // checkBoxLowerStats
            // 
            this.checkBoxLowerStats.AutoSize = true;
            this.checkBoxLowerStats.Location = new System.Drawing.Point(7, 30);
            this.checkBoxLowerStats.Name = "checkBoxLowerStats";
            this.checkBoxLowerStats.Size = new System.Drawing.Size(176, 17);
            this.checkBoxLowerStats.TabIndex = 0;
            this.checkBoxLowerStats.Text = "Adjust Key Regen Stats By 15%";
            this.checkBoxLowerStats.UseVisualStyleBackColor = true;
            // 
            // checkBoxReduceCrazyWages
            // 
            this.checkBoxReduceCrazyWages.AutoSize = true;
            this.checkBoxReduceCrazyWages.Location = new System.Drawing.Point(7, 76);
            this.checkBoxReduceCrazyWages.Name = "checkBoxReduceCrazyWages";
            this.checkBoxReduceCrazyWages.Size = new System.Drawing.Size(130, 17);
            this.checkBoxReduceCrazyWages.TabIndex = 4;
            this.checkBoxReduceCrazyWages.Text = "Reduce Crazy Wages";
            this.checkBoxReduceCrazyWages.UseVisualStyleBackColor = true;
            // 
            // SaveChangerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(430, 295);
            this.Controls.Add(this.groupBox);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveChangerForm";
            this.Text = "Save Changer Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonInputSelectFile;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonOutputSelectFile;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.GroupBox groupBox;
        private System.Windows.Forms.CheckBox checkBoxAddSuperStars;
        private System.Windows.Forms.CheckBox checkBoxLowerStats;
        private System.Windows.Forms.CheckBox checkBoxSaveCompressed;
        private System.Windows.Forms.CheckBox checkBoxContractStartDates;
        private System.Windows.Forms.CheckBox checkBoxCapReputation;
        private System.Windows.Forms.CheckBox checkBoxReduceCrazyWages;
    }
}