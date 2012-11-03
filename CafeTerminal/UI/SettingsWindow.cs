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
    public partial class SettingsWindow : Form
    {
        private Controller.MainController mc;

        public SettingsWindow()
        {
            InitializeComponent();
            Show();
        }

        public SettingsWindow(Controller.MainController mc)
        {
            this.mc = mc;
            InitializeComponent();
            Show();
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
    }
}
