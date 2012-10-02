using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Visitor_Registration.DomainObjects;
using Visitor_Registration.DataAccesLayer;

namespace Visitor_Registration
{
    public partial class RegisterKidForm : Form
    {
        private Form1 form1;

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
        public RegisterKidForm()
        {
            InitializeComponent();
        }

        public RegisterKidForm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string fnavn = fNavn.Text;
            string enavn = eNavn.Text;
            string age = fAar.Text;
            Boolean Gender = gender2.Checked;//if true, male
            string postcode = postCode.Text;
            string email = eMail.Text;
            string telephone = tlf.Text;
            string etnisity = ethn.Text;
            
            Boolean done = true;

            if (fnavn.Length <= 1)
            {
                fNavn.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                fNavn.ForeColor = System.Drawing.Color.Black;
            }
            if (enavn.Length <= 1)
            {
                eNavn.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                eNavn.ForeColor = System.Drawing.Color.Black;
            }
            if (postcode.Length != 4)
            {
                postCode.ForeColor = System.Drawing.Color.Red;
                done = false;
            }
            else
            {
                postCode.ForeColor = System.Drawing.Color.Black;
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
                KidProvider.Save(k);
                form1.Enabled = true;
                form1.AddVisit(fnavn);
                this.Dispose();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form1.Enabled = true;
            this.Dispose();
        }





    }
}
