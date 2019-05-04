﻿using System;
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


        private void Form1_Load(object sender, EventArgs e)
        {
            numberOfNeurons = 50;
            amountOfPoints = 200;
            range = 14;//9 * Math.Sqrt(2);


            //data_arr = new double 

            /*data_arr = new double[400][];
            Random rnd = new Random();

            for (int i = 0; i < 400; ++i)
            {
                data_arr[i] = new double[2];

                data_arr[i][0] = rnd.NextDouble() * 2;
                data_arr[i][1] = rnd.NextDouble() * 2;*/
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
            Data.Add(new Point((int) data[i][0], (int) data[i][1]));*/
            //}
            Console.WriteLine();
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
            for (int i = 0; i < a.Count(); i++)
            {
                Rectangle rect = new Rectangle(a[i], new Size(5, 5));
                g.FillRectangle(brush2, rect);
            }
            
            //Console.WriteLine(a.Count());
        }

        public void TrainAndDraw ()
        {
            NeuralNetwork nn = new NeuralNetwork(numberOfNeurons, range, 5, 45);
            List<Point> neurons = new List<Point>();

            for (int i = 0; i < numberOfNeurons; ++i)
                neurons.Add(new Point());

            Random rnd = new Random();
            //double[] data_arr = new double[2];
    
            Console.WriteLine("Neurons before training: ");
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                Console.WriteLine("Neuron " + i + ":\n" + "x: " + nn.neurons[i][0] + "\ny: " + nn.neurons[i][1]);
            }

            for (int i = 0; i < 200000; ++i)
            {
                /*foreach (int j in Enumerable.Range(0, data_arr.Length).OrderBy(x => rnd.Next()))
                {
                    /*data_arr[0] = Data[j].X;
                    data_arr[1] = Data[j].Y;*/

                /* nn.Train(data_arr[j]);
             }*/

                data_arr[0][0] = rnd.NextDouble() * 100 + 5;
                data_arr[0][1] = rnd.NextDouble() * 100 + 5;

                nn.Train(data_arr[0/*rnd.Next(0, amountOfPoints)*/]);
                nn.range = range * Math.Exp((-1) * i / (100000 / Math.Log10(range)));
                //Console.WriteLine("range: " + nn.range);
                //Console.WriteLine(i);

                /*for (int j = 0; j < numberOfNeurons; ++j)
                {
                    Point temp = new Point((int)nn.neurons[j][0], (int)nn.neurons[j][1]);
                    neurons[j] = temp;
                }

                Draw(neurons);*/
                //Console.WriteLine(neurons[0].X);
            }

            for (int j = 0; j < numberOfNeurons; ++j)
            {
                Point temp = new Point((int)nn.neurons[j][0], (int)nn.neurons[j][1]);
                neurons[j] = temp;
            }

            Draw(neurons);

            Console.WriteLine("Neurons after training: ");
            for (int i = 0; i < numberOfNeurons; ++i)
            {
                Console.WriteLine("Neuron " + i + ":\n" + "x: " + nn.neurons[i][0] + "\ny: " + nn.neurons[i][1]);
            }
            /*for (int i = 0; i < numberOfNeurons; ++i)
            {
                Console.WriteLine(neurons[i].X);
                Console.WriteLine(neurons[i].Y);
            }*/
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
            Random rnd = new Random();
            List<Point> temp = new List<Point>();
            int pointer = Data.Count();
             for(int i = 0; i < Convert.ToInt32(tBpoints.Text); i++)
             {
                /*data_arr[i][0] = rnd.Next(initialX, finalX);
                data_arr[i][1] = rnd.Next(initialY, finalY);*/
                data_arr[i][0] = rnd.NextDouble() * 100 + 5;
                data_arr[i][1] = rnd.NextDouble() * 100 + 5;
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
