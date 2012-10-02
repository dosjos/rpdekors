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


using FluentNHibernate;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using Visitor_Registration.DataAccesLayer;



namespace Visitor_Registration
{
    public partial class Form1 : Form
    {
        private BindingList<StringValue> visitors;
        private KidProvider kidProvider;
        RegisterKidForm registrerKid;
        public Form1()
        {
            InitializeComponent();
            kidProvider = new KidProvider();
            visitors = new BindingList<StringValue>();
            //Fyll opp listen visitors med dagens besøkende, konkatiner for og etternavn
            dataGridView1.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            dataGridView1.DataSource = visitors;
            modelColumn.HeaderText = "Navn";
            modelColumn.DataPropertyName = "Value";
            modelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns.Add(modelColumn);

            comboBox1.DataSource = kidProvider.getAllKids();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string kidName = (string)comboBox1.Text;
            if (kidProvider.RegisterKid(kidName))//Ligger i DB
            {
                Console.WriteLine("Kid in db");
                string fnavn = kidProvider.GetFirstName(kidName);
                Console.WriteLine("added kid to list");
                visitors.Add(new StringValue(fnavn));
            }
            else//Må opprette Kid
            {
                Console.WriteLine("Must make kid");
                registrerKid = new RegisterKidForm(this);
                registrerKid.Visible = true;
                this.Enabled = false;
            }
        }

        public void AddVisit(string name)
        {
            visitors.Add(new StringValue(name));
        }
        

        private void createDatabase()
        {

            String str;
            SqlConnection myConn = new SqlConnection("Server=JANOLE-PC\\SQLEXPRESS;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE VisitDatabase ON PRIMARY " +
                "(NAME = VisitDatabase_Data, " +
                "FILENAME = 'C:\\torrenter\\VisitDatabase.mdf', " +
                "SIZE = 10MB, MAXSIZE = 100MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = VisitDatabase_Log, " +
                "FILENAME = 'C:\\torrenter\\VisitDatabaseLog.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        private void ResetDatabaseButton(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }


    }
}
