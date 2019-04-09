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
        public OxyPlot.Series.LineSeries punktySerii = new OxyPlot.Series.LineSeries() {
           
        };
        public double[] data = new double[81];
        public double[] target = new double[81];
        public PlotModel MyModel { get; private set; }
        private static Random rnd = new Random();


        private void Form1_Load(object sender, EventArgs e)
        {
            Matrix.Matrix output = new Matrix.Matrix(1, 1);
            double sum = 0;
            int epoch = 0;
            Console.WriteLine("hello");


            LoadFromFile();
            //SortX(data, target);
            PlotView pv = new PlotView
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(pv);
            PlotModel pm = new PlotModel();

            pv.Model = pm;
            var punktySerii = new OxyPlot.Series.LineSeries
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Circle,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Red
            };
            var punktySieci = new OxyPlot.Series.LineSeries()
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Plus,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Blue
            };

            for (int i = 0; i < data.Length; i++)
            {
                punktySerii.Points.Add(new DataPoint(data[i], target[i]));
            }

            /*for (int i = 0; i < 20; i++)
            {
                NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork(1, 10, 1, true);
                //do
                //{
                    double[] d= new double[1];
                    sum = 0;

                    //training 
                    foreach (int j in Enumerable.Range(0, 81).OrderBy(x => rnd.Next()))
                    {
                        d[0] =  data[j];
                        double[] y = new double[1] { target[j] };
                        nn.Train(d, y);
                    }
                    
                    for (int j = 0; j < 81; ++j)
                    {
                        double[] Wyjscie = new double[81];
                        double[] dataCell = new double[1] { data[j] };
                        Wyjscie[j] = nn.FeedForward(dataCell).tab[0, 0];
                        output = nn.FeedForward(data);
                    punktySieci.Points.Add(new DataPoint(data[j], Wyjscie[j]));
                    }
                    ++epoch;
                //}
                //while (sum / 2 > 0.1);
            }*/
            //output.DisplayMatrix();

            Matrix.Matrix outputFF = new Matrix.Matrix(1, 1);
            double[] d = new double[1];
            NeuralNetwork.NeuralNetwork nn = new NeuralNetwork.NeuralNetwork(1, 20, 1, false);

            int c = 2;

            for (int i = 0; i < 3000; ++i)
            {
                foreach (int j in Enumerable.Range(0, 81).OrderBy(x => rnd.Next()))
                {
                    d[0] = data[j];
                    //double[] y = new double[1] { target[j] };
                    double[] y = new double[1] { c };
                    nn.Train(d, y);
                }

            }

            for (int i = 0; i < 81; ++i)
            {
                d[0] = data[i];
                outputFF = nn.FeedForward(d);
                punktySieci.Points.Add(new DataPoint(d[0], outputFF.tab[0, 0]));
                //Console.WriteLine("x: " + d[0] + " y: " + outputFF.tab[0, 0]);
                //outputFF.DisplayMatrix();
            }

            pm.Series.Add(punktySieci);
            pm.Series.Add(punktySerii);
        }

        private void LoadFromFile()
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