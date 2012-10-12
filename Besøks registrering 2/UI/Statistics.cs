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

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime start = dateTimePicker1.Value.Date;
            DateTime end = dateTimePicker2.Value.Date;
            List<Visit> res = mc.GetSortedVisitList(start, end);

            foreach (var item in res)
            {
                Console.WriteLine(item.VisitTime);
            }

            ChartArea chartArea1 = new ChartArea();

            // Add Chart Area to the Chart
            chart1.ChartAreas.Add(chartArea1);

            // Create a data series
            Series series1 = new Series();

            series1.Legend = "Besøksinformasjon";
            series1.AxisLabel = "test";


            // Add data points to the first series
            series1.Points.Add(34);
            series1.Points.Add(24);
            series1.Points.Add(32);
            series1.Points.Add(28);
            series1.Points.Add(44);

            // Add data points to the second series


            // Add series to the chart
            chart1.Series.Add(series1);


        }


    }
}
//http://archive.msdn.microsoft.com/mschart/Release/ProjectReleases.aspx?ReleaseId=1591