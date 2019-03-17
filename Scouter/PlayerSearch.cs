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
            Hide();
        }
    }
}
