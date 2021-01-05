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

            AddEnterKeyHook(this);

            var filterCols = new string[] { "", "Acceleration", "Aggression", "Agility", "Anticipation", "Balance", "Bravery", "Consistency", "Corners", "Creativity", "Crossing", "Decisions", "Determination",
                                            "Dirtiness", "Dribbling", "Finishing", "Flair", "Free Kicks", "Handling", "Heading", "Important Matches", "Influence", "Injury Proneness", "Jumping", "Left Foot",
                                            "Long Shots", "Marking", "Natural Fitness", "Off The Ball", "One On Ones", "Pace", "Passing", "Penalties", "Positioning", "Reflexes", "Right Foot", "Stamina", "Strength",
                                            "Tackling", "Teamwork", "Technique", "Throw Ins", "Versatility","Work Rate", "Player Morale" };
            comboBoxFilter1.Items.AddRange(filterCols);
            comboBoxFilter2.Items.AddRange(filterCols);
            comboBoxFilter3.Items.AddRange(filterCols);
            comboBoxFilter4.Items.AddRange(filterCols);
            comboBoxFilter5.Items.AddRange(filterCols);
            comboBoxFilter6.Items.AddRange(filterCols);
            comboBoxFilter7.Items.AddRange(filterCols);
            comboBoxFilter8.Items.AddRange(filterCols);
        }

        private void AddEnterKeyHook(Control controlContainer)
        {
            foreach (var control in controlContainer.Controls)
            {
                if (control is TextBox || control is NumericUpDown)
                {
                    var textBox = control as Control;
                    textBox.KeyDown += (s, e) =>
                    {
                        if (e.KeyCode == Keys.Enter)
                        {
                            buttonApply_Click(this, new EventArgs());
                        }
                    };
                }

                if (control is GroupBox || control is TabPage || control is TabControl)
                    AddEnterKeyHook(control as Control);
            }
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
            if (!string.IsNullOrEmpty(textBoxClub.Text))
            {
                RowFilter += string.Format("AND [{0}] LIKE '%{1}%' ", "Club", textBoxClub.Text);
            }
            if (!string.IsNullOrEmpty(textBoxNationality.Text))
            {
                RowFilter += string.Format("AND [{0}] LIKE '%{1}%' ", "Nationality", textBoxNationality.Text);
            }
            if (!string.IsNullOrEmpty(textBoxSecondNationality.Text))
            {
                RowFilter += string.Format("AND [{0}] LIKE '%{1}%' ", "2nd Nationality", textBoxSecondNationality.Text);
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
            if (numericUpDownValueMin.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", "Value", numericUpDownValueMin.Value);
            }
            if (numericUpDownValueMax.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", "Value", numericUpDownValueMax.Value);
            }
            if (numericUpDownScoutMin.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", "Scouter Rating", numericUpDownScoutMin.Value);
            }
            if (numericUpDownScoutMax.Value != 0)
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", "Scouter Rating", numericUpDownScoutMax.Value);
            }
            if (numericUpDownFilter1Min.Value != 0 && (comboBoxFilter1.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter1.SelectedItem as string, numericUpDownFilter1Min.Value);
            }
            if (numericUpDownFilter1Max.Value != 0 && (comboBoxFilter1.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter1.SelectedItem as string, numericUpDownFilter1Max.Value);
            }
            if (numericUpDownFilter2Min.Value != 0 && (comboBoxFilter2.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter2.SelectedItem as string, numericUpDownFilter2Min.Value);
            }
            if (numericUpDownFilter2Max.Value != 0 && (comboBoxFilter2.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter2.SelectedItem as string, numericUpDownFilter2Max.Value);
            }
            if (numericUpDownFilter3Min.Value != 0 && (comboBoxFilter3.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter3.SelectedItem as string, numericUpDownFilter3Min.Value);
            }
            if (numericUpDownFilter3Max.Value != 0 && (comboBoxFilter3.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter3.SelectedItem as string, numericUpDownFilter3Max.Value);
            }
            if (numericUpDownFilter4Min.Value != 0 && (comboBoxFilter4.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter4.SelectedItem as string, numericUpDownFilter4Min.Value);
            }
            if (numericUpDownFilter4Max.Value != 0 && (comboBoxFilter4.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter4.SelectedItem as string, numericUpDownFilter4Max.Value);
            }
            if (numericUpDownFilter5Min.Value != 0 && (comboBoxFilter5.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter5.SelectedItem as string, numericUpDownFilter5Min.Value);
            }
            if (numericUpDownFilter5Max.Value != 0 && (comboBoxFilter5.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter5.SelectedItem as string, numericUpDownFilter5Max.Value);
            }
            if (numericUpDownFilter6Min.Value != 0 && (comboBoxFilter6.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter6.SelectedItem as string, numericUpDownFilter6Min.Value);
            }
            if (numericUpDownFilter6Max.Value != 0 && (comboBoxFilter6.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter6.SelectedItem as string, numericUpDownFilter6Max.Value);
            }
            if (numericUpDownFilter7Min.Value != 0 && (comboBoxFilter7.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter7.SelectedItem as string, numericUpDownFilter7Min.Value);
            }
            if (numericUpDownFilter7Max.Value != 0 && (comboBoxFilter7.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter7.SelectedItem as string, numericUpDownFilter7Max.Value);
            }
            if (numericUpDownFilter8Min.Value != 0 && (comboBoxFilter8.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] >= {1} ", comboBoxFilter8.SelectedItem as string, numericUpDownFilter8Min.Value);
            }
            if (numericUpDownFilter8Max.Value != 0 && (comboBoxFilter8.SelectedItem as string) != "")
            {
                RowFilter += string.Format("AND [{0}] <= {1} ", comboBoxFilter8.SelectedItem as string, numericUpDownFilter8Max.Value);
            }
            if (checkBoxNotIndispensible.Checked)
            {
                RowFilter += string.Format("AND [Squad Status] NOT LIKE 'Indispensable' ");
            }
            if (checkBoxNotNewContract.Checked)
            {
                RowFilter += string.Format("AND [Contract Age] >= 6 ");
            }
            if (checkBoxNotAlreadyLeaving.Checked)
            {
                RowFilter += string.Format("AND [Leaving Club] LIKE 'No' ");
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
            // Nick: Changing this to an AND (think CMScout works like this)
            if (checkBoxLeftSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "L");
                sideAndOr = "AND";
            }
            if (checkBoxCentralSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "C");
                sideAndOr = "AND";
            }
            if (checkBoxRightSided.Checked)
            {
                RowFilter += string.Format(sideAndOr + " [{0}] LIKE '%{1}%' ", positionTitle, "R");
                sideAndOr = "AND";
            }
            if (sideAndOr.Equals("AND"))
            {
                //this means at least one side checkbox was checked, so we close parenthesis:
                RowFilter += string.Format(") ");
            }
            Hide();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ResetControls(this);
        }

        private void ResetControls(Control controlContainer)
        {
            foreach (var control in controlContainer.Controls)
            {
                if (control is TextBox)
                {
                    (control as Control).Text = "";
                }
                if (control is NumericUpDown)
                {
                    (control as NumericUpDown).Value = 0;
                }
                if (control is ComboBox)
                {
                    (control as ComboBox).SelectedIndex = 0;
                }
                if (control is CheckBox)
                {
                    (control as CheckBox).Checked = false;
                }

                if (control is GroupBox || control is TabPage || control is TabControl)
                    ResetControls(control as Control);
            }
        }
    }
}
