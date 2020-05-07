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
        const uint diffExpanded = 0x70B000;
        bool fireExpanded = true;

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

        void SetExpanded(uint val, int which)
        {
            fireExpanded = false;
            if (which != 1)
                textBoxBinaryHexExpanded.Text = string.Format("{0:x6}", val);
            if (which != 2)
                textBoxBinaryDecExpanded.Text = val.ToString();
            if (which != 3)
                textBoxOllyHexExpanded.Text = string.Format("{0:x6}", val + diffExpanded);
            if (which != 4)
                textBoxOllyDecExpanded.Text = (val + diffExpanded).ToString();
            fireExpanded = true;
        }

        private void textBoxBinaryHex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fire)
                {
                    var val = Convert.ToUInt32(textBoxBinaryHex.Text.Trim(), 16);
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
                    var val = Convert.ToUInt32(textBoxBinaryDec.Text.Trim(), 10);
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
                    var val = Convert.ToUInt32(textBoxOllyHex.Text.Trim(), 16);
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
                    var val = Convert.ToUInt32(textBoxOllyDec.Text.Trim(), 10);
                    Set(val - diff, 4);
                }
            }
            catch
            { }
        }

        private void textBoxBinaryHexExpanded_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fireExpanded)
                {
                    var val = Convert.ToUInt32(textBoxBinaryHexExpanded.Text.Trim(), 16);
                    SetExpanded(val, 1);
                }
            }
            catch
            { }
        }

        private void textBoxOllyHexExpanded_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fireExpanded)
                {
                    var val = Convert.ToUInt32(textBoxOllyHexExpanded.Text.Trim(), 16);
                    SetExpanded(val - diffExpanded, 3);
                }
            }
            catch
            { }
        }

        private void textBoxBinaryDecExpanded_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fireExpanded)
                {
                    var val = Convert.ToUInt32(textBoxBinaryDecExpanded.Text.Trim(), 10);
                    SetExpanded(val, 2);
                }
            }
            catch
            { }
        }

        private void textBoxOllyDecExpanded_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (fireExpanded)
                {
                    var val = Convert.ToUInt32(textBoxOllyDecExpanded.Text.Trim(), 10);
                    Set(val - diffExpanded, 4);
                }
            }
            catch
            { }
        }
    }
}
