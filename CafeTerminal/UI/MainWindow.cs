<<<<<<< HEAD
﻿using System;
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
        int logTime = 0;
        int totalsum = 0;
        int salgsum = 0;
        Timer t;
        List<OrderPanel> orderlist = new List<OrderPanel>();
        

        public MainWindow()
        {
            mc = new MainController(this);
            StartWindow();
            
        }

        private void StartWindow()
        {
           // using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Bruker\fil.txt"))
            //{
                
            //    file.WriteLine("1");
                InitializeComponent();
            //    file.WriteLine("2");
                try
                {
                    GetButtons();
                }
                catch (Exception e)
                {
                    NHibernateHelper.ResetDatabase();
                    GetButtons();
                }
             //   file.WriteLine("3");

                CreateDataGrid();
              //  file.WriteLine("4");


                GetLogg();

               // file.WriteLine("5");
                GetDagensSalg();
#if !DEBUG
            initialiserDatabaseToolStripMenuItem.Enabled = false;
#endif
            t = new Timer();
                //file.WriteLine("6");
                if (mc.HavePassSetting())
                {
                    
                    t.Tick += Tick;
                    t.Interval = 50;
                    t.Enabled = true;
                }
                //f//ile.WriteLine("7");

            //}
        }

        public MainWindow(MainController mainController)
        {
            // TODO: Complete member initialization
            this.mc = mainController;
            StartWindow();
        }

        private void Tick(object sender, EventArgs e)
        {
            t.Enabled = false;
            t.Tick -= Tick;
            new LoggInn(mc);

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
           // resetTime();
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
            string temp = sender.ToString().Remove(sender.ToString().Length - 2);
            temp = temp.Substring(temp.IndexOf(':') + 2);
            string[] salg = temp.Split('\n');
            int s = int.Parse(salg[1]);
            mc.LagreSalg(salg[0], s);

            sales.Add(new StringValue(salg[0] + " " + s));
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;

            salgsum += s;
            totalsum += s;

            salglabel.Text = salgsum + " nok";
            totalsumlabel.Text = totalsum + " nok";
            resetTime();
        }



        private void avsluttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void omToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
            resetTime();
        }

        private void instillingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow(mc, this);
            resetTime();
        }

        public void LockWindow()
        {
            Enabled = false;
            Visible = false;
            Hide();
            
        }

        private void initialiserDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            OrderPanel o = new OrderPanel(this);

            PopulateOrder(o);


            UpdatePanels();
            salgsum = 0;
            salglabel.Text = salgsum + " nok";
            sales.Clear();
            resetTime();
        }

        private void PopulateOrder(OrderPanel o)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                o.SetText(item.Cells[0].Value.ToString());
            }
           // o.SetText("En", "To", "Tre");
            orderlist.Add(o);
        }

        public void UpdatePanels()
        {
            int lastX = 0;
            splitContainer5.Panel1.Controls.Clear();
            foreach (var item in orderlist)
            {
                item.Location = new System.Drawing.Point(lastX, 0);
                splitContainer5.Panel1.Controls.Add(item);
                lastX += item.Width + 5; 
            }
        }

        private void salgsoppsettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetTime();
        }

        private void nyDagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mc.Restart();
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
            resetTime();
        }

        private void loggTextSkrevet(object sender, KeyPressEventArgs e)
        {
            LogText.SelectionColor = Color.Red;
        }

        internal void ReenableWindow()
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            resetTime();
        }

        internal void ReenableWindow(bool p)
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            if (p)
            {
                t.Interval = 120000;
                logTime = 120000;
            }
            else
            {
                t.Interval = 30000;
                logTime = 30000;
            }
            t.Enabled = true;
            t.Tick += LogOut;
        }

        internal void resetTime()
        {
            try
            {
                t.Stop();
                t.Interval = logTime;
                t.Start();
            }
            catch (Exception e)
            {
                
            }
        }

        private void LogOut(object sender, EventArgs e)
        
        {
            t.Tick -= LogOut;
            t.Enabled = false; 
            new LoggInn(mc);
        }

        private void loggUtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.Tick -= LogOut;
            t.Enabled = false;
            new LoggInn(mc);
        }

        internal void RemoveFromList(OrderPanel orderPanel)
        {
            int i = 0;
            foreach (var item in orderlist)
            {
                if (item == orderPanel)
                {
                    orderlist.RemoveAt(i);
                    return;
                }
                i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           HelperWindow hw =  new HelperWindow();
           hw.Show();
        }

        private void registrerNyBrukerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NyBruker(mc);
        }

        private void RegistrerArbeider_Click(object sender, EventArgs e)
        {
            new VelgBruker(mc);
        }

        internal void DagensBruker(UserLogg ul)
        {
            Users u = mc.GetBruker(ul.UserId);
            richTextBox1.AppendText(u.Navn + "\n");
        }
    }
}


=======
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using CafeTerminal.Controller;
using CafeTerminal.DataAccesLayer;
using CafeTerminal.UI;
using DomainObjecsSalg.Sales;
using DomainObjectsSalg.Sales;
using Timer = System.Windows.Forms.Timer;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        public MainController mc { get; set; }
        private BindingList<StringValue> sales = new BindingList<StringValue>();
        int logTime = 0;
        int totalsum = 0;
        int salgsum = 0;
        Timer t;
        List<OrderPanel> orderlist = new List<OrderPanel>();
        

        public MainWindow()
        {
            mc = new MainController(this);
            StartWindow();
            
        }

        public MainWindow(string s)
        {
        }

        public void Boot()
        {
            StartWindow();
        }

        private void StartWindow()
        {
                InitializeComponent();
                try
                {
                    GetButtons();
                }
                catch (Exception e)
                {
                    //NHibernateHelper.ResetDatabase();//TODO finn hvilken spesifikk exception som må kastes for at denne kommandoen skal kjøres
                    GetButtons();
                }
                CreateDataGrid();

                GetLogg();

                GetDagensSalg();
#if !DEBUG
            initialiserDatabaseToolStripMenuItem.Enabled = false;
#endif
                if (mc.HavePassSetting())
                {
                    t = new Timer();
                    t.Tick += Tick;
                    t.Interval = 50;
                    t.Enabled = true;
                }
            try
            {
                int Hour = 23;
                int Minute = 59;
                int Second = 59;
                int Year = DateTime.Now.Year;
                int Month = DateTime.Now.Month;
                int Day = DateTime.Now.Day;
                DateTime target = new DateTime(Year, Month, Day, Hour, Minute, Second);
                int interval = (int) (target - DateTime.Now).TotalMilliseconds;
                var timer = new System.Timers.Timer(interval);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Enabled = true;
            }
            catch (Exception e)
            {
                
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RestartWindow();
        }


        delegate void RestartCalback();

        private void RestartWindow()
        {
            if (this.InvokeRequired)
            {
                RestartCalback d = new RestartCalback(RestartWindow);
               this.Invoke(d);
            }
            else
            {
                mc.Restart();
            }
        }

        public MainWindow(MainController mainController)
        {
            this.mc = mainController;
            StartWindow();
        }

        private void Tick(object sender, EventArgs e)
        {
            t.Enabled = false;
            t.Tick -= Tick;
            new LoggInn(mc);

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
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
           // resetTime();
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
            string temp = sender.ToString().Remove(sender.ToString().Length - 2);
            temp = temp.Substring(temp.IndexOf(':') + 2);
            string[] salg = temp.Split('\n');
            int s = int.Parse(salg[1]);
           
            
            //

            sales.Add(new StringValue(salg[0] + ", " + s));
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;

            salgsum += s;
            //totalsum += s;

            salglabel.Text = salgsum + " nok";
            totalsumlabel.Text = totalsum + " nok";
            resetTime();
        }



        private void avsluttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void omToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
            resetTime();
        }

        private void instillingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow(mc, this);
            resetTime();
        }

        public void LockWindow()
        {
            Enabled = false;
            Visible = false;
            Hide();
            
        }

        private void initialiserDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                return;
            }

            OrderPanel o = new OrderPanel(this);

            PopulateOrder(o);


            UpdatePanels();
            totalsum += salgsum;
            salgsum = 0;
            totalsumlabel.Text = totalsum + " nok";
            salglabel.Text = salgsum + " nok";
            sales.Clear();
            resetTime();
        }

        private void PopulateOrder(OrderPanel o)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                o.SetText(item.Cells[0].Value.ToString());

                string s = item.Cells[0].Value.ToString();
                string[] salg = s.Split(',');
                int sum = int.Parse(salg[1]);


                mc.LagreSalg(salg[0], sum);
            }
            orderlist.Add(o);
        }

        public void UpdatePanels()
        {
            int lastX = 0;
            splitContainer5.Panel1.Controls.Clear();
            foreach (var item in orderlist)
            {
                item.Location = new System.Drawing.Point(lastX, 0);
                splitContainer5.Panel1.Controls.Add(item);
                lastX += item.Width + 5; 
            }
        }

        private void salgsoppsettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resetTime();
        }

        private void nyDagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mc.Restart();
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
            resetTime();
        }

        private void loggTextSkrevet(object sender, KeyPressEventArgs e)
        {
            LogText.SelectionColor = Color.Red;
        }

        internal void ReenableWindow()
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            resetTime();
        }

        internal void ReenableWindow(bool p)
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            if (p)
            {
                t.Interval = 120000;
                logTime = 120000;
            }
            else
            {
                t.Interval = 30000;
                logTime = 30000;
            }
            t.Enabled = true;
            t.Tick += LogOut;
        }

        internal void resetTime()
        {
            try
            {
                t.Stop();
                t.Interval = logTime;
                t.Start();
            }
            catch (Exception e)
            {
                
            }
        }

        private void LogOut(object sender, EventArgs e)
        
        {
            t.Tick -= LogOut;
            t.Enabled = false; 
            new LoggInn(mc);
        }

        private void loggUtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            t.Tick -= LogOut;
            t.Enabled = false;
            new LoggInn(mc);
        }

        internal void RemoveFromList(OrderPanel orderPanel)
        {
            int i = 0;
            foreach (var item in orderlist)
            {
                if (item == orderPanel)
                {
                    orderlist.RemoveAt(i);
                    return;
                }
                i++;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           HelperWindow hw =  new HelperWindow();
           hw.Show();
        }

        private void registrerNyBrukerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NyBruker(mc);
        }

        private void RegistrerArbeider_Click(object sender, EventArgs e)
        {
            new VelgBruker(mc);
        }

        internal void DagensBruker(UserLogg ul)
        {
            Users u = mc.GetBruker(ul.UserId);
            richTextBox1.AppendText(u.Navn + "\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sales.Count > 0)
            {
                //TODO finn tallet
                string s = sales.ElementAt(dataGridView1.SelectedRows[0].Index).Value;
                string[] salg = s.Split(',');
                int sum = int.Parse(salg[1]);

                salgsum -= sum;
                salglabel.Text = salgsum + " nok";
                sales.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }
    }
}


>>>>>>> 474350979bb1503efcfa629597afdced3b7966be
