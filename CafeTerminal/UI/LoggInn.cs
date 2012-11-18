using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CafeTerminal.UI
{
    public partial class LoggInn : Form
    {
        private Controller.MainController mc;
        private bool p;

        public LoggInn()
        {
            InitializeComponent();
        }

        public LoggInn(Controller.MainController mc)
        {
            InitializeComponent();
            this.mc = mc;
            mc.LockMainWindow();
            //name.Visible = false;
           // password.Visible = false;
            Show();
            BringToFront();
        }

        public LoggInn(Controller.MainController mc, bool p)
        {
            
            this.mc = mc;
            this.p = p;
            if (p)
            {
                InitializePassword();
            }
            else
            {
                InitializeName();
            }
            Show();
            button4.Click += Tilbake;
        }

        private void Tilbake(object sender, EventArgs e)
        {
            new LoggInn(mc);
            Dispose();
        }



        private void button1_Click(object sender, EventArgs e)
        {
           // mc.EnableMainWindow();
            new LoggInn(mc, true);
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new LoggInn(mc, false);
           // Visible = false;
            Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //lagre navn og stilling
            string navn = textBox2.Text;
            string yrke = textBox3.Text;
            if (navn.Length < 2 || yrke.Length < 2)
            {
                return;
            }
            mc.LagreBruker(navn, yrke);
            mc.EnableMainWindow(true);
            Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(mc.GetPassord()))
            {
                mc.EnableMainWindow(true);
                Dispose();
            }
        }

        private void Close(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void CheckEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Equals(mc.GetPassord()))
                {
                    mc.EnableMainWindow(true);
                    Dispose();
                }
            }
        }
        
    }
}
