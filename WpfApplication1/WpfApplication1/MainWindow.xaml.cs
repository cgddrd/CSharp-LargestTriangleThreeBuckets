using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private const int MAX_ROWS = 10000001;
        Random random = new Random();

        public MainWindow()
        {

            //var list = new List<string>(MAX_ROWS) { "Time, Chemical concentration readings" };
            //var now = DateTime.UtcNow;

            //for (var i = 0; i < MAX_ROWS - 1; i++)
            //{
            //    var date = now.AddSeconds(i);
            //    var random = GetRandomNumber(10, 30000);

            //    list.Add($"{date}, {random}");

            //}

            //File.WriteAllLines(@"C:\Users\cg141117\Desktop\data.csv", list.ToArray());

            InitializeComponent();
        }

        public double GetRandomNumber(double minimum, double maximum)
        {
            return random.NextDouble() * (maximum - minimum) + minimum;
        }


    }
}
