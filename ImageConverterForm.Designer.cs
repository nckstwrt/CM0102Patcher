namespace CM0102Patcher
{
    partial class ImageConverterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageConverterForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonInputSelectFolder = new System.Windows.Forms.Button();
            this.buttonInputSelectFile = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxIfImageHeight = new System.Windows.Forms.TextBox();
            this.textBoxIfImageWidth = new System.Windows.Forms.TextBox();
            this.checkBoxOnlyProcessIfSize = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxResizeImageHeight = new System.Windows.Forms.TextBox();
            this.textBoxResizeImageWidth = new System.Windows.Forms.TextBox();
            this.checkBoxResizeImagesTo = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.buttonOutputSelectFolder = new System.Windows.Forms.Button();
            this.buttonOutputSelectFile = new System.Windows.Forms.Button();
            this.textBoxOutput = new System.Windows.Forms.TextBox();
            this.buttonConvert = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.buttonInputSelectFolder);
            this.groupBox1.Controls.Add(this.buttonInputSelectFile);
            this.groupBox1.Controls.Add(this.textBoxInput);
            this.groupBox1.Location = new System.Drawing.Point(11, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(482, 56);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Images (RGN/PNG/JPG/BMP)";
            // 
            // buttonInputSelectFolder
            // 
            this.buttonInputSelectFolder.Location = new System.Drawing.Point(375, 19);
            this.buttonInputSelectFolder.Name = "buttonInputSelectFolder";
            this.buttonInputSelectFolder.Size = new System.Drawing.Size(95, 23);
            this.buttonInputSelectFolder.TabIndex = 2;
            this.buttonInputSelectFolder.Text = "Select Folder...";
            this.buttonInputSelectFolder.UseVisualStyleBackColor = true;
            this.buttonInputSelectFolder.Click += new System.EventHandler(this.buttonInputSelectFolder_Click);
            // 
            // buttonInputSelectFile
            // 
            this.buttonInputSelectFile.Location = new System.Drawing.Point(274, 19);
            this.buttonInputSelectFile.Name = "buttonInputSelectFile";
            this.buttonInputSelectFile.Size = new System.Drawing.Size(95, 23);
            this.buttonInputSelectFile.TabIndex = 1;
            this.buttonInputSelectFile.Text = "Select File...";
            this.buttonInputSelectFile.UseVisualStyleBackColor = true;
            this.buttonInputSelectFile.Click += new System.EventHandler(this.buttonInputSelectFile_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Location = new System.Drawing.Point(7, 20);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(261, 20);
            this.textBoxInput.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.textBoxIfImageHeight);
            this.groupBox2.Controls.Add(this.textBoxIfImageWidth);
            this.groupBox2.Controls.Add(this.checkBoxOnlyProcessIfSize);
            this.groupBox2.Location = new System.Drawing.Point(12, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(481, 49);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Conditional Processing";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(255, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "x";
            // 
            // textBoxIfImageHeight
            // 
            this.textBoxIfImageHeight.Enabled = false;
            this.textBoxIfImageHeight.Location = new System.Drawing.Point(271, 18);
            this.textBoxIfImageHeight.Name = "textBoxIfImageHeight";
            this.textBoxIfImageHeight.Size = new System.Drawing.Size(70, 20);
            this.textBoxIfImageHeight.TabIndex = 2;
            // 
            // textBoxIfImageWidth
            // 
            this.textBoxIfImageWidth.Enabled = false;
            this.textBoxIfImageWidth.Location = new System.Drawing.Point(180, 18);
            this.textBoxIfImageWidth.Name = "textBoxIfImageWidth";
            this.textBoxIfImageWidth.Size = new System.Drawing.Size(70, 20);
            this.textBoxIfImageWidth.TabIndex = 1;
            // 
            // checkBoxOnlyProcessIfSize
            // 
            this.checkBoxOnlyProcessIfSize.AutoSize = true;
            this.checkBoxOnlyProcessIfSize.Location = new System.Drawing.Point(7, 20);
            this.checkBoxOnlyProcessIfSize.Name = "checkBoxOnlyProcessIfSize";
            this.checkBoxOnlyProcessIfSize.Size = new System.Drawing.Size(168, 17);
            this.checkBoxOnlyProcessIfSize.TabIndex = 0;
            this.checkBoxOnlyProcessIfSize.Text = "Only Process Images if Size = ";
            this.checkBoxOnlyProcessIfSize.UseVisualStyleBackColor = true;
            this.checkBoxOnlyProcessIfSize.CheckedChanged += new System.EventHandler(this.checkBoxOnlyProcessIfSize_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.textBoxResizeImageHeight);
            this.groupBox3.Controls.Add(this.textBoxResizeImageWidth);
            this.groupBox3.Controls.Add(this.checkBoxResizeImagesTo);
            this.groupBox3.Location = new System.Drawing.Point(12, 131);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(481, 49);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Resize Images";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(255, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "x";
            // 
            // textBoxResizeImageHeight
            // 
            this.textBoxResizeImageHeight.Enabled = false;
            this.textBoxResizeImageHeight.Location = new System.Drawing.Point(271, 18);
            this.textBoxResizeImageHeight.Name = "textBoxResizeImageHeight";
            this.textBoxResizeImageHeight.Size = new System.Drawing.Size(70, 20);
            this.textBoxResizeImageHeight.TabIndex = 2;
            // 
            // textBoxResizeImageWidth
            // 
            this.textBoxResizeImageWidth.Enabled = false;
            this.textBoxResizeImageWidth.Location = new System.Drawing.Point(180, 18);
            this.textBoxResizeImageWidth.Name = "textBoxResizeImageWidth";
            this.textBoxResizeImageWidth.Size = new System.Drawing.Size(70, 20);
            this.textBoxResizeImageWidth.TabIndex = 1;
            // 
            // checkBoxResizeImagesTo
            // 
            this.checkBoxResizeImagesTo.AutoSize = true;
            this.checkBoxResizeImagesTo.Location = new System.Drawing.Point(7, 20);
            this.checkBoxResizeImagesTo.Name = "checkBoxResizeImagesTo";
            this.checkBoxResizeImagesTo.Size = new System.Drawing.Size(125, 17);
            this.checkBoxResizeImagesTo.TabIndex = 0;
            this.checkBoxResizeImagesTo.Text = "Resize Image(s) to = ";
            this.checkBoxResizeImagesTo.UseVisualStyleBackColor = true;
            this.checkBoxResizeImagesTo.CheckedChanged += new System.EventHandler(this.checkBoxResizeImagesTo_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.buttonOutputSelectFolder);
            this.groupBox4.Controls.Add(this.buttonOutputSelectFile);
            this.groupBox4.Controls.Add(this.textBoxOutput);
            this.groupBox4.Location = new System.Drawing.Point(11, 186);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(482, 56);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Output RGN File(s)";
            // 
            // buttonOutputSelectFolder
            // 
            this.buttonOutputSelectFolder.Location = new System.Drawing.Point(375, 19);
            this.buttonOutputSelectFolder.Name = "buttonOutputSelectFolder";
            this.buttonOutputSelectFolder.Size = new System.Drawing.Size(95, 23);
            this.buttonOutputSelectFolder.TabIndex = 2;
            this.buttonOutputSelectFolder.Text = "Select Folder...";
            this.buttonOutputSelectFolder.UseVisualStyleBackColor = true;
            this.buttonOutputSelectFolder.Click += new System.EventHandler(this.buttonOutputSelectFolder_Click);
            // 
            // buttonOutputSelectFile
            // 
            this.buttonOutputSelectFile.Location = new System.Drawing.Point(274, 19);
            this.buttonOutputSelectFile.Name = "buttonOutputSelectFile";
            this.buttonOutputSelectFile.Size = new System.Drawing.Size(95, 23);
            this.buttonOutputSelectFile.TabIndex = 1;
            this.buttonOutputSelectFile.Text = "Select File...";
            this.buttonOutputSelectFile.UseVisualStyleBackColor = true;
            this.buttonOutputSelectFile.Click += new System.EventHandler(this.buttonOutputSelectFile_Click);
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.Location = new System.Drawing.Point(7, 20);
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new System.Drawing.Size(261, 20);
            this.textBoxOutput.TabIndex = 0;
            // 
            // buttonConvert
            // 
            this.buttonConvert.Location = new System.Drawing.Point(204, 248);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(95, 23);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // ImageConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 278);
            this.Controls.Add(this.buttonConvert);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ImageConverterForm";
            this.Text = "RGN Image Converter";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonInputSelectFolder;
        private System.Windows.Forms.Button buttonInputSelectFile;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxIfImageHeight;
        private System.Windows.Forms.TextBox textBoxIfImageWidth;
        private System.Windows.Forms.CheckBox checkBoxOnlyProcessIfSize;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxResizeImageHeight;
        private System.Windows.Forms.TextBox textBoxResizeImageWidth;
        private System.Windows.Forms.CheckBox checkBoxResizeImagesTo;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button buttonOutputSelectFolder;
        private System.Windows.Forms.Button buttonOutputSelectFile;
        private System.Windows.Forms.TextBox textBoxOutput;
        private System.Windows.Forms.Button buttonConvert;
    }
}