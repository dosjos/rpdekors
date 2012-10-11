using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using Visitor_Registration.DomainObjects;
using Visitor_Registration.Controllers;

using FluentNHibernate;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using Visitor_Registration.DataAccesLayer;
using Visitor_Registration.UI;


namespace Visitor_Registration
{
    public partial class MainWindow : Form
    {
        private BindingList<StringValue> visitors;
        private MainController mc;

        public MainWindow()
        {
            InitializeComponent();
            mc = new MainController(this);
            //NHibernateHelper.ResetDatabase(); //RESET HOTFIX OF DATABASE
            visitors = new BindingList<StringValue>();
            ConfigureDataGrid();
            PopulateDataGrid();
            comboBox1.DataSource = mc.getAllKids();
            SizeChanged += WindowOnSizeChanged;
        }

        private void WindowOnSizeChanged(object sender, EventArgs e)
        {
            ResizeImage("image1.jpg", true);
            ResizeImage("image2.jpg", false);
        }

        private void ResizeImage(string imageName, Boolean side)
        {
            Image image = Image.FromFile("Images/" + imageName);
            PictureBox temp;
            SplitterPanel control;
            if (side)
            {
                temp = pictureBox1;
                control = splitContainer2.Panel1;
            }
            else {
                temp = pictureBox2;
                control = splitContainer2.Panel2;
            }
            temp.Image = image;
            temp.Width = control.Width;
            temp.Height = control.Height;
            temp.SizeMode = PictureBoxSizeMode.AutoSize;
            //temp.Image.Width = temp.Width;
           // temp.Image.Height = temp.Height;
        }


        public void AddVisit(string name)
        {
            visitors.Add(new StringValue(name));
        }

        private void ResetDatabaseButton(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }

        #region DATAGRID

        private void PopulateDataGrid()
        {
               var items = mc.GetTodaysVisits();
               foreach (var item in items)
               {
                   visitors.Add(item);
               }
        }

        private void ConfigureDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            dataGridView1.DataSource = visitors;
            modelColumn.HeaderText = "Navn";
            modelColumn.DataPropertyName = "Value";
            modelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(modelColumn);
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterKidFromComboBox();
        }

        private void keyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RegisterKidFromComboBox();
            }
        }

        private void RegisterKidFromComboBox()
        {
            mc.RegisterKid((string)comboBox1.Text);
        }

        private void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }

        #region GenericKids
        private void AddAnonym(object sender, EventArgs e)
        {
            AddGenericType("Anonym");
        }

        private void AddGirl(object sender, EventArgs e)
        {
            AddGenericType("Jente");
        }

        private void AddBoy(object sender, EventArgs e)
        {
            AddGenericType("Gutt");
        }

        private void AddUnknown(object sender, EventArgs e)
        {
            AddGenericType("Ukjent");
        }

        
        private void AddGenericType(string s)
        {
            MainController.AdddGenericVisit(s);
        }
        #endregion

        private void EnableAboutBox(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }
    }
}


////Musejeger er selve programmet, i programmets verden finnes det både katter og mus
////en katt kan fange mus
////en mus kan være redd for katter, men ikke nødvendigvis


////Denne går i Katt.Java

//public class Katt
//{
//    public String navn;
//    public int antallFangedeMus = 0;

//    //Denne konstruktøren kjører når katten blir opprettet og tar imot navned skrevet i Katt("navnher");
//    public Katt(String s)
//    {
//        navn = s;
//    }

//    //Denne metoden skjekker om katten kan fange og spise musen
//    public void fangMus(Mus m)
//    {
//        if (m.reddForKatt && !m.erSpist)
//        {
//            antallFangedeMus++;
//            m.erSpist = true;
//        }

//    }
//}

////denne i Mus.java
//public class Mus
//{
//    public Boolean reddForKatt;
//    public Boolean erSpist = false;

//    public Mus(Boolean b)
//    {
//        reddForKatt = b;
//    }
//}

////Denne går i Musejeger.java
//public class MuseJeger
//{
//    public static void main(String[] args)
//    {
//        //Jeg oppretter to katter
//        Katt k1 = new Katt("Jostein");
//        Katt k2 = new Katt("Kjell");


//        //MULIGHET 1
//        //Oppretter seks mus
//        Mus m1 = new Mus(true);
//        Mus m2 = new Mus(false);
//        Mus m3 = new Mus(true);
//        Mus m4 = new Mus(false);
//        Mus m5 = new Mus(true);
//        Mus m6 = new Mus(true);
//        //Nå har jeg 2 katter og seks mus rom alle sammen løper rundt og noen ganger klarer en katt å få tak i en mus 
//        //men katten klarer bare å fange den dersom han er redd for katten

//        k1.fangMus(m1);

//        k2.fangMus(m5);

//        k1.fangMus(m2);

//        System.out.println("Hittil har katt " + k1.navn + " spist " + k1.antallFangedeMus +" uheldige mus\n\n ");

//        k2.fangMus(m3);
//        k2.fangMus(m4);
//        k2.fangMus(m5);
//        System.out.println("Hittil har katt " + k1.navn + " spist " + k1.antallFangedeMus +" uheldige mus ");
//        System.out.println("Hittil har katt " + k2.navn + " spist " + k1.antallFangedeMus +" uheldige mus ");

//        //MULIGHET 1 SLUTT

//        //MULIGHET 2 med arrayer 
//        Mus[] museArray = new Mus[6];
//        museArray[0] = new Mus(true);
//        museArray[1] = new Mus(true);
//        museArray[2] = new Mus(false);
//        museArray[3] = new Mus(true);
//        museArray[4] = new Mus(false);
//        museArray[5] = new Mus(true);


//        k1.fangMus(museArray[0]);

//        k2.fangMus(museArray[4]);

//        k1.fangMus(museArray[1]);

//        System.out.println("Hittil har katt " + k1.navn + " spist " + k1.antallFangedeMus +" uheldige mus\n\n ");

//        k2.fangMus(museArray[2]);
//        k2.fangMus(museArray[3]);
//        k2.fangMus(museArray[5]);
//        System.out.println("Hittil har katt " + k1.navn + " spist " + k1.antallFangedeMus +" uheldige mus ");
//        System.out.println("Hittil har katt " + k2.navn + " spist " + k1.antallFangedeMus +" uheldige mus \n\n");
        
//        for(int i = 0; i < museArray.length; i++){
//            if(museArray[i].erSpist){
//                System.out.println("Mus " + (i+1) + " es spist, snufs");
//            }else{
//                System.out.println("Mus " + (i+1) + " lever, JIPPI");
//            }
//        }
//        //MULIGHET 2 SLUTT
//    }
//}

