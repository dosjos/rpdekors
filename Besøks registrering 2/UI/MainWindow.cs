
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using CafeTerminal.DomainObjects;
using CafeTerminal.Controllers;

using FluentNHibernate;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Automapping;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Criterion;
using CafeTerminal.DataAccesLayer;
using CafeTerminal.UI;
using System.Drawing.Drawing2D;
using CafeTerminal.Mocking;

namespace CafeTerminal
{
    public partial class MainWindow : Form
    {
        private BindingList<StringValue> visitors;
        private MainController mc;

        string imageleft = null;
        string imageright = null;
        public MainWindow(MainController mc)
        {
            this.mc = mc;
            InitializeComponent();

            initializeStuff();
        }

        public MainWindow()
        {
            InitializeComponent();
            mc = new MainController(this);
            // TODO: Complete member initialization

           // NHibernateHelper.ResetDatabase(); //RESET HOTFIX OF DATABASE

            initializeStuff();
        }

        #region startup
        private void initializeStuff()
        {

            mc.Settingscheck();
#if !DEBUG
            utviklingToolStripMenuItem.Enabled = false;
#endif
            visitors = new BindingList<StringValue>();
            try
            {
                ConfigureDataGrid();
                PopulateDataGrid();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            comboBox1.DataSource = mc.getAllKids();
            SizeChanged += WindowOnSizeChanged;

            InitializeImages();


        }


        #endregion

        #region images
        private void WindowOnSizeChanged(object sender, EventArgs e)
        {
            if (imageleft != null)
            {
                ResizeImage(imageleft, true);
            }
            if (imageright != null)
            {
                ResizeImage(imageright, false);
            }
        }

        public void InitializeImages()
        {
            if (mc.HaveLeftImage())
            {
                imageleft = mc.GetLeftImage();   
                ResizeImage(imageleft, true);
            }
            else
            {
                imageleft = null;
                pictureBox1.Image = null;
            }

            if (mc.HaveRightImage())
            {
                imageright = mc.GetRightImage();
                ResizeImage(imageright, false);
            }
            else
            {
                imageright = null;
                pictureBox2.Image = null;
            }
        }

        private void ResizeImage(string imageName, Boolean side)
        {
            Image image = Image.FromFile(imageName);
            PictureBox temp;
            SplitterPanel control;
            if (side)
            {
                temp = pictureBox1;
                control = splitContainer2.Panel1;
            }
            else
            {
                temp = pictureBox2;
                control = splitContainer2.Panel2;
            }
            temp.Width = control.Width;
            temp.Height = control.Height;

            Bitmap backgroundBitmap = new Bitmap(control.Width, control.Height);
            using (Bitmap tempBitmap = new Bitmap(image))
            {
                using (Graphics g = Graphics.FromImage(backgroundBitmap))
                {
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    // Get the set of points that determine our rectangle for resizing.
                    Point[] corners = { new Point(0, 0), new Point(backgroundBitmap.Width, 0), new Point(0, backgroundBitmap.Height) };
                    g.DrawImage(tempBitmap, corners);
                }
            }
            temp.Image = backgroundBitmap;
        }


        #endregion

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

        #region registerkids
        private void button1_Click(object sender, EventArgs e)
        {
            RegisterKidFromComboBox();
            comboBox1.Text = "";
        }

        private void keyPressed(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                RegisterKidFromComboBox();
                comboBox1.Text = "";
            }
        }

        private void RegisterKidFromComboBox()
        {
            mc.RegisterKid((string)comboBox1.Text);
        }

        public void AddVisit(string name)
        {
            visitors.Add(new StringValue(name));
        }
        #endregion

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

        #region Windows
        private void EnableAboutBox(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog(this);
        }

        private void OpenStatisticsWindow(object sender, EventArgs e)
        {
            Statistics stat = new Statistics(mc);
            stat.Visible = true;
        }

        private void nyDagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mc.restart();
        }

        #endregion

        #region development
        private void MockVisitData(object sender, EventArgs e)
        {
            visitMocker vm = new visitMocker();
            vm.MockVisits();
        }
        private void ResetDatabaseButton(object sender, EventArgs e)
        {
            NHibernateHelper.ResetDatabase();
        }
        #endregion

        #region exit
        public void ExitApplication(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        private void kontrollpanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mc.NewControllpanel();
        }


    }
}

