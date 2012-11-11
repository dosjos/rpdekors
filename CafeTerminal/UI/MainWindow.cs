using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeTerminal.Controller;
using CafeTerminal.DataAccesLayer;
using CafeTerminal.UI;
using DomainObjecsSalg.Sales;
using DomainObjectsSalg.Sales;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        public MainController mc { get; set; }
        private BindingList<StringValue> sales = new BindingList<StringValue>();

        int totalsum = 0;
        int salgsum = 0;

        public MainWindow()
        {
            InitializeComponent();
            //NHibernateHelper.ResetDatabase();
            mc = new MainController(this);
            GetButtons();
            CreateDataGrid();


            GetLogg();
            GetDagensSalg();

#if !DEBUG
            initialiserDatabaseToolStripMenuItem.Enabled = false;
#endif


        }

        private void GetDagensSalg()
        {
            totalsum = mc.GetDagensSalg();
            totalsumlabel.Text = "" + totalsum;
        }

        private void GetLogg()
        {
            Logg l = mc.GetLastLog();
            if (l != null)
            {
                LogText.Text = l.Text;
            }
        }

        private void CreateDataGrid()
        {
            this.Resize += new EventHandler(ResizeButtons);

            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            dataGridView1.DataSource = sales;
            modelColumn.HeaderText = "Salg";
            modelColumn.DataPropertyName = "Value";
            modelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(modelColumn);
        }

        private void ResizeButtons(object sender, EventArgs e)
        {
            GetButtons();
        }

        public void GetButtons()
        {
            splitContainer3.Panel1.Controls.Clear();
            var buttons = mc.GetVarerCurrentlyForSale();
            if (buttons.Count == 0)
            {

                return;
            }

            //create buttons code
            int lastX = 0;
            int lastY = 0;
            int total = 0;
            int divider = (int)Math.Ceiling(Math.Sqrt(buttons.Count));
            int w = splitContainer3.Panel1.Width;
            w = (w / divider);
            int h = splitContainer3.Panel1.Height;
            h = (h / (int)Math.Ceiling((buttons.Count / (double)divider)));

            for (int i = 0; i < Math.Sqrt(buttons.Count); i++)
            {
                for (int j = 0; j < Math.Sqrt(buttons.Count); j++)
                {
                    Button b = new Button();
                    b.Width = w;
                    b.Height = h;
                    b.BackColor = GetButtonColor(buttons[total]);
                    b.Font = new System.Drawing.Font("Viner Hand ITC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    b.Location = new Point(lastX, lastY);
                    b.Text = buttons[total].Navn + "\n" + buttons[total].Pris + " kr";
                    b.Click += new System.EventHandler(menuItem_Click);
                    splitContainer3.Panel1.Controls.Add(b);
                    lastX += b.Width;
                    total++;
                    if (total == buttons.Count)
                    {
                        return;
                    }
                }
                lastY += h;
                lastX = 0;
            }
        }

        private Color GetButtonColor(DomainObjecsSalg.Sales.Vare vare)
        {
            if (vare.Farge != null)
            {
                return vare.Farge;
            }
            else
            {
                return System.Drawing.Color.Yellow;
            }
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
            string temp = sender.ToString().Remove(sender.ToString().Length - 2);
            temp = temp.Substring(temp.IndexOf(':') + 2);
            string[] salg = temp.Split('\n');
            Console.WriteLine("Navn {0} og sum {1}", salg[0], salg[1]);
            int s = int.Parse(salg[1]);
            mc.LagreSalg(salg[0], s);

            sales.Add(new StringValue(salg[0] + " " + s));
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;

            salgsum += s;
            totalsum += s;

            salglabel.Text = salgsum + " nok";
            totalsumlabel.Text = totalsum + " nok";
        }



        private void avsluttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void omToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void instillingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow(mc, this);
        }

        private void LockWindow()
        {
            Enabled = false;
        }

        private void initialiserDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            salgsum = 0;
            salglabel.Text = salgsum + " nok";
            sales.Clear();
        }

        private void salgsoppsettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //new Salgsoppsett(mc);
            // Enabled = false;
        }

        private void nyDagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            totalsum = 0;
            totalsumlabel.Text = "0 nok";
            button1_Click(null, null);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Logg l = new Logg
            {
                Text = LogText.Text,
                LoggTid = DateTime.Now
            };

            mc.LagreLogg(l);
            string a = LogText.Text;
            LogText.SelectionColor = Color.Black;
            LogText.ForeColor = Color.Black;
            LogText.Text = "";
            LogText.SelectedText = "";
            LogText.SelectedText = a;
            Console.WriteLine(a);
        }

        private void loggTextSkrevet(object sender, KeyPressEventArgs e)
        {
            LogText.SelectionColor = Color.Red;
        }
    }
}


