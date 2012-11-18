using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visitor_Registration.UI
{
    public partial class NyBruker : Form
    {
        private Controller.MainController mc;

        public NyBruker()
        {
            InitializeComponent();
        }

        public NyBruker(Controller.MainController mc)
        {
            // TODO: Complete member initialization
            this.mc = mc;
        }
    }
}
