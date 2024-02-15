using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace poisk_extremum
{
    public partial class Poisk_Extremum_form : Form
    {
        Methods methods;
        public Poisk_Extremum_form()
        {
            InitializeComponent();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            methods = new Methods(Convert.ToInt32(tbN.Text), double.Parse(tbEpsilon.Text));

            methods_chart.Series[0].Points.Clear();
            methods_chart.Series[0].ChartType = SeriesChartType.Spline;
            methods_chart.Series[1].Points.Clear();
            methods_chart.Series[1].ChartType = SeriesChartType.Spline;
            methods_chart.Series[2].Points.Clear();
            methods_chart.Series[2].ChartType = SeriesChartType.Spline;
            methods_chart.Series[3].Points.Clear();
            methods_chart.Series[3].ChartType = SeriesChartType.Spline;

            Axis ax = new Axis();
            ax.Title = "N";
            methods_chart.ChartAreas[0].AxisX = ax;
            Axis ay = new Axis();
            ay.Title = "Lopt";
            methods_chart.ChartAreas[0].AxisY = ay;


            for (int i = 1; i < methods.get_n()+1; i++)
            {
                methods_chart.Series[0].Points.AddXY(i, methods.passive_method(i));
                methods_chart.Series[1].Points.AddXY(i, methods.dichotomy_method(i));
                methods_chart.Series[2].Points.AddXY(i, methods.fibb_method(i));
                methods_chart.Series[3].Points.AddXY(i, methods.gold_method(i));
            }
        }

        private void btnParabolaSet_Click(object sender, EventArgs e)
        {
            methods = new Methods(10, 0.001);

            tbParabolaExtremum.Text = methods.parabola_method(double.Parse(tbx0.Text), double.Parse(tbParabolaEpsilon.Text)).ToString();
            tbParabolaValue.Text = methods.get_parabola_value().ToString();
            tbParabolaN.Text = methods.get_parabola_n().ToString();
            tbSec.Text = methods.get_sec();

        }

        private void btnGRSet_Click(object sender, EventArgs e)
        {
            methods = new Methods(10, 0.001);

            tbGRExtr.Text = methods.gold_method_extr(double.Parse(tbA.Text), double.Parse(tbB.Text), double.Parse(tbEpsilonGr.Text)).ToString();
            tbGRValue.Text = methods.get_gr_value().ToString();
            tbGrN.Text = methods.get_gr_n().ToString();
        }

        private void btnSetMnogomer_Click(object sender, EventArgs e)
        {
            methods = new Methods(10, 0.001);

            tbExtremumGradient.Text = methods.grad_method(tbx0mnogomer.Text, double.Parse(tbLambda.Text), double.Parse(tbEpsilonMnogomer.Text));
            tbValueGradient.Text = methods.get_grad_value().ToString();
            tbNGradient.Text = methods.get_grad_n().ToString();

            tbExtremumMnogomer.Text = methods.coord_method(tbx0mnogomer.Text, double.Parse(tbLambda.Text), double.Parse(tbEpsilonMnogomer.Text));
            tbValueMnogomer.Text = methods.get_coord_value().ToString();
            tbNMnogomer.Text = methods.get_coord_n().ToString();
        }
    }
}
