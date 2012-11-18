using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DomainObjecsSalg.Sales;
using DomainObjecsSalg.Settings;

namespace Visitor_Registration.UI
{
    public partial class SettingsWindow : Form
    {
        private Controller.MainController mc;
        public MainWindow mainWindow;

        public SettingsWindow()
        {
            InitializeComponent();
            Show();
        }

        public SettingsWindow(Controller.MainController mc, MainWindow mainWindow)
        {
            this.mc = mc;
            this.mainWindow = mainWindow;
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
                DataGridViewRow row = new DataGridViewRow();
                dataGridView1.Rows.Add(new object[] { vare.CurrentlyInUse, vare.Navn, vare.Pris, "opp", "ned", vare.Farge, vare.Id });
            }

            BringToFront();
        }

        private void ListSetup()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 3;
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "I bruk";
            doWork.Name = "T";
            doWork.Width = 60;

            dataGridView1.Columns.Insert(0, doWork);


            DataGridViewButtonColumn button = new DataGridViewButtonColumn();
            button.HeaderText = "Opp";
            button.Width = 80;

            dataGridView1.Columns.Insert(3, button);
            DataGridViewButtonColumn button2 = new DataGridViewButtonColumn();
            button2.HeaderText = "Ned";
            button2.Width = 80;
            dataGridView1.Columns.Insert(4, button2);

            ColorPickerColumn cpc = new ColorPickerColumn();
            cpc.HeaderText = "Farge";
            cpc.Width = 100;
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
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);

                Vare v = mc.GetVare(t);
                v.CurrentlyInUse = (bool)checkCell.Value;
                mc.UpdateVare(v);
                mc.UpdateMainButtons();
            }
            if (e.ColumnIndex == 3)
            {
                int t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                Vare v = mc.GetVare(t);
                mc.PushVareUp(v);
                InitializeList();
                mc.UpdateMainButtons();
                //opp
            }
            if (e.ColumnIndex == 4)
            {
                int t = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[6].Value);
                Vare v = mc.GetVare(t);
                mc.PushVareDown(v);
                InitializeList();
                mc.UpdateMainButtons();
                //ned

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
                ColorPickerCell ColorCell = (ColorPickerCell)dataGridView1.Rows[j].Cells[5];
                Color TemoColor = (Color)dataGridView1.Rows[j].Cells[5].Value;
                Console.WriteLine(TemoColor);
                Vare v = mc.GetVare(Convert.ToInt32(dataGridView1.Rows[j].Cells[6].Value));
                v.Farge = TemoColor;
                mc.UpdateVare(v);
            }
            mc.UpdateMainButtons();
            BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool bytt = true;
            if (mc.HavePassSetting())
            {
                if (!mc.GetPassord().Equals(textBox1.Text))
                {
                    bytt = false;
                }
            }
            if (bytt)
            {
                if (textBox2.Text.Equals(textBox3.Text))
                {
                    Settings s = new Settings() { Type="Passord", Value = textBox3.Text};
                    mc.LagrePassord(s);
                    mc.EnableMainWindow();
                    Dispose();
                }
            }
        }
    }
}
