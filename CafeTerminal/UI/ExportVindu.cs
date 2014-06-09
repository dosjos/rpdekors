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
using CafeTerminal.DataAccess;
using DomainObjectsSalg.Sales;

namespace CafeTerminal.UI
{
    public partial class ExportVindu : Form
    {
        private readonly DataProvider dataProvider;

        public ExportVindu(DataProvider _dataProvider)
        {
            dataProvider = _dataProvider;
            InitializeComponent();
            Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Velg plassering for eksportering av dataProvider";
             fbd.RootFolder = System.Environment.SpecialFolder.MyComputer;

             DialogResult result = fbd.ShowDialog();
             if (result == DialogResult.OK)
             {
                 var salg = dataProvider.GetSalesIn(dateTimePicker1.Value, dateTimePicker2.Value);
                 var varer =  dataProvider.GetAlleVarer();
                 var kommentarer = dataProvider.GetAlleLogger(dateTimePicker1.Value, dateTimePicker2.Value);
                 foreach (var salg1 in salg)
                 {
                     Console.WriteLine(salg1.Pris);
                 }
                //fbd.SelectedPath
                 string file = fbd.SelectedPath + "\\" + dateTimePicker1.Value.Day + "." + dateTimePicker1.Value.Month +
                               "." + dateTimePicker1.Value.Year
                               + "-" + dateTimePicker2.Value.Day + "." + dateTimePicker2.Value.Month + "." +
                               dateTimePicker2.Value.Year;
                 System.IO.Directory.CreateDirectory(file);
                 FileStream stream = File.Open(file +"\\sales.con", FileMode.OpenOrCreate);
                 try
                 {
                     var serializer = new XmlSerializer(typeof(List<Salg>));
                     serializer.Serialize(stream, salg);
                 }
                 finally
                 {
                     stream.Close();
                 }

                 FileStream stream2 = File.Open(file + "\\logg.con", FileMode.OpenOrCreate);
                 try
                 {
                     var serializer = new XmlSerializer(typeof(List<Logg>));
                     serializer.Serialize(stream2, kommentarer);
                 }
                 finally
                 {
                     stream2.Close();
                 }

                 FileStream stream3 = File.Open(file + "\\varer.con", FileMode.OpenOrCreate);
                 try
                 {
                     var serializer = new XmlSerializer(typeof(List<Vare>));
                     serializer.Serialize(stream3, varer);
                 }
                 finally
                 {
                     stream3.Close();
                 }
                 this.Dispose();
             }
        }
    }
}
