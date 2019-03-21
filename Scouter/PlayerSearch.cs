using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher.Scouter
{
    public partial class PlayerSearch : Form
    {
        public string RowFilter = "";

        public PlayerSearch()
        {
            InitializeComponent();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            RowFilter = "1=1 ";
            if (!string.IsNullOrEmpty(textBoxName.Text))
            {
                RowFilter += string.Format("AND [{0}] LIKE '%{1}%' ", "Name", textBoxName.Text);
            }
            if (numericUpDownMinAge.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", "Age", numericUpDownMinAge.Value);
            }
            if (numericUpDownMaxAge.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", "Age", numericUpDownMaxAge.Value);
            }
            if (numericUpDownAbilityMin.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", "CA", numericUpDownAbilityMin.Value);
            }
            if (numericUpDownAbilityMax.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", "CA", numericUpDownAbilityMax.Value);
            }
            if (numericUpDownPAMin.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", "PA", numericUpDownPAMin.Value);
            }
            if (numericUpDownPAMax.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", "PA", numericUpDownPAMax.Value);
            }

            // MadScientist's Code
            //Position logic:            
            //GK: checkBoxGoalKeeper
            //SW: checkBox8
            //D: checkBox1
            //DM: checkBox7
            //M: checkBox2
            //AM: checkBox9
            //F and S: checkBox3
            string positionTitle = "Position";
            string positionAndOr = "AND ("; //if more than one position checked it appends 'OR' instead of 'AND (', and we use parenthesis because AND preceeds OR
            if (checkBoxGoalKeeper.Checked)
            {
                RowFilter += string.Format(positionAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "GK");
                positionAndOr = "OR";
            }
            if (checkBoxSweeper.Checked)
            {
                RowFilter += string.Format(positionAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "SW");
                positionAndOr = "OR";
            }
            if (checkBoxDefender.Checked)
            {
                RowFilter += string.Format(positionAndOr + " ( [{0}] LIKE '%{1}%' AND [{2}] NOT LIKE '%{3}%' ) ", positionTitle, "D", positionTitle, "DM");
                positionAndOr = "OR";
            }
            if (checkBoxDefMidfielder.Checked)
            {
                RowFilter += string.Format(positionAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "DM");
                positionAndOr = "OR";
            }
            if (checkBoxMidfielder.Checked)
            {
                RowFilter += string.Format(positionAndOr + " ( [{0}] LIKE '%{1}%' AND [{2}] NOT LIKE '%{3}%' AND [{4}] NOT LIKE '%{5}%' ) ", positionTitle, "M", positionTitle, "AM", positionTitle, "DM");
                positionAndOr = "OR";
            }
            if (checkBoxAttMidfielder.Checked)
            {
                RowFilter += string.Format(positionAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "AM");
                positionAndOr = "OR";
            }
            if (checkBoxAttacker.Checked)
            {
                RowFilter += string.Format(positionAndOr + " [{0}] LIKE '%{1}%' OR ([{2}] LIKE '%{3}%' AND [{4}] NOT LIKE '%{5}%') ", positionTitle, "F", positionTitle, "S", positionTitle, "SW");
                positionAndOr = "OR";
            }
            if (positionAndOr.Equals("OR"))
            {
                //this means at least one position checkbox was checked, so we close parenthesis:
                RowFilter += string.Format(") ");
            }

            //Sides logic:
            //Left side: checkBox4
            //Center side: checkBox5
            //Right side: checkBox6
            string sideAndOr = "AND ("; //if more than one side checked it appends 'OR' instead of 'AND (', and we use parenthesis because AND preceeds OR
            if (checkBoxLeftSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "L");
                sideAndOr = "OR";
            }
            if (checkBoxCentralSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "C");
                sideAndOr = "OR";
            }
            if (checkBoxRightSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "R");
                sideAndOr = "OR";
            }
            if (sideAndOr.Equals("OR"))
            {
                //this means at least one side checkbox was checked, so we close parenthesis:
                RowFilter += string.Format(") ");
            }
            Hide();
        }
    }
}
