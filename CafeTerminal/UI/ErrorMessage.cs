using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Visitor_Registration.UI
{
    public class ErrorMessage
    {
        public ErrorMessage(string p)
        {
            var i = MessageBox.Show(p,
                "Advarsel",
                MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation,
                MessageBoxDefaultButton.Button1);

            if (i == DialogResult.OK)
            {
            }
        }
    }
}
