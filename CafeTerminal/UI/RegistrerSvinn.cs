using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace CafeTerminal.UI
{
    public partial class RegistrerSvinn : Form
    {
        public RegistrerSvinn()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            textBox1.Focus();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || textBox1.Text.Length < 3)
            {
                textBox1.Focus();
                var tip = new ToolTip();
                tip.SetToolTip(textBox1, "Fyll ut tittel på varen det registreres svinn på");
                tip.Show("Varen må ha en tittel", textBox1);
            }
        }
    }
}
