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
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBoxRight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxBottom = new System.Windows.Forms.TextBox();
            this.textBoxLeft = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTop = new System.Windows.Forms.TextBox();
            this.checkBoxCrop = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
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
            this.textBoxInput.TextChanged += new System.EventHandler(this.textBoxInput_TextChanged);
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
            this.groupBox3.Location = new System.Drawing.Point(12, 186);
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
            this.groupBox4.Location = new System.Drawing.Point(11, 241);
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
            this.buttonConvert.Location = new System.Drawing.Point(204, 303);
            this.buttonConvert.Name = "buttonConvert";
            this.buttonConvert.Size = new System.Drawing.Size(95, 23);
            this.buttonConvert.TabIndex = 3;
            this.buttonConvert.Text = "Convert";
            this.buttonConvert.UseVisualStyleBackColor = true;
            this.buttonConvert.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.textBoxRight);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.textBoxBottom);
            this.groupBox5.Controls.Add(this.textBoxLeft);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.textBoxTop);
            this.groupBox5.Controls.Add(this.checkBoxCrop);
            this.groupBox5.Location = new System.Drawing.Point(12, 131);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(481, 49);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Crop Images";
            // 
            // textBoxRight
            // 
            this.textBoxRight.Enabled = false;
            this.textBoxRight.Location = new System.Drawing.Point(342, 17);
            this.textBoxRight.Name = "textBoxRight";
            this.textBoxRight.Size = new System.Drawing.Size(36, 20);
            this.textBoxRight.TabIndex = 8;
            this.textBoxRight.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(383, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Bottom:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(301, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Right:";
            // 
            // textBoxBottom
            // 
            this.textBoxBottom.Enabled = false;
            this.textBoxBottom.Location = new System.Drawing.Point(430, 17);
            this.textBoxBottom.Name = "textBoxBottom";
            this.textBoxBottom.Size = new System.Drawing.Size(36, 20);
            this.textBoxBottom.TabIndex = 6;
            this.textBoxBottom.Text = "0";
            // 
            // textBoxLeft
            // 
            this.textBoxLeft.Enabled = false;
            this.textBoxLeft.Location = new System.Drawing.Point(181, 19);
            this.textBoxLeft.Name = "textBoxLeft";
            this.textBoxLeft.Size = new System.Drawing.Size(36, 20);
            this.textBoxLeft.TabIndex = 4;
            this.textBoxLeft.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(227, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Top:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Left:";
            // 
            // textBoxTop
            // 
            this.textBoxTop.Enabled = false;
            this.textBoxTop.Location = new System.Drawing.Point(259, 18);
            this.textBoxTop.Name = "textBoxTop";
            this.textBoxTop.Size = new System.Drawing.Size(36, 20);
            this.textBoxTop.TabIndex = 2;
            this.textBoxTop.Text = "0";
            // 
            // checkBoxCrop
            // 
            this.checkBoxCrop.AutoSize = true;
            this.checkBoxCrop.Checked = true;
            this.checkBoxCrop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCrop.Location = new System.Drawing.Point(7, 20);
            this.checkBoxCrop.Name = "checkBoxCrop";
            this.checkBoxCrop.Size = new System.Drawing.Size(117, 17);
            this.checkBoxCrop.TabIndex = 0;
            this.checkBoxCrop.Text = "Crop Image(s) by = ";
            this.checkBoxCrop.UseVisualStyleBackColor = true;
            this.checkBoxCrop.CheckedChanged += new System.EventHandler(this.checkBoxCrop_CheckedChanged);
            // 
            // ImageConverterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 336);
            this.Controls.Add(this.groupBox5);
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
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
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
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox textBoxRight;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxBottom;
        private System.Windows.Forms.TextBox textBoxLeft;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTop;
        private System.Windows.Forms.CheckBox checkBoxCrop;
    }
}