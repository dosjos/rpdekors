using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using CafeTerminal.Controller;
using CafeTerminal.DataAccesLayer;
using CafeTerminal.DataAccess;
using DomainObjectsSalg.Sales;
using Timer = System.Windows.Forms.Timer;

namespace CafeTerminal.UI
{
    public partial class MainWindow : Form
    {
        public MainController Mc { get; set; }
        private readonly BindingList<StringValue> _sales = new BindingList<StringValue>();
        int _logTime;
        int _totalsum;
        int _salgsum;
        Timer _t;
        readonly List<OrderPanel> _orderlist = new List<OrderPanel>();
        DataProvider dataProvider = new DataProvider();


        public MainWindow()
        {
            Mc = new MainController(this, dataProvider);
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
               // NHibernateHelper.ResetDatabase();//TODO finn hvilken spesifikk exception som må kastes for at denne kommandoen skal kjøres
                GetButtons();
            }
            CreateDataGrid();

            GetLogg();

            GetDagensSalg();

            GetDagensArbeidere();
#if !DEBUG
            initialiserDatabaseToolStripMenuItem.Enabled = false;
#endif
            if (Mc.HavePassSetting())
            {
                _t = new Timer();
                _t.Tick += Tick;
                _t.Interval = 50;
                _t.Enabled = true;
            }
            try
            {
                const int hour = 00;
                const int minute = 05;
                const int second = 59;
                var year = DateTime.Now.Year;
                var month = DateTime.Now.Month;
                var day = DateTime.Now.Day;
                var target = new DateTime(year, month, day, hour, minute, second);
                int interval = (int)(target - DateTime.Now).TotalMilliseconds;
                var timer = new System.Timers.Timer(interval);
                timer.Elapsed += timer_Elapsed;
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
            if (InvokeRequired)
            {
                var d = new RestartCalback(RestartWindow);
                Invoke(d);
            }
            else
            {
                Mc.Restart();
            }
        }

        public MainWindow(MainController mainController)
        {
            Mc = mainController;
            StartWindow();
        }

        private void Tick(object sender, EventArgs e)
        {
            _t.Enabled = false;
            _t.Tick -= Tick;
            new LoggInn(Mc);
        }


        private void GetDagensSalg()
        {
            _totalsum = Mc.GetDagensSalg();
            totalsumlabel.Text = "" + _totalsum;
        }

        private void GetDagensArbeidere()
        {
            var l = Mc.GetTodaysUsers();

            foreach (var userLogg in l)
            {
                richTextBox1.Text += userLogg.Users.Navn + "\n";
            }
            //  richTextBox1.Text = Mc.GetDageUsers();
        }

        private void GetLogg()
        {
            var l = Mc.GetLastLog();
            if (l != null)
            {
                LogText.Text = l.Text;
            }
        }

        private void CreateDataGrid()
        {
            this.Resize += ResizeButtons;

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            var modelColumn = new DataGridViewTextBoxColumn();
            dataGridView1.DataSource = _sales;
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
            var buttons = Mc.GetVarerCurrentlyForSale();
            if (buttons.Count == 0)
            {

                return;
            }

            //create buttons code
            var lastX = 0;
            var lastY = 0;
            var total = 0;
            var divider = (int)Math.Ceiling(Math.Sqrt(buttons.Count));
            var w = splitContainer3.Panel1.Width;
            w = (w / divider);
            var h = splitContainer3.Panel1.Height;
            h = (h / (int)Math.Ceiling((buttons.Count / (double)divider)));

            for (var i = 0; i < Math.Sqrt(buttons.Count); i++)
            {
                for (var j = 0; j < Math.Sqrt(buttons.Count); j++)
                {
                    var b = new Button
                    {
                        Width = w,
                        Height = h,
                        BackColor = GetButtonColor(buttons[total]),
                        Font =
                            new Font("Viner Hand ITC", 20.25F, System.Drawing.FontStyle.Bold,
                                GraphicsUnit.Point, ((byte) (0))),
                        Location = new Point(lastX, lastY),
                        Text = buttons[total].Navn + "\n" + buttons[total].Pris + " kr"
                    };
                    b.Click += menuItem_Click;
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

        private Color GetButtonColor(Vare vare)
        {
            if (vare.Farge != null)
            {
                return Color.FromArgb(Convert.ToInt32(vare.Farge));
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

            _sales.Add(new StringValue(salg[0] + ", " + s));
            dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;

            _salgsum += s;
            //totalsum += s;

            salglabel.Text = _salgsum + " nok";
            totalsumlabel.Text = _totalsum + " nok";
            ResetTime();
        }



        private void avsluttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void omToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
            ResetTime();
        }

        private void instillingerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsWindow(Mc, this);
            ResetTime();
        }

        public void LockWindow()
        {
            Enabled = false;
            Visible = false;
            Hide();

        }

        private void initialiserDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // NHibernateHelper.ResetDatabase();
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
            _totalsum += _salgsum;
            _salgsum = 0;
            totalsumlabel.Text = _totalsum + " nok";
            salglabel.Text = _salgsum + " nok";
            _sales.Clear();
            ResetTime();
        }

        private void PopulateOrder(OrderPanel o)
        {
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                o.SetText(item.Cells[0].Value.ToString());

                string s = item.Cells[0].Value.ToString();
                string[] salg = s.Split(',');
                int sum = int.Parse(salg[1]);


                Mc.LagreSalg(salg[0], sum);
            }
            _orderlist.Add(o);
        }

        public void UpdatePanels()
        {
            int lastX = 0;
            splitContainer5.Panel1.Controls.Clear();
            foreach (var item in _orderlist)
            {
                item.Location = new System.Drawing.Point(lastX, 0);
                splitContainer5.Panel1.Controls.Add(item);
                lastX += item.Width + 5;
            }
        }

        private void salgsoppsettToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetTime();
        }

        private void nyDagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Mc.Restart();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var l = new Logg
            {
                Text = LogText.Text,
                LoggTid = DateTime.Now
            };

            Mc.LagreLogg(l);
            var a = LogText.Text;
            LogText.SelectionColor = Color.Black;
            LogText.ForeColor = Color.Black;
            LogText.Text = "";
            LogText.SelectedText = "";
            LogText.SelectedText = a;
            ResetTime();
        }

        private void LoggTextSkrevet(object sender, KeyPressEventArgs e)
        {
            LogText.SelectionColor = Color.Red;
        }

        internal void ReenableWindow()
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            ResetTime();
        }

        internal void ReenableWindow(bool p)
        {
            Enabled = true;
            Visible = true;
            Show();
            BringToFront();
            if (p)
            {
                _t.Interval = 120000;
                _logTime = 120000;
            }
            else
            {
                _t.Interval = 30000;
                _logTime = 30000;
            }
            _t.Enabled = true;
            _t.Tick += LogOut;
        }

        internal void ResetTime()
        {
            try
            {
                if (_t != null && _t.Enabled)
                {
                    _t.Stop();
                    _t.Interval = _logTime;
                    _t.Start();
                }
            }
            catch (Exception e)
            {

            }
        }

        private void LogOut(object sender, EventArgs e)
        {
            _t.Tick -= LogOut;
            _t.Enabled = false;
            new LoggInn(Mc);
        }

        private void loggUtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _t.Tick -= LogOut;
            _t.Enabled = false;
            new LoggInn(Mc);
        }

        internal void RemoveFromList(OrderPanel orderPanel)
        {
            int i = 0;
            foreach (var item in _orderlist)
            {
                if (item == orderPanel)
                {
                    _orderlist.RemoveAt(i);
                    return;
                }
                i++;
            }
        }
        /*
        private void button3_Click(object sender, EventArgs e)
        {
            var hw = new HelperWindow();
            hw.Show();
        }
        */

        private void registrerNyBrukerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new NyBruker(Mc);
        }

        private void RegistrerArbeider_Click(object sender, EventArgs e)
        {
            new VelgBruker(Mc);
        }

        internal void DagensBruker(UserLogg ul)
        {
            var u = Mc.GetBruker(ul.UsersId);
            richTextBox1.AppendText(u.Navn + "\n");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (_sales.Count > 0)
            {
                //TODO finn tallet
                string s = _sales.ElementAt(dataGridView1.SelectedRows[0].Index).Value;
                string[] salg = s.Split(',');
                int sum = int.Parse(salg[1]);

                _salgsum -= sum;
                salglabel.Text = _salgsum + " nok";
                _sales.RemoveAt(dataGridView1.SelectedRows[0].Index);
            }
        }

        private void eksporterDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ExportVindu(dataProvider);
        }

      

        private void sendRapportklageforslagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RapportVindu();
        }

        private void registrerSvinnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new RegistrerSvinn();
        }
    }
}


