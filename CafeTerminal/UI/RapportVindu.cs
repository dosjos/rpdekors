using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;

namespace CafeTerminal.UI
{
    public partial class RapportVindu : Form
    {
        public RapportVindu()
        {
            InitializeComponent();
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var message = new MailMessage();
            message.To.Add("j.skotterud@gmail.com");
            message.Subject = @"[Condio][Cafe] Rapport";
            message.From = new System.Net.Mail.MailAddress("condiocaferegn@gmail.com");
            message.Body = richTextBox1.Text;
            var smtp = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("condiocaferegn@gmail.com", "passforcondregn"),
                    EnableSsl = true
                };
             
            smtp.Send(message);
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dispose();
        }

  
    }
}
