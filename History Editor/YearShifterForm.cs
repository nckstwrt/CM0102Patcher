using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class YearShifterForm : Form
    {
        string indexFile;

        public YearShifterForm(string indexFile)
        {
            this.indexFile = indexFile;
            InitializeComponent();
        }

        private void buttonShiftYears_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Update Data Files with Year Shifts?", "Year Shifter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var yearChanger = new YearChanger();
                var dataDir = Path.GetDirectoryName(indexFile);

                var staffFile = Path.Combine(dataDir, "staff.dat");
                var playerConfigFile = Path.Combine(dataDir, "player_setup.cfg");
                var staffCompHistoryFile = Path.Combine(dataDir, "staff_comp_history.dat");
                var clubCompHistoryFile = Path.Combine(dataDir, "club_comp_history.dat");
                var staffHistoryFile = Path.Combine(dataDir, "staff_history.dat");
                var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");

                if (numericNationUpDownStaffAges.Value != 0)
                    yearChanger.UpdateStaff(indexFile, staffFile, (int)numericNationUpDownStaffAges.Value);

                if (numericUpDownPlayerConfig.Value != 0)
                    yearChanger.UpdatePlayerConfig(playerConfigFile, (int)numericUpDownPlayerConfig.Value);

                // Update History
                if (numericUpDownStaffCompetitions.Value != 0)
                    yearChanger.UpdateHistoryFile(staffCompHistoryFile, 0x3a, (int)numericUpDownStaffCompetitions.Value, 0x8, 0x30);
                if (numericUpDownPlayerHistories.Value != 0)
                    yearChanger.UpdateHistoryFile(staffHistoryFile, 0x11, (int)numericUpDownPlayerHistories.Value, 0x8, 0);

                int startIndexNationCompHistory = -1;
                if (numericUpDownClub.Value != 0 || numericUpDownYearCutOff.Value != 0)
                    startIndexNationCompHistory = yearChanger.UpdateHistoryFile(clubCompHistoryFile, 0x1a, (int)numericUpDownClub.Value, 0x8, 0, (int)numericUpDownYearCutOff.Value, indexFile, 0);
                if (numericUpDownNation.Value != 0 || numericUpDownYearCutOff.Value != 0)
                    yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, (int)numericUpDownNation.Value, 0x8, 0, (int)numericUpDownYearCutOff.Value, indexFile, startIndexNationCompHistory);

                MessageBox.Show("Years Shifted!", "Year Shifter", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
        }
    }
}
