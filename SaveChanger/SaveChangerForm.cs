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
    public partial class SaveChangerForm : Form
    {
        public SaveChangerForm()
        {
            InitializeComponent();
        }

        private void buttonInputSelectFile_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Save Game Files (*.sav)|*.sav|All files (*.*)|*.*";
            ofd.Title = "Select an input image file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxInput.Text = ofd.FileName;
                textBoxOutput.Text = Path.Combine(Path.GetDirectoryName(textBoxInput.Text), Path.GetFileNameWithoutExtension(textBoxInput.Text)) + "_Modified.sav";
            }
        }

        private void buttonOutputSelectFile_Click(object sender, EventArgs e)
        {
            var ofd = new SaveFileDialog();
            ofd.Filter = "Save Game Files (*.sav)|*.sav|All files (*.*)|*.*";
            ofd.Title = "Select an input image file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxOutput.Text = ofd.FileName;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This will freeze for a minute or two while it processes. Continue?", "Freeze Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
                return;

            if (!string.IsNullOrEmpty(textBoxInput.Text) && !string.IsNullOrEmpty(textBoxOutput.Text))
            {
                try
                {
                    SaveReader2 sr2 = new SaveReader2();
                    sr2.Load(textBoxInput.Text);
                    var staff = sr2.BlockToObjects<TStaff>("staff.dat");
                    var players = sr2.BlockToObjects<TPlayer>("player.dat");
                    var contracts = sr2.BlockToObjects<TContract>("contract.dat", true);
                    var firstNames = sr2.NamesFromBlock("first_names.dat");
                    var secondNames = sr2.NamesFromBlock("second_names.dat");
                    var gameDate = sr2.GetCurrentGameDate();
                    
                    if (checkBoxLowerStats.Checked)
                    {
                        for (int i = 0; i < staff.Count(); i++)
                        {
                            if (staff[i].Player < 0)
                                continue;
                            var staffData = staff[i];
                            var player = players[staffData.Player];

                            // Saturn: "There seems to be only seven significantly troublesome attributes: Anticipation; Creativity; Decisions; 
                            // Influence; Positioning; Reflexes and Technique. Adding in some Hidden Attributes from my testing we can bring this 
                            // up to eleven by including Ambition, Consistency, Throw-Ins and Versatitility"
                            // https://champman0102.co.uk/showthread.php?t=5310&p=221137#post221137
                            player.Anticipation = (sbyte)((((double)(byte)player.Anticipation) * 0.85) + 0.5);
                            player.Vision = (sbyte)((((double)(byte)player.Vision) * 0.85) + 0.5);
                            player.Decisions = (sbyte)((((double)(byte)player.Decisions) * 0.85) + 0.5);
                            player.Leadership = (sbyte)((((double)(byte)player.Leadership) * 0.85) + 0.5);
                            player.Positioning = (sbyte)((((double)(byte)player.Positioning) * 0.85) + 0.5);
                            player.Reflexes = (sbyte)Math.Min(255, ((((double)(byte)player.Reflexes) * 1.15) + 0.5));          // Going down, so raise
                            player.Technique = (sbyte)Math.Min(20, ((((double)(byte)player.Technique) * 1.15) + 0.5));        // Going down, so raise

                            staffData.Ambition = (byte)Math.Min(20, ((((double)(byte)staffData.Ambition) * 1.15) + 0.5));     // Going down, so raise
                            player.Consistency = (sbyte)((((double)(byte)player.Consistency) * 0.85) + 0.5);
                            player.ThrowIns = (sbyte)((((double)(byte)player.ThrowIns) * 0.85) + 0.5);
                            player.Versatility = (sbyte)((((double)(byte)player.Versatility) * 0.85) + 0.5);

                            staff[i] = staffData;
                            players[staffData.Player] = player;
                        }
                    }

                    if (checkBoxAddSuperStars.Checked)
                    {
                        string secret = "";
                        SuperStarMaker ssm = new SuperStarMaker();
                        foreach (var star in ssm.stars)
                        {
                            var staffId = ssm.MakeSuperStar(staff, players, star.staffBytes, star.playerBytes, gameDate, star.age);
                            string newName = firstNames[staff[staffId].FirstName] + " " + secondNames[staff[staffId].SecondName];
                            secret += string.Format("{0} = {1} ({2})\r\n", star.name, newName, star.age);
                        }
                        if (PatcherForm.SecretMode)
                        {
                            ssm.MakeTHERichardMolloy(staff, players, gameDate, 15, firstNames, secondNames);
                            Clipboard.SetText(secret);
                        }
                    }

                    if (checkBoxContractStartDates.Checked)
                    {
                        Dictionary<int, List<TContract>> contractLookup = new Dictionary<int, List<TContract>>();
                        
                        for (int i = 0; i < contracts.Count(); i++)
                        {
                            var contract = contracts[i];
                            if (contract.LeavingOnBosman != 1 && contract.DateStarted.Year > 1900 && contract.DateStarted.Year < 3000 && contract.ContractExpires.Year > gameDate.Year)
                            {
                                contract.DateStarted = TCMDate.FromDateTime(TCMDate.ToDateTime(contract.DateStarted).AddYears(-1));
                                contracts[i] = contract; 
                            }
                            if (!contractLookup.ContainsKey(contract.ID))
                                contractLookup[contract.ID] = new List<TContract>();
                            contractLookup[contract.ID].Add(contract);
                        }

                        var nathan = sr2.FindPlayer("Nathan", "Baxter", staff);
                        var nathanContract = contractLookup[nathan[0].ID];

                        for (int i = 0; i < staff.Count(); i++)
                        {
                            var staffData = staff[i];
                            bool LeavingOnBosman = false;
                            if (contractLookup.ContainsKey(staffData.ID))
                            {
                                LeavingOnBosman = (contractLookup[staffData.ID][0].LeavingOnBosman == 1);
                            }
                            if (!LeavingOnBosman && staffData.DateJoinedClub.Year > 1900 && staffData.DateJoinedClub.Year < 3000 && staffData.DateExpiresClub.Year > gameDate.Year)
                            {
                                staffData.DateJoinedClub = TCMDate.FromDateTime(TCMDate.ToDateTime(staffData.DateJoinedClub).AddYears(-1));
                                staff[i] = staffData;
                            }
                        }
                    }

                    if (checkBoxCapReputation.Checked)
                    {
                        for (int i = 0; i < staff.Count(); i++)
                        {
                            if (staff[i].Player < 0)
                                continue;
                            var staffData = staff[i];
                            var player = players[staffData.Player];
                            if (player.CurrentReputation > 5000)
                            {
                                player.CurrentReputation = 5000;
                            }
                            players[staffData.Player] = player;
                        }
                    }

                    if (checkBoxReduceCrazyWages.Checked)
                    {
                        for (int i = 0; i < contracts.Count(); i++)
                        {
                            var contract = contracts[i];
                            if (contract.Wage >= 250000)
                            {
                                contract.Wage /= 10;
                            }
                            contracts[i] = contract;
                        }
                    }

                    sr2.ObjectsToBlock("player.dat", players);
                    sr2.ObjectsToBlock("staff.dat", staff);
                    // Only write contracts if changed
                    if (checkBoxContractStartDates.Checked || checkBoxReduceCrazyWages.Checked)
                        sr2.ObjectsToBlock("contract.dat", contracts, true);
                    sr2.Write(textBoxOutput.Text, checkBoxSaveCompressed.Checked);

                    MessageBox.Show(string.Format("Modification complete!\r\n\r\nSaved to {0} successfully!", Path.GetFileName(textBoxOutput.Text)), "Modification Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error has occurred while processing the save game!\r\n\r\n" + ex.Message, "Save Game Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
                MessageBox.Show("Please select both an input and output save game file", "CM0102Patch Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public string ArrayToString(byte[] array)
        {
            return Encoding.GetEncoding("ISO-8859-1").GetString(array, 0, array.Length).TrimEnd('\0');
        }
    }
}
