using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CafeTerminal.DataAccess;
using DomainObjecsSalg.Sales;


namespace CafeTerminal.UI
{
    public partial class NewVare : Form
    {
        private Controller.MainController mc;
        private SettingsWindow settingsWindow;
        #region CloseButton
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        #endregion
        public NewVare()
        {
            InitializeComponent();
        }

        public NewVare(Controller.MainController mc)
        {
            this.mc = mc;
            InitializeComponent();
            Show();
        }

        public NewVare(Controller.MainController mc, SettingsWindow settingsWindow)
        {
            // TODO: Complete member initialization
            this.mc = mc;
            this.settingsWindow = settingsWindow;
            InitializeComponent();
            Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            settingsWindow.ReactivateSettingsWindow();
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (navn.Text.Length < 2)
                {
                    label1.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    label1.ForeColor = System.Drawing.Color.Black;
                }

                if (pris.Text.Length == 0)
                {
                    label2.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    label2.ForeColor = System.Drawing.Color.Black;
                }


                Vare vare = new Vare()
                {
                    Navn = navn.Text,
                    Pris = int.Parse(pris.Text)
                };
                Console.WriteLine("HEEER");
                mc.SaveVare(vare);
                try
                {
                    
                    
                    mc.UpdateMainButtons();
                    
                }
                catch (Exception ew)
                {
                    
                }
                
                settingsWindow.ReactivateSettingsWindow();
                settingsWindow.InitializeList();
                Dispose();
            }
            catch (Exception ee)
            {
                label2.ForeColor = System.Drawing.Color.Red;

            }
        }
    }
}
