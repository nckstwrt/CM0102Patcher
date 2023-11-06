
namespace CM0102Patcher
{
    partial class GoHomeForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoHomeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxSavedGame = new System.Windows.Forms.TextBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.comboBoxNations = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonSendPlayersHome = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(385, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "This is a recreation of John Locke\'s \"Go Home\" Tool";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(537, 35);
            this.label2.TabIndex = 1;
            this.label2.Text = "The tool allows you to workaround the bug whereby International players get stuck" +
    " with their International team and never return home to their club. This tool fo" +
    "rces a country to return the players.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Saved Game:";
            // 
            // textBoxSavedGame
            // 
            this.textBoxSavedGame.Location = new System.Drawing.Point(15, 88);
            this.textBoxSavedGame.Name = "textBoxSavedGame";
            this.textBoxSavedGame.Size = new System.Drawing.Size(447, 20);
            this.textBoxSavedGame.TabIndex = 3;
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(468, 87);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(46, 23);
            this.buttonBrowse.TabIndex = 4;
            this.buttonBrowse.Text = "...";
            this.buttonBrowse.UseVisualStyleBackColor = true;
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(15, 115);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(75, 23);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Load";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.buttonLoad_Click);
            // 
            // comboBoxNations
            // 
            this.comboBoxNations.FormattingEnabled = true;
            this.comboBoxNations.Location = new System.Drawing.Point(15, 164);
            this.comboBoxNations.Name = "comboBoxNations";
            this.comboBoxNations.Size = new System.Drawing.Size(224, 21);
            this.comboBoxNations.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 145);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Nations";
            // 
            // buttonSendPlayersHome
            // 
            this.buttonSendPlayersHome.Location = new System.Drawing.Point(246, 162);
            this.buttonSendPlayersHome.Name = "buttonSendPlayersHome";
            this.buttonSendPlayersHome.Size = new System.Drawing.Size(151, 23);
            this.buttonSendPlayersHome.TabIndex = 8;
            this.buttonSendPlayersHome.Text = "Send Nation\'s Players Home";
            this.buttonSendPlayersHome.UseVisualStyleBackColor = true;
            this.buttonSendPlayersHome.Click += new System.EventHandler(this.buttonSendPlayersHome_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(15, 199);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(110, 23);
            this.buttonSave.TabIndex = 9;
            this.buttonSave.Text = "Save Changes";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // GoHomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 236);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSendPlayersHome);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.comboBoxNations);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.textBoxSavedGame);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GoHomeForm";
            this.Text = "Go Home Tool";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxSavedGame;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.ComboBox comboBoxNations;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonSendPlayersHome;
        private System.Windows.Forms.Button buttonSave;
    }
}