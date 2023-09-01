using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class PatcherForm : Form
    {
        bool isTapani = false;
        bool shownTapaniWarning = false;
        public static UpdatingForm updatingForm;

        public PatcherForm()
        {
            InitializeComponent();

            try
            {
                updatingForm = new UpdatingForm();

                if (!string.IsNullOrEmpty(RegString.GetRegString()))
                {
                    var exeLocation = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                    if (!string.IsNullOrEmpty(exeLocation))
                    {
                        labelFilename.Text = Path.Combine(exeLocation, "cm0102.exe");
                    }
                }
                comboBoxGameSpeed.Items.AddRange(new ComboboxItem[]
                {
                    new ComboboxItem("don't modify", 0),
                    new ComboboxItem("x0.5", 20000),
                    new ComboboxItem("default", 10000),
                    new ComboboxItem("x2", 5000),
                    new ComboboxItem("x4", 2500),
                    new ComboboxItem("x8", 1250),
                    new ComboboxItem("x20", 500),
                    new ComboboxItem("x200", 50),
                    new ComboboxItem("Max", 1)
                });
                comboBoxGameSpeed.SelectedIndex = 4;

                // Set selectable leagues
                comboBoxReplacementLeagues.Items.Add("English National League North");
                comboBoxReplacementLeagues.Items.Add("English National League South");
                //comboBoxReplacementLeagues.Items.Add("English Southern Premier Central Division");

                // Screen Resolution
                comboBoxResolution.Items.AddRange(new ComboboxItem[]
                {
                    new ComboboxItem("720 x 480", new Point(720, 480)),
                    new ComboboxItem("800 x 600 (default)", new Point(800, 600)),
                    new ComboboxItem("1024 x 600", new Point(1024, 600)),
                    new ComboboxItem("1024 x 768", new Point(1024, 768)),
                    new ComboboxItem("1280 x 720", new Point(1280, 720)),
                    new ComboboxItem("1280 x 800 (recommended)", new Point(1280, 800)),
                    new ComboboxItem("1280 x 960", new Point(1280, 960)),
                    new ComboboxItem("1280 x 1024", new Point(1280, 1024)),
                    new ComboboxItem("1366 x 768", new Point(1366, 768)),
                    new ComboboxItem("1400 x 900", new Point(1400, 900)),
                    new ComboboxItem("1680 x 1050", new Point(1680, 1050)),
                    new ComboboxItem("1920 x 1080", new Point(1920, 1080)),
                });

                // Add AI Tactics to Combobox
                using (var zs = MiscFunctions.OpenZip("AITactics.zip"))
                {
                    var folders = zs.ReadCentralDir().FindAll(x => x.FilenameInZip.EndsWith("/"));
                    comboBoxReplaceAITactics.Items.AddRange(folders.Select(x => x.FilenameInZip.Substring(0, x.FilenameInZip.Length - 1)).ToArray());
                }

                // Set Default Start Year to this year if we're past July (else use last year) 
                var currentYear = DateTime.Now.Year;
                if (DateTime.Now.Month < 7)
                    currentYear--;
                numericGameStartYear.Value = currentYear;

                TapaniDetection();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetComboBox<T>(ComboBox comboBox, T value)
        {
            foreach (var item in comboBox.Items)
            {
                var castItem = (item as ComboboxItem);
                var itemValue = (T)Convert.ChangeType(castItem.Value, typeof(T));
                if (itemValue.Equals(value))
                {
                    comboBox.SelectedItem = item;
                }
            }
        }

        private void CheckForOriginalCode(string exeFile, CheckBox checkbox, string reversalPatch)
        {
            var patcher = new Patcher();
            if (checkbox.Checked == false && !patcher.DetectPatch(exeFile, patcher.ReversePatches[reversalPatch]))
            {
                checkbox.Checked = true;
                checkbox.Enabled = false;
            }
        }

        private void TapaniDetection()
        {
            try
            {
                var windowText = " - (TAPANI EXE DETECTED)";
                var exeFile = labelFilename.Text;

                if (string.IsNullOrEmpty(exeFile))
                    return;

                // Use the pattern finder in the NoCD patcher
                isTapani = false;
                NoCDPatch.FindPattern(exeFile, Encoding.ASCII.GetBytes("Tapani v"), (file, br, bw, offset) => { isTapani = true; });
                if (!isTapani)
                    NoCDPatch.FindPattern(exeFile, Encoding.ASCII.GetBytes("TapaniPatch"), (file, br, bw, offset) => { isTapani = true; });
                if (!isTapani)
                    NoCDPatch.FindPattern(exeFile, Encoding.ASCII.GetBytes("Tapani 2"), (file, br, bw, offset) => { isTapani = true; });
                this.Text = this.Text.Replace(windowText, "");
                if (isTapani)
                {
                    this.Text += windowText;
                }
                ResetControls(this);
                checkBoxChangeStartYear.Enabled = !isTapani;
                checkBoxIdleSensitivity.Enabled = !isTapani;
                checkBoxDisableSplashScreen.Enabled = !isTapani;
                checkBox7Subs.Enabled = !isTapani;
                checkBoxAllowCloseWindow.Enabled = !isTapani;
                checkBoxShowStarPlayers.Enabled = !isTapani;
                checkBoxRegenFixes.Enabled = !isTapani;
                checkBoxJobsAbroadBoost.Enabled = !isTapani;
                checkBoxRemove3NonEULimit.Enabled = !isTapani;
                checkBoxReplaceWelshPremier.Enabled = !isTapani;
                checkBoxNewRegenCode.Enabled = !isTapani;
                checkBoxManageAnyTeam.Enabled = !isTapani;
                checkBoxUpdateNames.Enabled = !isTapani;
                checkBoxApplyYearSpecificPatches.Enabled = !isTapani;
                checkBoxSwapSKoreaForChina.Enabled = !isTapani;
                checkBoxPositionInTacticsView.Enabled = !isTapani;
                checkBoxMakeYourPotential200.Enabled = !isTapani;
                checkBoxChangeStartYear_CheckedChanged(null, null);
                numericGameStartYear_ValueChanged(null, null);

                // Reset options that might have been locked from a previous exe load
                checkBoxRestrictTactics.Enabled = true;
                comboBoxReplacementLeagues.Enabled = false;
                comboBoxReplacementLeagues.SelectedIndex = -1;

                if (isTapani && !shownTapaniWarning)
                {
                    MessageBox.Show("You have selected to patch an exe that has already been patched by a Tapani/Saturn patch\r\n\r\nSome options will be greyed out because they are either already enabled by the Tapani/Saturn patch or because they are not applyable to a Tapani/Saturn executable", "Tapani/Saturn Exe Detected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    shownTapaniWarning = true;
                }

                YearChanger yearChanger = new YearChanger();
                var detectedYear = yearChanger.GetCurrentExeYear(exeFile);
                if (detectedYear > 1900 && detectedYear < 2500)
                    numericGameStartYear.Value = detectedYear;

                DetectAndSelectAITactics();

                var patcher = new Patcher();
                short speedHack;
                double currencyMultiplier;
                var appliedPatches = patcher.DetectPatches(exeFile, out speedHack, out currencyMultiplier);

                if (appliedPatches.Count != 0)
                {
                    checkBoxIdleSensitivity.Checked = isTapani || appliedPatches.Contains("idlesensitivity"); // Tapani implements it in a different way
                    checkBoxCDRemoval.Checked = appliedPatches.Contains("disablecdremove");
                    checkBoxDisableSplashScreen.Checked = appliedPatches.Contains("disablesplashscreen");
                    checkBox7Subs.Checked = appliedPatches.Contains("sevensubs");
                    checkBoxNewRegenCode.Checked = isTapani || appliedPatches.Contains("tapaninewregencode"); // Tapani implements it in a different way
                    checkBoxSwapSKoreaForChina.Checked = isTapani || appliedPatches.Contains("chinapatch"); // Tapani implements it in a different way (Is Saturn really)
                    checkBoxAllowCloseWindow.Checked = appliedPatches.Contains("allowclosewindow");

                    // For some, if patch isn't detected - but the normal code isn't there - don't let people patch it
                    CheckForOriginalCode(exeFile, checkBox7Subs, "sevensubs");
                    CheckForOriginalCode(exeFile, checkBoxNewRegenCode, "tapaninewregencode");
                    CheckForOriginalCode(exeFile, checkBoxSwapSKoreaForChina, "chinapatch");
                    CheckForOriginalCode(exeFile, checkBoxAllowCloseWindow, "allowclosewindow");

                    checkBoxShowStarPlayers.Checked = appliedPatches.Contains("showstarplayers");
                    checkBoxRegenFixes.Checked = isTapani || appliedPatches.Contains("regenfixes"); // Tapani implements it in a different way
                    checkBoxJobsAbroadBoost.Checked = isTapani || appliedPatches.Contains("jobsabroadboost"); // Tapani implements it in a different way
                    checkBoxRemove3NonEULimit.Checked = appliedPatches.Contains("remove3playerlimit");
                    checkBoxManageAnyTeam.Checked = appliedPatches.Contains("manageanyteam");
                    checkBoxUpdateNames.Checked = isTapani || appliedPatches.Contains("transferwindowpatchdetect"); // Tapani implements it in a different way
                    checkBoxPositionInTacticsView.Checked = appliedPatches.Contains("positionintacticsview");
                    checkBoxMakeYourPotential200.Checked = isTapani || appliedPatches.Contains("makeyourpotential200"); // Tapani implements it in a different way

                    // These are irreversible
                    if (checkBoxUpdateNames.Checked)
                        checkBoxUpdateNames.Enabled = false;
                    if (checkBoxSwapSKoreaForChina.Checked)
                        checkBoxSwapSKoreaForChina.Enabled = false;
                    if (appliedPatches.Contains("englishleaguenorthpatch") || appliedPatches.Contains("englishleaguesouthawards") || appliedPatches.Contains("englishleaguenorthawards"))
                    {
                        string patchedLeague;
                        using (var fin = File.Open(exeFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        using (var br = new BinaryReader(fin))
                        {
                            fin.Seek(0x6d56b8, SeekOrigin.Begin);
                            patchedLeague = MiscFunctions.GetTextFromBytes(br.ReadBytes(30));
                        }
                        comboBoxReplacementLeagues.SelectedIndex = -1;

                        switch (patchedLeague)
                        {
                            case "English National League North":
                                comboBoxReplacementLeagues.SelectedIndex = 0;
                                break;
                            case "English National League South":
                                comboBoxReplacementLeagues.SelectedIndex = 1;
                                break;
                            case "English Southern Premier Central":
                                comboBoxReplacementLeagues.SelectedIndex = 2;
                                break;
                        }

                        comboBoxReplacementLeagues.Enabled = false;
                        checkBoxReplaceWelshPremier.Enabled = false;
                    }

                    if (isTapani)
                    {
                        checkBoxReplaceWelshPremier.Checked = true;
                        comboBoxReplacementLeagues.SelectedItem = "English National League North";
                        comboBoxReplacementLeagues.Enabled = false;
                    }

                    // Tapani Selectable
                    checkBoxEnableColouredAtts.Checked = appliedPatches.Contains("colouredattributes");
                    checkBoxDisableUnprotectedContracts.Checked = appliedPatches.Contains("disableunprotectedcontracts");
                    checkBoxHideNonPublicBids.Checked = appliedPatches.Contains("hideprivatebids");
                    checkBoxForceLoadAllPlayers.Checked = appliedPatches.Contains("forceloadallplayers");
                    checkBoxMakeExecutablePortable.Checked = (appliedPatches.Contains("changeregistrylocation") && appliedPatches.Contains("memorycheckfix") && appliedPatches.Contains("removemutexcheck"));
                    checkBoxRestrictTactics.Checked = appliedPatches.Contains("restricttactics");
                    if (checkBoxRestrictTactics.Checked)
                        checkBoxRestrictTactics.Enabled = false;

                    // Check for year specific patches
                    if (patcher.DetectPatch(exeFile, new List<Patcher.HexPatch> { new Patcher.HexPatch(0x2390C7, "01") }))
                    {
                        checkBoxApplyYearSpecificPatches.Checked = true;
                        checkBoxApplyYearSpecificPatches.Enabled = false;
                    }

                    // Don't let 
                    checkBoxShowHiddenAttributes.Checked = appliedPatches.Contains("addadditionalcolumns") || appliedPatches.Contains("addadditionalcolumns_italy");
                    checkBoxBugFixes.Checked = appliedPatches.Contains("bugfixes");

                    SetComboBox(comboBoxGameSpeed, speedHack);
                    numericCurrencyInflation.Value = (decimal)currencyMultiplier;

                    int resWidth, resHeight;
                    ResolutionChanger.GetResolution(exeFile, out resWidth, out resHeight);
                    SetComboBox(comboBoxResolution, new Point(resWidth, resHeight));
                    checkBoxChangeResolution.Checked = !(resWidth == 800 && resHeight == 600);
                }
                else
                {
                    var result = MessageBox.Show("You are patching an executable that has not been patched before.\r\n\r\nWould you like the patcher to suggest some options to apply?", "Default Options?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        checkBoxEnableColouredAtts.Checked = true;
                        checkBoxDisableUnprotectedContracts.Checked = true;
                        checkBoxDisableSplashScreen.Checked = true;
                        checkBoxCDRemoval.Checked = true;
                        checkBox7Subs.Checked = true;
                        checkBoxRegenFixes.Checked = true;
                        checkBoxIdleSensitivity.Checked = true;
                        checkBoxAllowCloseWindow.Checked = true;
                        checkBoxShowStarPlayers.Checked = true;
                        checkBoxJobsAbroadBoost.Checked = true;
                        checkBoxRemove3NonEULimit.Checked = true;
                        checkBoxMakeExecutablePortable.Checked = true;
                        checkBoxPositionInTacticsView.Checked = true;
                        numericCurrencyInflation.Value = 2.5m;
                        comboBoxGameSpeed.SelectedIndex = 4;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open executable for patching!\r\n\r\nException:\r\n" + ex.Message, "Error Opening File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelFilename.Text = "";
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "CM0102.exe Files|*.exe|All files (*.*)|*.*";
            ofd.Title = "Select a CM0102.exe file";
            try
            {
                if (!string.IsNullOrEmpty(RegString.GetRegString()))
                {
                    var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                    if (!string.IsNullOrEmpty(path))
                        ofd.InitialDirectory = path;
                }
            }
            catch { }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                labelFilename.Text = ofd.FileName;
                TapaniDetection();
            }
        }

        private int yearExeSyncDecrement = 0;
        private void buttonApply_Click(object sender, EventArgs e)
        {
            try
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

                // Check for Restore Point
                // if on Mono - don't try this
                if (!Program.RunningInMono())
                {
                    if (!RestorePoint.CheckForRestorePoint(labelFilename.Text))
                    {
                        var result = MessageBox.Show("You have not yet made a Restore Point.\r\n\r\nRestore Points allow you to revert any changes you have made by clicking Restore in the patcher.\r\n\r\nWould you like to make one now before you apply changes?", "Make a Restore Point?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                            RestorePoint.Save(labelFilename.Text);
                        if (result == DialogResult.Cancel)
                            return;
                    }
                }

                // Warn about irreversible patches
                if (!isTapani && (checkBoxUpdateNames.Checked && checkBoxUpdateNames.Enabled) || (checkBoxReplaceWelshPremier.Checked && checkBoxReplaceWelshPremier.Enabled) || (checkBoxSwapSKoreaForChina.Checked && checkBoxSwapSKoreaForChina.Enabled))
                {
                    string options = "";
                    if (checkBoxUpdateNames.Checked && checkBoxUpdateNames.Enabled)
                        options += "Update Names + Transfer Windows\r\n";
                    if (checkBoxReplaceWelshPremier.Checked && checkBoxReplaceWelshPremier.Enabled)
                        options += "Replace Welsh League\r\n";
                    if (checkBoxSwapSKoreaForChina.Checked && checkBoxSwapSKoreaForChina.Enabled)
                        options += "Swap South Korea for China\r\n";
                    var result = MessageBox.Show(string.Format("The following options you have selected are irreversible:\r\n\r\n{0}\r\nMeaning that once applied you will not be able unapply them.\r\n\r\nTo unapply them you will have to do a \"Restore\" from a previous \"Save\" point made by this patcher or a reinstall of CM0102.\r\n\r\nDo you wish to continue?", options), "Irreversible Changes Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.No)
                        return;
                }

                using (var fileLock = File.Open(labelFilename.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
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

                    // We now always expand the EXE!
                    patcher.ExpandExe(labelFilename.Text);
                    Logger.Log(labelFilename.Text, "Applying to {0} using {1} (isTapani: {2})", labelFilename.Text, this.Text, isTapani.ToString());

                    // Log the MD5 of the DB if you can find it
                    try
                    {
                        var staffFile = Path.Combine(dataDir, "staff.dat");
                        string staffFileHash = "";
                        using (var md5 = MD5.Create())
                        using (var staffFileStream = File.Open(labelFilename.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                            staffFileHash = BitConverter.ToString(md5.ComputeHash(staffFileStream)).Replace("-", "");
                        if (!string.IsNullOrEmpty(staffFileHash))
                            Logger.Log(labelFilename.Text, "MD5 Hash of staff.dat: {0}", staffFileHash);
                    }
                    catch { }

                    // Initialise the name patcher
                    var namePatcher = new NamePatcher(labelFilename.Text, dataDir);

                    // Game speed hack
                    var speed = (short)(int)(comboBoxGameSpeed.SelectedItem as ComboboxItem).Value;
                    if (speed != 0)
                        patcher.SpeedHack(labelFilename.Text, speed);

                    // Currency Inflation
                    if (numericCurrencyInflation.Value != 0)
                        patcher.CurrencyInflationChanger(labelFilename.Text, (double)numericCurrencyInflation.Value);

                    // Year Change
                    if (checkBoxChangeStartYear.Checked)
                    {
                        // Check staff.dat file to see if it's original data
                        bool forceOldMethod = false;
                        var staffFile = Path.Combine(dataDir, "staff.dat");
                        if (File.Exists(staffFile))
                        {
                            using (var fin = File.Open(staffFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                            {
                                fin.Seek(0x24, SeekOrigin.Begin);
                                var typeByte = fin.ReadByte();
                                if (typeByte == 0xfe)
                                {
                                    var result = MessageBox.Show("This looks like you are changing the date on the original database rather than an update?\r\n\r\nIf so the player's birth years will not update unless you use the old methodology.\r\nDo you want to use the old date changing methodology instead?", "ODB Detected", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                    if (result == DialogResult.Yes)
                                        forceOldMethod = true;
                                }
                            }
                        }

                        if (Control.ModifierKeys == Keys.Shift)
                            forceOldMethod = true;

                        var indexFile = Path.Combine(dataDir, "index.dat");
                        var playerConfigFile = Path.Combine(dataDir, "player_setup.cfg");
                        var staffCompHistoryFile = Path.Combine(dataDir, "staff_comp_history.dat");
                        var clubCompHistoryFile = Path.Combine(dataDir, "club_comp_history.dat");
                        var staffHistoryFile = Path.Combine(dataDir, "staff_history.dat");
                        var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");

                        if (forceOldMethod || ((int)numericGameStartYear.Value) < 2001)
                        {
                            // Old Version
                            try
                            {
                                Logger.Log(labelFilename.Text, "Changing Year to {0} (OLD VERSION!)", ((int)numericGameStartYear.Value).ToString());

                                // Reverse out any date patches (this could break Tapani!!)
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["datecalcpatch"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["datecalcpatchjumps"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["comphistory_datecalcpatch"]);
                                
                                YearChanger yearChanger = new YearChanger();
                                var currentYear = yearChanger.GetCurrentExeYear(labelFilename.Text);
                                if (currentYear != (int)numericGameStartYear.Value)
                                {
                                    if (!File.Exists(staffFile) || !File.Exists(indexFile))
                                    {
                                        MessageBox.Show("staff.dat or index.dat not found in Data directory. Aborting year change.", "Files Missing", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        return;
                                    }
                                    var yesNo = MessageBox.Show("The Start Year Changer updates staff.dat and other files in the Data directory with the correct years as well as the cm0102.exe.\r\n\r\nThis should only be done on a fresh exe that has not already been patched!\r\n(else you may get issues with player ages!)\r\n\r\nAre you happy to proceed?", "CM0102Patcher - Year Changer", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (yesNo == DialogResult.No)
                                        return;

                                    int yearIncrement = (((int)numericGameStartYear.Value) - currentYear);

                                    // e.g. When using 2018 data, to make it 2019 birth dates, etc
                                    yearIncrement -= yearExeSyncDecrement;

                                    yearChanger.ApplyYearChangeToExe(labelFilename.Text, (int)numericGameStartYear.Value);

                                    if (Control.ModifierKeys != Keys.Control)
                                    {
                                        yearChanger.UpdateStaff(indexFile, staffFile, yearIncrement);
                                        yearChanger.UpdatePlayerConfig(playerConfigFile, yearIncrement);

                                        yearChanger.UpdateHistoryFile(staffCompHistoryFile, 0x3a, yearIncrement, 0x8, 0x30);
                                        yearChanger.UpdateHistoryFile(staffHistoryFile, 0x11, yearIncrement, 0x8);

                                        yearChanger.UpdateHistoryFile(clubCompHistoryFile, 0x1a, yearIncrement, 0x8);

                                        yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, yearIncrement + 1, 0x8);

                                    }
                                    else
                                    {
                                        MessageBox.Show("Because Control was held down - did not update Data files!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                ExceptionMsgBox.Show(ex);
                                return;
                            }
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "Changing Year to {0} (NEW VERSION!)", ((int)numericGameStartYear.Value).ToString());

                            // New EXE Version
                            YearChanger yearChanger = new YearChanger();
                            var currentYear = yearChanger.GetCurrentExeYear(labelFilename.Text);
                            if (currentYear != (int)numericGameStartYear.Value)
                            {
                                int yearIncrement = (((int)numericGameStartYear.Value) - currentYear);
                                yearChanger.ApplyYearChangeToExe(labelFilename.Text, (int)numericGameStartYear.Value);
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["datecalcpatch"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["datecalcpatchjumps"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["comphistory_datecalcpatch"]);
                            }
                        }
                    }

                    // Patches
                    if (checkBoxEnableColouredAtts.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying ColouredAttributes");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["colouredattributes"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying ColouredAttributes");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["colouredattributes"]);
                    }

                    if (checkBoxHideNonPublicBids.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying hideprivatebids");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["hideprivatebids"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying disableunprotectedcontracts");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["hideprivatebids"]);
                    }

                    if (checkBoxDisableUnprotectedContracts.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying disableunprotectedcontracts");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["disableunprotectedcontracts"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying disableunprotectedcontracts");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["disableunprotectedcontracts"]);
                    }

                    if (checkBoxForceLoadAllPlayers.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying forceloadallplayers");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["forceloadallplayers"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying forceloadallplayers");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["forceloadallplayers"]);
                    }

                    if (checkBoxRestrictTactics.Enabled)
                    {
                        if (checkBoxRestrictTactics.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying restricttactics");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["restricttactics"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["changegeneraldat"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["changenamecolour"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying restricttactics");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["restricttactics"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["changegeneraldat"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["changenamecolour"]);
                        }
                    }

                    if (checkBoxMakeExecutablePortable.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying MakeExecutablePortable");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["changeregistrylocation"]);
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["memorycheckfix"]);
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["removemutexcheck"]);
                        patcher.ApplyPatch(labelFilename.Text, new List<Patcher.HexPatch> { new Patcher.HexPatch("APPLYMISCPATCH", "Misc Patches/MakeExeRequireAdmin.patch", null) });
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying MakeExecutablePortable");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["changeregistrylocation"]);
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["memorycheckfix"]);
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["removemutexcheck"]);
                        patcher.UnApplyPatch(labelFilename.Text, new List<Patcher.HexPatch> { new Patcher.HexPatch("APPLYMISCPATCH", "Misc Patches/MakeExeRequireAdmin.patch", null) });
                    }

                    if (checkBoxShowHiddenAttributes.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying addadditionalcolumns");
                        patcher.ExpandExe(labelFilename.Text);
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["addadditionalcolumns"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying addadditionalcolumns");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["addadditionalcolumns"]);
                    }

                    if (checkBoxBugFixes.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying Bug Fixes");
                        patcher.ExpandExe(labelFilename.Text);
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["bugfixes"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying Bug Fixes");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["bugfixes"]);
                    }

                    if (checkBoxCDRemoval.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying disablecdremove");
                        patcher.ApplyPatch(labelFilename.Text, patcher.patches["disablecdremove"]);
                    }
                    else
                    {
                        Logger.Log(labelFilename.Text, "UnApplying disablecdremove");
                        patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["disablecdremove"]);
                    }

                    if (!isTapani)
                    {
                        if (checkBoxIdleSensitivity.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying idlesensitivity");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["idlesensitivity"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["idlesensitivitytransferscreen"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["idlesensitivitytransferstatusscreen"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["idlesensitivitydisciplinescreen"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying idlesensitivity");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["idlesensitivity"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["idlesensitivitytransferscreen"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["idlesensitivitytransferstatusscreen"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["idlesensitivitydisciplinescreen"]);
                        }

                        if (checkBox7Subs.Enabled)
                        {
                            if (checkBox7Subs.Checked)
                            {
                                Logger.Log(labelFilename.Text, "Applying 7subs");
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["sevensubs"]);
                            }
                            else
                            {
                                Logger.Log(labelFilename.Text, "UnApplying 7subs");
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["sevensubs"]);
                            }
                        }

                        if (checkBoxShowStarPlayers.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying showstarplayers");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["showstarplayers"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying showstarplayers");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["showstarplayers"]);
                        }

                        if (checkBoxDisableSplashScreen.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying disablesplashscreen");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["disablesplashscreen"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying disablesplashscreen");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["disablesplashscreen"]);
                        }

                        if (checkBoxAllowCloseWindow.Enabled)
                        {
                            if (checkBoxAllowCloseWindow.Checked)
                            {
                                Logger.Log(labelFilename.Text, "Applying allowclosewindow");
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["allowclosewindow"]);
                            }
                            else
                            {
                                Logger.Log(labelFilename.Text, "UnApplying allowclosewindow");
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["allowclosewindow"]);
                            }
                        }

                        if (checkBoxRegenFixes.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying regenfixes");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["regenfixes"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying regenfixes");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["regenfixes"]);
                        }

                        if (checkBoxJobsAbroadBoost.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying jobsabroadboost");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["jobsabroadboost"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying jobsabroadboost");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["jobsabroadboost"]);
                        }

                        if (checkBoxManageAnyTeam.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying manageanyteam");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["manageanyteam"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying manageanyteam");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["manageanyteam"]);
                        }

                        if (checkBoxRemove3NonEULimit.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying remove3playerlimit");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["remove3playerlimit"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying remove3playerlimit");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["remove3playerlimit"]);
                        }

                        if (checkBoxNewRegenCode.Enabled)
                        {
                            if (checkBoxNewRegenCode.Checked)
                            {
                                Logger.Log(labelFilename.Text, "Applying tapaninewregencode");
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapaninewregencode"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                            }
                            else
                            {
                                Logger.Log(labelFilename.Text, "UnApplying tapaninewregencode");
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["tapaninewregencode"]);
                            }
                        }

                        if (checkBoxMakeYourPotential200.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying MakeYourPotential200");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["makeyourpotential200"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying MakeYourPotential200");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["makeyourpotential200"]);
                        }

                        if (checkBoxPositionInTacticsView.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying PositionInTacticsView");
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["positionintacticsview"]);
                        }
                        else
                        {
                            Logger.Log(labelFilename.Text, "UnApplying PositionInTacticsView");
                            patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["positionintacticsview"]);
                        }

                        // Irreversible, only try and apply it if the checkbox is Enabled
                        if (checkBoxUpdateNames.Checked && checkBoxUpdateNames.Enabled)
                        {
                            Logger.Log(labelFilename.Text, "Applying NamePatch");
                            namePatcher.RunPatch();
                        }

                        // Irreversible, only try and apply it if the checkbox is Enabled
                        if (checkBoxReplaceWelshPremier.Checked && checkBoxReplaceWelshPremier.Enabled)
                        {
                            patcher.ExpandExe(labelFilename.Text);
                            switch (comboBoxReplacementLeagues.SelectedIndex)
                            {
                                case 0:
                                    Logger.Log(labelFilename.Text, "Applying PatchWelshWithNorthernLeague");
                                    namePatcher.PatchWelshWithNorthernLeague();
                                    break;
                                case 1:
                                    Logger.Log(labelFilename.Text, "Applying PatchWelshWithSouthernLeague");
                                    namePatcher.PatchWelshWithSouthernLeague();
                                    break;
                                case 2:
                                    Logger.Log(labelFilename.Text, "Applying PatchWelshWithSouthernPremierCentral");
                                    namePatcher.PatchWelshWithSouthernPremierCentral();
                                    break;
                            }

                            // Correct the FA Cup code with Saturn v9's when this is done (this will have the required Tapani 00601D94 func)
                            patcher.ApplyPatch(labelFilename.Text, new List<Patcher.HexPatch> { new Patcher.HexPatch("APPLYMISCPATCH", "Misc Patches/[HIDDEN} 3.9.68 to Saturn 9 - FA Cup Changes.patch", null) });
                        }
                        
                        // Irreversible, only try and apply it if the checkbox is Enabled
                        if (checkBoxSwapSKoreaForChina.Checked && checkBoxSwapSKoreaForChina.Enabled)
                        {
                            patcher.ExpandExe(labelFilename.Text);
                            patcher.ApplyPatch(labelFilename.Text, patcher.patches["chinapatch"]);
                            namePatcher.FindFreePos();
                            namePatcher.PatchStaffAward("South Korean Best 11 Of The Year", "Chinese Super League Best XI", true, true);
                            namePatcher.PatchStaffAward("South Korean Most Assisted Player Of The Year", "Chinese Super League Top Assistor", true, true);
                            namePatcher.PatchStaffAward("South Korean Top Goal Scorer Of The Year", "Chinese Super League Top Scorer", true, true);
                            namePatcher.PatchStaffAward("South Korean Young Player Of The Year", "Super League Young Player Of the Year", true, true);
                            namePatcher.PatchStaffAward("South Korean Manager Of The Year", "Super League Manager Of the Year", true, true);
                            namePatcher.PatchStaffAward("South Korean Player Of The Month", "Super League Player Of the Month", true, true);
                            namePatcher.PatchStaffAward("South Korean Player Of The Year", "Super League Player Of The Year", true, true);
                            namePatcher.PatchComp("club_comp.dat", "Chinese First Division A", "Chinese Super League", "First Division A", "Super League", "CSL");

                            Logger.Log(labelFilename.Text, "Applying SwapSKoreaForChina");
                        }
                    }

                    if (checkBoxChangeResolution.Checked)
                    {
                        int newWidth = ((Point)((comboBoxResolution.SelectedItem as ComboboxItem).Value)).X;
                        int newHeight = ((Point)((comboBoxResolution.SelectedItem as ComboboxItem).Value)).Y;

                        int oldWidth, oldHeight;
                        ResolutionChanger.GetResolution(labelFilename.Text, out oldWidth, out oldHeight);

                        // Check we should change the resolution
                        if (newWidth != oldWidth && newHeight != oldHeight)
                        {

                            if (newWidth == 800 && newHeight == 600)
                            {
                                // If 800x600 - revert
                                patcher.ApplyPatch(labelFilename.Text, patcher.ReversePatches["to1280x800"]);
                            }
                            else
                            {
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["to1280x800"]);
                                patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                                ResolutionChanger.SetResolution(labelFilename.Text, newWidth, newHeight);
                            }

                            Logger.Log(labelFilename.Text, "Applying Resolution Change: {0}x{1}", newWidth.ToString(), newHeight.ToString());

                            // Convert the core gfx
                            int menuWidth = newWidth > 800 ? 126 : 90;
                            RGNConverter.RGN2RGN(Path.Combine(dataDir, "DEFAULT_PIC.RGN"), Path.Combine(dataDir, "bkg1280_800.rgn"), newWidth, newHeight);

                            // For Alan + Cam F and the like who hate menu bars :)
                            if (File.Exists(Path.Combine(dataDir, "match.mbr")))
                                RGNConverter.RGN2RGN(Path.Combine(dataDir, "match.mbr"), Path.Combine(dataDir, "m800.mbr"), menuWidth, newHeight); // 800 => 90 - 1280 => 126
                            if (File.Exists(Path.Combine(dataDir, "game.mbr")))
                                RGNConverter.RGN2RGN(Path.Combine(dataDir, "game.mbr"), Path.Combine(dataDir, "g800.mbr"), menuWidth, newHeight);

                            var picturesDir = Path.Combine(dir, "Pictures");

                            if (Directory.Exists(picturesDir))
                            {
                                var yesNo = MessageBox.Show(string.Format("Do you wish to convert your CM0102 Pictures directory to {0}x{1} too?\r\n\r\nIf no, please turn off Background Changes in CM0102's Options else pictures will not appear correctly.\r\n\r\nIf yes, this takes a few moments.", newWidth, newHeight), "CM0102Patcher - Resolution Change", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                if (yesNo == DialogResult.Yes)
                                {
                                    Logger.Log(labelFilename.Text, "Converting Pictures...");
                                    var pf = new PictureConvertProgressForm();

                                    pf.OnLoadAction = () =>
                                    {
                                        new Thread(() =>
                                        {
                                            var lastPic = "";
                                            try
                                            {
                                                int converting = 1;
                                                Thread.CurrentThread.IsBackground = true;

                                                var picFiles = Directory.GetFiles(picturesDir, "*.rgn");
                                                foreach (var picFile in picFiles)
                                                {
                                                    lastPic = picFile;
                                                    pf.SetProgressText(string.Format("Converting {0}/{1} ({2})", converting++, picFiles.Length, Path.GetFileName(picFile)));
                                                    pf.SetProgressPercent((int)(((double)(converting - 1) / ((double)picFiles.Length)) * 100.0));
                                                    int Width, Height;
                                                    if (RGNConverter.GetImageSize(picFile, out Width, out Height))
                                                    {
                                                        if (Width == 800 && Height == 600 && newWidth == 1280 && newHeight == 800)
                                                            RGNConverter.RGN2RGN(picFile, picFile + ".tmp", newWidth, newHeight, 0, 35, 0, 100);
                                                        else
                                                        {
                                                            RGNConverter.RGN2RGN(picFile, picFile + ".tmp", newWidth, newHeight);
                                                        }
                                                        File.SetAttributes(picFile, FileAttributes.Normal);
                                                        File.Delete(picFile);
                                                        File.Move(picFile + ".tmp", picFile);
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(string.Format("Failed when converting images!\r\nLast Pic: {0}", lastPic), "Image Convert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                                ExceptionMsgBox.Show(ex);
                                            }

                                            pf.CloseForm();
                                        }).Start();
                                    };

                                    pf.ShowDialog();
                                }
                            }
                        }
                    }

                    updatingForm.Show();

                    if (checkBoxReplaceAITactics.Checked && comboBoxReplaceAITactics.SelectedIndex >= 0)
                    {
                        var selectedPack = comboBoxReplaceAITactics.SelectedItem as string;

                        updatingForm.SetUpdateText(selectedPack);
                        Logger.Log(labelFilename.Text, "Applying Tactics Pack: {0}", selectedPack);

                        using (var zs = MiscFunctions.OpenZip("AITactics.zip"))
                        {
                            var files = zs.ReadCentralDir();
                            foreach (var file in files)
                            {
                                if (file.FilenameInZip.StartsWith(selectedPack) && !file.FilenameInZip.EndsWith("/"))
                                {
                                    var fileName = file.FilenameInZip.Substring(file.FilenameInZip.IndexOf('/')+1);
                                    var outputFile = Path.Combine(dataDir, fileName);

                                    // Make sure the file is writeable (File in 2020/1 April Update had Read Only files ?!
                                    File.SetAttributes(outputFile, FileAttributes.Normal);

                                    zs.ExtractFile(file, outputFile);
                                }
                            }
                        }
                    }

                    if (checkBoxApplyYearSpecificPatches.Checked && checkBoxApplyYearSpecificPatches.Enabled)
                    {
                        if (checkBoxUpdateNames.Checked)
                        {
                            Logger.Log(labelFilename.Text, "Applying Year Specific Patches for {0}!", ((int)numericGameStartYear.Value).ToString());
                            patcher.ApplyPatch(labelFilename.Text, new List<Patcher.HexPatch> { new Patcher.HexPatch("APPLYMISCPATCH", string.Format("{0} Patches/All Tested {0} + Saturn Patches.patch", (int)numericGameStartYear.Value), null) });
                        }
                        else
                        {
                            MessageBox.Show("Unable to apply Year Specific patches without having the Updated Names!", "Year Specific Patch Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    // NOCD Crack
                    if (checkBoxRemoveCDChecks.Checked)
                    {
                        Logger.Log(labelFilename.Text, "Applying NOCD Crack");
                        var patched = NoCDPatch.PatchEXEFile(labelFilename.Text);
                    }

                    updatingForm.Hide();
                    MessageBox.Show("Patched Successfully!", "CM0102 Patcher", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Logger.Log(labelFilename.Text, "Patched Successfully!");
                    TapaniDetection();
                }
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
            finally
            {
                if (updatingForm.Visible)
                    updatingForm.Hide();
            }
        }

        private void checkBoxChangeStartYear_CheckedChanged(object sender, EventArgs e)
        {
            labelGameStartYear.Enabled = numericGameStartYear.Enabled = checkBoxChangeStartYear.Checked;
        }

        private void buttonTools_Click(object sender, EventArgs e)
        {
            Tools tools = new Tools(labelFilename.Text);
            tools.ShowDialog();
            TapaniDetection();
        }

        private void ResetControls(Control controlContainer)
        {
            foreach (var control in controlContainer.Controls)
            {
                if (control is CheckBox)
                {
                    (control as CheckBox).Checked = false;
                }

                if (control is GroupBox || control is TabPage || control is TabControl)
                    ResetControls(control as Control);
            }
            numericCurrencyInflation.Value = 0;
            comboBoxGameSpeed.SelectedIndex = 0;
        }

        public static bool SecretMode = false;
        private void PatcherForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control &&
                (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                if (e.KeyChar == (char)19) // (S)ecret Mode
                {
                    checkBoxRemoveCDChecks.Checked = checkBoxRemoveCDChecks.Visible = true;
                    SecretMode = true;
                    e.Handled = true;
                }
                if (e.KeyChar == (char)14) // N - Blank out all fields
                {
                    ResetControls(this);
                    e.Handled = true;
                }
                if (e.KeyChar == (char)1 && SecretMode) // A
                {
                    string doubleWarning = labelFilename.Text.Contains("cm0001.exe") ? "" : "\r\n\r\nTHIS DOES NOT LOOK LIKE A CM0001.EXE!!!!!!!!!!\r\nDOUBLE CHECK BEFORE HITTING YES!!\r\n";
                    if (MessageBox.Show(string.Format("This will change exe:\r\n{0}\r\nTo Year: {1}\r\n\r\nARE YOU SURE YOU WANT TO DO THIS?!{2}", labelFilename.Text, (int)numericGameStartYear.Value, doubleWarning), "CM 00/01 Year Changer", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        var yearChanger = new YearChanger();
                        var currentYear = yearChanger.GetCurrentExeYear(labelFilename.Text, 0x0001009F);
                        yearChanger.ApplyYearChangeTo0001Exe(labelFilename.Text, (int)numericGameStartYear.Value);

                        var dir = Path.GetDirectoryName(labelFilename.Text);
                        var dataDir = Path.Combine(dir, "Data");
                        var staffFile = Path.Combine(dataDir, "staff.dat");
                        var indexFile = Path.Combine(dataDir, "index.dat");
                        var playerConfigFile = Path.Combine(dataDir, "player_setup.cfg");
                        var staffCompHistoryFile = Path.Combine(dataDir, "staff_comp_history.dat");
                        var clubCompHistoryFile = Path.Combine(dataDir, "club_comp_history.dat");
                        var staffHistoryFile = Path.Combine(dataDir, "staff_history.dat");
                        var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");

                        /*
                        // Update Data Too
                        int yearIncrement = (((int)numericGameStartYear.Value) - currentYear);
                        yearChanger.UpdateStaff(indexFile, staffFile, yearIncrement);
                        yearChanger.UpdatePlayerConfig(playerConfigFile, yearIncrement);

                        // Update History
                        yearChanger.UpdateHistoryFile(staffCompHistoryFile, 0x3a, yearIncrement, 0x8, 0x30);
                        yearChanger.UpdateHistoryFile(clubCompHistoryFile, 0x1a, yearIncrement, 0x8);
                        yearChanger.UpdateHistoryFile(staffHistoryFile, 0x11, yearIncrement, 0x8);
                        yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, yearIncrement + 1, 0x8);
                        */

                        MessageBox.Show("CM0001 Year Changer Patch Applied!", "CM 00/01 Year Changer", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    e.Handled = true;
                }
                if (e.KeyChar == (char)2 && SecretMode) // B
                {
                    var yearIncrement = 0;
                    var cutIfEqualOrAbove = 1999;

                    var yearChanger = new YearChanger();
                    var dir = Path.GetDirectoryName(labelFilename.Text);
                    var dataDir = Path.Combine(dir, "Data");

                    var staffFile = Path.Combine(dataDir, "staff.dat");
                    var indexFile = Path.Combine(dataDir, "index.dat");
                    var playerConfigFile = Path.Combine(dataDir, "player_setup.cfg");
                    var staffCompHistoryFile = Path.Combine(dataDir, "staff_comp_history.dat");
                    var clubCompHistoryFile = Path.Combine(dataDir, "club_comp_history.dat");
                    var staffHistoryFile = Path.Combine(dataDir, "staff_history.dat");
                    var nationCompHistoryFile = Path.Combine(dataDir, "nation_comp_history.dat");
                   
                    //yearChanger.UpdateStaff(indexFile, staffFile, yearIncrement);
                    //yearChanger.UpdatePlayerConfig(playerConfigFile, yearIncrement);

                    // Update History
                    //yearChanger.UpdateHistoryFile(staffCompHistoryFile, 0x3a, yearIncrement, 0x8, 0x30);
                    //yearChanger.UpdateHistoryFile(staffHistoryFile, 0x11, yearIncrement, 0x8, 0);

                    yearChanger.UpdateHistoryFile(clubCompHistoryFile, 0x1a, yearIncrement, 0x8, 0, cutIfEqualOrAbove, indexFile);
                    yearChanger.UpdateHistoryFile(nationCompHistoryFile, 0x1a, yearIncrement, 0x8, 0, cutIfEqualOrAbove, indexFile);

                    MessageBox.Show(string.Format("Patched Years To Jump By: {0} years", yearIncrement), "Data Increment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (e.KeyChar == (char)3 && SecretMode) // C
                {
                    NoCDPatch.PatchEXEFile0001FixV2(labelFilename.Text);
                }
                if (e.KeyChar == (char)4 && SecretMode) // D
                {
                    var yearChanger = new YearChanger();
                    var dir = Path.GetDirectoryName(labelFilename.Text);
                    var dataDir = Path.Combine(dir, "Data");
                    var staffFile = Path.Combine(dataDir, "staff.dat");
                    var indexFile = Path.Combine(dataDir, "index.dat");
                    yearChanger.UpdateStaff(indexFile, staffFile, 17);
                    MessageBox.Show("staff.dat updated");
                }
                if (e.KeyChar == (char)5 && SecretMode) // E
                {
                    var patcher = new Patcher();
                    patcher.CurrencyInflationChanger0001(labelFilename.Text, (double)numericCurrencyInflation.Value);
                    MessageBox.Show("CM0001 EXE Updated with new Inflation Multiplier!");
                }
                if (e.KeyChar == (char)6 && SecretMode) // F
                {
                    var yearChanger = new YearChanger();
                    var patcher = new Patcher();
                    using (var pp = new ProcessPatch())
                    {
                        var currentYear = yearChanger.GetCurrentExeYear(labelFilename.Text);
                        if (pp.LoadProcess(labelFilename.Text))
                        {
                            var ms = pp.ReadIn();
                            patcher.ApplyPatch(ms, patcher.patches["changeregistrylocation"]);
                            patcher.ApplyPatch(ms, patcher.patches["memorycheckfix"]);
                            patcher.ApplyPatch(ms, patcher.patches["removemutexcheck"]);
                            patcher.ApplyPatch(ms, patcher.patches["colouredattributes"]);
                            patcher.ApplyPatch(ms, patcher.patches["allowclosewindow"]);

                            yearChanger.ApplyYearChangeToExe(ms, (int)numericGameStartYear.Value);
                            patcher.ApplyPatch(ms, patcher.patches["datecalcpatch"]);
                            patcher.ApplyPatch(ms, patcher.patches["datecalcpatchjumps"]);
                            patcher.ApplyPatch(ms, patcher.patches["comphistory_datecalcpatch"]);

                            NoCDPatch.PatchMemoryStream(ms);
                            pp.Write();
                            pp.Start();
                        }
                    }
                }
                if (e.KeyChar == (char)7 && SecretMode) // G
                {
                    var patcher = new Patcher();
                    patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                }
                if (e.KeyChar == (char)8 && SecretMode) // H
                {
                    var yearChanger = new YearChanger();
                    yearChanger.ApplyYearChangeToExe(labelFilename.Text, (int)numericGameStartYear.Value);
                    MessageBox.Show("Forced Exe to Year: " + ((int)numericGameStartYear.Value).ToString(), "Forced Year", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (e.KeyChar == (char)9 && SecretMode) // I
                {
                    var patcher = new Patcher();
                    patcher.ExpandExe(labelFilename.Text);
                    MessageBox.Show("EXE EXPANDED!!!", "Expando Magico", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (e.KeyChar == (char)10 && SecretMode) // J
                {
                    NoCDPatch.GenericCDCrack2(labelFilename.Text);
                    NoCDPatch.SPBBGenericCrack(labelFilename.Text);
                    MessageBox.Show("Generic Crack 2 Applied", "Secret", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (e.KeyChar == (char)11 && SecretMode) // K
                {
                    var patcher = new Patcher();
                    patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanispacemaker"]);
                    patcher.ApplyPatch(labelFilename.Text, patcher.patches["tapanieurofix"]);
                    MessageBox.Show("Applied Euro Fix");
                }
                if (e.KeyChar == (char)12 && SecretMode) // L
                {
                    var fileBytes = ByteWriter.LoadFile(labelFilename.Text);
                    var offsets1 = ByteWriter.SearchBytesForAll(fileBytes, new byte[] { 0x68, 0xCE, 0x07, 0x00, 0x00 }, 0x57FA60 - 0x400000, false);
                    var offsets2 = ByteWriter.SearchBytesForAll(fileBytes, new byte[] { 0x68, 0xCF, 0x07, 0x00, 0x00 }, 0x57FA60 - 0x400000, false);

                    short offset1ToChangeTo = (short)(numericGameStartYear.Value - 2);
                    short offset2ToChangeTo = (short)(numericGameStartYear.Value - 1);

                    using (var file = File.Open(labelFilename.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        using (var bw = new BinaryWriter(file))
                        {
                            foreach (var offset in offsets1)
                            {
                                if (offset < (0x582EED - 0x400000))
                                {
                                    file.Seek(offset + 1, SeekOrigin.Begin);
                                    bw.Write(offset1ToChangeTo);
                                }
                            }
                            foreach (var offset in offsets2)
                            {
                                if (offset < (0x582EED - 0x400000))
                                {
                                    file.Seek(offset + 1, SeekOrigin.Begin);
                                    bw.Write(offset2ToChangeTo);
                                }
                            }

                            file.Seek((0x582F4A - 0x400000) + 2, SeekOrigin.Begin);
                            bw.Write(offset1ToChangeTo);
                            file.Seek((0x582F54 - 0x400000) + 2, SeekOrigin.Begin);
                            bw.Write(offset2ToChangeTo);
                        }
                    }

                    MessageBox.Show(string.Format("Moved Euro Quals to: {0} and {1}", offset1ToChangeTo, offset2ToChangeTo));
                }
                if (e.KeyChar == (char)31 && SecretMode) // -
                {
                    yearExeSyncDecrement = 1;
                    MessageBox.Show("Year Exe vs Data Sync Decrement Set to 1");
                }
            }
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("CM0102Patcher by Nick\r\n\r\nAll credit should go to the geniuses that found and shared their code and great patching work:\r\nTapani\r\nJohnLocke\r\nSaturn\r\nxeno\r\nMadScientist\r\nAnd so many others!\r\n\r\nThanks to everyone at www.champman0102.co.uk for keeping the game alive :)", "CM0102Patcher", MessageBoxButtons.OK, MessageBoxIcon.None);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            RestorePoint.Save(labelFilename.Text);
        }

        private void buttonRestore_Click(object sender, EventArgs e)
        {
            if (RestorePoint.Restore(labelFilename.Text))
                TapaniDetection();
        }

        private void checkBoxAddNorthernLeague_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxReplacementLeagues.Enabled = checkBoxReplaceWelshPremier.Checked;
            if (checkBoxReplaceWelshPremier.Checked && comboBoxReplacementLeagues.SelectedIndex == -1)
                comboBoxReplacementLeagues.SelectedIndex = 0;
        }

        private void checkBoxChangeResolution_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxResolution.Enabled = checkBoxChangeResolution.Checked;
            if (checkBoxChangeResolution.Checked && comboBoxResolution.SelectedIndex == -1)
                comboBoxResolution.SelectedIndex = 5;
        }

        private void checkBoxReplaceAITactics_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxReplaceAITactics.Enabled = checkBoxReplaceAITactics.Checked;
        }

        private void DetectAndSelectAITactics()
        {
            var selectedTactics = "Original 3.9.68 Tactics";

            try
            {
                if (!string.IsNullOrEmpty(labelFilename.Text))
                {
                    var compareTactic = "343_default.pct";
                    var dir = Path.GetDirectoryName(labelFilename.Text);
                    var dataDir = Path.Combine(dir, "Data");
                    var pct424defaultFile = Path.Combine(dataDir, compareTactic);

                    if (File.Exists(pct424defaultFile))
                    {
                        byte[] ourHash;
                        using (var outPCTFile = File.Open(pct424defaultFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                        {
                            using (var md5 = MD5.Create())
                                ourHash = md5.ComputeHash(outPCTFile);
                        }

                        using (var zs = MiscFunctions.OpenZip("AITactics.zip"))
                        {
                            var files = zs.ReadCentralDir();
                            foreach (var file in files)
                            {
                                if (file.FilenameInZip.EndsWith(compareTactic))
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        zs.ExtractFile(file, ms);
                                        ms.Position = 0;
                                        var md5 = MD5.Create();
                                        var hashBytes = md5.ComputeHash(ms);
                                        if (MiscFunctions.CompareByteArrays(hashBytes, ourHash))
                                        {
                                            var slash = file.FilenameInZip.IndexOf('/');
                                            if (slash != -1)
                                            {
                                                selectedTactics = file.FilenameInZip.Substring(0, slash);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            comboBoxReplaceAITactics.SelectedItem = selectedTactics;
        }

        private void numericGameStartYear_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int year = (int)numericGameStartYear.Value;
                switch (year)
                {
                    case 2020:
                    case 2021:
                    case 2022:
                        checkBoxApplyYearSpecificPatches.Enabled = true;
                        break;
                    default:
                        checkBoxApplyYearSpecificPatches.Enabled = false;
                        break;
                }
            }
            catch 
            {
                checkBoxApplyYearSpecificPatches.Enabled = false;
            }
        }

        private void numericGameStartYear_KeyUp(object sender, KeyEventArgs e)
        {
            if (numericGameStartYear.Text.Length == 4)
                numericGameStartYear_ValueChanged(sender, e);
            else
                checkBoxApplyYearSpecificPatches.Enabled = false;
        }

        private void checkBoxApplyYearSpecificPatches_Click(object sender, EventArgs e)
        {
            if (checkBoxApplyYearSpecificPatches.Checked && checkBoxUpdateNames.Enabled && !checkBoxUpdateNames.Checked)
                checkBoxUpdateNames.Checked = true;
        }

        private void checkBoxUpdateNames_Click(object sender, EventArgs e)
        {
            if (!checkBoxUpdateNames.Checked && checkBoxApplyYearSpecificPatches.Checked)
                checkBoxUpdateNames.Checked = true;
        }
    }

    public class ComboboxItem
    {
        public ComboboxItem()
        {
            Text = "";
            Value = null;
        }

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
        public ComboboxItem Self { get { return this; } }
        public string Description { get { return ToString(); } }
    }
}
