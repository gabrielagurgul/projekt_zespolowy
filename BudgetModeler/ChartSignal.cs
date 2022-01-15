using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BudgetModeler
{
    class ChartSignal : Chart
    {
        List<DataSeries> SeriesBufor;

        private ChartArea chartArea = new ChartArea("chartArea");

        private Legend legend = new Legend("legend");

        private Title title = new Title("title");

        public ChartSignal()
        {
            this.Series.Clear();
            SeriesBufor = new List<DataSeries>();

            //chartArea
            if (this.ChartAreas.Count > 0) this.ChartAreas.Clear();
            this.ChartAreas.Add(chartArea);

            chartArea.BorderWidth = 0;
            chartArea.AlignmentOrientation = AreaAlignmentOrientations.All;

            chartArea.AxisX.Enabled = AxisEnabled.True;
            chartArea.AxisX.IsMarksNextToAxis = true;
            chartArea.AxisX.MajorGrid.Enabled = true;
            chartArea.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.MajorTickMark.Enabled = false;
            chartArea.AxisX.ArrowStyle = AxisArrowStyle.Lines;
            chartArea.AxisX.Title = "The next templates";
            chartArea.AxisX.TitleAlignment = StringAlignment.Far;
            chartArea.AxisY.Enabled = AxisEnabled.True;
            chartArea.AxisY.IsMarksNextToAxis = true;
            chartArea.AxisY.MajorGrid.Enabled = true;
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisY.MajorTickMark.Enabled = false;
            chartArea.AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
            chartArea.AxisY.ArrowStyle = AxisArrowStyle.Lines;
            chartArea.AxisY.Title = "NN answare";
            chartArea.AxisY.TitleAlignment = StringAlignment.Far;

            chartArea.BackColor = Color.Transparent;

            //chartArea.AxisY.Maximum = 500;
            //chartArea.AxisY.Minimum = -500;
            chartArea.AxisX.Minimum = 0;
            //chartArea.AxisX.Maximum = 366;
            //this.chartArea.AxisY.Interval = 100;
            //this.chartArea.AxisX.Interval = 4;
            //chartArea.AxisY.Crossing = 0;
            //chartArea.AxisY.

            //legend
            if (this.Legends.Count > 0) this.Legends.Clear();

            legend.DockedToChartArea = this.chartArea.Name;
            legend.IsDockedInsideChartArea = true;
            legend.Alignment = StringAlignment.Near;
            legend.Enabled = true;
            legend.BackColor = Color.FromArgb(127, 255, 255, 255);
            legend.Docking = Docking.Top;
            legend.IsDockedInsideChartArea = false;
            this.Legends.Add(legend);
            //title
            title.Text = "";
            this.Titles.Add(title);

            //chart
            this.Name = "chart";
            this.BackColor = Color.Transparent;
            this.BorderlineWidth = 0;

            this.DoubleBuffered = true;
            this.AntiAliasing = AntiAliasingStyles.Text;
            ClearStripLineOnAxicX();
        }

        public void SetAxisTitle(string AxisXTitle, string AxisYTitle)
        {
            chartArea.AxisX.Title = AxisXTitle;
            chartArea.AxisY.Title = AxisYTitle;
        }

        public void AddSeries(string SeriesName, Color color)
        {
            DataSeries data = new DataSeries(SeriesName);
            this.Series.Add(data);
            SeriesBufor.Add(data);
            data.Color = color;
        }

        public void RedrawSeries(string SeriesName, List<double> DataX, List<double> DataY)
        {
            ClearStripLineOnAxicX();
            foreach (DataSeries seria in SeriesBufor)
            {
                if (seria.Name != SeriesName) continue;

                if (!this.Series.Contains(seria)) this.Series.Add(seria);

                seria.ClearSeries();
                seria.UpdateSeries(DataX, DataY);

                if (DataY.Last() >= 5)
                    this.chartArea.AxisX.Interval = Math.Round(DataY.Last() / 5, 0);
                else
                    this.chartArea.AxisX.Interval = Math.Round(DataY.Last() / 5, 1);

                seria.UpdateChartXY();

                return;
            }
        }

        public void ClearAllSeries()
        {
            foreach (DataSeries seria in SeriesBufor)
            {
                seria.ClearSeries();
            }
            SetTitle("");
        }

        public void HideSeries(string SeriesName)
        {
            foreach (DataSeries seria in SeriesBufor)
            {
                if (seria.Name != SeriesName) continue;

                if (this.Series.Contains(seria)) this.Series.Remove(seria);

                return;
            }
        }

        public void HideAllSeries()
        {
            foreach (DataSeries seria in SeriesBufor)
            {
                if (!Series.Contains(seria)) continue;

                if (this.Series.Contains(seria)) this.Series.Remove(seria);
            }
        }

        public void ShowSeries(string SeriesName)
        {
            foreach (DataSeries seria in SeriesBufor)
            {
                if (seria.Name != SeriesName) continue;

                if (!this.Series.Contains(seria)) this.Series.Add(seria);

                return;
            }
        }

        public void RedrawSeries(string SeriesName)
        {
            foreach (DataSeries seria in SeriesBufor)
            {
                if (seria.Name != SeriesName) continue;

                if (!this.Series.Contains(seria)) this.Series.Add(seria);

                seria.UpdateChartXY();

                return;
            }
        }

        public List<DataSeries> GetCopyAllSeries()
        {
            List<DataSeries> LocalSeriesBufor = new List<DataSeries>();

            foreach (DataSeries seriaDanych in SeriesBufor)
            {
                DataSeries seria = new DataSeries(seriaDanych.Name);
                List<double> valuesSeriesX = seriaDanych.GetCopySeriesX();
                List<double> valuesSeriesY = seriaDanych.GetCopySeriesY();
                seria.UpdateSeries(valuesSeriesX, valuesSeriesY);

                LocalSeriesBufor.Add(seria);
            }

            return LocalSeriesBufor;
        }

        public void SetTitle(string Title)
        {
            title.Visible = true;
            title.Text = Title;
        }

        public void AddStripLineOnAxicX(double starttPoint, double endPoint)
        {
            StripLine stripline = new StripLine();
            stripline.Interval = starttPoint;
            stripline.StripWidth = endPoint;
            stripline.BackColor = Color.LightBlue;
            stripline.Text = "Okres  zbierania  pomiarów";
            stripline.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.0F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            stripline.TextAlignment = StringAlignment.Center;
            stripline.TextLineAlignment = StringAlignment.Center;
            this.ChartAreas[0].AxisX.StripLines.Add(stripline);
        }

        public void ClearStripLineOnAxicX()
        {
            this.ChartAreas[0].AxisX.StripLines.Clear();
        }

    }
}
