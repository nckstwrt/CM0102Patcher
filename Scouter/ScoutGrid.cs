using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CM0102Scout;
using System.Reflection;
using System.Globalization;
using Microsoft.Win32;

namespace CM0102Patcher.Scouter
{
    public partial class ScoutGrid : Form
    {
        CultureInfo ci = CultureInfo.CreateSpecificCulture("en-GB");
        SaveReader saveReader;
        PlayerSearch ps = new PlayerSearch();

        public ScoutGrid()
        {
            InitializeComponent();
            EnableDoubleBuffering(true);
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridView.AllowUserToResizeColumns = true;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.AutoSize = false;
            dataGridView.ReadOnly = true;
            dataGridView.VirtualMode = true;

            ScoutGrid_Resize(null, null);
        }

        private void EnableDoubleBuffering(bool enable)
        {
            typeof(DataGridView).InvokeMember(
            "DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null,
            dataGridView,
            new object[] { enable });
        }

        private Color ColorMaker(Color x, Color y, sbyte oneAboveRange, sbyte val)
        {
            var r = x.R + ((y.R - x.R) / (oneAboveRange - val));
            var g = x.G + ((y.G - x.G) / (oneAboveRange - val));
            var b = x.B + ((y.B - x.B) / (oneAboveRange - val));
            return Color.FromArgb(r, g, b);
        }

        private void DataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            var cell = dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            cell.Style.BackColor = Color.Black;
            cell.Style.ForeColor = Color.White;

            if (cell.ValueType == typeof(sbyte))
            {
                if (cell.Value != null)
                {
                    var val = ((sbyte)cell.Value);

                    if (checkBoxShowIntrinstics.Checked)
                    {
                        var colName = dataGridView.Columns[e.ColumnIndex].Name;
                        switch (colName)
                        {
                            case "Anticipation":
                            case "Crossing":
                            case "Decisions":
                            case "Dribbling":
                            case "Finishing":
                            case "Heading":
                            case "Long Shots":
                            case "Movement":
                            case "Marking":
                            case "Passing":
                            case "Penalties":
                            case "Positioning":
                            case "Throw Ins":
                            case "Vision":
                            case "Tackling":
                                val = (sbyte)(((((int)val) + 127.0) / 230.0) * 20.0);
                                break;
                        }
                    }
                    switch (val)
                    {
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                        case 5:
                            cell.Style.ForeColor = Color.Gray;
                            break;
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                        case 11:
                            cell.Style.ForeColor = ColorMaker(Color.LightYellow, Color.FromArgb(251, 241, 84), 12, val);
                            break;
                        case 12:
                        case 13:
                        case 14:
                        case 15:
                        case 16:
                            cell.Style.ForeColor = ColorMaker(Color.FromArgb(251, 241, 84), Color.Orange, 17, val);
                            break;
                        case 17:
                        case 18:
                        case 19:
                        case 20:
                            cell.Style.ForeColor = ColorMaker(Color.Orange, Color.Red, 21, val);
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ((DataTable)dataGridView.DataSource).DefaultView.RowFilter =
                string.Format("[{0}] > {1} AND [{2}] > {3} AND [{4}] < {5}", "Finishing", 30, "Movement", 30, "Value", 100000);
            dataGridView.Update();
            dataGridView.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView.Columns["Drug"].Visible = false;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            ofd.Filter = "CM0102 Save Files|*.sav|All files (*.*)|*.*";
            ofd.Title = "Select a CM0102 Save file";
            try
            {
                var path = (string)Registry.GetValue(RegString.GetRegString(), "Location", "");
                if (!string.IsNullOrEmpty(path))
                    ofd.InitialDirectory = path;
            }
            catch { }
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadSaveFile(ofd.FileName);
            }
        }

        private void LoadSaveFile(string saveFileName)
        {
            using (saveReader = new SaveReader(saveFileName))
            {
                saveReader.LoadPlayers();
                dataGridView.SuspendLayout();
                dataGridView.DataSource = saveReader.CreateDataTable(checkBoxShowIntrinstics.Checked);
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dataGridView.AllowUserToResizeColumns = true;
                dataGridView.Columns["Value"].DefaultCellStyle.FormatProvider = ci;
                dataGridView.Columns["Value"].DefaultCellStyle.Format = "C0";
                dataGridView.Columns[1].Width -= 20;
                for (int i = 5; i < dataGridView.Columns.Count; i++)
                    if (i != 7)
                        dataGridView.Columns[i].Width -= 20;
                dataGridView.ResumeLayout();
                dataGridView.Update();
                dataGridView.Refresh();
            }
        }

        private void ScoutGrid_Resize(object sender, EventArgs e)
        {
            var newSize = this.Size;
            newSize.Width -= 40;
            newSize.Height -= 110;
            dataGridView.Size = newSize;
            checkBoxShowIntrinstics.Location = new Point(dataGridView.Location.X + (dataGridView.Width - checkBoxShowIntrinstics.Width),
                                                         dataGridView.Location.Y + (dataGridView.Height + 10));
            buttonColumns.Location = new Point(dataGridView.Location.X + 0,
                                                         dataGridView.Location.Y + (dataGridView.Height + 8));
            buttonFilter.Location = new Point(buttonColumns.Location.X + buttonColumns.Width + 5, buttonColumns.Location.Y);
        }

        private void checkBoxShowIntrinstics_CheckedChanged(object sender, EventArgs e)
        {
            if (saveReader != null)
                dataGridView.DataSource = saveReader.CreateDataTable(checkBoxShowIntrinstics.Checked);
            RefreshGrid();
        }

        private void buttonColumns_Click(object sender, EventArgs e)
        {
            dataGridView.CellFormatting -= DataGridView_CellFormatting;
            var CS = new ColumnSelector(dataGridView);
            CS.ShowDialog();
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            RefreshGrid();
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            dataGridView.CellFormatting -= DataGridView_CellFormatting;
            ps.ShowDialog();
            dataGridView.CellFormatting += DataGridView_CellFormatting;
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            if (dataGridView.DataSource != null)
            {
                ((DataTable)dataGridView.DataSource).DefaultView.RowFilter = ps.RowFilter;
                dataGridView.Update();
                dataGridView.Refresh();
            }
        }
    }
}
