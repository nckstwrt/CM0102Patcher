namespace CM0102Patcher
{
    partial class Tools
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tools));
            this.buttonApplyPatchfile = new System.Windows.Forms.Button();
            this.buttonEECPatcher = new System.Windows.Forms.Button();
            this.buttonOffsetCalculator = new System.Windows.Forms.Button();
            this.buttonRefereePatcher = new System.Windows.Forms.Button();
            this.buttonSaveScouter = new System.Windows.Forms.Button();
            this.buttonRGNImageConverter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonApplyPatchfile
            // 
            this.buttonApplyPatchfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonApplyPatchfile.Location = new System.Drawing.Point(12, 37);
            this.buttonApplyPatchfile.Name = "buttonApplyPatchfile";
            this.buttonApplyPatchfile.Size = new System.Drawing.Size(165, 24);
            this.buttonApplyPatchfile.TabIndex = 0;
            this.buttonApplyPatchfile.Text = "Apply Patchfile...";
            this.buttonApplyPatchfile.UseVisualStyleBackColor = true;
            this.buttonApplyPatchfile.Click += new System.EventHandler(this.buttonApplyPatchfile_Click);
            // 
            // buttonEECPatcher
            // 
            this.buttonEECPatcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonEECPatcher.Location = new System.Drawing.Point(12, 66);
            this.buttonEECPatcher.Name = "buttonEECPatcher";
            this.buttonEECPatcher.Size = new System.Drawing.Size(165, 24);
            this.buttonEECPatcher.TabIndex = 1;
            this.buttonEECPatcher.Text = "EEC Patcher...";
            this.buttonEECPatcher.UseVisualStyleBackColor = true;
            this.buttonEECPatcher.Click += new System.EventHandler(this.buttonEECPatcher_Click);
            // 
            // buttonOffsetCalculator
            // 
            this.buttonOffsetCalculator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOffsetCalculator.Location = new System.Drawing.Point(12, 124);
            this.buttonOffsetCalculator.Name = "buttonOffsetCalculator";
            this.buttonOffsetCalculator.Size = new System.Drawing.Size(165, 24);
            this.buttonOffsetCalculator.TabIndex = 2;
            this.buttonOffsetCalculator.Text = "Offset Calculator...";
            this.buttonOffsetCalculator.UseVisualStyleBackColor = true;
            this.buttonOffsetCalculator.Click += new System.EventHandler(this.buttonOffsetCalculator_Click);
            // 
            // buttonRefereePatcher
            // 
            this.buttonRefereePatcher.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRefereePatcher.Location = new System.Drawing.Point(12, 95);
            this.buttonRefereePatcher.Name = "buttonRefereePatcher";
            this.buttonRefereePatcher.Size = new System.Drawing.Size(165, 24);
            this.buttonRefereePatcher.TabIndex = 3;
            this.buttonRefereePatcher.Text = "Referee Patcher..";
            this.buttonRefereePatcher.UseVisualStyleBackColor = true;
            this.buttonRefereePatcher.Click += new System.EventHandler(this.buttonRefereePatcher_Click);
            // 
            // buttonSaveScouter
            // 
            this.buttonSaveScouter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveScouter.Location = new System.Drawing.Point(12, 8);
            this.buttonSaveScouter.Name = "buttonSaveScouter";
            this.buttonSaveScouter.Size = new System.Drawing.Size(165, 24);
            this.buttonSaveScouter.TabIndex = 4;
            this.buttonSaveScouter.Text = "Save Scouter...";
            this.buttonSaveScouter.UseVisualStyleBackColor = true;
            this.buttonSaveScouter.Click += new System.EventHandler(this.buttonSaveScouter_Click);
            // 
            // buttonRGNImageConverter
            // 
            this.buttonRGNImageConverter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRGNImageConverter.Location = new System.Drawing.Point(12, 153);
            this.buttonRGNImageConverter.Name = "buttonRGNImageConverter";
            this.buttonRGNImageConverter.Size = new System.Drawing.Size(165, 24);
            this.buttonRGNImageConverter.TabIndex = 5;
            this.buttonRGNImageConverter.Text = "RGN Image Converter...";
            this.buttonRGNImageConverter.UseVisualStyleBackColor = true;
            this.buttonRGNImageConverter.Click += new System.EventHandler(this.buttonRGNImageConverter_Click);
            // 
            // Tools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 185);
            this.Controls.Add(this.buttonRGNImageConverter);
            this.Controls.Add(this.buttonSaveScouter);
            this.Controls.Add(this.buttonRefereePatcher);
            this.Controls.Add(this.buttonOffsetCalculator);
            this.Controls.Add(this.buttonEECPatcher);
            this.Controls.Add(this.buttonApplyPatchfile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Tools";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tools";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonApplyPatchfile;
        private System.Windows.Forms.Button buttonEECPatcher;
        private System.Windows.Forms.Button buttonOffsetCalculator;
        private System.Windows.Forms.Button buttonRefereePatcher;
        private System.Windows.Forms.Button buttonSaveScouter;
        private System.Windows.Forms.Button buttonRGNImageConverter;
    }
}