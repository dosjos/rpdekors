using System;
using System.Drawing;
using System.Windows.Forms;
using DomainObjectsSalg.Sales;
using DomainObjectsSalg.Settings;

namespace CafeTerminal.UI
{
    public partial class SettingsWindow : Form
    {
        private Controller.MainController mc;
        public MainWindow MainWindow;

        public SettingsWindow()
        {
            InitializeComponent();
            Show();
        }

        public SettingsWindow(Controller.MainController mc, MainWindow mainWindow)
        {
            this.mc = mc;
            this.MainWindow = mainWindow;
            InitializeComponent();
            InitializeList();
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            Show();
        }

        public void InitializeList()
        {
            ListSetup();
            var varer = mc.GetAlleVarer();
            foreach (var vare in varer)
            {
               dataGridView1.Rows.Add(new object[] { vare.CurrentlyInUse, vare.Navn, vare.Pris, "opp", "ned", Color.FromArgb(vare.Farge), vare.Id });
            }

            BringToFront();
        }

        private void ListSetup()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 3;
            var doWork = new DataGridViewCheckBoxColumn {HeaderText = "I bruk", Name = "T", Width = 60};

            dataGridView1.Columns.Insert(0, doWork);


            var button = new DataGridViewButtonColumn {HeaderText = "Opp", Width = 80};

            dataGridView1.Columns.Insert(3, button);
            var button2 = new DataGridViewButtonColumn {HeaderText = "Ned", Width = 80};
            dataGridView1.Columns.Insert(4, button2);

            var cpc = new ColorPickerColumn {HeaderText = "Farge", Width = 100};
            dataGridView1.Columns.Insert(5, cpc);


            dataGridView1.Columns[1].Name = "Vare";
            dataGridView1.Columns[1].Width = 120;
            dataGridView1.Columns[2].Name = "Pris";
            dataGridView1.Columns[2].Width = 60;

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;
            dataGridView1.Columns[6].Name = "ID";
            dataGridView1.Columns[6].Width = 0;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                var checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                var t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);

                var v = mc.GetVare(t);
                v.CurrentlyInUse = (bool)checkCell.Value;
                mc.UpdateVare(v);
                mc.UpdateMainButtons();
            }
            if (e.ColumnIndex == 3)
            {
                var t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                var v = mc.GetVare(t);
                mc.PushVareUp(v);
                InitializeList();
                mc.UpdateMainButtons();
            }
            if (e.ColumnIndex == 4)
            {
                var t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                var v = mc.GetVare(t);
                mc.PushVareDown(v);
                InitializeList();
                mc.UpdateMainButtons();
            }
            BringToFront();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Enabled = false;
            new NewVare(mc, this);
        }

        internal void ReactivateSettingsWindow()
        {
            Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Antall rader " + dataGridView1.RowCount);
            for (int j = 0; j < dataGridView1.RowCount; j++)
            {
                var colorCell = (ColorPickerCell)dataGridView1.Rows[j].Cells[5];
                Color TemoColor;
                if (dataGridView1.Rows[j].Cells[5].Value is int)
                {
                    TemoColor = Color.FromArgb((int)dataGridView1.Rows[j].Cells[5].Value);
                }
                else
                {
                    TemoColor = (Color) dataGridView1.Rows[j].Cells[5].Value;
                }
                var v = mc.GetVare(Convert.ToInt32(dataGridView1.Rows[j].Cells[6].Value));
                v.Farge = TemoColor.ToArgb();
                mc.UpdateVare(v);
            }
            mc.UpdateMainButtons();
            BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var bytt = true;
            if (mc.HavePassSetting())
            {
                if (!mc.GetPassord().Equals(textBox1.Text))
                {
                    bytt = false;
                }
            }
            if (!bytt) return;
            if (!textBox2.Text.Equals(textBox3.Text)) return;
            var s = new Settings() { Type="Passord", Value = textBox3.Text};
            mc.LagrePassord(s);
            mc.EnableMainWindow();
            Dispose();
        }
    }
}
