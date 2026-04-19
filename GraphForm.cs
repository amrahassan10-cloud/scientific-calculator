using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using NCalc;

namespace WindowsFormsApp1
{
    public partial class GraphForm : Form
    {
        public GraphForm()
        {
            InitializeComponent();

           
            chart1.ChartAreas[0].AxisX.Title = "X";
            chart1.ChartAreas[0].AxisY.Title = "Y";
            chart1.Titles.Add("Plot of f(x)");
            chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;

           
            var chartArea = chart1.ChartAreas[0];
            chartArea.CursorX.IsUserEnabled = true;
            chartArea.CursorX.IsUserSelectionEnabled = true;
            chartArea.CursorY.IsUserEnabled = true;
            chartArea.CursorY.IsUserSelectionEnabled = true;
            chartArea.AxisX.ScaleView.Zoomable = true;
            chartArea.AxisY.ScaleView.Zoomable = true;
            chartArea.AxisX.ScrollBar.IsPositionedInside = true;
            chartArea.AxisY.ScrollBar.IsPositionedInside = true;

            chart1.Visible = true;
        }

        private void btnPlot_Click_1(object sender, EventArgs e)
        {
            chart1.Series.Clear();

            Series series = new Series
            {
                Name = "Function",
                Color = System.Drawing.Color.Blue,
                ChartType = SeriesChartType.Line
            };

            string expressionText = txtFunction.Text.Trim();

            // Parse and validate inputs
            if (!double.TryParse(txtXMin.Text, out double xMin) ||
                !double.TryParse(txtXMax.Text, out double xMax) ||
                string.IsNullOrWhiteSpace(expressionText))
            {
                MessageBox.Show("Please enter valid X range and function.");
                return;
            }

            if (xMin >= xMax)
            {
                MessageBox.Show("X Min must be less than X Max.");
                return;
            }

            double step = 0.1;
            if (!double.TryParse(txtStepSize.Text, out step) || step <= 0)
            {
                MessageBox.Show("Please enter a valid positive step size.");
                return;
            }

            for (double x = xMin; x <= xMax; x += step)
            {
                try
                {
                    Expression expr = new Expression(expressionText);
                    expr.Parameters["x"] = x;

                    var result = expr.Evaluate();
                    if (result != null && double.TryParse(result.ToString(), out double y))
                    {
                        if (double.IsNaN(y) || double.IsInfinity(y)) continue;
                        series.Points.AddXY(x, y);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Invalid function expression: {ex.Message}");
                    return;
                }
            }

            chart1.Series.Add(series);
            chart1.ChartAreas[0].RecalculateAxesScale();
        }

       
    }
}
