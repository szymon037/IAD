using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using OxyPlot;
using OxyPlot.WindowsForms;
using OxyPlot.Series;
using OxyPlot.Axes;


namespace RBF_NeuralNetwork
{
    public partial class Form1 : Form
    {
        //public double[] data = new double[81];
        public double[] target = new double[81];
        //public double[,] data = new double[81, 2];
        private Matrix.Matrix data = new Matrix.Matrix(81, 2);
        private double[] inputArray = new double[81];
        private double[] targetArray = new double[81];

        //plot
        public PlotModel MyModel { get; private set; }

        public Form1()
        {
            InitializeComponent();
        }

        public double epochs;

        private void Form1_Load(object sender, EventArgs e)
        {
            epochs = 1000;
            LoadFromFile(data);

            //plot
            //OxyPlot.WindowsForms.PlotView pv = new PlotView();
            this.Controls.Add(pv);
            PlotModel pm = new PlotModel();
            pv.Model = pm;

            

            LineSeries punktySerii = new LineSeries
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Diamond,
                MarkerSize = 2,
                MarkerStroke = OxyColors.Red,
                Title = "Punkty treningowe"
            };

            LineSeries punktySieci = new LineSeries()
            {
                LineStyle = LineStyle.None,
                MarkerType = MarkerType.Plus,
                MarkerSize = 3,
                MarkerStroke = OxyColors.Blue,
                Title = "Punkty wyjściowe"
            };

            LineSeries punktyKresek = new LineSeries
            {
                Color = OxyColors.Blue,
                StrokeThickness = 1
            };

            for (int i = 0; i < inputArray.Length; i++)
            {
                punktySerii.Points.Add(new DataPoint(inputArray[i], targetArray[i]));
            }           

            


            //SIEĆ

            Random rnd = new Random();
            NeuralNetwork nn = new NeuralNetwork(1, 10, 1, data);

            for (int i = 0; i < epochs; ++i)
            {
                foreach (int j in Enumerable.Range(0, 81).OrderBy(x => rnd.Next()))
                {
                    double[] inp = new double[1];
                    double[] tar = new double[1];

                    inp[0] = inputArray[j];
                    tar[0] = targetArray[j];


                    nn.Train(inp, tar);
                }
            }

            Matrix.Matrix output = new Matrix.Matrix(1, 1);

            for (int i = 0; i < 81; ++i)
            {
                double[] inp = new double[1];
                double[] d = new double[1];
                inp[0] = inputArray[i];

                output = nn.FeedForward(inp);

                d[0] = inputArray[i];
                punktySieci.Points.Add(new DataPoint(d[0], output.tab[0, 0]));
            }
            output.DisplayMatrix();

            pm.Series.Add(punktySerii);
            pm.Series.Add(punktySieci);
        }

        private void LoadFromFile(Matrix.Matrix data)
        {
            var fileStream = new FileStream(@"..\..\input.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                int i = 0;
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    var num1 = Double.Parse(line.Split(' ')[0]);
                    var num2 = Convert.ToDouble(line.Split(' ')[1]);
                    data.tab[i, 0] = num1;
                    data.tab[i, 1] = num2;
                    inputArray[i] = num1;
                    targetArray[i] = num2;
                    i++;
                }
            }
        }
    }
}
