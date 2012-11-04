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
using DomainObjectsSalg.Sales;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        public MainController mc { get; set; }
        Random r = new Random();
        private BindingList<StringValue> sales = new BindingList<StringValue>();

        int totalsum = 0;
        int salgsum = 0;

        public MainWindow()
        {
            InitializeComponent();
            mc = new MainController(this);
           // this.Size = Screen.PrimaryScreen.WorkingArea.Size;
           // this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            GetButtons();
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
           var buttons =  mc.GetVarerCurrentlyForSale();
           if (buttons.Count == 0)
           {
               return;
            }

           splitContainer3.Panel1.Controls.Clear();
            //create buttons code
           int lastX = 0;
           int lastY = 0;
           int total = 0;
           int divider= (int)Math.Ceiling(Math.Sqrt(buttons.Count));
           int w = splitContainer3.Panel1.Width;
           w = (w / divider);
           int h = splitContainer3.Panel1.Height;
           h = (h / (int)Math.Ceiling((buttons.Count/(double)divider)));
           
           for (int i = 0; i < Math.Sqrt(buttons.Count); i++)
           {
               for (int j = 0; j < Math.Sqrt(buttons.Count); j++)
               {
                   Button b = new Button();
                   b.Width = w;
                   b.Height = h;
                   b.BackColor = GetButtonColor();  
                   b.Font = new System.Drawing.Font("Viner Hand ITC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                   b.Location = new Point(lastX, lastY);
                   b.Text = buttons[total].Navn + "\n" + buttons[total].Pris + " kr" ;
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

        private void menuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine(sender.ToString());
            string temp = sender.ToString().Remove(sender.ToString().Length-2);
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

        private Color GetButtonColor()
        {
            
            Color[] farger = new Color[11];
            farger[0] = System.Drawing.Color.YellowGreen;
            farger[1] = System.Drawing.Color.Azure;
            farger[2] = System.Drawing.Color.BurlyWood;
            farger[3] = System.Drawing.Color.DarkOliveGreen;
            farger[4] = System.Drawing.Color.YellowGreen;
            farger[5] = System.Drawing.Color.MidnightBlue;
            farger[6] = System.Drawing.Color.Violet;
            farger[7] = System.Drawing.Color.WhiteSmoke;
            farger[8] = System.Drawing.Color.Tomato;
            farger[9] = System.Drawing.Color.Wheat;
            farger[10] = System.Drawing.Color.SaddleBrown;

            return farger[r.Next(farger.Length)];
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
    }
}
