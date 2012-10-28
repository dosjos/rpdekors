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
using Visitor_Registration.DataAccesLayer;

namespace Visitor_Registration.UI
{
    public partial class Statistics : Form
    {
        private BindingList<StringValue> years = new BindingList<StringValue>();
        private BindingList<StringValue> monthYears = new BindingList<StringValue>();
        private BindingList<StringValue> months = new BindingList<StringValue>();
        private Controllers.MainController mc;

        #region dictionary
        Dictionary<int, string> WeekDays = new Dictionary<int, string>()
{
    { 0, "Mandag"},
    { 1, "Tirsdag"},
    { 2, "Onsdag"}, 
    { 3, "Torsdag"},
    { 4, "Fredag"},
    { 5, "Lørdag"},
    { 6,  "Søndag"}
};
        #endregion

        #region constructors
        public Statistics()
        {
            InitializeComponent();

        }

        public Statistics(Controllers.MainController mc)
        {
            InitializeComponent();
            this.mc = mc;
            radioButton3.CheckedChanged += WeeklyRadioButtons;
            dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-7);
            groupBox3.MaximumSize = new System.Drawing.Size(242, 10000);

            UpdateYearPage();
            UpdateMonthPage();

        }

        private void UpdateMonthPage()
        {
            monthYear.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            monthYear.DataSource = years;
            modelColumn.HeaderText = "År";
            modelColumn.DataPropertyName = "Value";
            modelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            monthYear.Columns.Add(modelColumn);

            foreach (var item in mc.GetAllYearsWithVisits())
            {
                monthYears.Add(new StringValue("" + item));
            }
        }
        #endregion

        #region showVisitGraph
        private void ShowVisitGraph(object sender, EventArgs e)
        {
            DisplayVisitGraph();
        }

        public void DisplayVisitGraph()
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
            // Series series2 = new Series();
            // Series series3 = new Series();
            series1.LegendText = "Besøkende";
            // series2.LegendText = "Gutter";
            // series3.LegendText = "Jenter";

            series1.ChartType = radioButton1.Checked ? SeriesChartType.Column : SeriesChartType.FastLine;
            //  series2.ChartType = radioButton1.Checked ? SeriesChartType.StackedColumn : SeriesChartType.FastLine;
            //  series3.ChartType = radioButton1.Checked ? SeriesChartType.StackedColumn : SeriesChartType.FastLine;
            DateTime temp = start;
            do
            {
                List<Visit> res2 = new List<Visit>(from item in res
                                                   where item.VisitTime.Date.Equals(temp.Date)
                                                   select item);
                //  int gutt = mc.GetGutterThisDay(temp.Date);


                series1.Points.Add(res2.Count).AxisLabel = temp.Date.ToString().Substring(0, 10);
                //  series2.Points.Add(gutt);
                //  series3.Points.Add(res2.Count - gutt);
                temp = temp.AddDays(1);
                //
            } while (!temp.Date.Equals(end.Date));
            chart1.Series.Clear();
            chart1.Series.Add(series1);
            // chart1.Series.Add(series2);
            // chart1.Series.Add(series3);
            //chart1.Series[0]["StackedGroupName"] = "Group1";
            //chart1.Series[1]["StackedGroupName"] = "Group1";
            // chart1.Series[2]["StackedGroupName"] = "Group1";

        }


        private void button5_Click(object sender, EventArgs e)
        {
            new InformationBox("På denne siden kan du selv velge perioden du vil se besøksstatistikk fra. Denne grafen viser kun statistikk fra registerte brukere. Dersom man velger en periode fra feks. Mandag til Søndag vil det være enkelt å bruke neste og forrige periode knappene for å bla mellom ukene");
        }


        #endregion

        #region tabchanged
        private void changedTab(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {

            }
            else if (tabControl1.SelectedIndex == 2)
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
            Console.WriteLine(tabControl1.SelectedIndex);
        }
        #endregion

        #region UpdateMonth
        private void monthYearChoosen(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)yearList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    var monthsfromYear = mc.GetMonthsWithVisits("" + cell.Value);
                }
            }
        }



        #endregion

        #region updateyear
        private void UpdateYearPage()
        {
            //yearList.DataSource = mc.GetAllYearsWithVisits();

            yearList.AutoGenerateColumns = false;
            DataGridViewTextBoxColumn modelColumn = new DataGridViewTextBoxColumn();
            yearList.DataSource = years;
            modelColumn.HeaderText = "År";
            modelColumn.DataPropertyName = "Value";
            modelColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            yearList.Columns.Add(modelColumn);

            foreach (var item in mc.GetAllYearsWithVisits())
            {
                years.Add(new StringValue("" + item));
            }
        }
        private void YearChoosen(object sender, DataGridViewCellEventArgs e)
        {
            toolStripStatusLabel1.Text = "Laster statistikk";
            toolStripProgressBar1.Value = 100;
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewTextBoxCell cell = (DataGridViewTextBoxCell)yearList.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (cell.Value != null)
                {
                    //cell.Value;

                    List<Kid> kids = mc.GetVisitByYear("" + cell.Value);
                    List<GenericVisitor> generic = mc.GetGenericVisitByYear("" + cell.Value); //TODO Finnish implementation

                    int total = kids.Count;
                    int jenterReg = GetGirlsFromVisit(kids);
                    int gutterReg = GetBoysFromVisits(kids);

                    int anonyme = GetAnonymFromGeneric(generic);
                    int ukjent = GetUnknownFromGeneric(generic);
                    int jente = GetGirlsFromGeneric(generic);
                    int gutt = GetBoysFromGenericList(generic);

                    yearRegistred.Text = "" + total;
                    yearBoys.Text = "" + gutterReg;
                    yearGirls.Text = "" + jenterReg;
                    yearGutt.Text = "" + gutt;
                    yearJente.Text = "" + jente;
                    yearUkjent.Text = "" + ukjent;
                    yearAnonym.Text = "" + anonyme;
                    yearTotal.Text = "" + (total + generic.Count);

                    CreateBoyGirlChart(jenterReg, gutterReg, yearChart1);
                    CreateGenericChart(anonyme, ukjent, jente, gutt, yearChart2);
                    CreateRegistredVSGenericChart(total, anonyme, ukjent, jente, gutt, yearChart3);
                }
            }
        }
        #endregion

        #region StatistickHelpers
        private static int GetGirlsFromVisit(List<Kid> kids)
        {
            int jenterReg = new List<Kid>(from kid in kids
                                          where kid.Gender.Equals("Kvinne")
                                          select kid).Count;
            return jenterReg;
        }

        private static int GetBoysFromVisits(List<Kid> kids)
        {
            int gutterReg = new List<Kid>(from kid in kids
                                          where kid.Gender.Equals("Mann")
                                          select kid).Count;
            return gutterReg;
        }

        private static int GetAnonymFromGeneric(List<GenericVisitor> generic)
        {
            int anonyme = new List<GenericVisitor>(from v in generic
                                                   where v.Type.Equals("Anonym")
                                                   select v).Count;
            return anonyme;
        }

        private static int GetUnknownFromGeneric(List<GenericVisitor> generic)
        {
            int ukjent = new List<GenericVisitor>(from v in generic
                                                  where v.Type.Equals("Ukjent")
                                                  select v).Count;
            return ukjent;
        }

        private static int GetGirlsFromGeneric(List<GenericVisitor> generic)
        {
            int jente = new List<GenericVisitor>(from v in generic
                                                 where v.Type.Equals("Jente")
                                                 select v).Count;
            return jente;
        }

        private static int GetBoysFromGenericList(List<GenericVisitor> generic)
        {
            int gutt = new List<GenericVisitor>(from v in generic
                                                where v.Type.Equals("Gutt")
                                                select v).Count;
            return gutt;
        }

        #endregion

        #region weeklytrends
        private void UpdateWeekStats()
        {
            //hent ut registrerte og generiske, sorter på ukedager og vis mandag til fredag med to søyler på hver

            var v = mc.GetAllVisitsThisYear();
            // List<GenericVisitor> g = mc.GetAllGenericVisitsThisYear();

            DateTime d = DateTime.Now;
            Console.WriteLine(d.DayOfWeek);

            //var res = v.Select(i => i.VisitTime.DayOfWeek).Distinct().Count(); 
            //items.Select(i => i.Value).Distinct().Count()
            ChartArea chartArea1 = new ChartArea();
            ukestrendChart.ChartAreas.Clear();
            ukestrendChart.ChartAreas.Add(chartArea1);
            Series series1 = new Series();
            series1.IsValueShownAsLabel = true;
            series1.ChartType = radioButton3.Checked ? SeriesChartType.Pie : SeriesChartType.Column;
            for (int i = 0; i < v.Count; i++)
            {
                series1.Points.Add(v[i]).AxisLabel = WeekDays[i];
            }

            ukestrendChart.Series.Clear();
            ukestrendChart.Series.Add(series1);
            ukestrendChart.Titles.Clear();
            ukestrendChart.Titles.Add(new Title("Ukestrend", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));
        }

        private void WeeklyRadioButtons(object sender, EventArgs e)
        {
            UpdateWeekStats();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new InformationBox("Denne grafen viser hvilken ukedager som er mest besøkte i løpet av inneværende år. Denne informasjonen kan brukes til å styre aktivitetstilbudene.");
        }
        #endregion

        #region UpdateTodayTab
        private void UpdateTodayTab()
        {
            List<Kid> kids = mc.GetTodaysVisitKids();
            List<GenericVisitor> generic = mc.GetTodaysGenericVisits(); //TODO Finnish implementation

            int total = kids.Count;
            int jenterReg = GetGirlsFromVisit(kids);
            int gutterReg = GetBoysFromVisits(kids);

            int anonyme = GetAnonymFromGeneric(generic);
            int ukjent = GetUnknownFromGeneric(generic);
            int jente = GetGirlsFromGeneric(generic);
            int gutt = GetBoysFromGenericList(generic);

            idagTotal.Text = "" + (total + anonyme + ukjent + jente + gutt);
            idagRegistrerte.Text = "" + total;
            idagRegGutter.Text = "" + gutterReg;
            idagRegJenter.Text = "" + jenterReg;

            idagAnonyme.Text = "" + anonyme;
            idagUkjent.Text = "" + ukjent;
            idagJenter.Text = "" + jente;
            idagGutter.Text = "" + gutt;

            CreateBoyGirlChart(jenterReg, gutterReg, idagRegistrerteChart);
            CreateGenericChart(anonyme, ukjent, jente, gutt, idagUkjenteChart);
            CreateRegistredVSGenericChart(total, anonyme, ukjent, jente, gutt, idagRegUkjentChart);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            new InformationBox("Her kan du se besøksinformasjonen for dagen i dag. Den er delt opp i to bolker, en for registrerte og en for uregistrerte besøkende. De registrerte er også delt opp etter kjønn.");
        }
        #endregion

        #region Charts

        private void CreateRegistredVSGenericChart(int total, int anonyme, int ukjent, int jente, int gutt, Chart chart)
        {
            ChartArea chartArea3 = new ChartArea();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea3);
            Series series3 = new Series();
            // series1.LegendText = "Registrerte besøkende";
            series3.IsValueShownAsLabel = true;
            series3.ChartType = SeriesChartType.Pie;
            series3.Points.Add(total).AxisLabel = "registrerte";
            series3.Points.Add(jente + gutt + anonyme + ukjent).AxisLabel = "ukjente";

            chart.Series.Clear();
            chart.Series.Add(series3);
            chart.Titles.Clear();
            chart.Titles.Add(new Title("Registrerte og uregistrerte", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));
        }

        private void CreateGenericChart(int anonyme, int ukjent, int jente, int gutt, Chart chart)
        {
            ChartArea chartArea2 = new ChartArea();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea2);
            Series series2 = new Series();
            // series1.LegendText = "Registrerte besøkende";
            series2.IsValueShownAsLabel = true;
            series2.ChartType = SeriesChartType.Pie;
            series2.Points.Add(gutt).AxisLabel = "gutter";
            series2.Points.Add(jente).AxisLabel = "jenter";
            series2.Points.Add(ukjent).AxisLabel = "ukjente";
            series2.Points.Add(anonyme).AxisLabel = "anonyme";
            chart.Series.Clear();
            chart.Series.Add(series2);
            chart.Titles.Clear();
            chart.Titles.Add(new Title("Uregistrerte besøkende", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));
        }

        private void CreateBoyGirlChart(int jenterReg, int gutterReg, Chart chart)
        {
            ChartArea chartArea1 = new ChartArea();
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add(chartArea1);
            Series series1 = new Series();
            // series1.LegendText = "Registrerte besøkende";
            series1.IsValueShownAsLabel = true;
            series1.ChartType = SeriesChartType.Pie;
            series1.Points.Add(gutterReg).AxisLabel = "gutter";
            series1.Points.Add(jenterReg).AxisLabel = "jenter";
            chart.Series.Clear();
            chart.Series.Add(series1);
            chart.Titles.Clear();
            chart.Titles.Add(new Title("Registrerte besøkende", new Docking(), new Font("", 9, FontStyle.Bold), Color.Black));
        }


        #endregion

        #region navigatePeriod
        private void button2_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;

            double diff = (end - start).TotalDays + 1;

            start = start.AddDays((-1 * diff));
            end = end.AddDays((-1 * diff));

            dateTimePicker1.Value = start;
            dateTimePicker2.Value = end;
            DisplayVisitGraph();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value;
            DateTime end = dateTimePicker2.Value;

            double diff = (end - start).TotalDays + 1;

            start = start.AddDays(diff);
            end = end.AddDays(diff);

            dateTimePicker1.Value = start;
            dateTimePicker2.Value = end;
            DisplayVisitGraph();
        }
        #endregion

        private void button7_Click(object sender, EventArgs e)
        {

        }





    }
}
//http://archive.msdn.microsoft.com/mschart/Release/ProjectReleases.aspx?ReleaseId=1591

