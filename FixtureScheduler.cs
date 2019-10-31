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
    public partial class FixtureScheduler : Form
    {
        // At Arg7                                      1         2         3         4         5         6         7         8         9         10        11        12        13        14        15        16        17
        public List<int> eplOffsets = new List<int>() { 0x173722, 0x173750, 0x173790, 0x1737f7, 0x173837, 0x17389e, 0x173905, 0x17396c, 0x1739ac, 0x173a13, 0x173a7a, 0x173ae1, 0x173b48, 0x173baf, 0x173c16, 0x173c7d, 0x173ce4,
        // 18     19        20        21        22        23        24        25        26        27        28        29        30        31        32        33        34        35        36        37        38
        0x173d4b, 0x173dea, 0x173e18, 0x173e46, 0x173e74, 0x174430, 0x174497, 0x1744fe, 0x174565, 0x1745cc, 0x174633, 0x17469a, 0x174701, 0x174768, 0x1747cf, 0x174836, 0x174864, 0x174892, 0x1748f9, 0x174960, 0x1749c7 };
        //0x173ed5, 0x173f03, 0x173f31, 0x173f64, 0x173f92, 0x173fc0, 0x173f03, 0x173fee, 0x174021, 0x17404f, 0x17407d, 0x1740ab, 0x1740de, 0x17410c };

        List<FixtureDateControl> eplControls;

        public FixtureScheduler(string exeFile, int currentYear)
        {
            InitializeComponent();

            eplControls = new List<FixtureDateControl>();
            for (int i = 0; i < eplOffsets.Count; i++)
            {
                eplControls.Add(new FixtureDateControl(i >= 20 ? 280 : 0, i >= 20 ? ((i * 25)-20*25) : i * 25, string.Format("Game {0}:", i + 1), tabPageEPL.Controls, exeFile, currentYear, eplOffsets[i]));
            }
        }

        private void buttonApplyEPL_Click(object sender, EventArgs e)
        {
            foreach (var eplControl in eplControls)
            {
                eplControl.SetToFile();
            }

            MessageBox.Show("New Fixtures Applied!", "Fixture Scheduler", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class FixtureDateControl
    {
        public FixtureDateControl(int x, int y, string name, Control.ControlCollection controls, string exeFile, int currentYear, int offset)
        {
            label = new Label();
            label.Text = name;
            label.Location = new Point(x + 7, y + 10);
            label.Size = new Size(55, 13);
            datePicker = new DateTimePicker();
            datePicker.Location = new Point(x + 65, y + 7);
            datePicker.Size = new Size(137, 20);
            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "ddd d MMM yyyy";
            comboBox = new ComboBox();
            comboBox.Location = new Point(x + 209, y + 7);
            comboBox.Size = new Size(54, 21);
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.Items.AddRange(new object[] { "AM", "PM", "EVE" });

            controls.Add(label);
            controls.Add(datePicker);
            controls.Add(comboBox);

            this.exeFile = exeFile;
            this.currentYear = currentYear;
            this.offset = offset;

            SetFromFile();
        }

        /*
        ARG 7 = TIME OF DAY (AM/PM/EVE)
        ARG 6 = DAY OF WEEK (MONDAY etc...)
        ARG 4 = MONTH OF YEAR (JANUARY etc...)
        ARG 3 = DATE OF MONTH (1st etc...)
        */
        public void SetFromFile()
        {
            using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                file.Seek(offset+1, SeekOrigin.Begin);
                var timeOfDay = file.ReadByte();
                file.ReadByte();
                var dayOfWeek = file.ReadByte();
                // Arg 5 is ignored
                file.ReadByte();
                file.ReadByte();
                file.ReadByte();
                var monthOfYear = file.ReadByte();
                file.ReadByte();
                var dateOfMonth = file.ReadByte();

                // Assume before middle of the year means it's the next year
                int year = currentYear;
                if (monthOfYear <= 6)
                    year++;
                try
                {
                    datePicker.Value = new DateTime(year, monthOfYear + 1, dateOfMonth - 1);
                    comboBox.SelectedIndex = timeOfDay;
                }
                catch 
                {
                }
            }
        }

        public void SetToFile()
        {
            using (var file = File.Open(exeFile, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                var date = datePicker.Value;
                byte dayOfWeek = (byte)((int)date.DayOfWeek-1);
                if (dayOfWeek < 0)
                    dayOfWeek = 6;
                byte monthOfYear = (byte)((int)date.Month-1);
                byte dateOfMonth = (byte)((int)date.Day + 1);

                file.Seek(offset + 1, SeekOrigin.Begin);
                file.WriteByte((byte)comboBox.SelectedIndex);
                file.Seek(1, SeekOrigin.Current);
                file.WriteByte(dayOfWeek);
                file.Seek(3, SeekOrigin.Current);
                file.WriteByte(monthOfYear);
                file.Seek(1, SeekOrigin.Current);
                file.WriteByte(dateOfMonth);
            }
        }

        /*
        CPU Disasm
        Address Hex dump Command                                  Comments
        00573722  |.  6A 01         PUSH 1                                   ; |Arg7 = 1
        00573724  |.  6A 05         PUSH 5                                   ; |Arg6 = 5
        00573726  |.  6A 00         PUSH 0                                   ; |Arg5 = 0
        00573728  |.  6A 07         PUSH 7                                   ; |Arg4 = 7
        0057372A  |.  6A 13         PUSH 13                                  ; |Arg3 = 13
        0057372C  |.  6A 00         PUSH 0                                   ; |Arg2 = 0
        0057372E  |.  56            PUSH ESI; |Arg1
        0057372F  |.  E8 2C6A1100 CALL 0068A160                            ; \cm0102_-_Fresh_-_3_9_68.0068A160
        */

        public Label label;
        public DateTimePicker datePicker;
        public ComboBox comboBox;
        public string exeFile;
        public int currentYear;
        public int offset;
    }
}
