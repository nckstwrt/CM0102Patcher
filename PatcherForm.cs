﻿using Microsoft.Win32;
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
    public partial class PatcherForm : Form
    {
        public PatcherForm()
        {
            InitializeComponent();

            try
            {
                var exeLocation = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                if (!string.IsNullOrEmpty(exeLocation))
                {
                    labelFilename.Text = Path.Combine(exeLocation, "cm0102.exe");
                }
                comboBoxGameSpeed.Items.AddRange(new ComboboxItem[]
                {
                    new ComboboxItem("x0.5", 20000),
                    new ComboboxItem("default", 10000),
                    new ComboboxItem("x2", 5000),
                    new ComboboxItem("x4", 2500),
                    new ComboboxItem("x8", 1250),
                    new ComboboxItem("x20", 500),
                    new ComboboxItem("x200", 50),
                    new ComboboxItem("Max", 1)
                });
                comboBoxGameSpeed.SelectedIndex = 3;

                // Set Default Start Year to this year if we're past July (else use last year) 
                var currentYear = DateTime.Now.Year;
                if (DateTime.Now.Month < 7) 
                    currentYear--;
                numericGameStartYear.Value = currentYear;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "CM0102.exe Files|*.exe|All files (*.*)|*.*";
            ofd.Title = "Select a CM0102.exe file";
            try
            {
                var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                if (!string.IsNullOrEmpty(path))
                    ofd.InitialDirectory = path;
            }
            catch { }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                labelFilename.Text = ofd.FileName;
            }
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            // Let's go! - check the file exists and is writeable
            if (string.IsNullOrEmpty(labelFilename.Text))
            {
                MessageBox.Show("Please select a cm0102.exe file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!File.Exists(labelFilename.Text))
            {
                MessageBox.Show("Cannot find cm0102.exe file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                using (var file = File.Open(labelFilename.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                }
            }
            catch
            {
                MessageBox.Show("Unable to open and/or write to cm0102.exe file", "File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dir = Path.GetDirectoryName(labelFilename.Text);
            var dataDir = Path.Combine(dir, "Data");

            // Start the patcher
            Patcher patcher = new Patcher();
            if (!patcher.CheckForV3968(labelFilename.Text))
            {
                var YesNo = MessageBox.Show("This does not look to be a 3.9.68 exe. Are you sure you wish to continue?", "3.9.68 Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (YesNo == DialogResult.No)
                    return;
            }

            // Game speed hack
            patcher.SpeedHack(labelFilename.Text, (short)(int)(comboBoxGameSpeed.SelectedItem as ComboboxItem).Value);

            // Currency Inflation
            patcher.CurrencyInflationChanger(labelFilename.Text, (double)numericCurrencyInflation.Value);

            // Year Change
            if (checkBoxChangeStartYear.Checked)
            {
                // Assume Staff.data is in Data
                var staffFile = Path.Combine(dataDir,"staff.dat");
                var indexFile = Path.Combine(dataDir, "index.dat");
                var playerConfigFile = Path.Combine(dataDir, "player_setup.cfg");
                var staffCompHistoryFile = Path.Combine(dataDir, "staff_comp_history.dat");
                var clubCompHistoryFile = Path.Combine(dataDir, "club_comp_history.dat");
                var staffHistoryFile = Path.Combine(dataDir, "staff_history.dat");
                var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");
                try
                {
                    YearChanger yearChanger = new YearChanger();
                    var currentYear = yearChanger.GetCurrentExeYear(labelFilename.Text);
                    if (currentYear != (int)numericGameStartYear.Value)
                    {
                        if (!File.Exists(staffFile) || !File.Exists(indexFile))
                        {
                            MessageBox.Show("staff.dat or index.dat not found in Data directory. Aborting year change.", "Files Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        var yesNo = MessageBox.Show("The Start Year Changer updates staff.dat and other files in the Data directory with the correct years as well as the cm0102.exe. Are you happy to proceed?", "CM0102Patcher - Year Changer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (yesNo == DialogResult.No)
                            return;
                        int yearIncrement = (((int)numericGameStartYear.Value) - currentYear);
                        yearChanger.ApplyYearChangeToExe(labelFilename.Text, (int)numericGameStartYear.Value);
                        yearChanger.UpdateStaff(indexFile, staffFile, yearIncrement);
                        yearChanger.UpdatePlayerConfig(playerConfigFile, yearIncrement);

                        yearChanger.UpdateHistoryFile(staffCompHistoryFile, 0x3a, yearIncrement, 0x8, 0x30);
                        yearChanger.UpdateHistoryFile(clubCompHistoryFile, 0x1a, yearIncrement, 0x8);
                        yearChanger.UpdateHistoryFile(staffHistoryFile, 0x11, yearIncrement, 0x8);
                        yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, yearIncrement, 0x8);
                    }
                }
                catch (Exception ex)
                {
                    ExceptionMsgBox.Show(ex);
                    return;
                }
            }

            // Patches
            if (checkBoxEnableColouredAtts.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["colouredattributes"]);
            if (checkBoxIdleSensitivity.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["idlesensitivity"]);
            if (checkBoxHideNonPublicBids.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["hideprivatebids"]);
            if (checkBox7Subs.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["sevensubs"]);
            if (checkBoxShowStarPlayers.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["showstarplayers"]);
            if (checkBoxDisableUnprotectedContracts.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["disableunprotectedcontracts"]);
            if (checkBoxCDRemoval.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["disablecdremove"]);
            if (checkBoxDisableSplashScreen.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["disablesplashscreen"]);
            if (checkBoxAllowCloseWindow.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["allowclosewindow"]);
            if (checkBoxForceLoadAllPlayers.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["forceloadallplayers"]);
            if (checkBoxRegenFixes.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["regenfixes"]);
            if (checkBoxChangeResolution1280s800.Checked)
            {
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["to1280x800"]);
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);

                // Convert the core gfx
                RGNConverter.RGN2RGN(Path.Combine(dataDir, "DEFAULT_PIC.RGN"), Path.Combine(dataDir, "bkg1280_800.rgn"), 1280, 800);
                RGNConverter.RGN2RGN(Path.Combine(dataDir, "match.mbr"), Path.Combine(dataDir, "m800.mbr"), 126, 800);
                RGNConverter.RGN2RGN(Path.Combine(dataDir, "game.mbr"), Path.Combine(dataDir, "g800.mbr"), 126, 800);

                var yesNo = MessageBox.Show("Do you wish to convert your CM0102 Pictures directory to 1280x800 too?\r\n\r\nIf no, please turn off Background Changes in CM0102's Options else pictures will not appear correctly.\r\n\r\nIf yes, this takes a few moments.", "CM0102Patcher - Resolution Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (yesNo == DialogResult.Yes)
                {
                    var pf = new PictureConvertProgressForm(Path.Combine(dir, "Pictures"));
                    pf.ShowDialog();
                }
            }
            if (checkBoxFindAllPlayers.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["findallplayers"]);
            if (checkBoxJobsAbroadBoost.Checked)
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["jobsabroadboost"]);
            if (checkBoxNewRegenCode.Checked)
            {
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapaninewregencode"]);
                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
            }
            if (checkBoxUpdateNames.Checked)
            {
                var namePatcher = new NamePatcher(labelFilename.Text, dataDir);
                namePatcher.RunPatch();
            }

            // NOCD Crack
            if (checkBoxRemoveCDChecks.Checked)
            {
                NoCDPatch nocd = new NoCDPatch();
                var patched = nocd.PatchEXEFile(labelFilename.Text);
            }

            MessageBox.Show("Patched Successfully!", "CM0102 Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkBoxChangeStartYear_CheckedChanged(object sender, EventArgs e)
        {
            labelGameStartYear.Enabled = numericGameStartYear.Enabled = checkBoxChangeStartYear.Checked;
        }

        private void buttonTools_Click(object sender, EventArgs e)
        {
            Tools tools = new Tools(labelFilename.Text);
            tools.ShowDialog();
        }

        private void PatcherForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control &&
                (Control.ModifierKeys & Keys.Shift) == Keys.Shift &&
                e.KeyChar == (char)19) // S
            {
                checkBoxRemoveCDChecks.Checked = checkBoxRemoveCDChecks.Visible = true;
            }
        }

        public class ComboboxItem
        {
            public ComboboxItem(string Text, object Value)
            {
                this.Text = Text;
                this.Value = Value;
            }
            public string Text { get; set; }
            public object Value { get; set; }
            public override string ToString()
            {
                return Text;
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CM0102Patcher by Nick\r\n\r\nAll credit should go to the geniuses that found and shared their code and great patching work:\r\nTapani\r\nJohnLocke\r\nSaturn\r\nxeno\r\nMadScientist\r\nAnd so many others!\r\n\r\nThanks to everyone at www.champman0102.co.uk for keeping the game alive :)", "CM0102Patcher", MessageBoxButtons.OK, MessageBoxIcon.None);
        }
    }
}