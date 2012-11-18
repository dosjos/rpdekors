using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Visitor_Registration.Controllers;
using DomainObjects;
using Visitor_Registration.DataAccesLayer;
using DomainObjects.Visit;


namespace Visitor_Registration
{
    public partial class RegisterKidForm : Form
    {
        MainController mainController;

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
        public RegisterKidForm()
        {
            InitializeComponent();
        }

        public RegisterKidForm(MainController mainController)
        {
            InitializeComponent();
            InitializeYearChooser();
            this.mainController = mainController;
            this.Visible = true;
        }

        private void InitializeYearChooser()
        {
            if (Visitor_Registration.DataAccesLayer.SettingsProvider.HaveAgeSettings())
            {
                int min = (int)Visitor_Registration.DataAccesLayer.SettingsProvider.GetLowestYear();
                int max = (int)Visitor_Registration.DataAccesLayer.SettingsProvider.GetHighestYear();
                for (int i = min; i <= max; i++)
                {
                    fAar.Items.Add(i);
                }
            }
            else
            {
                int min = CustomizationManager.GetLowestYear();
                int max = CustomizationManager.GetHighestYear();
                for (int i = min; i <= max; i++)
                {
                    fAar.Items.Add(i);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string fnavn = fNavn.Text.Trim();
            string enavn = eNavn.Text.Trim();
            string age = fAar.Text;
            Boolean Gender = gender2.Checked;//if true, male
            string postcode = postCode.Text.Trim();
            string email = eMail.Text.Trim();
            string telephone = tlf.Text.Trim();
            string etnisity = ethn.Text.Trim();
            
            Boolean done = true;

            if (fnavn.Length <= 1)
            {
                label1.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                label1.ForeColor = System.Drawing.Color.Black;
            }
            if (enavn.Length <= 1)
            {
                label2.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                label2.ForeColor = System.Drawing.Color.Black;
            }
            if (postcode.Length != 4)
            {
                label6.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                label6.ForeColor = System.Drawing.Color.Black;
            }
            if (fAar.Text.Length != 4)
            {
                label3.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                label3.ForeColor = System.Drawing.Color.Black;
            }

            if (done)
            {
                Kid k = new Kid();
                k.FirstName = fnavn;
                k.LastName = enavn;
                k.Age = Convert.ToInt32(age);
                k.Gender = Gender ? "Mann" : "Kvinne";
                k.Email = email;
                k.TLF = telephone;
                k.Postcode = Convert.ToInt32(postcode);
                mainController.SaveKid(k);
                //TODO Skjekk om det finnes en med samme navn fra før og evt fiks dette

                //this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mainController.ReEnableMainWindow();
            this.Dispose();
        }
    }
}
