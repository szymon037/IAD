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
        public int width;
        public int height;

        public int mode;
        public double data_width;
        public double data_height;
        public double data_radius;
        public double data_size;




        private void Form1_Load(object sender, EventArgs e)
        {
            epochs = 40000;
            width = 10;
            height = 10;
            data_size = 100;
            numberOfNeurons = 50;

            mode = 0;
            Console.WriteLine("Enter number: \n" + "1. Square \n2. Rectangle 1 \n3. Rectangle 2 \n4. Circle");
            mode = Console.Read();
            mode -= 48;
            Console.WriteLine(mode);

            if (mode == 1)
            {
                data_width = data_size;
                data_height = data_size;
            }
            else if (mode == 2)
            {
                data_width = data_size * 1.5;
                data_height = data_size;
            }
            else if (mode == 3)
            {
                data_width = data_size;
                data_height = data_size * 1.5;
            }
            else if (mode == 4)
            {
                data_radius = data_size;
            }
        }

        public void TrainAndDraw()
        {
            //KOHONEN
            /*Kohonen n2 = new Kohonen(width, height);
            n2.Randomise(10, 25);*/

            NeuralGas ng = new NeuralGas(numberOfNeurons, 50, 50);

            List<Point> neurons = new List<Point>();

            //KOHONEN
            /*
            for (int i = 0; i < height * width; ++i)
                neurons.Add(new Point());            
*/
            for (int i = 0; i < numberOfNeurons; ++i)
                neurons.Add(new Point());         
            Random rnd = new Random();
            int index = 0;

            /*foreach (int j in Enumerable.Range(0, amountOfPoints).OrderBy(x => rnd.Next()))
            {
                n2.Train(data_arr[j], epochs, index);
                index++;
            }*/

            for (int i = 0; i < epochs; ++i)
            {                
                index = rnd.Next(0, amountOfPoints);
                //n2.Train(data_arr[index], epochs, i);
                ng.Train(data_arr[index], epochs, i);
                
                if (i % 10000 == 0)
                {
                   
                    /*for (int z = 0; z < width; ++z)
                    {
                        for (int j = 0; j < height; ++j)
                        {
                            neurons[z * width + j] = new Point((int)n2.neurons[z][j].X, (int)n2.neurons[z][j].Y);
                        }
                    }

                    
                    */
                    for(int j = 0; j < numberOfNeurons; ++j)
                    {
                        neurons[j] = new Point((int)ng.neurons[j][0], (int)ng.neurons[j][1]);
                    }

                    Draw(neurons);
                }
            }

            /*for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    neurons[i * width + j] = new Point((int)n2.neurons[i][j].X, (int)n2.neurons[i][j].Y);
                }
            }*/

            for (int j = 0; j < numberOfNeurons; ++j)
            {
                neurons[j] = new Point((int)ng.neurons[j][0], (int)ng.neurons[j][1]);
            }

            Draw(neurons);

           
            
        }


        public void Draw(List<Point> a)
        {
            System.Drawing.Graphics g = pB.CreateGraphics(); ;
            pB.Refresh();
            SolidBrush brush = new SolidBrush(Color.LimeGreen);
            for (int i = 0; i < Data.Count(); i++)
            {
                Rectangle rect = new Rectangle(Data[i], new Size(3, 3));
                g.FillRectangle(brush, rect);
            }
            SolidBrush brush2 = new SolidBrush(Color.Red);
            for (int i = 0; i < a.Count(); i++)
            {
                Rectangle rect = new Rectangle(a[i], new Size(5, 5));
                g.FillRectangle(brush2, rect);
            }

           /* SolidBrush brush3 = new SolidBrush(Color.Black);
            Pen pen1 = new Pen(brush3, 1);

            for (int i = 0; i < a.Count() / width; ++i)
            {
                for (int j = 0; j < a.Count() / height - 1; ++j)
                {
                    g.DrawLine(pen1, a[i * width + j].X + 2, a[i * width + j].Y + 2, a[i * width + j + 1].X + 2, a[i * width + j + 1].Y + 2);
                }
            }

            for (int i = 0; i < a.Count() / width - 1; ++i)
            {
                for (int j = 0; j < a.Count() / height; ++j)
                {
                    g.DrawLine(pen1, a[i * width + j].X + 2, a[i * width + j].Y + 2, a[(i + 1) * width + j].X + 2, a[(i + 1) * width + j].Y + 2);
                }
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

            ////
            ///KOŁO
            ///

            double c_x = 120, c_y = 120;

            for (int i = 0; i < Convert.ToInt32(tBpoints.Text); i++)
            {             

                if (mode == 1 || mode == 2 || mode == 3)
                {
                    data_arr[i][0] = rnd.NextDouble() * data_width + 5;
                    data_arr[i][1] = rnd.NextDouble() * data_height + 5;
                }
                else if (mode == 4)
                {                    
                    data_arr[i][0] = rnd.NextDouble() * 2 * data_radius - data_radius;
                    data_arr[i][1] = rnd.NextDouble() * 2 * data_radius - data_radius;
                    while ((data_arr[i][0] * data_arr[i][0] + data_arr[i][1] * data_arr[i][1]) > data_radius * data_radius)
                    {
                        data_arr[i][0] = rnd.NextDouble() * 2 * data_radius - data_radius;
                        data_arr[i][1] = rnd.NextDouble() * 2 * data_radius - data_radius;
                    }
                    data_arr[i][0] += c_x;
                    data_arr[i][1] += c_y;
                }                

                Point p = new Point((int) data_arr[i][0], (int)data_arr[i][1]);
                Data.Add(p);                 
            }
            
            TrainAndDraw();
      
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
