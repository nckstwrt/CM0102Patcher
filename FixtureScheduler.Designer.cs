namespace CM0102Patcher
{
    partial class FixtureScheduler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FixtureScheduler));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageEPL = new System.Windows.Forms.TabPage();
            this.buttonApplyEPL = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageEPL.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageEPL);
            this.tabControl.Location = new System.Drawing.Point(12, 8);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(556, 534);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageEPL
            // 
            this.tabPageEPL.BackColor = System.Drawing.SystemColors.Control;
            this.tabPageEPL.Controls.Add(this.buttonApplyEPL);
            this.tabPageEPL.Location = new System.Drawing.Point(4, 22);
            this.tabPageEPL.Name = "tabPageEPL";
            this.tabPageEPL.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageEPL.Size = new System.Drawing.Size(548, 508);
            this.tabPageEPL.TabIndex = 0;
            this.tabPageEPL.Text = "EPL";
            // 
            // buttonApplyEPL
            // 
            this.buttonApplyEPL.Location = new System.Drawing.Point(467, 473);
            this.buttonApplyEPL.Name = "buttonApplyEPL";
            this.buttonApplyEPL.Size = new System.Drawing.Size(75, 23);
            this.buttonApplyEPL.TabIndex = 0;
            this.buttonApplyEPL.Text = "Apply";
            this.buttonApplyEPL.UseVisualStyleBackColor = true;
            this.buttonApplyEPL.Click += new System.EventHandler(this.buttonApplyEPL_Click);
            // 
            // FixtureScheduler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 554);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FixtureScheduler";
            this.Text = "Fixture Scheduler";
            this.tabControl.ResumeLayout(false);
            this.tabPageEPL.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageEPL;
        private System.Windows.Forms.Button buttonApplyEPL;
    }
}