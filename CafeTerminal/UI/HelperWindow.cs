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
    public partial class HelperWindow : Form
    {
        public HelperWindow()
        {
            InitializeComponent();
            this.Opacity = 0.50;
            Text = ""; 
            ControlBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
           // button1.BackColor = Color.FromArgb(255, Color.Red);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
