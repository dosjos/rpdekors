using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visitor_Registration.Controllers;

namespace Visitor_Registration.UI
{
    public class ErrorMessage
    {
        MainController mainController;
        public ErrorMessage(MainController mainController, string p)
        {
            this.mainController = mainController;
            var i = MessageBox.Show(p,
                "Advarsel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (i == DialogResult.OK)
            {
                mainController.ReEnableMainWindow();
            }
        }
    }
}
