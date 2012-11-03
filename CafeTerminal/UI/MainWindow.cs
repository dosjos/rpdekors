using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CafeTerminal.Controller;
using CafeTerminal.DataAccesLayer;
using CafeTerminal.UI;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        public MainController mc { get; set; }


        public MainWindow()
        {
            InitializeComponent();
            mc = new MainController(this);
           // this.Size = Screen.PrimaryScreen.WorkingArea.Size;
           // this.Location = Screen.PrimaryScreen.WorkingArea.Location;
            GetButtons();
            this.Resize += new EventHandler(ResizeButtons);
        }

        private void ResizeButtons(object sender, EventArgs e)
        {
            GetButtons();
        }

        private void GetButtons()
        {
           var buttons =  mc.GetVarerCurrentlyForSale();
           splitContainer3.Panel1.Controls.Clear();
            //create buttons code
           int lastX = 0;
           int lastY = 0;
           int total = 0;
           int divider= (int)Math.Ceiling(Math.Sqrt(buttons.Count));
           int w = splitContainer3.Panel1.Width;
           w = (w / divider);
           int h = splitContainer3.Panel1.Height;
           h = (h / (int)Math.Ceiling((buttons.Count/(double)divider)));
           
           for (int i = 0; i < Math.Sqrt(buttons.Count); i++)
           {
               for (int j = 0; j < Math.Sqrt(buttons.Count); j++)
               {
                   Button b = new Button();
                   b.Width = w;
                   b.Height = h;
                   b.BackColor = GetButtonColor();  
                   b.Font = new System.Drawing.Font("Viner Hand ITC", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                   b.Location = new Point(lastX, lastY);
                   b.Text = buttons[total].Navn + "\n" + buttons[total].Pris + " kr" ;
                   splitContainer3.Panel1.Controls.Add(b);
                   lastX += b.Width;
                   total++;
                   if (total == buttons.Count)
                   {
                       return;
                   }
               }
               lastY += h;
               lastX = 0;
           }
        }

        private Color GetButtonColor()
        {
            Random r = new Random();

            
            Color[] farger = new Color[11];
            farger[0] = System.Drawing.Color.YellowGreen;
            farger[1] = System.Drawing.Color.Azure;
            farger[2] = System.Drawing.Color.BurlyWood;
            farger[3] = System.Drawing.Color.DarkOliveGreen;
            farger[4] = System.Drawing.Color.YellowGreen;
            farger[5] = System.Drawing.Color.MidnightBlue;
            farger[6] = System.Drawing.Color.Violet;
            farger[7] = System.Drawing.Color.WhiteSmoke;
            farger[8] = System.Drawing.Color.Tomato;
            farger[9] = System.Drawing.Color.Wheat;
            farger[10] = System.Drawing.Color.SaddleBrown;

            return farger[r.Next(farger.Length)];
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
            new SettingsWindow(mc);
        }

        private void LockWindow()
        {
            Enabled = false;
        }

        private void initialiserDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }




    }
}
