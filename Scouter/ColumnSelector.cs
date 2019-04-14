using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Web.Script.Serialization;

namespace CM0102Patcher.Scouter
{
    public partial class ColumnSelector : Form
    {
        public DataGridView dataGrid;

        public ColumnSelector(DataGridView dataGrid)
        {
            this.dataGrid = dataGrid;
            InitializeComponent();

            List<string> colNames = new List<string>();
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                checkedListBox.Items.Add(column.Name, column.Visible);
                colNames.Add(column.Name);
            }

            LoadPresets();

            if (comboBoxPresets.Items.Count == 0)
            {
                // https://champman0102.co.uk/showthread.php?t=6625
                comboBoxPresets.Items.Add(new ColumnPreset("All", false, colNames.ToArray()));
                comboBoxPresets.Items.Add(new ColumnPreset("None", false));
                comboBoxPresets.Items.Add(new ColumnPreset("Goalkeepers", true, "Goalkeepers", "Handling", "Agility", "Jumping", "Passing", "Positioning", "Reflexes", "Anticipation", "Decisions", "One On Ones"));
                comboBoxPresets.Items.Add(new ColumnPreset("Defenders", true, "Position", "Determination", "Anticipation", "Marking", "Passing", "Tackling", "Jumping", "Pace", "Passing", "Positioning", "Stamina", "Strength"));
                comboBoxPresets.Items.Add(new ColumnPreset("Wingers", true, "Position", "Determination", "Acceleration", "Agility", "Flair", "Crossing", "Corners", "Finishing", "Dribbling", "Free Kicks", "Technique", "Passing", "Creativity", "Off The Ball"));
                comboBoxPresets.Items.Add(new ColumnPreset("Midfielders", true, "Position", "Determination", "Marking", "Passing", "Crossing", "Dribbling", "Heading", "Pace", "Strength", "Stamina" ));
                comboBoxPresets.Items.Add(new ColumnPreset("Attackers", true, "Position", "Determination", "Acceleration", "Aggression", "Bravery", "Off The Ball", "Finishing", "Jumping", "Pace", "Flair", "Stamina", "Strength"));
                comboBoxPresets.SelectedIndex = 0;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                dataGrid.Columns[(string)checkedListBox.Items[i]].Visible = checkedListBox.GetItemChecked(i);
            }
            Close();
        }

        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var colPreset = comboBoxPresets.SelectedItem as ColumnPreset;
            for (int i = 0; i < checkedListBox.Items.Count; i++)
            {
                if (colPreset.columns.Contains(checkedListBox.GetItemText(checkedListBox.Items[i])))
                    checkedListBox.SetItemChecked(i, true);
                else
                    checkedListBox.SetItemChecked(i, false);
            }
        }

        private void buttonSavePreset_Click(object sender, EventArgs e)
        {
            var pnf = new PresetNameForm();
            if (pnf.ShowDialog() == DialogResult.OK)
            {
                List<string> colNames = new List<string>();
                for (int i = 0; i < checkedListBox.Items.Count; i++)
                {
                    if (checkedListBox.GetItemChecked(i))
                    {
                        colNames.Add(checkedListBox.GetItemText(checkedListBox.Items[i]));
                    }
                }
                comboBoxPresets.Items.Add(new ColumnPreset(pnf.PresetName, false, colNames.ToArray()));
                comboBoxPresets.SelectedIndex = comboBoxPresets.Items.Count - 1;
                comboBoxPresets.Update();
                comboBoxPresets.Refresh();
                SavePresets();
            }
        }

        private void buttonDeletePreset_Click(object sender, EventArgs e)
        {
            var idx = comboBoxPresets.SelectedIndex;
            if ((comboBoxPresets.SelectedItem as ColumnPreset).name == "All" || (comboBoxPresets.SelectedItem as ColumnPreset).name == "None")
            {
                MessageBox.Show(this, "The All and None presets cannot be removed", "Presets", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            comboBoxPresets.Items.RemoveAt(comboBoxPresets.SelectedIndex);
            idx--;
            if (idx < 0)
                idx = 0;
            if (idx < comboBoxPresets.Items.Count)
                comboBoxPresets.SelectedIndex = idx;
            comboBoxPresets.Update();
            comboBoxPresets.Refresh();
            SavePresets();
        }

        void SavePresets()
        {
            try
            {
                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var presetFile = Path.Combine(exePath, "presetfile.txt");
                List<ColumnPreset> presets = new List<ColumnPreset>();
                foreach (var item in comboBoxPresets.Items)
                    presets.Add(item as ColumnPreset);
                var Serializer = new JavaScriptSerializer();
                Serializer.MaxJsonLength = Int32.MaxValue;
                string JSON = Serializer.Serialize(presets);
                using (var sw = new StreamWriter(presetFile))
                    sw.Write(JSON);
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }

        void LoadPresets()
        {
            try
            {
                var exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                var presetFile = Path.Combine(exePath, "presetfile.txt");
                if (File.Exists(presetFile))
                {
                    using (var sr = new StreamReader(presetFile))
                    {
                        var JSON = sr.ReadToEnd();
                        var Serializer = new JavaScriptSerializer();
                        Serializer.MaxJsonLength = Int32.MaxValue;
                        var presets = Serializer.Deserialize<List<ColumnPreset>>(JSON);
                        comboBoxPresets.Items.Clear();
                        foreach (var preset in presets)
                            comboBoxPresets.Items.Add(preset);
                        if (comboBoxPresets.Items.Count > 0)
                            comboBoxPresets.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionMsgBox.Show(ex);
            }
        }
    }

    public class ColumnPreset
    {
        public string name;
        public List<string> columns = new List<string>();
        string[] defaultColumns = new string[] { "Name", "Age", "Club", "CA", "PA", "Value" };

        public ColumnPreset()
        {

        }

        public ColumnPreset(string name, bool addDefaults, params string[] columns)
        {
            this.name = name;
            if (addDefaults)
                this.columns.AddRange(defaultColumns);
            this.columns.AddRange(columns);
        }

        public override string ToString()
        {
            return name;
        }
    }
}
