using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainObjects.Settings;
using Visitor_Registration.DataAccesLayer;
using Visitor_Registration.DomainObjects;

namespace Visitor_Registration.UI
{
    public partial class ControlPanel : Form
    {
        private Kid kk;
        private Controllers.MainController mainController;

        public ControlPanel()
        {

        }

        public ControlPanel(Controllers.MainController mainController)
        {
            // TODO: Complete member initialization
            this.mainController = mainController;
            InitializeComponent();

            InitializeNumberPickers();
            ControlBox = false;
            Text = "";

            Visible = true;
            openFileDialog1.Filter = "Image files|*.jpg; *.jpeg; *.png; *.gif";
            GetImages();
            WodoPanel.Visible = false;



            comboBox1.DataSource = mainController.getAllKids();
          
        }

        private void GetImages()
        {
            if (mainController.HaveLeftImage())
            {
                vbilde.Text = mainController.GetLeftImage();
            }
            if (mainController.HaveRightImage())
            {
                hbilde.Text = mainController.GetRightImage();
            }
        }

        private void InitializeNumberPickers()
        {
            if (SettingsProvider.HaveAgeSettings())
            {
                numericUpDown1.Value = SettingsProvider.GetLowestYear();
                numericUpDown2.Value = SettingsProvider.GetHighestYear();
            }
            else
            {
                numericUpDown1.Value = CustomizationManager.GetLowestYear();
                numericUpDown2.Value = CustomizationManager.GetHighestYear();
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

            mainController.ReEnableMainWindow();
            Dispose();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SettingsProvider.SaveLowestYear(numericUpDown1.Value);
            SettingsProvider.SaveHighestYear(numericUpDown2.Value);

            mainController.InsertLeftImage(vbilde.Text);
            mainController.InsertRightImage(hbilde.Text);
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                vbilde.Text = openFileDialog1.FileName;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                hbilde.Text = openFileDialog1.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (mainController.HavePassSetting())
            {
                if (mainController.GetPassord().Equals(textBox1.Text))
                {
                    WodoPanel.Visible = true;
                }
            }
            else
            {
                WodoPanel.Visible = true;
            }
            
        }

        private void ValgtKiddClick(object sender, EventArgs e)
        {
            VelgKid();
        }

        private void VelgKid()
        {
            if (comboBox1.Text.Length > 1)
            {
                var k = mainController.GetKid(comboBox1.Text);
                textBox2.Text = k.FirstName;
                textBox3.Text = k.LastName;
                textBox4.Text = ""+k.Age;
                textBox5.Text = k.Email;
                textBox6.Text = k.Ethnisity;
                textBox7.Text = ""+k.Postcode;
                textBox8.Text = k.TLF;
                if (k.Gender.Equals("Mann"))
                {
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                }
                else
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                }
                kk = k;
            }
        }

        private void keyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                VelgKid();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            kk.Deleted = true;
            mainController.UpdateKid(kk);
            comboBox1.DataSource = null;
            comboBox1.Items.Clear();
            
            comboBox1.DataSource = mainController.getAllKids();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kk.FirstName = textBox2.Text ;
            kk.LastName=textBox3.Text  ;
            kk.Age=Convert.ToInt32(textBox4.Text)  ;
            kk.Email=textBox5.Text ;
            kk.Ethnisity=textBox6.Text ;
            kk.Postcode=Convert.ToInt32(textBox7.Text) ;
            kk.TLF=textBox8.Text;

            kk.Gender = radioButton1.Checked ? "Mann" : "Kvinne";
            mainController.UpdateKid(kk);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool bytt = true;
            if (mainController.HavePassSetting())
            {
                if (!mainController.GetPassord().Equals(textBox9.Text))
                {
                    bytt = false;
                }
            }
            if (bytt)
            {
                if (textBox10.Text.Equals(textBox11.Text))
                {
                    Settings s = new Settings() { Type = "Passord", Value = textBox11.Text };
                    mainController.LagrePassord(s);
                    mainController.ReEnableMainWindow();
                    Dispose();
                }
            }
        }
    }
}
