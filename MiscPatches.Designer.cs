namespace CM0102Patcher
{
    partial class MiscPatches
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MiscPatches));
            this.checkedListBoxPatches = new ExpandedCheckedListBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.textBoxDescription = new System.Windows.Forms.TextBox();
            this.textBoxFilter = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkedListBoxPatches
            // 
            this.checkedListBoxPatches.CheckOnClick = true;
            this.checkedListBoxPatches.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkedListBoxPatches.FormattingEnabled = true;
            this.checkedListBoxPatches.Location = new System.Drawing.Point(9, 13);
            this.checkedListBoxPatches.Name = "checkedListBoxPatches";
            this.checkedListBoxPatches.Size = new System.Drawing.Size(572, 259);
            this.checkedListBoxPatches.TabIndex = 0;
            this.checkedListBoxPatches.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxPatches_ItemCheck);
            this.checkedListBoxPatches.SelectedValueChanged += new System.EventHandler(this.checkedListBoxPatches_SelectedValueChanged);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(506, 278);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 1;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // textBoxDescription
            // 
            this.textBoxDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F);
            this.textBoxDescription.Location = new System.Drawing.Point(598, 12);
            this.textBoxDescription.Multiline = true;
            this.textBoxDescription.Name = "textBoxDescription";
            this.textBoxDescription.ReadOnly = true;
            this.textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxDescription.Size = new System.Drawing.Size(199, 261);
            this.textBoxDescription.TabIndex = 2;
            // 
            // textBoxFilter
            // 
            this.textBoxFilter.Location = new System.Drawing.Point(82, 279);
            this.textBoxFilter.Name = "textBoxFilter";
            this.textBoxFilter.Size = new System.Drawing.Size(158, 20);
            this.textBoxFilter.TabIndex = 3;
            this.textBoxFilter.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Search Filter:";
            // 
            // MiscPatches
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 311);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFilter);
            this.Controls.Add(this.textBoxDescription);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.checkedListBoxPatches);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MiscPatches";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Misc Patches";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExpandedCheckedListBox checkedListBoxPatches;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.TextBox textBoxFilter;
        private System.Windows.Forms.Label label1;
    }
}