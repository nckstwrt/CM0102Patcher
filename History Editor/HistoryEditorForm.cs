using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class HistoryEditorForm : Form
    {
        enum HistoryType
        {
            International,
            Club,
            StaffComp,
            StaffHistory
        }

        HistoryLoader historyLoader = new HistoryLoader();
        ListBoxItem lastSelectedNationComp = null;
        List<List<string>> lastSelectNationCompRows = null;
        ListBoxItem lastSelectedClubComp = null;
        List<List<string>> lastSelectClubCompRows = null;
        ListBoxItem lastSelectedStaffComp = null;
        Dictionary<int, ComboboxItem> StaffIDToComboBoxMap;
        ListBoxItem lastSelectedStaffHistory = null;
        Dictionary<int, ComboboxItem> ClubIDToComboBoxMap;
        List<ListBoxItem> staffItemsStore = null;

        public HistoryEditorForm()
        {
            InitializeComponent();
        }

        public string IndexFile
        {
            set
            {
                textBoxIndexFile.Text = value;
            }
        }

        private void LoadComboBoxes(HistoryType historyType)
        {
            DataGridView dgv;
            List<TClub> compClubs = null;

            switch (historyType)
            {
                default:
                case HistoryType.International:
                    compClubs = historyLoader.nat_club;
                    dgv = dataGridViewNationComp;
                    break;
                case HistoryType.Club:
                    compClubs = historyLoader.club;
                    dgv = dataGridViewClubComp;
                    break;
                case HistoryType.StaffComp:
                    dgv = dataGridViewStaffComp;
                    break;
                case HistoryType.StaffHistory:
                    dgv = dataGridViewStaffHistory;
                    break;
            }

            dgv.Rows.Clear();


            if (historyType == HistoryType.StaffHistory)
            {
                // Only loading clubs, so can hack this in
                var clubColumn = dgv.Columns[1] as DataGridViewComboBoxColumn;
                clubColumn.ValueType = typeof(ComboboxItem);
                clubColumn.ValueMember = "Self";
                clubColumn.DisplayMember = "Description";

                List<ComboboxItem> comboBoxItems = new List<ComboboxItem>();
                foreach (var club in historyLoader.club)
                {
                    if (historyLoader.staffNames.ContainsKey(club.ID))
                        comboBoxItems.Add(new ComboboxItem(historyLoader.GetTextFromBytes(club.Name), club));
                }
                comboBoxItems.Sort((x, y) => x.Text.CompareTo(y.Text));
                clubColumn.DataSource = comboBoxItems.ToArray();

                ClubIDToComboBoxMap = new Dictionary<int, ComboboxItem>();
                foreach (var comboBox in comboBoxItems)
                    ClubIDToComboBoxMap[((TClub)comboBox.Value).ID] = comboBox;

                return;
            }
            
            var nationWinners = (dgv.Columns[1] as DataGridViewComboBoxColumn).Items;
            var nationRunnersUp = (dgv.Columns[2] as DataGridViewComboBoxColumn).Items;
            var nationThirdPlace = (dgv.Columns[3] as DataGridViewComboBoxColumn).Items;

            DataGridViewComboBoxCell.ObjectCollection nationHost = null;
            nationWinners.Clear();
            nationRunnersUp.Clear();
            nationThirdPlace.Clear();

            if (historyType != HistoryType.StaffComp)
            {
                nationHost = (dgv.Columns[4] as DataGridViewComboBoxColumn).Items;
                nationHost.Clear();
            }

            if (historyType == HistoryType.StaffComp)
            {
                (dgv.Columns[1] as DataGridViewComboBoxColumn).ValueType = typeof(ComboboxItem);
                (dgv.Columns[1] as DataGridViewComboBoxColumn).ValueMember = "Self";
                (dgv.Columns[1] as DataGridViewComboBoxColumn).DisplayMember = "Description";
                (dgv.Columns[2] as DataGridViewComboBoxColumn).ValueType = typeof(ComboboxItem);
                (dgv.Columns[2] as DataGridViewComboBoxColumn).ValueMember = "Self";
                (dgv.Columns[2] as DataGridViewComboBoxColumn).DisplayMember = "Description";
                (dgv.Columns[3] as DataGridViewComboBoxColumn).ValueType = typeof(ComboboxItem);
                (dgv.Columns[3] as DataGridViewComboBoxColumn).ValueMember = "Self";
                (dgv.Columns[3] as DataGridViewComboBoxColumn).DisplayMember = "Description";


                List<ComboboxItem> comboBoxItems = new List<ComboboxItem>();
                foreach (var staffMember in historyLoader.staff)
                {
                    if (historyLoader.staffNames.ContainsKey(staffMember.ID))
                        comboBoxItems.Add(new ComboboxItem(historyLoader.staffNames[staffMember.ID], staffMember));
                }
                var orderedArray = comboBoxItems.OrderBy(x => x.Text).ToArray();
                nationWinners.AddRange(orderedArray);
                nationRunnersUp.AddRange(orderedArray);
                nationThirdPlace.AddRange(orderedArray);
                StaffIDToComboBoxMap = new Dictionary<int, ComboboxItem>();
                foreach (var comboBox in orderedArray)
                {
                    StaffIDToComboBoxMap[((TStaff)comboBox.Value).ID] = comboBox;
                }
            }
            else
            {
                var orderedClubs = compClubs.OrderBy(x => historyLoader.GetTextFromBytes(x.Name)).Select(y => historyLoader.GetTextFromBytes(y.Name)).Distinct().ToArray();
                nationWinners.AddRange(orderedClubs);
                nationRunnersUp.AddRange(orderedClubs);
                nationThirdPlace.AddRange(orderedClubs);
                nationHost.AddRange(orderedClubs);
            }
        }

        private List<List<string>> MakeRows(int compID, HistoryType historyType)
        {
            var rows = new List<List<string>>();

            List<TCompHistory> compHistory;
            List<TClub> compClubs;

            switch (historyType)
            {
                default:
                case HistoryType.International:
                    compHistory = historyLoader.nation_comp_history;
                    compClubs = historyLoader.nat_club;
                    break;
                case HistoryType.Club:
                    compHistory = historyLoader.club_comp_history;
                    compClubs = historyLoader.club;
                    break;
            }

            var histories = compHistory.Where(x => x.Comp == compID).ToList();

            foreach (var history in histories)
            {
                List<string> row = new List<string>();
                row.Add(history.Year.ToString());
                row.Add(historyLoader.GetTextFromBytes(compClubs.FirstOrDefault(x => x.ID == history.Winners).Name));
                row.Add(historyLoader.GetTextFromBytes(compClubs.FirstOrDefault(x => x.ID == history.RunnersUp).Name));
                row.Add(historyLoader.GetTextFromBytes(compClubs.FirstOrDefault(x => x.ID == history.ThirdPlace).Name));
                row.Add(historyLoader.GetTextFromBytes(compClubs.FirstOrDefault(x => x.ID == history.Host).Name));
                rows.Add(row);
            }

            return rows;
        }

        private List<List<string>> GetRowsFromGrid(HistoryType historyType)
        {
            DataGridView dgv;

            switch (historyType)
            {
                default:
                case HistoryType.International:
                    dgv = dataGridViewNationComp;
                    break;
                case HistoryType.Club:
                    dgv = dataGridViewClubComp;
                    break;
            }

            var rows = new List<List<string>>();
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                List<string> rowStrings = new List<string>();
                var row = dgv.Rows[i];
                for (int j = 0; j < row.Cells.Count; j++)
                {
                    var val = row.Cells[j].Value;
                    rowStrings.Add(val == null ? "" : val.ToString());
                }

                // Needs a date or it won't be added
                if (row.Cells[0].Value != null)
                    rows.Add(rowStrings);
            }
            return rows;
        }

        private int FindClub(string clubName, HistoryType historyType)
        {
            List<TClub> compClubs;

            switch (historyType)
            {
                default:
                case HistoryType.International:
                    compClubs = historyLoader.nat_club;
                    break;
                case HistoryType.Club:
                    compClubs = historyLoader.club;
                    break;
            }

            var club = compClubs.FirstOrDefault(x => historyLoader.GetTextFromBytes(x.Name) == clubName);
            if (club.Name == null)
                return -1;
            else
                return club.ID;
        }

        private bool CompareGridWithRows(List<List<string>> gridViewRows, List<List<string>> gridRows)
        {
            // Compare
            bool matched = true;
            if (gridRows.Count == gridViewRows.Count)
            {
                for (int i = 0; i < gridRows.Count; i++)
                {
                    for (int j = 0; j < gridRows[i].Count; j++)
                    {
                        if (gridRows[i][j] != gridViewRows[i][j])
                        {
                            matched = false;
                            break;
                        }
                    }
                }
            }
            else
                matched = false;
            return matched;
        }

        private void SaveFromGrid(int compID, List<List<string>> gridRows, HistoryType historyType)
        {
            // Save data
            List<TCompHistory> compHistory;

            switch (historyType)
            {
                default:
                case HistoryType.International:
                    compHistory = historyLoader.nation_comp_history;
                    break;
                case HistoryType.Club:
                    compHistory = historyLoader.club_comp_history;
                    break;
            }

            compHistory.RemoveAll(x => x.Comp == compID);

            for (int i = 0; i < gridRows.Count; i++)
            {
                var gridRow = gridRows[i];
                TCompHistory newHistory = new TCompHistory();
                newHistory.ID = compHistory.Max(x => x.ID) + 1;
                newHistory.Comp = compID;
                short year = 1970;
                short.TryParse(gridRow[0], out year);
                newHistory.Year = year;
                newHistory.Winners = FindClub(gridRow[1], historyType);
                newHistory.RunnersUp = FindClub(gridRow[2], historyType);
                newHistory.ThirdPlace = FindClub(gridRow[3], historyType);
                newHistory.Host = FindClub(gridRow[4], historyType);
                compHistory.Add(newHistory);
            }
        }

        private void CheckAndSave(HistoryType historyType)
        {
            if (historyType == HistoryType.StaffHistory)
            {
                if (lastSelectedStaffHistory != null)
                {
                    var StaffID = ((TStaff)lastSelectedStaffHistory.Obj).ID;
                    historyLoader.staff_history.RemoveAll(x => x.StaffID == StaffID);
                    for (int i = 0; i < dataGridViewStaffHistory.Rows.Count; i++)
                    {
                        var yearValue = dataGridViewStaffHistory.Rows[i].Cells[0].Value;
                        var clubValue = dataGridViewStaffHistory.Rows[i].Cells[1].Value;
                        var appsValue = dataGridViewStaffHistory.Rows[i].Cells[2].Value;
                        var goalsValue = dataGridViewStaffHistory.Rows[i].Cells[3].Value;
                        var loanValue = dataGridViewStaffHistory.Rows[i].Cells[4].Value;
                        if (yearValue != null)
                        {
                            short year = 1970;
                            short.TryParse(yearValue.ToString(), out year);
                            TStaffHistory newHistory = new TStaffHistory();
                            newHistory.Year = year;
                            newHistory.ID = historyLoader.staff_history.Max(x => x.ID) + 1;
                            newHistory.StaffID = StaffID;
                            if (clubValue != null)
                                newHistory.ClubID = ((TClub)(clubValue as ComboboxItem).Value).ID;
                            else
                                newHistory.ClubID = -1;

                            if (appsValue != null && appsValue is string)
                            {
                                sbyte tempApps = 0;
                                sbyte.TryParse(appsValue as string, out tempApps);
                                newHistory.Apps = tempApps;
                            }
                            else
                                newHistory.Apps = 0;
                            if (goalsValue != null && goalsValue is string)
                            {
                                sbyte tempGoals = 0;
                                sbyte.TryParse(goalsValue as string, out tempGoals);
                                newHistory.Goals = tempGoals;
                            }
                            else
                                newHistory.Goals = 0;
                            if (loanValue != null && loanValue is string)
                            {
                                sbyte tempLoan = 0;
                                sbyte.TryParse(loanValue as string, out tempLoan);
                                newHistory.OnLoan = tempLoan;
                            }
                            else
                                newHistory.OnLoan = 0;
                            historyLoader.staff_history.Add(newHistory);
                        }
                    }
                }
            }
            else if (historyType == HistoryType.StaffComp)
            {
                if (lastSelectedStaffComp != null)
                {
                    // Save the data
                    var LastCompID = ((TStaffComp)lastSelectedStaffComp.Obj).ID;
                    historyLoader.staff_comp_history.RemoveAll(x => x.Comp == LastCompID);
                    for (int i = 0; i < dataGridViewStaffComp.Rows.Count; i++)
                    {
                        var yearValue = dataGridViewStaffComp.Rows[i].Cells[0].Value;
                        var winnerValue = dataGridViewStaffComp.Rows[i].Cells[1].Value;
                        var runnersUpValue = dataGridViewStaffComp.Rows[i].Cells[2].Value;
                        var thirdPlaceValue = dataGridViewStaffComp.Rows[i].Cells[3].Value;
                        if (yearValue != null)
                        {
                            short year = 1970;
                            short.TryParse(yearValue.ToString(), out year);
                            TStaffCompHistory newHistory = new TStaffCompHistory();
                            newHistory.ID = historyLoader.staff_comp_history.Max(x => x.ID) + 1;
                            newHistory.Comp = LastCompID;
                            newHistory.Year = year;
                            newHistory.WinnerID = newHistory.WinnersFirstName = newHistory.WinnersSecondName = newHistory.WinnerInfo = -1;
                            newHistory.RunnersUpID = newHistory.RunnersUpFirstName = newHistory.RunnersUpSecondName = newHistory.RunnersUpInfo = -1;
                            newHistory.ThirdPlaceID = newHistory.ThirdPlaceFirstName = newHistory.ThirdPlaceSecondName = newHistory.ThirdPlaceInfo = -1;
                            if (winnerValue != null)
                            {
                                newHistory.WinnerID = ((TStaff)(winnerValue as ComboboxItem).Value).ID;
                                newHistory.WinnersFirstName = ((TStaff)(winnerValue as ComboboxItem).Value).FirstName;
                                newHistory.WinnersSecondName = ((TStaff)(winnerValue as ComboboxItem).Value).SecondName;
                                newHistory.WinnerInfo = 28;
                            }
                            if (runnersUpValue != null)
                            {
                                newHistory.RunnersUpID = ((TStaff)(runnersUpValue as ComboboxItem).Value).ID;
                                newHistory.RunnersUpFirstName = ((TStaff)(runnersUpValue as ComboboxItem).Value).FirstName;
                                newHistory.RunnersUpSecondName = ((TStaff)(runnersUpValue as ComboboxItem).Value).SecondName;
                                newHistory.RunnersUpInfo = 28;
                            }
                            if (thirdPlaceValue != null)
                            {
                                newHistory.ThirdPlaceID = ((TStaff)(thirdPlaceValue as ComboboxItem).Value).ID;
                                newHistory.ThirdPlaceFirstName = ((TStaff)(thirdPlaceValue as ComboboxItem).Value).FirstName;
                                newHistory.ThirdPlaceSecondName = ((TStaff)(thirdPlaceValue as ComboboxItem).Value).SecondName;
                                newHistory.ThirdPlaceInfo = 28;
                            }

                            historyLoader.staff_comp_history.Add(newHistory);
                        }
                    }
                }
            }
            else
            {
                ListBoxItem lastSelected;
                List<List<string>> lastRows;

                switch (historyType)
                {
                    default:
                    case HistoryType.International:
                        lastSelected = lastSelectedNationComp;
                        lastRows = lastSelectNationCompRows;
                        break;
                    case HistoryType.Club:
                        lastSelected = lastSelectedClubComp;
                        lastRows = lastSelectClubCompRows;
                        break;
                }

                if (lastSelected != null)
                {
                    // Check if changes
                    var gridRows = GetRowsFromGrid(historyType);

                    // Compare
                    bool matched = CompareGridWithRows(lastRows, gridRows);
                    if (!matched)
                    {
                        // Save data
                        var compID = ((TComp)lastSelected.Obj).ID;
                        SaveFromGrid(compID, gridRows, historyType);
                    }
                }
            }
        }

        private void listBoxNationComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndSave(HistoryType.International);

            var selectedItem = listBoxNationComps.SelectedItem as ListBoxItem;
            lastSelectedNationComp = selectedItem;

            if (selectedItem != null)
            {
                dataGridViewNationComp.Rows.Clear();

                var rows = MakeRows(((TComp)selectedItem.Obj).ID, HistoryType.International);
                rows.Sort((x, y) => x[0].CompareTo(y[0]));
                foreach (var row in rows)
                {
                    dataGridViewNationComp.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
                }

                lastSelectNationCompRows = rows;
            }
        }

        private void listBoxClubComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndSave(HistoryType.Club);

            var selectedItem = listBoxClubComps.SelectedItem as ListBoxItem;
            lastSelectedClubComp = selectedItem;

            if (selectedItem != null)
            {
                dataGridViewClubComp.Rows.Clear();

                var rows = MakeRows(((TComp)selectedItem.Obj).ID, HistoryType.Club);
                rows.Sort((x, y) => x[0].CompareTo(y[0]));
                foreach (var row in rows)
                {
                    dataGridViewClubComp.Rows.Add(row[0], row[1], row[2], row[3], row[4]);
                }

                lastSelectClubCompRows = rows;
            }
        }

        private void listBoxStaffComps_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndSave(HistoryType.StaffComp);

            var selectedItem = listBoxStaffComps.SelectedItem as ListBoxItem;
            lastSelectedStaffComp = selectedItem;

            dataGridViewStaffComp.Rows.Clear();

            var CompID = ((TStaffComp)selectedItem.Obj).ID;
            var staff_comp_history = historyLoader.staff_comp_history.Where(x => x.Comp == CompID).ToList();//FindAll(x => x.Comp == CompID);
            int count = 0;
            foreach (var history in staff_comp_history)
            {
                ComboboxItem winner = null, runnersup = null, thirdplace = null;
                var year = history.Year.ToString();
                if (history.WinnerID != -1)
                    winner = StaffIDToComboBoxMap[history.WinnerID];
                if (history.RunnersUpID != -1)
                    runnersup = StaffIDToComboBoxMap[history.RunnersUpID];
                if (history.ThirdPlaceID != -1)
                    thirdplace = StaffIDToComboBoxMap[history.ThirdPlaceID];
                dataGridViewStaffComp.Rows.Add(year, winner, runnersup, thirdplace);
                
                // Some data updates have broken staff comp data - so cut off after 100 years worth
                if (count++ == 100)
                    break;
            }
        }

        private void listBoxStaff_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAndSave(HistoryType.StaffHistory);
            var selectedItem = listBoxStaff.SelectedItem as ListBoxItem;
            lastSelectedStaffHistory = selectedItem;

            dataGridViewStaffHistory.Rows.Clear();

            var StaffID = ((TStaff)selectedItem.Obj).ID;
            var staff_history = historyLoader.staff_history.Where(x => x.StaffID == StaffID).ToList();
            staff_history.Sort((x, y) => x.Year.CompareTo(y.Year));
            foreach (var staff in staff_history)
            {
                ComboboxItem club = null;
                var year = staff.Year.ToString();
                var apps = staff.Apps.ToString();
                var goals = staff.Goals.ToString();
                var loan = staff.OnLoan.ToString();
                if (staff.ClubID != -1)
                {
                    if (ClubIDToComboBoxMap.ContainsKey(staff.ClubID))
                        club = ClubIDToComboBoxMap[staff.ClubID];
                }
                dataGridViewStaffHistory.Rows.Add(year, club, apps, goals, loan);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            CheckAndSave(HistoryType.International);
            CheckAndSave(HistoryType.Club);
            CheckAndSave(HistoryType.StaffComp);
            CheckAndSave(HistoryType.StaffHistory);
            historyLoader.Save(textBoxIndexFile.Text);
            MessageBox.Show("History Saved!", "History Editor", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridViewComp_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool validClick = (e.RowIndex != -1 && e.ColumnIndex != -1); //Make sure the clicked row/column is valid.
            var datagridview = sender as DataGridView;

            // Check to make sure the cell clicked is the cell containing the combobox 
            if (validClick)
            {
                datagridview.BeginEdit(true);

                if (datagridview.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                    ((ComboBox)datagridview.EditingControl).DroppedDown = true;
            }
        }

        private void dataGridViewComp_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            var datagridview = sender as DataGridView;
            datagridview.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            try
            {
                lastSelectedNationComp = null;
                lastSelectNationCompRows = null;
                lastSelectedClubComp = null;
                lastSelectClubCompRows = null;
                lastSelectedStaffComp = null;
                lastSelectedStaffHistory = null;
                staffItemsStore = null;

                historyLoader.Load(textBoxIndexFile.Text);

                listBoxNationComps.Items.Clear();
                List<ListBoxItem> nationCompsItems = new List<ListBoxItem>();
                for (int i = 0; i < historyLoader.nation_comp.Count; i++)
                    nationCompsItems.Add(new ListBoxItem(historyLoader.GetTextFromBytes(historyLoader.nation_comp[i].Name), i, historyLoader.nation_comp[i]));
                nationCompsItems = nationCompsItems.OrderBy(x => x.Name).ToList();
                foreach (var nationCompsItem in nationCompsItems)
                    listBoxNationComps.Items.Add(nationCompsItem);

                listBoxClubComps.Items.Clear();
                List<ListBoxItem> clubCompsItems = new List<ListBoxItem>();
                for (int i = 0; i < historyLoader.club_comp.Count; i++)
                    clubCompsItems.Add(new ListBoxItem(historyLoader.GetTextFromBytes(historyLoader.club_comp[i].Name), i, historyLoader.club_comp[i]));
                clubCompsItems = clubCompsItems.OrderBy(x => x.Name).ToList();
                foreach (var clubCompsItem in clubCompsItems)
                    listBoxClubComps.Items.Add(clubCompsItem);

                listBoxStaffComps.Items.Clear();
                List<ListBoxItem> staffCompsItems = new List<ListBoxItem>();
                for (int i = 0; i < historyLoader.staff_comp.Count; i++)
                    staffCompsItems.Add(new ListBoxItem(historyLoader.GetTextFromBytes(historyLoader.staff_comp[i].Name), i, historyLoader.staff_comp[i]));
                staffCompsItems = staffCompsItems.OrderBy(x => x.Name).ToList();
                foreach (var staffCompsItem in staffCompsItems)
                    listBoxStaffComps.Items.Add(staffCompsItem);

                listBoxStaff.Items.Clear();
                staffItemsStore = new List<ListBoxItem>();
                for (int i = 0; i < historyLoader.staff.Count; i++)
                    staffItemsStore.Add(new ListBoxItem(historyLoader.staffNames[historyLoader.staff[i].ID], i, historyLoader.staff[i]));
                //staffItems.Sort((x, y) => x.Name.CompareTo(y.Name));
                //listBoxStaff.Items.AddRange(staffItems.ToArray());

                LoadComboBoxes(HistoryType.International);
                LoadComboBoxes(HistoryType.Club);
                LoadComboBoxes(HistoryType.StaffComp);
                LoadComboBoxes(HistoryType.StaffHistory);
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "Index.dat Files (index.dat)|index.dat|All files (*.*)|*.*";
            ofd.Title = "Select an index.dat file...";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxIndexFile.Text = ofd.FileName;
                buttonLoad_Click(null, null);
            }
        }

        private void HistoryEditorForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxIndexFile.Text))
            {
                buttonLoad_Click(null, null);
            }
        }

        private void buttonNationDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewNationComp.Rows.Remove(dataGridViewNationComp.CurrentRow);
        }

        private void buttonClubDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewClubComp.Rows.Remove(dataGridViewClubComp.CurrentRow);
        }

        private void buttonStaffDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewStaffComp.Rows.Remove(dataGridViewStaffComp.CurrentRow);
        }

        private void buttonStaffHistoryDeleteRow_Click(object sender, EventArgs e)
        {
            dataGridViewStaffHistory.Rows.Remove(dataGridViewStaffHistory.CurrentRow);
        }

        private void ShiftYears(DataGridView dgv, NumericUpDown upDown)
        {
            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                try
                {
                    var value = dgv.Rows[i].Cells[0].Value;
                    short year;
                    if (value != null && short.TryParse(value as string, out year))
                    {
                        year += (short)upDown.Value;
                        dgv.Rows[i].Cells[0].Value = year.ToString();
                    }
                }
                catch { }
            }
        }

        private void buttonNationShiftYears_Click(object sender, EventArgs e)
        {
            ShiftYears(dataGridViewNationComp, numericNationUpDown);
        }

        private void buttonClubShiftYears_Click(object sender, EventArgs e)
        {
            ShiftYears(dataGridViewClubComp, numericClubUpDown);
        }

        private void buttonStaffCompShiftYears_Click(object sender, EventArgs e)
        {
            ShiftYears(dataGridViewStaffComp, numericStaffCompUpDown);
        }

        private void buttonStaffHistoryShiftYears_Click(object sender, EventArgs e)
        {
            ShiftYears(dataGridViewStaffHistory, numericStaffHistoryUpDown);
        }

        private void textBoxSearchStaff_TextChanged(object sender, EventArgs e)
        {
            if (staffItemsStore != null)
            {
                var searchText = textBoxSearchStaff.Text.ToLower();

                listBoxStaff.Items.Clear();
                listBoxStaff.Items.AddRange(staffItemsStore.Where(x => x.Name.ToLower().Contains(searchText)).ToArray());
            }
        }

        private void buttonYearShifter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This is only meant for people that know what they are doing. Do not use if you do not.\r\n\r\nUnderstand?", "Year Shifter", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (!string.IsNullOrEmpty(textBoxIndexFile.Text))
                {
                    YearShifterForm ysf = new YearShifterForm(textBoxIndexFile.Text);
                    ysf.ShowDialog();
                    buttonLoad_Click(null, null);
                }
            }
        }

        private void buttonOrderClubNames_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("This is only meant for people that know what they are doing. Do not use if you do not.\r\n\r\nUnderstand?", "Club Name Reorder", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                historyLoader.SortClubNames();
                historyLoader.Save(textBoxIndexFile.Text, true, true);
                MessageBox.Show("Data Saved", "Club Reorderer", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class ListBoxItem
    {
        public ListBoxItem(string Name, int Index, object Obj)
        {
            this.Name = Name;
            this.Index = Index;
            this.Obj = Obj;
        }

        public override string ToString()
        {
            return Name;
        }

        public string Name;
        public int Index;
        public object Obj;
    }
}
