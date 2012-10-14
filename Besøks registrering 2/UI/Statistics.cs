using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using DomainObjects.Visit;
using System.Windows.Forms.DataVisualization.Charting;
using Visitor_Registration.DomainObjects;

namespace Visitor_Registration.UI
{
    public partial class Statistics : Form
    {
        private Controllers.MainController mc;

        public Statistics()
        {
            InitializeComponent();
        }

        public Statistics(Controllers.MainController mc)
        {
            InitializeComponent();
            this.mc = mc;
        }

        #region showVisitGraph
        private void ShowVisitGraph(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value.Date;
            DateTime end = dateTimePicker2.Value.Date;
            end = end.AddDays(1);

            if (end.Date < start.Date)
            {
                new ErrorMessage(mc, "Sluttdato kan ikke være tidligere enn startdato");
                return;
            }

            List<Visit> res = mc.GetSortedVisitList(start, end);
            ChartArea chartArea1 = new ChartArea();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(chartArea1);
            Series series1 = new Series();
            series1.LegendText = "Besøkende";

            series1.ChartType = radioButton1.Checked ? SeriesChartType.Column : SeriesChartType.FastLine;
            DateTime temp = start;
            do
            {
                List<Visit> res2 = new List<Visit>(from item in res
                                                   where item.VisitTime.Date.Equals(temp.Date)
                                                   select item);

                series1.Points.Add(res2.Count).AxisLabel = temp.Date.ToString().Substring(0, 10) ;

                temp = temp.AddDays(1);
                //
            } while (!temp.Date.Equals(end.Date));
            chart1.Series.Clear();
            chart1.Series.Add(series1);
        }
        #endregion


        private void changedTab(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1) 
            {

            }
            else if(tabControl1.SelectedIndex == 2) 
            {

            }
            else if (tabControl1.SelectedIndex == 3) //i dag
            {
                UpdateTodayTab();
            }
            else if (tabControl1.SelectedIndex == 4)
            {
                UpdateWeekStats();
            }
            Console.WriteLine(tabControl1.SelectedIndex  );
        }

        private void UpdateWeekStats()
        {
            //hent ut registrerte og generiske, sorter på ukedager og vis mandag til fredag med to søyler på hver

            var v =  mc.GetAllVisitsThisYear();
           // List<GenericVisitor> g = mc.GetAllGenericVisitsThisYear();

            DateTime d = DateTime.Now;
            Console.WriteLine(d.DayOfWeek);

            //var res = v.Select(i => i.VisitTime.DayOfWeek).Distinct().Count(); 
            //items.Select(i => i.Value).Distinct().Count()
            foreach (var item in v)
            {
                Console.WriteLine(v);
            }
            //Console.WriteLine(res);
            
        }

        #region UpdateTodayTab
        private void UpdateTodayTab()
        {
            List<Kid> kids =  mc.GetTodaysVisitKids();
            List<GenericVisitor> generic = mc.GetTodaysGenericVisits(); //TODO Finnish implementation

            int total = kids.Count;
            int jenterReg = new List<Kid>(from kid in kids
                                     where kid.Gender.Equals("Kvinne")
                                     select kid).Count;
            int gutterReg = new List<Kid>(from kid in kids
                                          where kid.Gender.Equals("Mann")
                                          select kid).Count;

            int anonyme = new List<GenericVisitor>(from v in generic
                                                   where v.Type.Equals("Anonym")
                                                   select v).Count;
            int ukjent = new List<GenericVisitor>(from v in generic
                                                   where v.Type.Equals("Ukjent")
                                                   select v).Count;
            int jente = new List<GenericVisitor>(from v in generic
                                                   where v.Type.Equals("Jente")
                                                   select v).Count;
            int gutt = new List<GenericVisitor>(from v in generic
                                                   where v.Type.Equals("Gutt")
                                                   select v).Count;

            idagTotal.Text = "" + (total + anonyme + ukjent + jente + gutt);
            idagRegistrerte.Text = "" + total;
            idagRegGutter.Text = "" + gutterReg;
            idagRegJenter.Text = "" + jenterReg;

            idagAnonyme.Text = "" +  anonyme;
            idagUkjent.Text = "" + ukjent;
            idagJenter.Text = "" + jente;
            idagGutter.Text = "" + gutt;

            ChartArea chartArea1 = new ChartArea();
            idagRegistrerteChart.ChartAreas.Clear();
            idagRegistrerteChart.ChartAreas.Add(chartArea1);
            Series series1 = new Series();
           // series1.LegendText = "Registrerte besøkende";
            series1.IsValueShownAsLabel = true;
            series1.ChartType = SeriesChartType.Pie;
            series1.Points.Add(gutterReg).AxisLabel = "gutter";
            series1.Points.Add(jenterReg).AxisLabel = "jenter";
            idagRegistrerteChart.Series.Clear();
            idagRegistrerteChart.Series.Add(series1);
            idagRegistrerteChart.Titles.Clear();
            idagRegistrerteChart.Titles.Add(new Title("Registrerte besøkende", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));



            ChartArea chartArea2 = new ChartArea();
            idagUkjenteChart.ChartAreas.Clear();
            idagUkjenteChart.ChartAreas.Add(chartArea2);
            Series series2 = new Series();
            // series1.LegendText = "Registrerte besøkende";
            series2.IsValueShownAsLabel = true;
            series2.ChartType = SeriesChartType.Pie;
            series2.Points.Add(gutt).AxisLabel = "gutter";
            series2.Points.Add(jente).AxisLabel = "jenter";
            series2.Points.Add(ukjent).AxisLabel = "ukjente";
            series2.Points.Add(anonyme).AxisLabel = "anonyme";
            idagUkjenteChart.Series.Clear();
            idagUkjenteChart.Series.Add(series2);
            idagRegUkjentChart.Titles.Clear();
            idagUkjenteChart.Titles.Add(new Title("Uregistrerte besøkende", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));


            ChartArea chartArea3 = new ChartArea();
            idagRegUkjentChart.ChartAreas.Clear();
            idagRegUkjentChart.ChartAreas.Add(chartArea3);
            Series series3 = new Series();
            // series1.LegendText = "Registrerte besøkende";
            series3.IsValueShownAsLabel = true;
            series3.ChartType = SeriesChartType.Pie;
            series3.Points.Add(total).AxisLabel = "registrerte";
            series3.Points.Add(jente + gutt + anonyme + ukjent).AxisLabel = "ukjente";

            idagRegUkjentChart.Series.Clear();
            idagRegUkjentChart.Series.Add(series3);
            idagRegUkjentChart.Titles.Clear();
            idagRegUkjentChart.Titles.Add(new Title("Registrerte og uregistrerte", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));
        }
        #endregion

    }
}
//http://archive.msdn.microsoft.com/mschart/Release/ProjectReleases.aspx?ReleaseId=1591