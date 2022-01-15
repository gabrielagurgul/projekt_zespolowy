using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace BudgetModeler
{
    public class DataSeries : Series
    {
        private List<double> valueSeries_Y = new List<double>();
        private List<double> valueSeries_X = new List<double>();
        private List<double> standardizedSeries = new List<double>();
        private int MaxCountPoints;
        private int FoldOfMaximumCountPoints;
        private int ActualMaxCountPoints;

        public DataSeries(string Name)
        {
            MaxCountPoints = 2010;
            FoldOfMaximumCountPoints = 1;
            ActualMaxCountPoints = MaxCountPoints * FoldOfMaximumCountPoints;

            this.Name = Name;

            CompleteStartValue();
            this.ChartType = SeriesChartType.Line;

            this.Points.DataBindY(valueSeries_Y);
        }

        private void CompleteStartValue()
        {
            for (int i = 1; valueSeries_Y.Count < valueSeries_Y.Capacity; i++)
                valueSeries_Y.Add(0);
        }

        public void UpdateSeries(List<double> value_x, List<double> value_y)
        {

            for (int i = 0; i < value_x.Count; i++)
            {
                if (value_x[i] > 20000000000000000000000000000d)
                {
                    value_x[i] = 20000000000000000000000000000d;
                }

                if (value_x[i] < -20000000000000000000000000000d)
                {
                    value_x[i] = -20000000000000000000000000000d;
                }
            }

            valueSeries_Y.AddRange(value_y);
            valueSeries_X.AddRange(value_x);
        }

        public void UpdateChartXY()
        {
            this.Points.DataBindXY(valueSeries_Y, valueSeries_X);
        }

        public void ClearSeries()
        {
            valueSeries_Y = new List<double>();
            valueSeries_Y.Clear();
            valueSeries_X = new List<double>();
            valueSeries_X.Clear();
            FoldOfMaximumCountPoints = 1;
            ActualMaxCountPoints = MaxCountPoints * FoldOfMaximumCountPoints;

            this.Points.Clear();
        }

        public List<double> GetCopySeriesX()
        {
            List<double> bufor = new List<double>();

            foreach (double value in valueSeries_X)
            {
                bufor.Add(value);
            }

            return bufor;
        }

        public List<double> GetCopySeriesY()
        {
            List<double> bufor = new List<double>();

            foreach (double value in valueSeries_Y)
            {
                bufor.Add(value);
            }

            return bufor;
        }
    }
}
