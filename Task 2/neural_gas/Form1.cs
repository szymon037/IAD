using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace task_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //init variables 
        public int initialX;
        public int initialY;
        public int finalX;
        public int finalY;
        public List<Point> Data = new List<Point>();
        public double[][] data_arr;
        public int numberOfNeurons;
        public int amountOfPoints;
        public double range;
        public int epochs;

        private void Form1_Load(object sender, EventArgs e)
        {
            range = 10;
            numberOfNeurons = 10;
            amountOfPoints = 200;
            epochs = 200000;

            


            //data_arr = new double[1600][];
            //Random rnd = new Random();
            

            /*for (int i = 0; i < 1600; ++i)
            {
                data_arr[i] = new double[2];

                data_arr[i][0] = rnd.NextDouble() * 2;
                data_arr[i][1] = rnd.NextDouble() * 2;
            }*/

                /*for (int i = 0; i < 40; ++i)
                {
                    for (int j = 0; j < 40; ++j)
                    {
                        data_arr[i * 40 + j][0] = j;
                        data_arr[i * 40 + j][1] = i;

                        Data.Add(new Point(j, i));
                    }
                }*/
                /*if (i < 30)
                {
                    data[i][0] = rnd.NextDouble() * 15 + 10;
                    data[i][1] = rnd.NextDouble() * 15 + 150;
                }
                else if (i >= 30 && i < 60)
                {
                    data[i][0] = rnd.NextDouble() * 15 + 10;
                    data[i][1] = rnd.NextDouble() * 15 + 10;
                }
                else if (i >= 60 && i < 90)
                {
                    data[i][0] = rnd.NextDouble() * 15 + 150;
                    data[i][1] = rnd.NextDouble() * 15 + 10;
                }
                Data.Add(new Point((int) data[i][0], (int) data[i][1]));
                }*/
        }

        public void Draw(List<Point> a)
        {
            System.Drawing.Graphics g = pB.CreateGraphics(); ;
            pB.Refresh();
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            for (int i = 0; i < Data.Count(); i++)
            {
                Rectangle rect = new Rectangle(Data[i], new Size(4, 4));
                g.FillRectangle(brush, rect);
                //Console.WriteLine(Data[i].X);
            }
            SolidBrush brush2 = new SolidBrush(Color.Red);
            //Console.WriteLine("a.Count(): " + a.Count());
            for (int i = 0; i < a.Count(); i++)
            {
                Rectangle rect = new Rectangle(a[i], new Size(5, 5));
                g.FillRectangle(brush2, rect);
            }
            
            //Console.WriteLine(a.Count());
        }

        public void TrainAndDraw ()
        {
            //NeuralNetwork nn = new NeuralNetwork(numberOfNeurons, range, 5, 8);
            NeuralGas ng = new NeuralGas(numberOfNeurons, 15, 80);

            List<Point> neurons = new List<Point>();

            for (int i = 0; i < numberOfNeurons; ++i)
                neurons.Add(new Point());

            Random rnd = new Random();
            //double[] data_arr = new double[2];
            /*data_arr = new double[1000][];

            for (int i = 0; i < 1000; ++i)
            {
                data_arr[i] = new double[2];

                data_arr[i][0] = rnd.NextDouble() * 100 + 50;
                data_arr[i][1] = rnd.NextDouble() * 100 + 50;

            }*/
            Console.WriteLine("Neurons before training: ");
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                Console.WriteLine("Neuron " + i + ":\n" + "x: " + ng.neurons[i][0] + "\ny: " + ng.neurons[i][1]);
            }

            int t = 0;
            for (int i = 0; i < epochs; ++i)
            {

                data_arr[0][0] = rnd.NextDouble() * 100 + 5;
                data_arr[0][1] = rnd.NextDouble() * 100 + 5;

                ng.Train(data_arr[0]/*data_arr[rnd.Next(0, amountOfPoints)]*/);
                /*foreach (int j in Enumerable.Range(0, data_arr.Length).OrderBy(x => rnd.Next()))
                {
                    //nn.Train(data_arr[j]);
                    ng.Train(data_arr[j]);
                    ng.Update(2000, t);
                    ++t;
                    
                    for (int z = 0; z < numberOfNeurons; ++z)
                    {
                        Point temp = new Point((int)ng.neurons[z][0], (int)ng.neurons[z][1]);
                        neurons[z] = temp;
                    }
                    Draw(neurons);
                    Console.WriteLine(t);
                }*/
                //nn.range = range * Math.Exp((-1) * i / (10000 / Math.Log10(range)));
                //Console.WriteLine("epoch: " + i);
                ng.Update(epochs, i);

               /* for (int j = 0; j < numberOfNeurons; ++j)
                {
                    Point temp = new Point((int)ng.neurons[j][0], (int)ng.neurons[j][1]);
                    neurons[j] = temp;
                }

                Draw(neurons);*/
            }

            for (int j = 0; j < numberOfNeurons; ++j)
            {
                Point temp = new Point((int)ng.neurons[j][0], (int)ng.neurons[j][1]);
                neurons[j] = temp;
            }

            Draw(neurons);

            Console.WriteLine("Neurons after training: ");
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                Console.WriteLine("Neuron " + i + ":\n" + "x: " + ng.neurons[i][0] + "\ny: " + ng.neurons[i][1]);
            }
            
        }

        

        private void pB_MouseDown(object sender, MouseEventArgs e)
        {
            initialX = e.X;
            initialY = e.Y;
        }

        private void pB_MouseUp(object sender, MouseEventArgs e)
        {
            finalX = e.X;
            finalY = e.Y;
            if(finalX < initialX)
            {
                initialX = initialX + finalX;
                finalX = initialX - finalX;
                initialX = initialX - finalX;
            }
            if(finalY < initialY)
            {
                initialY = initialY + finalY;
                finalY = initialY - finalY;
                initialY = initialY - finalY;
            }
            DrawRectangle(initialX, initialY, finalX, finalY);
            //Console.WriteLine(initialY);
        }

        private void DrawRectangle(int initialX, int initialY, int finalX, int finalY)
        {
            System.Drawing.Graphics g;
            //pB.Refresh();
            g = pB.CreateGraphics();
            Pen mypen = new System.Drawing.Pen(Brushes.Black);
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            g.DrawRectangle(mypen, initialX, initialY, Math.Abs(initialX-finalX), Math.Abs(initialY - finalY));

            amountOfPoints = Convert.ToInt32(tBpoints.Text);
            data_arr = new double[amountOfPoints][];
            for (int i = 0; i < amountOfPoints; ++i)
            {
                data_arr[i] = new double[2];    
            }
            Random rng = new Random();
            List<Point> temp = new List<Point>();
            int pointer = Data.Count();
            for(int i = 0; i < Convert.ToInt32(tBpoints.Text); i++)
            {
                data_arr[i][0] = rng.Next(initialX, finalX);
                data_arr[i][1] = rng.Next(initialY, finalY);
                //Point p = new Point(rng.Next(initalX, finalX), rng.Next(initalY, finalY));
                Point p = new Point((int) data_arr[i][0], (int)data_arr[i][1]);
                Data.Add(p);
                 
            }
            /*Console.WriteLine("initialX: " + initialX + "\ninitialY: " + initialY);
            Console.WriteLine("finalX: " + finalX + "\nfinalY: " + finalY);*/
            TrainAndDraw();
            //Draw(Data);
            //pB.Refresh();
        }

        private void InitDraw()
        {
            System.Drawing.Graphics g = pB.CreateGraphics(); ;
            pB.Refresh();
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            for(int i = 0; i < Data.Count(); i++)
            {
                Rectangle rect = new Rectangle(Data[i], new Size(4, 4));
                g.FillRectangle(brush, rect);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitDraw();
        }
    }
}
