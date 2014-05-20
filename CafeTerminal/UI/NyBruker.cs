using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CafeTerminal.DataAccess;
using DomainObjectsSalg.Sales;

namespace CafeTerminal.UI
{
    public partial class NyBruker : Form
    {
        private Controller.MainController mc;

        public NyBruker()
        {
            InitializeComponent();
        }

        public NyBruker(Controller.MainController mc) : this()
        {
            this.mc = mc;
            mc.LockMainWindow();

            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mc.EnableMainWindow();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 2)
            {
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    mc.LagreBruker(textBox1.Text, (radioButton1.Checked ? radioButton1.Text : radioButton2.Text));
                    mc.EnableMainWindow();
                    Dispose();
                }
            }
        }
    }
}
