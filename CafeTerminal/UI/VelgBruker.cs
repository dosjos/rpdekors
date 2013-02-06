using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DomainObjecsSalg.Sales;

namespace CafeTerminal.UI
{
    public partial class VelgBruker : Form
    {
        private Controller.MainController mc;

        public VelgBruker()
        {
            InitializeComponent();
        }

        public VelgBruker(Controller.MainController mc)
        {
            // TODO: Complete member initialization
            this.mc = mc;
            InitializeComponent();

            dataGridView1.Columns.Clear();
            dataGridView1.ColumnCount = 3;
            DataGridViewCheckBoxColumn doWork = new DataGridViewCheckBoxColumn();
            doWork.HeaderText = "På jobb";
            doWork.Name = "T";
            doWork.Width = 50;
            
            dataGridView1.Columns.Insert(0, doWork);
            dataGridView1.Columns[1].Name = "Navn";
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Name = "Stilling";
            dataGridView1.Columns[2].Width = 80;

            dataGridView1.Columns[3].Name = "id";
            dataGridView1.Columns[3].Width = 1;

            List<Users> br = mc.GetAlleBrukere();
            List<UserLogg> ulogs = mc.GetTodaysUsers(); 
            foreach (var item in br)
            {
                var r = from l in ulogs
                        where l.UserId == item.Id select l;
                ;
                if (r.Count() == 0)
                {

                    DataGridViewRow row = new DataGridViewRow();
                    dataGridView1.Rows.Add(new object[] { false, item.Navn, item.Rolle, item.Id });
                }
            
            }


            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            int i = dataGridView1.RowCount;

            for (int j = 0; j < i; j++)
            {
                DataGridViewCheckBoxCell checkCell = (DataGridViewCheckBoxCell)dataGridView1.Rows[j].Cells[0];
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                int t = Convert.ToInt32(dataGridView1.Rows[j].Cells[3].Value);

                Boolean b = (bool)checkCell.Value;
                if (b)
                {
                    dataGridView1.Rows[j].ReadOnly = true;
                    Users u = mc.GetBruker(t);
                    UserLogg ul = new UserLogg()
                    {
                        UserId = u.Id,
                        Brukstid = DateTime.Now
                    };
                    mc.SaveUserLog(ul);

                }


            }

            Dispose();
            
        }
    }
}
