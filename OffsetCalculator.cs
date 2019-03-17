using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CM0102Patcher
{
    public partial class OffsetCalculator : Form
    {
        const uint diff = 0x400000;
        bool fire = true;

        public OffsetCalculator()
        {
            InitializeComponent();
        }

        void Set(uint val, int which)
        {
            fire = false;
            if (which != 1)
                textBoxBinaryHex.Text = string.Format("{0:x6}", val);
            if (which != 2)
                textBoxBinaryDec.Text = val.ToString();
            if (which != 3)
                textBoxOllyHex.Text = string.Format("{0:x6}", val + diff);
            if (which != 4)
                textBoxOllyDec.Text = (val + diff).ToString();
            fire = true;
        }

        private void textBoxBinaryHex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fire)
                {
                    var val = Convert.ToUInt32(textBoxBinaryHex.Text, 16);
                    Set(val, 1);
                }
            }
            catch
            { }
        }

        private void textBoxBinaryDec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fire)
                {
                    var val = Convert.ToUInt32(textBoxBinaryDec.Text, 10);
                    Set(val, 2);
                }
            }
            catch
            { }
        }

        private void textBoxOllyHex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fire)
                {
                    var val = Convert.ToUInt32(textBoxOllyHex.Text, 16);
                    Set(val - diff, 3);
                }
            }
            catch
            { }
        }

        private void textBoxOllyDec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fire)
                {
                    var val = Convert.ToUInt32(textBoxOllyDec.Text, 10);
                    Set(val - diff, 4);
                }
            }
            catch
            { }
        }
    }
}
