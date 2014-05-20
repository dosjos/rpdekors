using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using CafeRegnskap.Controller;
using DomainObjecsSalg2.Sales;

namespace CafeRegnskap
{
    public partial class Form1 : Form
    {
     //   private MainController mc;
        /**
         * Konstruktorrrr
         * */
        public Form1()
        {
            InitializeComponent();
        }

        /**
         * Oppstartsmetoden til dette vinduet, duh
         */
        private void Form1_Load(object sender, EventArgs e)
        {
    //        mc = new MainController();
          //  salg = mc.GetAllSales(); // henter fra database i stedet for
            salg = new List<Salg>();
            SetUpBoxes();
        }


        /**
         * Setter opp comboboxene
         */
        private void SetUpBoxes()
        {
            var y = (from s in salg
                     select s.SlagsTid.Year).Distinct().ToList();
            comboBox1.DataSource = y;

            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                var m = (from s in salg
                         where s.SlagsTid.Year == Convert.ToInt32(comboBox1.SelectedValue)
                         select s.SlagsTid.Month).Distinct().ToList();
                comboBox2.DataSource = m;
                comboBox2.SelectedIndex = -1;
                comboBox3.SelectedIndex = -1;
            }
            catch (Exception ee)
            {
                
            }
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            var d = (from s in salg
                     where s.SlagsTid.Year == Convert.ToInt32(comboBox1.SelectedValue)
                     && s.SlagsTid.Month == Convert.ToInt32(comboBox2.SelectedValue)
                     select s.SlagsTid.Day).Distinct().ToList();
            comboBox3.DataSource = d;
            comboBox3.SelectedIndex = -1;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var d = (from s in salg
                     where s.SlagsTid.Year == Convert.ToInt32(comboBox1.SelectedValue)
                           && s.SlagsTid.Month == Convert.ToInt32(comboBox2.SelectedValue)
                           && s.SlagsTid.Day == Convert.ToInt32(comboBox3.SelectedValue)
                     select s).ToList();

    

            var query = d.GroupBy(p => new
                        {
                            p.VareId,
                            p.Pris
                        })
                        .Select(g => new SalgView()
                        {
                            Id = g.Key.VareId,
                            Pris = g.Key.Pris,
                            Antall = g.Count(),
                            VareNavn = (from p in varer where p.Id == g.Key.VareId select p.Navn).Single()
                        }).ToList<SalgView>();

           // Console.WriteLine(query.Count());
            dataGridView1.Rows.Clear();
            int sum = 0;
            foreach (var salgView in query)
            {
                //String navn = mc.GetVareNavn(salgView.Id);
                var navn = from n in varer where n.Id == salgView.Id select n.Navn;
                string Navn = navn.ToString();
                dataGridView1.Rows.Add(salgView.VareNavn, salgView.Pris, salgView.Antall, salgView.Antall * salgView.Pris);
                sum += salgView.Antall*salgView.Pris;
            }
            label6.Text = "" + sum;
            SetRettLogg();
        }

        private void SetRettLogg()
        {
            richTextBox1.Clear();
            richTextBox1.Clear();
            try
            {
                logs = (from l in logg
                            where
                                l.LoggTid.Day == (int) comboBox3.SelectedValue &&
                                l.LoggTid.Month == (int) comboBox2.SelectedValue &&
                                l.LoggTid.Year == (int) comboBox1.SelectedValue
                            select l).ToList();

                int mes = 1;
                foreach (var l in logs)
                {
                    richTextBox1.Text += "MELDING " + mes + ": " + l.Text + "\n\n";
                   
                    mes++;
                }
            }
            catch (Exception e)
            {
               // Console.WriteLine(e.StackTrace);
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Velg regnskapet som skal importeres";

            fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                string path = fbd.SelectedPath;
                try
                {
                    FileStream stream = File.Open(path + "\\sales.con", FileMode.Open, FileAccess.Read);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof (List<Salg>));
                        salg = (List<Salg>) serializer.Deserialize(stream);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        stream.Close();
                    }

                    FileStream stream2 = File.Open(path + "\\logg.con", FileMode.Open, FileAccess.Read);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof (List<Logg>));
                        logg = (List<Logg>) serializer.Deserialize(stream2);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        stream2.Close();
                    }

                    FileStream stream3 = File.Open(path + "\\varer.con", FileMode.Open, FileAccess.Read);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof (List<Vare>));
                        varer = (List<Vare>) serializer.Deserialize(stream3);
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        stream3.Close();
                    }
                }
                catch (Exception eee)
                {
                    richTextBox1.Text = "Noen filer ble ikke hentet: " + eee.StackTrace;
                }
               // SetRettLogg();
               

                SetUpBoxes();
            }
        }

        public List<Vare> varer { get; set; }
        public List<Logg> logg { get; set; }
        public List<Salg> salg { get; set; }


        public List<Logg> logs { get; set; }
    }

    public class SalgView
    {
        public int Id;
        public string VareNavn;
        public int Antall;
        public int Pris;
        public int Sum;

    }
}
