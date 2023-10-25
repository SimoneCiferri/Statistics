using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homework3_Csh
{
    public partial class Form1 : Form
    {
        private Point mouseDownLocation;
        private bool isDragging = false;


        private Bitmap graphBitmap;

        public Form1()
        {
            InitializeComponent();

            // generate attacks
            Adversary adv = new Adversary(1000, 20, 0.5f);
            adv.generateAttacks();
            // retrieve data
            List<List<int>> data1 = adv.GetLineChart1AttackList();
            List<List<int>> data2 = adv.GetLineChart2AttackList();
            
            List<List<float>> data3 = adv.GetLineChart3AttackList();
            List<List<float>> data4 = adv.GetLineChart4AttackList();
            
            // generate LineCharts
            LineChart chart1 = new LineChart(this, pictureBox1);
            chart1.DrawChart(this, data1);

            LineChart chart2 = new LineChart(this, pictureBox2);
            chart2.DrawChart(this, data2);

            LineChart chart3 = new LineChart(this, pictureBox3);
            chart3.DrawChart(this, data3);

            LineChart chart4 = new LineChart(this, pictureBox4);
            chart4.DrawChart(this, data4);

            int k = 20;

            // retrieve histogram data
            Dictionary<int, int> histogramDistChart1 = adv.createHistoDistrib(data1, k);
            Dictionary<int, int> histogramDistChart2 = adv.createHistoDistrib(data2, k);
            Dictionary<int, float> histogramDistChart3 = adv.createHistoDistrib(data3, k);
            Dictionary<int, float> histogramDistChart4 = adv.createHistoDistrib(data4, k);

            // generate Histograms
            Histogram histogram1 = new Histogram(this, chart1, histogramDistChart1, k);
            Histogram histogram2 = new Histogram(this, chart2, histogramDistChart2, k);
            Histogram histogram3 = new Histogram(this, chart3, histogramDistChart3, k);
            Histogram histogram4 = new Histogram(this, chart4, histogramDistChart4, k);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
