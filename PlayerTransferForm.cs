using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class PlayerTransferForm : Form
    {
        const string NoClub = "NO CLUB";
        const string LoanText = "[LOAN]";
        HistoryLoader hl = null;

        public PlayerTransferForm()
        {
            InitializeComponent();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Index.dat Files (index.dat)|index.dat|All files (*.*)|*.*";
            ofd.Title = "Select an index.dat file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxIndexFile.Text = ofd.FileName;
                LoadData();
            }
        }

        private void LoadData()
        {
            hl = new HistoryLoader();
            hl.Load(textBoxIndexFile.Text, false);
            // LoadLoans(textBoxIndexFile.Text);

            // Load the data into the listboxes
            listBoxTransfers.Items.Clear();

            RefreshPlayerData();
            RefreshTeamData();
        }

        void RefreshPlayerData()
        {
            var filterText = textBoxPlayerFilter.Text.ToLower();
            filterText = MiscFunctions.RemoveDiacritics(filterText);
            var staff = hl.staff.Where(x => x.Player != -1 && (filterText == "" || filterText.Length <= 3 || hl.staffNamesNoDiacritics[x.ID].ToLower().Contains(filterText))).OrderBy(x => hl.staffNames[x.ID]).ToList();

            listBoxPlayers.Items.Clear();
            labelAllPlayers.Text = string.Format("All Players ({0})", staff.Count);
            List<TransferListBoxItem> listBoxPlayerItems = new List<TransferListBoxItem>();
            foreach (var playing_staff in staff)
            {
                var name = hl.staffNames[playing_staff.ID];
                var club = NoClub;
                if (playing_staff.ClubJob != -1)
                    club = hl.club[playing_staff.ClubJob].Name.ReadString();
                var tlbi = new TransferListBoxItem(name, club, playing_staff.ID);
                listBoxPlayerItems.Add(tlbi);
            }
            listBoxPlayers.Items.AddRange(listBoxPlayerItems.ToArray());
            listBoxPlayers.ExternalScrollBar = vScrollBarPlayers;

            if (listBoxPlayers.Items.Count == 1)
                listBoxPlayers.SelectedIndex = 0;
        }

        void RefreshTeamData()
        {
            var filterText = textBoxTeamFilter.Text.ToLower();
            //filterText = MiscFunctions.RemoveDiacritics(filterText);
            var sortedClubs = hl.club.OrderBy(x => x.Name.ReadString()).Where(x => /*MiscFunctions.RemoveDiacritics*/(x.Name.ReadString()).ToLower().Contains(filterText)).ToArray();

            listBoxTeams.Items.Clear();
            labelAllTeams.Text = string.Format("Team To Transfer To ({0})", sortedClubs.Length);
            List<TransferListBoxItem> listBoxTeamItems = new List<TransferListBoxItem>();
            foreach (var club in sortedClubs)
            {
                listBoxTeamItems.Add(new TransferListBoxItem(club.Name.ReadString(), club.ID));
            }
            if (filterText == "" || NoClub.ToLower().Contains(filterText))
                listBoxTeams.Items.Add(new TransferListBoxItem(NoClub, -1));
            listBoxTeams.Items.AddRange(listBoxTeamItems.ToArray());
            listBoxTeams.ExternalScrollBar = vScrollBarTeams;

            if (listBoxTeams.Items.Count == 1)
                listBoxTeams.SelectedIndex = 0;
        }

        private void textBoxPlayerFilter_TextChanged(object sender, EventArgs e)
        {
            if (textBoxPlayerFilter.Text == "" || textBoxPlayerFilter.Text.Length > 3)
                RefreshPlayerData();
        }

        private void textBoxTeamFilter_TextChanged(object sender, EventArgs e)
        {
            RefreshTeamData();
        }

        private void Transfer(bool isLoan)
        {
            if (listBoxPlayers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a player!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listBoxTeams.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a team!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var selectedPlayer = listBoxPlayers.SelectedItem as TransferListBoxItem;
            var selectedTeam = listBoxTeams.SelectedItem as TransferListBoxItem;

            // Check team does not have 50 players already
            if (selectedTeam.ID != -1)
            {
                var teamPlayerCount = hl.staff.Where(x => x.Player != -1 && x.ClubJob == selectedTeam.ID).Count();
                teamPlayerCount += CountTransferredPlayersInTeam(selectedTeam.ToString());
                if (teamPlayerCount >= 50)
                {
                    MessageBox.Show(string.Format("Team {0} already has 50 players", selectedTeam.ToString()), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            listBoxTransfers.Items.Add(selectedPlayer.ToString() + " -> " + selectedTeam.ToString() + (isLoan ? (" " + LoanText) : ""));
            listBoxTransfers.TopIndex = listBoxTransfers.Items.Count - 1;   // Scroll to bottom
        }

        private void buttonTransfer_Click(object sender, EventArgs e)
        {
            Transfer(false);
        }

        private void buttonLoan_Click(object sender, EventArgs e)
        {
            Transfer(true);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxTransfers.SelectedIndex != -1)
            {
                listBoxTransfers.Items.RemoveAt(listBoxTransfers.SelectedIndex);
            }
        }

        private void buttonclear_Click(object sender, EventArgs e)
        {
            listBoxTransfers.Items.Clear();
        }

        private bool DecodeLine(string transferLine, out string playerName, out string clubFrom, out string clubTo, out bool loan)
        {
            bool ret = false;
            playerName = clubFrom = clubTo = null;
            loan = false;

            try
            {
                var components = transferLine.Split(new string[] { "->" }, StringSplitOptions.None);

                int bracket1 = components[0].IndexOf('(');
                int bracket2 = components[0].IndexOf(')');

                if (bracket1 != -1 && bracket2 != -1)
                {
                    playerName = components[0].Substring(0, bracket1 - 1);
                    clubFrom = components[0].Substring(bracket1 + 1, (bracket2 - bracket1) - 1);
                    clubTo = components[1];

                    if (clubTo.Contains(LoanText))
                    {
                        loan = true;
                        clubTo = clubTo.Replace(LoanText, "");
                    }

                    clubTo = clubTo.Trim(' ');

                    ret = true;
                }
            }
            catch { }

            return ret;
        }

        private int CountTransferredPlayersInTeam(string club)
        {
            int count = 0;
            for (int i = 0; i < listBoxTransfers.Items.Count; i++)
            {
                var transferLine = listBoxTransfers.Items[i].ToString();
                string playerName, clubFrom, clubTo;
                bool loan;
                if (DecodeLine(transferLine, out playerName, out clubFrom, out clubTo, out loan))
                {
                    if (clubTo.ToLower() == club.ToLower())
                        count++;
                    if (clubFrom.ToLower() == club.ToLower())
                        count--;
                }
            }
            return count;
        }

        private void buttonApplyChanges_Click(object sender, EventArgs e)
        {
            bool outputGoodPlayers = false;     // change to true output to Console those that would work

            bool success = true;
            List<ClubPlayerTuple> transfers = new List<ClubPlayerTuple>();
            for (int i = 0; i < listBoxTransfers.Items.Count; i++)
            {
                var transferLine = listBoxTransfers.Items[i].ToString();

                string playerName, clubFrom, clubTo;
                bool loan;
                if (DecodeLine(transferLine, out playerName, out clubFrom, out clubTo, out loan))
                {
                    try
                    {
                        var clubFromObj = hl.club.FirstOrDefault(x => x.Name.ReadString().ToLower() == clubFrom.ToLower());
                        var clubToObj = hl.club.FirstOrDefault(x => x.Name.ReadString().ToLower() == clubTo.ToLower());

                        string tempName, tempBasicName;
                        var playersFound = hl.staff.Where(x => (x.ClubJob == ((clubFrom.ToLower() == NoClub.ToLower()) ? -1 : clubFromObj.ID)) && hl.StaffToName(x, out tempName, out tempBasicName) == true && (tempName == playerName || tempBasicName == playerName || MiscFunctions.RemoveDiacritics(tempBasicName) == playerName || MiscFunctions.RemoveDiacritics(tempName) == playerName)).ToList();

                        if (playersFound.Count == 0)
                        {
                            if (!outputGoodPlayers)
                                MessageBox.Show(string.Format("Player ({0}) at club ({1}) can not be found", playerName, clubFrom), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            success = false;
                        }
                        else
                        if (playersFound.Count > 1)
                        {
                            if (!outputGoodPlayers)
                                MessageBox.Show(string.Format("Multiple Players named ({0}) found at club ({1})", playerName, clubFrom), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            success = false;
                        }
                        else
                        {
                            if (outputGoodPlayers)
                            {
                                Console.WriteLine(transferLine);
                            }
                            else
                                transfers.Add(new ClubPlayerTuple(playersFound[0].ID, clubFromObj == null ? -1 : clubFromObj.ID, clubTo.ToLower() == NoClub.ToLower() ? -1 : clubToObj.ID, loan));
                        }
                    }
                    catch
                    {
                        MessageBox.Show(string.Format("Unable to apply Transfer Line: ({0}) with components: ({1}) ({2}) ({3})", transferLine, playerName, clubFrom, clubTo), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        success = false;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("Unable to parse Transfer Line: ({0})", transferLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    success = false;
                }

                if (!outputGoodPlayers && !success)
                    break;
            }

            if (outputGoodPlayers)
                return;

            if (success)
            {
                foreach (var transfer in transfers)
                {
                    if (transfer.Loan)
                        AddLoan(transfer.Player, transfer.ClubTo);
                    else
                    {
                        int backupClubJob = hl.staff[transfer.Player].ClubJob;
                        hl.staff[transfer.Player].ClubJob = transfer.ClubTo;

                        if (!AddPlayerToClubSquad(transfer.Player, transfer.ClubFrom, transfer.ClubTo))
                        {
                            hl.staff[transfer.Player].ClubJob = backupClubJob;
                            var playerName = "Unknown";
                            var basicPlayerName = "Unknown";
                            hl.StaffToName(hl.staff.FirstOrDefault(x => x.ID == transfer.Player), out playerName, out basicPlayerName);
                            MessageBox.Show(string.Format("Was not able to attach player ({0}) to squad! (possible duplicate?) Rest of the changes we still be applied!", playerName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                hl.Save(textBoxIndexFile.Text, true, true, false);
                MessageBox.Show("Changes Applied!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Changes not applied due to errors!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void AddLoan(int staffID, int clubToID)
        {
            var latin1 = Encoding.GetEncoding("ISO-8859-1");
            var playerSetup = Path.Combine(Path.GetDirectoryName(textBoxIndexFile.Text), "player_setup.cfg");

            using (var playerSetupFile = File.Open(playerSetup, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            using (var sw = new StreamWriter(playerSetupFile, latin1))
            {
                // Example: "LOAN" "Martín" "" "Calderón" "Cádiz CF B" "CD Mirandés" 1 6 2001 15 6 2002
                var firstName = hl.first_names[hl.staff[staffID].FirstName].Name.ReadString();
                var commonName = hl.common_names[hl.staff[staffID].CommonName].Name.ReadString();
                var secondName = hl.second_names[hl.staff[staffID].SecondName].Name.ReadString();
                var currentClub = hl.club[hl.staff[staffID].ClubJob].Name.ReadString();
                var goingToClub = hl.club[clubToID].Name.ReadString();
                sw.WriteLine(string.Format("\"LOAN\" \"{0}\" \"{1}\" \"{2}\" \"{3}\" \"{4}\" 1 6 2001 15 6 2002", firstName, commonName, secondName, currentClub, goingToClub));
            }
        }

        private bool AddPlayerToClubSquad(int staffID, int clubFromID, int clubToID)
        {
            bool ret = false;

            // Find first available -1 and change to our new staff id
            if (clubToID != -1)
            {
                for (int i = 0; i < 50; i++)
                {
                    if (hl.club[clubToID].Squad[i] == -1)
                    {
                        hl.club[clubToID].Squad[i] = staffID;
                        ret = true;
                        break;
                    }
                }
            }
            else
                ret = true;

            if (ret)
            {
                ret = false;

                if (clubFromID != -1)
                {
                    // Remove staff ID from squad and replace with -1
                    for (int i = 0; i < 50; i++)
                    {
                        if (hl.club[clubFromID].Squad[i] == staffID)
                        {
                            hl.club[clubFromID].Squad[i] = -1;
                            ret = true;
                            break;
                        }
                    }
                }
                else
                    ret = true;
            }

            return ret;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            var latin1 = Encoding.GetEncoding("ISO-8859-1");
            var ofd = new OpenFileDialog();
            ofd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            ofd.Title = "Select a transfer text file to import...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                listBoxTransfers.Items.Clear();
                using (var importFile = File.Open(ofd.FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (var sr = new StreamReader(importFile, latin1))
                {
                    while (true)
                    {
                        var transferLine = sr.ReadLine();
                        if (transferLine == null)
                            break;
                        if (!transferLine.Contains("#")) // Skip comment lines
                        {
                            string playerName, clubFrom, clubTo;
                            bool loan;
                            if (DecodeLine(transferLine, out playerName, out clubFrom, out clubTo, out loan))
                            {
                                listBoxTransfers.Items.Add(transferLine);
                            }
                            else
                                MessageBox.Show(string.Format("Unable to decode line ({0})!", transferLine), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var latin1 = Encoding.GetEncoding("ISO-8859-1");
            var sfd = new SaveFileDialog();
            sfd.Filter = "Text Files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.Title = "Select a transfer text file to export...";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                using (var exportFile = File.Open(sfd.FileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
                using (var sw = new StreamWriter(exportFile, latin1))
                {
                    for (int i = 0; i < listBoxTransfers.Items.Count; i++)
                    {
                        var transferLine = listBoxTransfers.Items[i].ToString();
                        sw.WriteLine(transferLine);
                    }
                }
            }
        }

        private void buttonCopyAndPaste_Click(object sender, EventArgs e)
        {
            var form = new PlayerTransferScratchPadForm();

            string currentText = "";
            for (int i = 0; i < listBoxTransfers.Items.Count; i++)
            {
                var transferLine = listBoxTransfers.Items[i].ToString();
                currentText += transferLine;
                currentText += "\r\n";
            }
            form.SetText(currentText);
            if (form.ShowDialog() == DialogResult.OK)
            {
                var lines = form.TransferText.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                listBoxTransfers.Items.Clear();
                foreach (var line in lines)
                {
                    string playerName, clubFrom, clubTo;
                    bool loan;
                    if (DecodeLine(line, out playerName, out clubFrom, out clubTo, out loan))
                    {
                        listBoxTransfers.Items.Add(line);
                    }
                    else
                        MessageBox.Show(string.Format("Unable to decode line ({0})!", line), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        List<int> loanedPlayers = new List<int>();
        private void LoadLoans(string indexFile)
        {
            var latin1 = Encoding.GetEncoding("ISO-8859-1");
            var playerSetup = Path.Combine(Path.GetDirectoryName(indexFile), "player_setup.cfg");
            int loanCount = 0;
            int badLoans = 0;

            loanedPlayers.Clear();

            using (var playerSetupFile = File.Open(playerSetup, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            using (var sr = new StreamReader(playerSetupFile, latin1))
            {
                while (true)
                {
                    var loanLine = sr.ReadLine();
                    if (loanLine == null)
                        break;

                    try
                    {
                        if (loanLine.StartsWith("\"LOAN\""))
                        {
                            // Example: "LOAN" "Martín" "" "Calderón" "Cádiz CF B" "CD Mirandés" 1 6 2001 15 6 2002
                            var lsplit = loanLine.Split(new string[] { "\"" }, StringSplitOptions.None).Where(x => x != " ").Select(x => x.Trim()).ToArray();
                            var firstName = lsplit[2];
                            var commonName = lsplit[3];
                            var secondName = lsplit[4];
                            var clubFrom = lsplit[5];
                            var clubTo = lsplit[6];
                            var dateComponents = lsplit[7].Split(' ');

                            var staffIdx = hl.FindStaffIndex(firstName, secondName, commonName, clubFrom);
                            if (staffIdx == -1)
                            {
                                badLoans++;
                                // Console.WriteLine("Bad Loan: {0} {1} {2} {3}", firstName, secondName, commonName, clubFrom);
                            }
                            else
                            {
                                loanedPlayers.Add(staffIdx);
                            }
                            loanCount++;
                        }
                    }
                    catch { }
                }
            }
        }
    }

    public class ClubPlayerTuple
    {
        public ClubPlayerTuple(int Player, int ClubFrom, int ClubTo, bool Loan)
        {
            this.Player = Player;
            this.ClubFrom = ClubFrom;
            this.ClubTo = ClubTo;
            this.Loan = Loan;
        }
        public int ClubFrom;
        public int ClubTo;
        public int Player;
        public bool Loan;
    }

    public class TransferListBoxItem
    {
        public TransferListBoxItem(string Club, int ID, object Obj = null)
        {
            this.Name = null;
            this.Club = Club;
            this.ID = ID;
            this.Obj = Obj;
        }

        public TransferListBoxItem(string Name, string Club, int ID, object Obj = null)
        {
            this.Name = Name;
            this.Club = Club;
            this.ID = ID;
            this.Obj = Obj;
        }

        public override string ToString()
        {
            if (Name == null)
                return Club;
            else
                return Name + " (" + Club + ")";
        }

        public string Name;
        public string Club;
        public int ID;
        public object Obj;
    }

    public class NoScrollBarListBox : ListBox
    {
        private bool mShowScroll;
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!mShowScroll)
                    cp.Style = cp.Style & ~0x200000;
                return cp;
            }
        }

        public bool ShowScrollbar
        {
            get { return mShowScroll; }
            set
            {
                if (value != mShowScroll)
                {
                    mShowScroll = value;
                    if (IsHandleCreated)
                        RecreateHandle();
                }
            }
        }

        VScrollBar _ExternalScrollBar;
        public VScrollBar ExternalScrollBar
        {
            get
            {
                return _ExternalScrollBar;
            }
            set
            {
                _ExternalScrollBar = value;
                if (_ExternalScrollBar != null)
                {
                    _ExternalScrollBar.Maximum = Items.Count;
                    _ExternalScrollBar.Scroll += _ExternalScrollBar_Scroll;
                }
            }
        }

        private void _ExternalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            TopIndex = e.NewValue;
        }

        const int WM_MOUSEWHEEL = 0x020A;
        const int WM_KEYDOWN = 0x0100;
        const int WM_KEYUP = 0x0101;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_MOUSEWHEEL)
                SendMessage(_ExternalScrollBar.Handle, m.Msg, m.WParam, m.LParam);
            if (m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP)
            {
                _ExternalScrollBar.Value = TopIndex;
            }
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
    }
}
