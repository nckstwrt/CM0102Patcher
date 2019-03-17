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
            this.SuspendLayout();
            // 
            // textBoxBinaryHex
            // 
            this.textBoxBinaryHex.Location = new System.Drawing.Point(12, 25);
            this.textBoxBinaryHex.Name = "textBoxBinaryHex";
            this.textBoxBinaryHex.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryHex.TabIndex = 0;
            this.textBoxBinaryHex.TextChanged += new System.EventHandler(this.textBoxBinaryHex_TextChanged);
            // 
            // textBoxOllyHex
            // 
            this.textBoxOllyHex.Location = new System.Drawing.Point(170, 25);
            this.textBoxOllyHex.Name = "textBoxOllyHex";
            this.textBoxOllyHex.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyHex.TabIndex = 1;
            this.textBoxOllyHex.TextChanged += new System.EventHandler(this.textBoxOllyHex_TextChanged);
            // 
            // textBoxBinaryDec
            // 
            this.textBoxBinaryDec.Location = new System.Drawing.Point(12, 70);
            this.textBoxBinaryDec.Name = "textBoxBinaryDec";
            this.textBoxBinaryDec.Size = new System.Drawing.Size(100, 20);
            this.textBoxBinaryDec.TabIndex = 2;
            this.textBoxBinaryDec.TextChanged += new System.EventHandler(this.textBoxBinaryDec_TextChanged);
            // 
            // textBoxOllyDec
            // 
            this.textBoxOllyDec.Location = new System.Drawing.Point(170, 70);
            this.textBoxOllyDec.Name = "textBoxOllyDec";
            this.textBoxOllyDec.Size = new System.Drawing.Size(100, 20);
            this.textBoxOllyDec.TabIndex = 3;
            this.textBoxOllyDec.TextChanged += new System.EventHandler(this.textBoxOllyDec_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Binary File (Hex)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Binary File (Dec)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(181, 54);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Olly Offset (Dec)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(181, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Olly Offset (Hex)";
            // 
            // OffsetCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(285, 121);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxOllyDec);
            this.Controls.Add(this.textBoxBinaryDec);
            this.Controls.Add(this.textBoxOllyHex);
            this.Controls.Add(this.textBoxBinaryHex);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "OffsetCalculator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Offset Calculator";
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
    }
}