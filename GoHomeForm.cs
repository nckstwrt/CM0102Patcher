using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class GoHomeForm : Form
    {
        SaveReader2 sr2 = null;
        List<TClub> nat_club;
        List<TStaff> staff;

        public GoHomeForm()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Save Files (*.sav)|*.sav|All files (*.*)|*.*";
            ofd.Title = "Select a saved game file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxSavedGame.Text = ofd.FileName;
                buttonLoad_Click(null, null);
            }
        }

        enum PLAYING_SQUAD
        {
            INVALID_SQUAD = 0,//not set?
	        CLUB_SENIOR_SQUAD = 1 ,
	        CLUB_RESERVE_SQUAD = 2,
	        NATION_MAIN_SQUAD = 4,
	        NATION_B_SQUAD = 8
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxSavedGame.Text))
            {
                MessageBox.Show("Please select a saved game file", "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(textBoxSavedGame.Text))
            {
                MessageBox.Show("Cannot find specified saved game file", "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                sr2 = new SaveReader2();
                sr2.Load(textBoxSavedGame.Text);
                staff = sr2.BlockToObjects<TStaff>("staff.dat");
                nat_club = sr2.BlockToObjects<TClub>("nat_club.dat");

                //sr2.DumpBlockToFile("FIFA World Cup_448.tmp", @"C:\champman\FIFA World Cup_448.tmp");
                //sr2.DumpBlockToFile("national_teams.dat", @"C:\champman\national_teams.dat");

                /*
                List<TClub> nationsWithPlayersAssigned = new List<TClub>();
                foreach (var club in nat_club)
                {
                    if (club.Name.ReadString() == "Mexico")
                        Console.WriteLine();
                    foreach (var staffID in club.Squad)
                    {
                        if (staffID >= 0 && staffID < staff.Count)
                        {
                            if (((staff[staffID].PlayingSquad & (byte)PLAYING_SQUAD.NATION_MAIN_SQUAD) == (byte)PLAYING_SQUAD.NATION_MAIN_SQUAD) || ((staff[staffID].PlayingSquad & (byte)PLAYING_SQUAD.NATION_B_SQUAD) == (byte)PLAYING_SQUAD.NATION_B_SQUAD))
                            {
                                nationsWithPlayersAssigned.Add(club);
                                break;
                            }
                        }
                    }
                }*/

                comboBoxNations.Items.Clear();
                var sortedNames = nat_club.Select(x => x.ShortName.ReadString()).OrderBy(x => x).Distinct().ToArray();
                comboBoxNations.Items.AddRange(sortedNames);
                if (comboBoxNations.Items.Count > 0)
                    comboBoxNations.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception occurred: " + ex.Message, "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSendPlayersHome_Click(object sender, EventArgs e)
        {
            if (sr2 != null && comboBoxNations.Items.Count > 0)
            {
                var selectedNations = nat_club.Where(x => x.Name.ReadString().ToLower() == (comboBoxNations.Text as string).ToLower()).ToList();
                if (selectedNations.Count > 0)
                {
                    foreach (var selectedNation in selectedNations)
                    {
                        foreach (var staffID in selectedNation.Squad)
                        {
                            if (staffID >= 0 && staffID < staff.Count)
                            {
                                staff[staffID].PlayingSquad = (byte)PLAYING_SQUAD.CLUB_SENIOR_SQUAD;
                            }
                        }
                    }
                    MessageBox.Show("Players sent home! Press Save Changes when finished!", "Go Home Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Can't find nation", "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Please load a saved game file and select a nation first", "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (sr2 != null)
            {
                try
                {
                    sr2.ObjectsToBlock("staff.dat", staff);
                    sr2.Write(textBoxSavedGame.Text, sr2.WasCompressed);
                    MessageBox.Show("All Changes Saved!", "Go Home Tool", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Exception occurred: " + ex.Message, "Go Home Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
