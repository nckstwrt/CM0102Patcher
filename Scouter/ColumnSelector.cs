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
    public partial class ColumnSelector : Form
    {
        public DataGridView dataGrid;

        public ColumnSelector(DataGridView dataGrid)
        {
            this.dataGrid = dataGrid;
            InitializeComponent();

            foreach (DataGridViewColumn column in dataGrid.Columns)
                checkedListBox.Items.Add(column.Name, column.Visible);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                dataGrid.Columns[(string)checkedListBox.Items[i]].Visible = checkedListBox.GetItemChecked(i);
            }
            Close();
        }
    }
}
