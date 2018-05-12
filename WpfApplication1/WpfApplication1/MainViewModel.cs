using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace WpfApplication1
{
    using OxyPlot;

    public class MainViewModel
    {
        public MainViewModel()
        {
            MyModel = new PlotModel();

            var data = ReadCSVFile(@"C:\Users\cg141117\Desktop\data2.csv");

            foreach (var tuple in data)
            {
                Points.Add(new DataPoint(DateTimeAxis.ToDouble(tuple.Item1), tuple.Item2));
            }

            var lineSeries = new LineSeries
            {
                StrokeThickness = 2,
                Color = OxyColor.FromRgb(255, 0, 0),
                ItemsSource = this.Points
            };

            MyModel.Series.Add(lineSeries);

            DateTimeAxis xAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd/MM/yyyy",
                Title = "Year",
                MinorIntervalType = DateTimeIntervalType.Auto,
                IntervalType = DateTimeIntervalType.Auto,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                Minimum = DateTimeAxis.ToDouble(data.OrderBy(s => s.Item1).First().Item1),
                Maximum = DateTimeAxis.ToDouble(data.OrderBy(s => s.Item1).Last().Item1)
            };

            MyModel.Axes.Add(xAxis);
            MyModel.Title = $"No of points: {data.Count}";


            var other = Points.Select(e => new Tuple<double, double>(e.X, e.Y)).ToList();

            Stopwatch stopwatch = Stopwatch.StartNew();
            var reducedSet = LargestTriangleThreeBuckets.Get(other, 8000);
            stopwatch.Stop();

            var mydata = reducedSet.Select(e => new DataPoint(DateTimeAxis.ToDouble(e.Item1), e.Item2)).ToList();

            MyModel2 = new PlotModel();
            MyModel2.Title = $"No of points: {mydata.Count} - {stopwatch.ElapsedMilliseconds}ms";

            var lineSeries2 = new LineSeries()
            {
                ItemsSource = mydata
            };

            DateTimeAxis xAxis2 = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "dd/MM/yyyy",
                Title = "Year",
                MinorIntervalType = DateTimeIntervalType.Auto,
                IntervalType = DateTimeIntervalType.Auto,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,
                Minimum = DateTimeAxis.ToDouble(data.OrderBy(s => s.Item1).First().Item1),
                Maximum = DateTimeAxis.ToDouble(data.OrderBy(s => s.Item1).Last().Item1)
            };

            MyModel2.Series.Add(lineSeries2);
            MyModel2.Axes.Add(xAxis2);

            var blah = 2;

        }

        public PlotModel MyModel { get; private set; }
        public PlotModel MyModel2 { get; private set; }
        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; } = new List<DataPoint>();

        private static List<Tuple<double, double>> ReadCSVFile(string filename, bool ignoreHeader = true)
        {

            var lines = File.ReadLines(filename).Select(s => s.Split(',')).ToList();

            if (ignoreHeader)
            {
                lines.RemoveAt(0);
            }

            //var csv = lines.Select(s => new Tuple<DateTime, double>(Convert.ToDateTime(s[0]), Convert.ToDouble(s[1])));
            var csv = lines.Select(s =>
                new Tuple<double, double>(Convert.ToDouble(s[0]), Convert.ToDouble(s[1])));


            return csv.ToList();
        }
    }


}
