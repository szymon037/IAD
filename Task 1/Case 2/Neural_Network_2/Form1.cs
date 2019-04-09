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
            Console.WriteLine("hello");
            for (int i = 0; i < 81; i++)
               // Console.WriteLine(data[i] + " " + target[i]);
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
            var punktySieci = new OxyPlot.Series.LineSeries();

            for (int i = 0; i < data.Length; i++)
            {
                punktySerii.Points.Add(new DataPoint(data[i], target[i]));
            }
            

            NeuralNetworks.NeuralNetwork nn = new NeuralNetworks.NeuralNetwork(81,4,81, true);

            for(int i = 0; i < 1000; i++)
            {
                Shuffle(data, target);
                nn.Train(data, target);
            }

            Matrix.Matrix output = new Matrix.Matrix(81, 2);

            for(int i = 0; i < 81; i++)
            {
                output = nn.FeedForward(data);
                punktySieci.Points.Add(new DataPoint(data[i], output.tab[i,0]));
                Console.WriteLine(output.tab[i, 0]);
            }

           // output.DisplayMatrix();
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
            foreach (double key in data)
            {
                Console.WriteLine(key);
            }
            foreach (double item in target)
            {
                Console.WriteLine(item); 
            }
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