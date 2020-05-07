namespace CM0102Patcher
{
    partial class OffsetCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OffsetCalculator));
            this.textBoxBinaryHex = new System.Windows.Forms.TextBox();
            this.textBoxOllyHex = new System.Windows.Forms.TextBox();
            this.textBoxBinaryDec = new System.Windows.Forms.TextBox();
            this.textBoxOllyDec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxBinaryHexExpanded = new System.Windows.Forms.TextBox();
            this.textBoxOllyHexExpanded = new System.Windows.Forms.TextBox();
            this.textBoxBinaryDecExpanded = new System.Windows.Forms.TextBox();
            this.textBoxOllyDecExpanded = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxBinaryHex
            // 
            this.textBoxBinaryHex.Location = new System.Drawing.Point(12, 35);
            this.textBoxBinaryHex.Name = "textBoxBinaryHex";
            this.textBoxBinaryHex.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryHex.TabIndex = 0;
            this.textBoxBinaryHex.TextChanged += new System.EventHandler(this.textBoxBinaryHex_TextChanged);
            // 
            // textBoxOllyHex
            // 
            this.textBoxOllyHex.Location = new System.Drawing.Point(170, 35);
            this.textBoxOllyHex.Name = "textBoxOllyHex";
            this.textBoxOllyHex.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyHex.TabIndex = 1;
            this.textBoxOllyHex.TextChanged += new System.EventHandler(this.textBoxOllyHex_TextChanged);
            // 
            // textBoxBinaryDec
            // 
            this.textBoxBinaryDec.Location = new System.Drawing.Point(12, 80);
            this.textBoxBinaryDec.Name = "textBoxBinaryDec";
            this.textBoxBinaryDec.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryDec.TabIndex = 2;
            this.textBoxBinaryDec.TextChanged += new System.EventHandler(this.textBoxBinaryDec_TextChanged);
            // 
            // textBoxOllyDec
            // 
            this.textBoxOllyDec.Location = new System.Drawing.Point(170, 80);
            this.textBoxOllyDec.Name = "textBoxOllyDec";
            this.textBoxOllyDec.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyDec.TabIndex = 3;
            this.textBoxOllyDec.TextChanged += new System.EventHandler(this.textBoxOllyDec_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Binary File (Hex)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Binary File (Dec)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Olly Offset (Dec)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Olly Offset (Hex)";
            // 
            // groupBox2
            // 
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 109);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Original Exe";
            // 
            // textBoxBinaryHexExpanded
            // 
            this.textBoxBinaryHexExpanded.Location = new System.Drawing.Point(11, 36);
            this.textBoxBinaryHexExpanded.Name = "textBoxBinaryHexExpanded";
            this.textBoxBinaryHexExpanded.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryHexExpanded.TabIndex = 8;
            this.textBoxBinaryHexExpanded.TextChanged += new System.EventHandler(this.textBoxBinaryHexExpanded_TextChanged);
            // 
            // textBoxOllyHexExpanded
            // 
            this.textBoxOllyHexExpanded.Location = new System.Drawing.Point(169, 36);
            this.textBoxOllyHexExpanded.Name = "textBoxOllyHexExpanded";
            this.textBoxOllyHexExpanded.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyHexExpanded.TabIndex = 9;
            this.textBoxOllyHexExpanded.TextChanged += new System.EventHandler(this.textBoxOllyHexExpanded_TextChanged);
            // 
            // textBoxBinaryDecExpanded
            // 
            this.textBoxBinaryDecExpanded.Location = new System.Drawing.Point(11, 81);
            this.textBoxBinaryDecExpanded.Name = "textBoxBinaryDecExpanded";
            this.textBoxBinaryDecExpanded.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryDecExpanded.TabIndex = 10;
            this.textBoxBinaryDecExpanded.TextChanged += new System.EventHandler(this.textBoxBinaryDecExpanded_TextChanged);
            // 
            // textBoxOllyDecExpanded
            // 
            this.textBoxOllyDecExpanded.Location = new System.Drawing.Point(169, 81);
            this.textBoxOllyDecExpanded.Name = "textBoxOllyDecExpanded";
            this.textBoxOllyDecExpanded.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyDecExpanded.TabIndex = 11;
            this.textBoxOllyDecExpanded.TextChanged += new System.EventHandler(this.textBoxOllyDecExpanded_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Binary File (Hex)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Binary File (Dec)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(180, 65);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Olly Offset (Dec)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Olly Offset (Hex)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxOllyDecExpanded);
            this.groupBox1.Controls.Add(this.textBoxBinaryDecExpanded);
            this.groupBox1.Controls.Add(this.textBoxOllyHexExpanded);
            this.groupBox1.Controls.Add(this.textBoxBinaryHexExpanded);
            this.groupBox1.Location = new System.Drawing.Point(4, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 113);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "For the Expanded Exe Portion Only";
            // 
            // OffsetCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 232);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOllyDec);
            this.Controls.Add(this.textBoxBinaryDec);
            this.Controls.Add(this.textBoxOllyHex);
            this.Controls.Add(this.textBoxBinaryHex);
            this.Controls.Add(this.groupBox2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OffsetCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Offset Calculator";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxBinaryHex;
        private System.Windows.Forms.TextBox textBoxOllyHex;
        private System.Windows.Forms.TextBox textBoxBinaryDec;
        private System.Windows.Forms.TextBox textBoxOllyDec;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBoxBinaryHexExpanded;
        private System.Windows.Forms.TextBox textBoxOllyHexExpanded;
        private System.Windows.Forms.TextBox textBoxBinaryDecExpanded;
        private System.Windows.Forms.TextBox textBoxOllyDecExpanded;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}