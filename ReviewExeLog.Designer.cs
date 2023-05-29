
namespace CM0102Patcher
{
    partial class ReviewExeLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReviewExeLog));
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.buttonCopyToClipboard = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.HideSelection = false;
            this.textBoxLog.Location = new System.Drawing.Point(14, 12);
            this.textBoxLog.MaxLength = 1000000;
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxLog.Size = new System.Drawing.Size(652, 356);
            this.textBoxLog.TabIndex = 0;
            this.textBoxLog.WordWrap = false;
            // 
            // buttonCopyToClipboard
            // 
            this.buttonCopyToClipboard.Location = new System.Drawing.Point(556, 377);
            this.buttonCopyToClipboard.Name = "buttonCopyToClipboard";
            this.buttonCopyToClipboard.Size = new System.Drawing.Size(110, 23);
            this.buttonCopyToClipboard.TabIndex = 1;
            this.buttonCopyToClipboard.Text = "Copy to Clipboard";
            this.buttonCopyToClipboard.UseVisualStyleBackColor = true;
            this.buttonCopyToClipboard.Click += new System.EventHandler(this.buttonCopyToClipboard_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(304, 377);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 23);
            this.buttonClose.TabIndex = 2;
            this.buttonClose.Text = "Close";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ReviewExeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(678, 412);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonCopyToClipboard);
            this.Controls.Add(this.textBoxLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReviewExeLog";
            this.Text = "CM0102.EXE Log";
            this.ResizeEnd += new System.EventHandler(this.ReviewExeLog_ResizeEnd);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Button buttonCopyToClipboard;
        private System.Windows.Forms.Button buttonClose;
    }
}