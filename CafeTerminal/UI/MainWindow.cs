using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeTerminal.UI;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
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
            new Settings();
        }

        //create buttons code
//        int lastX = 0;
//for (int i = 0; i < 4; i++) {
//  Button b = new Button();
//  b.Location = new Point(lastX, 0);
//  this.Controls.Add(b);
//  lastX += b.Width;
//}
    }
}
