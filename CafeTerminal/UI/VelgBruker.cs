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
    public partial class VelgBruker : Form
    {
        private Controller.MainController mc;

        public VelgBruker()
        {
            InitializeComponent();
        }

        public VelgBruker(Controller.MainController mc)
        {
            // TODO: Complete member initialization
            this.mc = mc;
        }
    }
}
