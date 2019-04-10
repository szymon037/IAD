using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using System.IO;
using OxyPlot.Axes;
//using OxyPlot.Wpf;

namespace Neural_Network_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        public double[] data = new double[81];
        public double[] target = new double[81];
        public double[] outerSpace = new double[81];
        public double[] outerSpaceTest = new double[1000];
        public double[] dataTest = new double[1000];
        public double[] targetTest = new double[1000];
        public PlotModel MyModel { get; private set; }
        private static Random rnd = new Random();
        public List<DataPoint> EpochError = new List<DataPoint>();
        public List<DataPoint> EpochErrorTest = new List<DataPoint>();


        private void Form1_Load(object sender, EventArgs e)
        {
            Matrix.Matrix output = new Matrix.Matrix(1, 1);
            double sum = 0;
            double sumTest = 0;
            int epochs = 0;
            int epoch = 600;


            LoadFromFile(data, target);
            LoadFromFileTest(dataTest, targetTest);
            //SortX(data, target);
            this.Controls.Add(pv);
            this.Controls.Add(pv2);
            PlotModel pm = new PlotModel();

            pv.Model = pm;
            PlotModel pm2 = new PlotModel();

            pv2.Model = pm2;

            LineSeries punktySerii = new LineSeries
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Red
            };
            LineSeries punktySeriiTestu = new LineSeries
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Red,
                Title = "Punkty wejściowe"
            };

            LineSeries punktySieci = new LineSeries()
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Plus,
                MarkerSize = 3,
                MarkerStroke = OxyColors.Blue,
                Title = "Punkty wyjściowe"
            };

            LineSeries punktyBledu = new LineSeries()
            {
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.Diamond,
                Color = OxyColors.Red,
                MarkerSize = 1,
                MarkerStroke = OxyColors.Red,
                Title = "Zbior treningowy"
            };

            LineSeries punktyBleduTestowego = new LineSeries()
            {
                LineStyle = LineStyle.Solid,
                MarkerType = MarkerType.Diamond,
                Color = OxyColors.Blue,
                MarkerSize = 1,
                MarkerStroke = OxyColors.Blue,
                Title = "Zbior testowy"
            };

            LineSeries punktyKresek = new LineSeries
            {
                Color = OxyColors.Blue,
                StrokeThickness = 1
            };


            /*
            for (int i = 0; i < data.Length; i++)
            {
                punktySerii.Points.Add(new DataPoint(data[i], target[i]));
            }
            */
            for (int i = 0; i< dataTest.Length; i++)
            {
                punktySeriiTestu.Points.Add(new DataPoint(dataTest[i], targetTest[i]));
            }

            Matrix.Matrix outputFF = new Matrix.Matrix(1, 1);
            double[] d = new double[1];
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork(1, 10, 1, true);

            //uczenie
            
            for (int i = 0; i < epoch; ++i)
            {
                sum = 0;
                sumTest = 0;
                foreach (int j in Enumerable.Range(0, 81).OrderBy(x => rnd.Next()))
                {
                    d[0] = data[j];
                    double[] y = new double[1] { target[j] };
                    nn.Train(d, y);
                }
                

                for(int j = 0; j < data.Length; j++)
                {
                    d[0] = data[j];
                    outputFF = nn.FeedForward(d);
                    sum += (outputFF.tab[0,0] - target[j]) * (outputFF.tab[0, 0] - target[j])/2;
                }

                for (int j = 0; j < dataTest.Length; j++)
                {
                    d[0] = dataTest[j];
                    outputFF = nn.FeedForward(d);
                    sumTest += (outputFF.tab[0, 0] - targetTest[j]) * (outputFF.tab[0, 0] - targetTest[j])/2;
                }

                EpochErrorTest.Add(new DataPoint(epochs, sumTest / dataTest.Length));
                EpochError.Add(new DataPoint(epochs,sum / data.Length ));
                 ++epochs;
            }
            /*
            for (int i = 0; i < data.Length; ++i)
            {
                d[0] = data[i];
                outputFF = nn.FeedForward(d);
                punktySieci.Points.Add(new DataPoint(d[0], outputFF.tab[0, 0]));
                outerSpace[i] = outputFF.tab[0, 0];
                //Console.WriteLine("x: " + d[0] + " y: " + outputFF.tab[0, 0]);
                //outputFF.DisplayMatrix();
            }
            */

            for (int i = 0; i < dataTest.Length; ++i)
            {
                d[0] = dataTest[i];
                outputFF = nn.FeedForward(d);
                punktySieci.Points.Add(new DataPoint(d[0], outputFF.tab[0, 0]));
                outerSpaceTest[i] = outputFF.tab[0, 0];
            }

            SortX(data, outerSpace);
            SortX(dataTest, outerSpaceTest);
            /*
            for(int i = 0; i < data.Length; ++i)
            {
                punktyKresek.Points.Add(new DataPoint(data[i], outerSpace[i]));
            }
            
            for (int i = 0; i < dataTest.Length; ++i)
            {
                punktyKresek.Points.Add(new DataPoint(dataTest[i], outerSpaceTest[i]));
            }
            */
            //for error:

            for (int i = 0; i < epoch; ++i)
            {
                punktyBleduTestowego.Points.Add(EpochErrorTest[i]);
                punktyBledu.Points.Add(EpochError[i]);
            }
            
            pm2.Series.Add(punktyBleduTestowego);
            pm2.Series.Add(punktyBledu);
            pm2.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Bottom, MinimumPadding = 0.1, MaximumPadding = 0.1, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Epoka" });
            pm2.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = OxyPlot.Axes.AxisPosition.Left, MinimumPadding = 0.1, MaximumPadding = 0.1, MajorGridlineStyle = LineStyle.Solid, MinorGridlineStyle = LineStyle.Dot, Title = "Błąd" });
            
            //for points


            pm.Series.Add(punktySieci);
            pm.Series.Add(punktySeriiTestu);
            //pm.Series.Add(punktyKresekTestu);
            //pm.Series.Add(punktyKresek);


        }

        private void LoadFromFileTest(double[] data, double[] target)
        {
            var fileStream = new FileStream(@"D:\test.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                int i = 0;
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var num1 = Double.Parse(line.Split(' ')[0]);
                    var num2 = Convert.ToDouble(line.Split(' ')[1]);
                    data[i] = num1;
                    target[i] = num2;
                    i++;
                }
            }
        }
        private void LoadFromFile(double[] data, double[] target)
        {
            var fileStream = new FileStream(@"D:\input.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                int i = 0;
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var num1 = Double.Parse(line.Split(' ')[0]);
                    var num2 = Convert.ToDouble(line.Split(' ')[1]);
                    data[i] = num1;
                    target[i] = num2;
                    i++;
                }
            }
        }


        private void SortX(double[] data, double[] target)
        {
            Array.Sort(data, target);
            /*foreach (double key in data)
            {
                Console.WriteLine(key);
            }
            foreach (double item in target)
            {
                Console.WriteLine(item); 
            }*/
        }

        private static void Shuffle(double[] data, double[] target)
        {
            int n = data.Length;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                double value = data[k];
                data[k] = data[n];
                data[n] = value;
                double value2 = target[k];
                target[k] = target[n];
                target[n] = value2;
            }
        }
    }
}