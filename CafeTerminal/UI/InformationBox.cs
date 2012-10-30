using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CafeTerminal
{
    public class InformationBox
    {
        public InformationBox(string p)
        {
            var i = MessageBox.Show(p,
                "Informasjon",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1);
        }
    }
}
