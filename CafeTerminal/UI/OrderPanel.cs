using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Visitor_Registration.UI
{
    public partial class OrderPanel : UserControl
    {
        private MainWindow mainWindow;

        public OrderPanel()
        {
            InitializeComponent();
        }

        public OrderPanel(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            label1.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        public void SetText(params string[]list )
        {
           // Orders.DataS = list;
            foreach (var item in list)
            {
                Orders.Text += item + '\n';
            }
        }
        private void ClosePanel_Click(object sender, EventArgs e)
        {
            mainWindow.RemoveFromList(this);
            mainWindow.UpdatePanels();
            this.Dispose();
        }
    }
}
