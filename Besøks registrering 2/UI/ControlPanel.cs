using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visitor_Registration.DataAccesLayer;

namespace Visitor_Registration.UI
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
        }
    }
}
