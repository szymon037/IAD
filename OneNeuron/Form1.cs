using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;


namespace OneNeuron
{
    public partial class Form1 : Form
    {

        private List<Object> points = new List<Object>();
        private static Random rnd = new Random(); 

        public Form1()
        {

            InitializeComponent();
            Graphics g = this.CreateGraphics();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

            RandomiseData(100);
            SelectClassType(points);
            if (!File.Exists("D:\\foo.txt"))
            {
                StreamWriter sw = File.CreateText("D:\\foo.txt");
                for (var i = 0; i < points.Count; i++)
                {
                    sw.Write(i);
                    sw.Write("-");
                    sw.Write(points[i].X);
                    sw.Write(" ");
                    sw.Write(points[i].Y);
                    sw.Write(" ");
                    sw.WriteLine(points[i].ClassType);
                }
            }
        }

        public void RandomiseData(int n)
        {
            for (var i = 0; i < n; i++)
            {

                Object point = new Object(rnd,pictureBox1.Size.Width, pictureBox1.Size.Height);
                points.Add(point);
            }
        }

        private void SelectClassType(List<Object> array)
        {
            for(var i = 0; i < array.Count; i++)
            {
                if (Function(array[i].X) <= array[i].Y)
                {
                    array[i].ClassType = 1;
                }
            }

        }

        public double Function(double x)
        {
            return x+1;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
          
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (var i = 0; i < points.Count; i++)
            {
                if (points[i].ClassType == 1)
                {
                    g.FillEllipse(System.Drawing.Brushes.Red, new Rectangle(points[i].X, points[i].Y, 5, 5));
                }
                else
                {
                    g.FillEllipse(System.Drawing.Brushes.Blue, new Rectangle(points[i].X, points[i].Y, 5, 5));
                }
            }
            g.DrawLine(System.Drawing.Pens.Black, 0, Convert.ToInt32(Function(0)), pictureBox1.Size.Width , Convert.ToInt32(Function(pictureBox1.Size.Width)));

            Neuron neuron = new Neuron();
            //Console.WriteLine("w0: " + neuron.w0 + "\nw1:" + neuron.w1 + "\nw2: " + neuron.w2);
            for (int n = 0; n < 2; n++)
            {
                RandomiseData(100);
                SelectClassType(points);
                for (int j = 0; j < 50; ++j)
                {
                    Shuffle(points);
                    for (int i = 0; i < points.Count; ++i)
                    {
                        neuron.operation(points[i]);
                    }
                    Console.WriteLine("\nloop " + j + "\nCorrect: " + neuron.correct + "\nIncorrect: " + neuron.incorrect);
                    Console.WriteLine("w1/w2: " + (-1) * neuron.w1 / neuron.w2 + "\nw0/w2: " + (-1) * neuron.w0 / neuron.w2);

                    neuron.correct = 0;
                    neuron.incorrect = 0;

                }                               
            }
            g.DrawLine(System.Drawing.Pens.Yellow, 0, (int)((-neuron.w1 / neuron.w2) * 0 - neuron.w0 / neuron.w2), pictureBox1.Size.Width, (int)(-neuron.w1 / neuron.w2 * pictureBox1.Size.Width - neuron.w0 / neuron.w2));


            Object a = new Object(rnd, 600, 600);
            NeuronChoise(a, neuron);
            if(a.ClassType == 1)
            {
                g.FillEllipse(System.Drawing.Brushes.DeepSkyBlue, new Rectangle(a.X, a.Y, 8, 8));
            }
            else
            
            g.FillEllipse(System.Drawing.Brushes.DarkRed, new Rectangle(a.X, a.Y, 8, 8));

        }
        private void NeuronChoise(Object a, Neuron n)
        {
            if ((-n.w1 / n.w2) * a.X - n.w0 / n.w2 > a.Y)
            {
                a.ClassType = 1;
            }
        }

        private static void Shuffle(List<Object> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rnd.Next(n + 1);
                Object value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
