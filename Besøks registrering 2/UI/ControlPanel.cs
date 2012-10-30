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
using CafeTerminal.DataAccesLayer;

namespace CafeTerminal.UI
{
    public partial class ControlPanel : Form
    {
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
            Console.WriteLine("Kom her");
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
    }
}
